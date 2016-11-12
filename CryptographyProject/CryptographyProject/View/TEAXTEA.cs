using CryptographyProject.EncryptionAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptographyProject.View
{
    public partial class TEAXTEA : Form
    {
        private Main mainForm;
        private bool buttonOkPressed = false;
        private int index;
        public TEAXTEA(Main mainForm, int index)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.index = index;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Validate
            if (txtKey.Text.Length < 16 || txtKey.Text.Length > 16)
            {
                MessageBox.Show("Enter 128bits long key (16 characters long)!");
                return;
            }

            //Set
            if (index == 2)
            {
                TEA.Key = txtKey.Text;
            }
            else
            {
                XTEA.Key = txtKey.Text;
            }

            buttonOkPressed = true;
            this.Close();
        }

        private void TEAXTEA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!buttonOkPressed)
            {
                this.mainForm.ClearAlgorithmsViewData();
            }
        }
    }
}
