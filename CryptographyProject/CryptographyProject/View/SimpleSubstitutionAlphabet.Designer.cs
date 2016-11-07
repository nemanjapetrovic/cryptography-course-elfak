namespace CryptographyProject.View
{
    partial class SimpleSubstitutionAlphabet
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblStandrad = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEncryptionAlphabet = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Standrad alphabet:";
            // 
            // lblStandrad
            // 
            this.lblStandrad.AutoSize = true;
            this.lblStandrad.Location = new System.Drawing.Point(149, 41);
            this.lblStandrad.Name = "lblStandrad";
            this.lblStandrad.Size = new System.Drawing.Size(0, 13);
            this.lblStandrad.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Encrypted alphabet:";
            // 
            // txtEncryptionAlphabet
            // 
            this.txtEncryptionAlphabet.Location = new System.Drawing.Point(152, 73);
            this.txtEncryptionAlphabet.MaxLength = 26;
            this.txtEncryptionAlphabet.Name = "txtEncryptionAlphabet";
            this.txtEncryptionAlphabet.Size = new System.Drawing.Size(258, 20);
            this.txtEncryptionAlphabet.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(373, 105);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(218, 105);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(149, 23);
            this.btnLoadFromFile.TabIndex = 5;
            this.btnLoadFromFile.Text = "Load from file";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "(*.txt)|*.txt";
            // 
            // SimpleSubstitutionAlphabet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 140);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtEncryptionAlphabet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStandrad);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SimpleSubstitutionAlphabet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Substitution Alphabet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimpleSubstitutionAlphabet_FormClosing);
            this.Load += new System.EventHandler(this.SimpleSubstitutionAlphabet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStandrad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEncryptionAlphabet;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}