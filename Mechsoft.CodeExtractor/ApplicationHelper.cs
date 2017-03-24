using Mechsoft.CodeExtractor.Models;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechsoft.CodeExtractor
{
    public class ApplicationHelper
    {
        public static ExtractResult ExtractVault(string PathForExtract, Vault Vault)
        {
            var exResult = new ExtractResult();


            //Get Validations
            var di = new DirectoryInfo(PathForExtract);
            if (!di.Exists)
            {
                di.Create();
            }

            var vaultPath = Path.Combine(PathForExtract, Vault.Name);

            var vaultPathDi = new DirectoryInfo(vaultPath);

            if (!vaultPathDi.Exists)
            {
                di.CreateSubdirectory(Vault.Name);
            }

            //Props
            foreach (PropertyDefAdmin item in Vault.PropertyDefOperations.GetPropertyDefsAdmin())
            {
                if (item.Validation.VBScript != null)
                {
                    CreateFileForPropertyDefValidation(Path.Combine(vaultPathDi.FullName), item.Validation.VBScript, item.PropertyDef.ID.ToString(), item.PropertyDef.Name);
                    exResult.ValidationsCount++;
                }

                if (item.AutomaticValue.ANVCode != null)
                {
                    CreateFileForPropertyDefAutoNumbering(Path.Combine(vaultPathDi.FullName), item.AutomaticValue.ANVCode, item.PropertyDef.ID.ToString(), item.PropertyDef.Name);
                    exResult.NumberingCount++;
                }

                if (item.AutomaticValue.CVVCode != null)
                {
                    CreateFileForPropertyDefCalculatedValue(Path.Combine(vaultPathDi.FullName), item.AutomaticValue.CVVCode, item.PropertyDef.ID.ToString(), item.PropertyDef.Name);
                    exResult.CalculatedValuesCount++;
                }
            }



            //EventHandlers
            foreach (MFilesAPI.EventHandler item in Vault.ManagementOperations.GetEventHandlers())
            {
                if (item.Active)
                {
                    CreateFileForEventHandlers(Path.Combine(vaultPathDi.FullName), item.VBScript, item.EventType.ToString().Replace("MFEventHandler", ""), item.Description);
                    exResult.EventHandlersCount++;
                }
            }


            //Download Applications
            foreach (CustomApplication item in Vault.CustomApplicationManagementOperations.GetCustomApplications())
            {
                var appsPath = Path.Combine(vaultPathDi.FullName, "Applications");
                if (!Directory.Exists(appsPath))
                    Directory.CreateDirectory(appsPath);
 
                var fileSession = Vault.CustomApplicationManagementOperations.DownloadCustomApplicationBlockBegin(item.ID);

                long llFileSize = 0;
                llFileSize = fileSession.FileSize;

                var lBlockSize = 65536;
                byte[] arrBuff = null;
                long llTotalDownloaded = 0;
                long llOffset = 0;

                var iDownloadID = fileSession.DownloadID;

                using (var fileStream = new FileStream(Path.Combine(vaultPathDi.FullName, string.Format(@"Applications\{0}.zip", item.Name)), FileMode.OpenOrCreate))
                {
                    while (arrBuff == null || arrBuff.Length == lBlockSize)
                    {

                        arrBuff = Vault.CustomApplicationManagementOperations.DownloadCustomApplicationBlock(iDownloadID, lBlockSize, llOffset);

                        llTotalDownloaded = llTotalDownloaded + arrBuff.Length;

                        fileStream.Write(arrBuff, 0, arrBuff.Length);

                        llOffset = llOffset + arrBuff.Length;
                    }
                }

                exResult.ApplicationsCount++;
            }



            foreach (WorkflowAdmin wfItem in Vault.WorkflowOperations.GetWorkflowsAdmin())
            {
                //Automatic Triggers
                foreach (StateTransition transitionItem in wfItem.StateTransitions)
                {
                    if (transitionItem.TriggerMode == MFAutoStateTransitionMode.MFASTModeAllowedByScript)
                    {
                        CreateFileForAutomaticTransitions(Path.Combine(vaultPathDi.FullName), transitionItem.TriggerAllowedByVBScript, wfItem.Workflow.Name, transitionItem.Name);
                        exResult.TriggerCount++;
                    }
                }

                //StateActions
                foreach (StateAdmin stateItem in wfItem.States)
                {
                    if (stateItem.ActionRunVBScript)
                    {
                        CreateFileForStateAction(Path.Combine(vaultPathDi.FullName), stateItem.ActionRunVBScriptDefinition, wfItem.Workflow.Name, stateItem.Name);
                        exResult.StateActionCount++;
                    }

                    if (stateItem.Postconditions.VBScript)
                    {
                        CreateFileForPostConditions(Path.Combine(vaultPathDi.FullName), stateItem.ActionRunVBScriptDefinition, wfItem.Workflow.Name, stateItem.Name);
                        exResult.ConditionCount++;
                    }

                    if (stateItem.Preconditions.VBScript)
                    {
                        CreateFileForPreConditions(Path.Combine(vaultPathDi.FullName), stateItem.ActionRunVBScriptDefinition, wfItem.Workflow.Name, stateItem.Name);
                        exResult.ConditionCount++;
                    }


                }
            }

            return exResult;

        }

        private static void CreateFileForAutomaticTransitions(string filePath, string VbScript, string WfName, string TransitionName)
        {

            var targetDirectory = Path.Combine(filePath, "Triggers");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("Trigger-({0})-{1}.vbs", CleanIllegalCharacters(WfName), CleanIllegalCharacters(TransitionName));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForPostConditions(string filePath, string VbScript, string WfName, string StateName)
        {

            var targetDirectory = Path.Combine(filePath, "Conditions");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("PostCondition-({0})-{1}.vbs", CleanIllegalCharacters(WfName), CleanIllegalCharacters(StateName));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForPreConditions(string filePath, string VbScript, string WfName, string StateName)
        {

            var targetDirectory = Path.Combine(filePath, "Conditions");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("PreConditions-({0})-{1}.vbs", CleanIllegalCharacters(WfName), CleanIllegalCharacters(StateName));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForStateAction(string filePath, string VbScript, string WfName, string StateName)
        {

            var targetDirectory = Path.Combine(filePath, "StateActions");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("StateAction-({0})-{1}.vbs", CleanIllegalCharacters(WfName), CleanIllegalCharacters(StateName));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForEventHandlers(string filePath, string VbScript, string Type, string Description)
        {
            var targetDirectory = Path.Combine(filePath, "EventHandlers");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }



            var fileName = string.Format("EventHandler-({0})-{1}.vbs", Type, CleanIllegalCharacters(Description));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static string CleanIllegalCharacters(string pathToClean)
        {
            var text = pathToClean;
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                text = text.Replace(c.ToString(), "");
            }

            return text;
        }

        private static void CreateFileForPropertyDefAutoNumbering(string filePath, string VbScript, string Id, string Name)
        {
            var targetDirectory = Path.Combine(filePath, "Numbering");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("AutoNumbering-({0})-{1}.vbs", Id, CleanIllegalCharacters(Name));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForPropertyDefCalculatedValue(string filePath, string VbScript, string Id, string Name)
        {
            var targetDirectory = Path.Combine(filePath, "CalculatedValues");
            var pathExists = Directory.Exists(targetDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var fileName = string.Format("CalculatedValue-({0})-{1}.vbs", Id, CleanIllegalCharacters(Name));

            File.AppendAllText(Path.Combine(targetDirectory, fileName), VbScript);
        }

        private static void CreateFileForPropertyDefValidation(string filePath, string VbScript, string Id, string Name)
        {

            var validationDirectory = Path.Combine(filePath, "Validations");
            var pathExists = Directory.Exists(validationDirectory);

            if (!pathExists)
            {
                Directory.CreateDirectory(validationDirectory);
            }

            var fileName = string.Format("Validation-({0})-{1}.vbs", Id, CleanIllegalCharacters(Name));

            File.AppendAllText(Path.Combine(validationDirectory, fileName), VbScript);
        }
    }
}
