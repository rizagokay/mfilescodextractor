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

namespace Mechsoft.ConsultantToolbox
{
    public partial class ExractValueListForm : Form
    {

        private VaultsOnServer _vaults;

        public ExractValueListForm(VaultsOnServer VaultsOnServer)
        {
            _vaults = VaultsOnServer;
            InitializeComponent();
        }

        private void ExractValueListForm_Load(object sender, EventArgs e)
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

                    LoggedInVaultHolder.LoggedInVault = selectedVault.LogIn();

                    var selectValueListForm = new ExtractValueListItemsForm();
                   var result = selectValueListForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        //Create & Save Excel
                        ApplicationHelper.CreateExcelFile(selectValueListForm.SelectedValueListId, selectValueListForm.FilePath);
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Seçilen kütüphaneye bağlanılamadı;\n" + Ex.Message.Substring(0, Ex.Message.IndexOf("\r\n\r\n")), "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

