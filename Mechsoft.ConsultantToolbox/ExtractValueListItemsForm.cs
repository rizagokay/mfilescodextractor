using Mechsoft.ConsultantToolbox.Models;
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
    public partial class ExtractValueListItemsForm : Form
    {
        private string _filePath;

        private int _selectedValueListId;
        
        public string FilePath { get { return _filePath; } }
        public static bool IsExternalChecked;
        public int SelectedValueListId { get { return _selectedValueListId; } }

        public ExtractValueListItemsForm()
        {
            InitializeComponent();
    }

        private void btn_Extract_Click(object sender, EventArgs e)
        {
            if (LoggedInVaultHolder.LoggedInVault == null)
            {
                MessageBox.Show("Lütfen bir kütüphaneye giriş yapınız.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir değer listesi seçiniz.", "M-Files Code Extractor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this._selectedValueListId = (comboBox1.SelectedItem as ValueListModel).ID;

            var savedialog = saveFileDialog1.ShowDialog();

            IsExternalChecked = checkBox1.Checked;
            
            if (savedialog == DialogResult.OK)
            {
                _filePath = saveFileDialog1.FileName;
                this.DialogResult = DialogResult.OK;
            }

        }

        private void ExtractValueListItemsForm_Load(object sender, EventArgs e)
        {
            if (LoggedInVaultHolder.LoggedInVault != null)
            {
                var valueLists = LoggedInVaultHolder.LoggedInVault.ValueListOperations.GetValueLists();

                var convertedValueLists = new List<ValueListModel>();

                foreach (MFilesAPI.ObjType item in valueLists)
                {
                    convertedValueLists.Add(new ValueListModel()
                    {

                        ID = item.ID,
                        Name = item.NamePlural

                    });
                }

                comboBox1.DataSource = convertedValueLists;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "ID";                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
