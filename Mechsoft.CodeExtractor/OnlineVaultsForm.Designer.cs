namespace Mechsoft.CodeExtractor
{
    partial class OnlineVaultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OnlineVaultsForm));
            this.list_Vaults = new System.Windows.Forms.ListBox();
            this.btn_Extract = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_ExtractAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_Vaults
            // 
            this.list_Vaults.FormattingEnabled = true;
            this.list_Vaults.Location = new System.Drawing.Point(3, 12);
            this.list_Vaults.Name = "list_Vaults";
            this.list_Vaults.Size = new System.Drawing.Size(326, 199);
            this.list_Vaults.TabIndex = 0;
            // 
            // btn_Extract
            // 
            this.btn_Extract.Location = new System.Drawing.Point(254, 226);
            this.btn_Extract.Name = "btn_Extract";
            this.btn_Extract.Size = new System.Drawing.Size(75, 23);
            this.btn_Extract.TabIndex = 1;
            this.btn_Extract.Text = "Extract";
            this.btn_Extract.UseVisualStyleBackColor = true;
            this.btn_Extract.Click += new System.EventHandler(this.btn_Extract_Click);
            // 
            // btn_ExtractAll
            // 
            this.btn_ExtractAll.Location = new System.Drawing.Point(173, 226);
            this.btn_ExtractAll.Name = "btn_ExtractAll";
            this.btn_ExtractAll.Size = new System.Drawing.Size(75, 23);
            this.btn_ExtractAll.TabIndex = 2;
            this.btn_ExtractAll.Text = "Extract All";
            this.btn_ExtractAll.UseVisualStyleBackColor = true;
            this.btn_ExtractAll.Click += new System.EventHandler(this.btn_ExtractAll_Click);
            // 
            // OnlineVaultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 261);
            this.Controls.Add(this.btn_ExtractAll);
            this.Controls.Add(this.btn_Extract);
            this.Controls.Add(this.list_Vaults);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OnlineVaultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "M-Files Code Extractor: Online Vaults";
            this.Load += new System.EventHandler(this.OnlineVaultsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_Vaults;
        private System.Windows.Forms.Button btn_Extract;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btn_ExtractAll;
    }
}