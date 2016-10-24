namespace CryptographyProject
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chSaveLocation = new System.Windows.Forms.CheckBox();
            this.btnChangeOutputFolder = new System.Windows.Forms.Button();
            this.btnChangeInputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logView = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listAlgorithms = new System.Windows.Forms.CheckedListBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.dialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.threadsNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chSaveLocation);
            this.groupBox1.Controls.Add(this.btnChangeOutputFolder);
            this.groupBox1.Controls.Add(this.btnChangeInputFolder);
            this.groupBox1.Controls.Add(this.txtOutputFolder);
            this.groupBox1.Controls.Add(this.txtInputFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 118);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folders";
            // 
            // chSaveLocation
            // 
            this.chSaveLocation.AutoSize = true;
            this.chSaveLocation.Location = new System.Drawing.Point(12, 95);
            this.chSaveLocation.Name = "chSaveLocation";
            this.chSaveLocation.Size = new System.Drawing.Size(109, 17);
            this.chSaveLocation.TabIndex = 5;
            this.chSaveLocation.Text = "Save the location";
            this.chSaveLocation.UseVisualStyleBackColor = true;
            this.chSaveLocation.CheckedChanged += new System.EventHandler(this.chSaveLocation_CheckedChanged);
            // 
            // btnChangeOutputFolder
            // 
            this.btnChangeOutputFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeOutputFolder.Image = global::CryptographyProject.Properties.Resources.folder;
            this.btnChangeOutputFolder.Location = new System.Drawing.Point(456, 67);
            this.btnChangeOutputFolder.Name = "btnChangeOutputFolder";
            this.btnChangeOutputFolder.Size = new System.Drawing.Size(51, 20);
            this.btnChangeOutputFolder.TabIndex = 4;
            this.btnChangeOutputFolder.UseVisualStyleBackColor = true;
            this.btnChangeOutputFolder.Click += new System.EventHandler(this.btnChangeOutputFolder_Click);
            // 
            // btnChangeInputFolder
            // 
            this.btnChangeInputFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeInputFolder.Image = global::CryptographyProject.Properties.Resources.folder;
            this.btnChangeInputFolder.Location = new System.Drawing.Point(456, 29);
            this.btnChangeInputFolder.Name = "btnChangeInputFolder";
            this.btnChangeInputFolder.Size = new System.Drawing.Size(51, 20);
            this.btnChangeInputFolder.TabIndex = 1;
            this.btnChangeInputFolder.UseVisualStyleBackColor = true;
            this.btnChangeInputFolder.Click += new System.EventHandler(this.btnChangeInputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(88, 68);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(362, 20);
            this.txtOutputFolder.TabIndex = 3;
            this.txtOutputFolder.Click += new System.EventHandler(this.btnChangeOutputFolder_Click);
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Location = new System.Drawing.Point(88, 29);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.ReadOnly = true;
            this.txtInputFolder.Size = new System.Drawing.Size(362, 20);
            this.txtInputFolder.TabIndex = 2;
            this.txtInputFolder.Click += new System.EventHandler(this.btnChangeInputFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output folder:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input folder:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.logView);
            this.groupBox2.Location = new System.Drawing.Point(531, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 299);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // logView
            // 
            this.logView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logView.FormattingEnabled = true;
            this.logView.HorizontalScrollbar = true;
            this.logView.Location = new System.Drawing.Point(3, 16);
            this.logView.Name = "logView";
            this.logView.ScrollAlwaysVisible = true;
            this.logView.Size = new System.Drawing.Size(313, 280);
            this.logView.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listAlgorithms);
            this.groupBox3.Location = new System.Drawing.Point(12, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(513, 122);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Encryption algorithm";
            // 
            // listAlgorithms
            // 
            this.listAlgorithms.CheckOnClick = true;
            this.listAlgorithms.FormattingEnabled = true;
            this.listAlgorithms.Items.AddRange(new object[] {
            "Algorithm 1",
            "Algorithm 2",
            "Algorithm 3",
            "Algorithm 4"});
            this.listAlgorithms.Location = new System.Drawing.Point(6, 22);
            this.listAlgorithms.Name = "listAlgorithms";
            this.listAlgorithms.Size = new System.Drawing.Size(501, 94);
            this.listAlgorithms.TabIndex = 1;
            this.listAlgorithms.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listAlgorithms_ItemCheck);
            // 
            // btnMain
            // 
            this.btnMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMain.ForeColor = System.Drawing.Color.Green;
            this.btnMain.Location = new System.Drawing.Point(12, 264);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(391, 44);
            this.btnMain.TabIndex = 0;
            this.btnMain.Text = "S T A R T";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // threadsNumber
            // 
            this.threadsNumber.Location = new System.Drawing.Point(409, 285);
            this.threadsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadsNumber.Name = "threadsNumber";
            this.threadsNumber.Size = new System.Drawing.Size(109, 20);
            this.threadsNumber.TabIndex = 3;
            this.threadsNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threadsNumber.ValueChanged += new System.EventHandler(this.threadsNumber_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Threads numbers:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 323);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.threadsNumber);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cryptography - Nemanja Petrovic";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.threadsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Button btnChangeOutputFolder;
        private System.Windows.Forms.Button btnChangeInputFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox logView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.CheckedListBox listAlgorithms;
        private System.Windows.Forms.FolderBrowserDialog dialogFolder;
        private System.Windows.Forms.CheckBox chSaveLocation;
        private System.Windows.Forms.NumericUpDown threadsNumber;
        private System.Windows.Forms.Label label3;
    }
}