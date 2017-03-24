using Mechsoft.CodeExtractor.Models;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mechsoft.CodeExtractor
{
    public partial class OnlineVaultsForm : Form
    {

        private VaultsOnServer _vaults;

        public OnlineVaultsForm(VaultsOnServer VaultsOnServer)
        {
            InitializeComponent();
            _vaults = VaultsOnServer;
        }

        private void OnlineVaultsForm_Load(object sender, EventArgs e)
        {
            foreach (VaultOnServer item in _vaults)
            {
                list_Vaults.Items.Add(item.Name);
            }

            Cursor.Current = Cursors.Default;
        }

        private void btn_Extract_Click(object sender, EventArgs e)
        {
            if (list_Vaults.SelectedIndex != -1)
            {

                var itemName = list_Vaults.SelectedItem.ToString();


                VaultOnServer selectedVault = null;

                foreach (VaultOnServer item in _vaults)
                {
                    if (item.Name == itemName)
                    {
                        selectedVault = item;
                        break;
                    }
                }

                try
                {

                    ExtractResult exResult = null;

                    LoggedInVaultHolder.LoggedInVault = selectedVault.LogIn();

                    
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string folderName = folderBrowserDialog1.SelectedPath;
                        Cursor.Current = Cursors.WaitCursor;
                        exResult = ApplicationHelper.ExtractVault(folderName, LoggedInVaultHolder.LoggedInVault);
                        Cursor.Current = Cursors.Default;

                        var sb = new StringBuilder();
                        sb.AppendLine("Succesfully Extracted!");
                        sb.AppendLine("Event Handlers: " + exResult.EventHandlersCount.ToString());
                        sb.AppendLine("State Actions: " + exResult.StateActionCount.ToString());
                        sb.AppendLine("State Triggers: " + exResult.TriggerCount.ToString());
                        sb.AppendLine("Conditions: " + exResult.ConditionCount.ToString());
                        sb.AppendLine("Automatic Numbering: " + exResult.NumberingCount.ToString());
                        sb.AppendLine("Calculated Values: " + exResult.CalculatedValuesCount.ToString());
                        sb.AppendLine("Validations: " + exResult.ValidationsCount.ToString());
                        sb.AppendLine("Applications: " + exResult.ApplicationsCount.ToString());

                        MessageBox.Show(sb.ToString(), "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }



                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Seçilen kütüphaneye bağlanılamadı;\n" + Ex.Message.Substring(0,Ex.Message.IndexOf("\r\n\r\n")), "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen bir kütüphane seçiniz.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_ExtractAll_Click(object sender, EventArgs e)
        {
            int extractedVaults = 0;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                Cursor.Current = Cursors.WaitCursor;
                foreach (VaultOnServer item in _vaults)
                {

                    try
                    {
                        ApplicationHelper.ExtractVault(folderName, item.LogIn());
                        extractedVaults++;


                    }
                    catch (Exception)
                    {
                        continue;
                    }

                }

                Cursor.Current = Cursors.Default;
                MessageBox.Show(string.Format("Successfully extracted {0} vaults!", extractedVaults), "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
    }
}
