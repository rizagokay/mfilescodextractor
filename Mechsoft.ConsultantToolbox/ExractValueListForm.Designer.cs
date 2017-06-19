namespace Mechsoft.ConsultantToolbox
{
    partial class ExractValueListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Extract = new System.Windows.Forms.Button();
            this.list_Vaults = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btn_Extract
            // 
            this.btn_Extract.Location = new System.Drawing.Point(224, 226);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(119, 23);
            this.btn_Extract.TabIndex = 4;
            this.btn_Extract.Text = "Select Vault";
            this.btn_Extract.UseVisualStyleBackColor = true;
            this.btn_Extract.Click += new System.EventHandler(this.btn_Extract_Click);
            // 
            // list_Vaults
            // 
            this.list_Vaults.FormattingEnabled = true;
            this.list_Vaults.Location = new System.Drawing.Point(17, 12);
            this.list_Vaults.Name = "list_Vaults";
            this.list_Vaults.Size = new System.Drawing.Size(326, 199);
            this.list_Vaults.TabIndex = 3;
            // 
            // ExractValueListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 261);
            this.Controls.Add(this.btn_Extract);
            this.Controls.Add(this.list_Vaults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExractValueListForm";
            this.Text = "M-Files Consultant Toolbox  | Value List Extractor";
            this.Load += new System.EventHandler(this.ExractValueListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Extract;
        private System.Windows.Forms.ListBox list_Vaults;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}