using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFilesAPI;
using Mechsoft.ConsultantToolbox.Models;
using Mechsoft.ConsultantToolbox;

namespace Mechsoft.ConsultantToolbox
{
    public partial class LoginForm : Form
    {

        private MFilesServerApplication serverApp;

        public LoginForm()
        {
            InitializeComponent();
            serverApp = new MFilesServerApplication();
        }

        private void FillUserTypes()
        {

            var listOfusers = new List<UserType>();
            listOfusers.Add(new UserType() { Id = 1, Name = "Windows" });
            listOfusers.Add(new UserType() { Id = 2, Name = "M-Files" });

            userTypeList.DataSource = listOfusers;
            userTypeList.ValueMember = "Id";
            userTypeList.DisplayMember = "Name";
            

        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (txt_Password.Text.Trim() == "" || txt_ServerAddress.Text.Trim() == "" || txt_Password.Text.Trim() == "" && userTypeList.SelectedIndex != -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userType = userTypeList.SelectedItem as UserType;
            try
            {
                if (userType.Id == 1)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    serverApp.Connect(MFAuthType.MFAuthTypeSpecificWindowsUser, txt_Username.Text, txt_Password.Text, "", "ncacn_ip_tcp", txt_ServerAddress.Text);
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    serverApp.Connect(MFAuthType.MFAuthTypeSpecificMFilesUser, txt_Username.Text, txt_Password.Text, "", "ncacn_ip_tcp", txt_ServerAddress.Text);
              
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Sunucuya bağlanılamadı.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cursor.Current = Cursors.Default;
                return;
            }

           
            

            //Get Online Vaults
            var onlineVaults = serverApp.GetOnlineVaults();

            

            //Pop The Vaults Form
            var oForm = new ExtractScriptsForm(onlineVaults);
            oForm.ShowDialog();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillUserTypes();
        }
    }
}
