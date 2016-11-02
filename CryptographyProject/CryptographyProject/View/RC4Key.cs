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
    public partial class RC4Key : Form
    {
        public RC4Key()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Read
            var bytes = Encoding.ASCII.GetBytes(new string(txtKey.Text.ToCharArray()));

            //Validate
            if (bytes.Length < 5 || bytes.Length > 256)
            {
                MessageBox.Show("The minimum of 40 bits is conventional, below 40 bits or 5 characters of key material, is just too unsafe.");
                return;
            }

            //Set
            RC4.Key = bytes;

            this.Close();
        }
    }
}
