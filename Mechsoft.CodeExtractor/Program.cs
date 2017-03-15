using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mechsoft.CodeExtractor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (MFilesIsInstalled())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("M-Files Sunucu Yönetim Konsolu Yüklü Değil.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        static bool MFilesIsInstalled()
        {
           
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(@"SOFTWARE\Motive\M-Files"))
            {
                if (key != null)
                {
                    foreach (var item in key.GetSubKeyNames())
                    {
                        var subIntalledKey = key.OpenSubKey(item);

                        if (subIntalledKey != null)
                        {
                            foreach (var underVersionKey in subIntalledKey.GetSubKeyNames())
                            {
                                if (underVersionKey == "ServerTools")
                                {
                                    var Installed = subIntalledKey.OpenSubKey(underVersionKey).GetValue("Installed");

                                    if (Installed != null)
                                    {
                                        if ((int)Installed == 1)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    } 
                }
            }

            return false;
        }
    }
}
