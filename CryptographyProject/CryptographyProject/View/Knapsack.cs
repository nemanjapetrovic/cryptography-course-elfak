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
    public partial class Knapsack : Form
    {
        private Main mainForm;
        private bool buttonOkPressed = false;
        private bool userChangedSmth = false;
        public Knapsack(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SetUpKeys()
        {
            if (txtPrivate.Text.Length != 0)
            {
                string[] tmp = null;
                try
                {
                    tmp = txtPrivate.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            int n, m;
            int[] privateKeyArray = new int[8];
            try
            {
                n = int.Parse(txtN.Text);
                m = int.Parse(txtM.Text);

                string[] privateKeyData = txtPrivate.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < privateKeyData.Length; i++)
                {
                    privateKeyArray[i] = int.Parse(privateKeyData[i]);
                }
                int suma = 0;
                for (int i = 0; i < privateKeyArray.Length - 1; i++)
                {
                    suma += privateKeyArray[i];
                    if (suma > privateKeyArray[i + 1])
                    {
                        throw new Exception("Your private key array is not valid, element with index is not valid: " + (i + 1));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            CryptographyProject.EncryptionAlgorithms.KnapsackAlg.SetKeys(privateKeyArray, n, m);

            //Private
            var privateKey = CryptographyProject.EncryptionAlgorithms.KnapsackAlg.P;
            string[] dataPrivate = privateKey.Select(x => x.ToString()).ToArray();
            var dataPrivateKey = string.Join(",", dataPrivate);
            txtPrivate.Text = dataPrivateKey;

            //Public
            var publicKey = CryptographyProject.EncryptionAlgorithms.KnapsackAlg.J;
            string[] data = publicKey.Select(x => x.ToString()).ToArray();
            var dataFull = string.Join(",", data);
            txtPublic.Text = dataFull;

        }

        private void GenerateKeys()
        {
            if (txtN.Text.Length == 0 || txtM.Text.Length == 0)
            {
                MessageBox.Show("Insert N and M!");
                return;
            }

            int n, m;
            try
            {
                n = int.Parse(txtN.Text);
                m = int.Parse(txtM.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            CryptographyProject.EncryptionAlgorithms.KnapsackAlg.GenerateKeys(n, m);

            //Private
            var privateKey = CryptographyProject.EncryptionAlgorithms.KnapsackAlg.P;
            string[] dataPrivate = privateKey.Select(x => x.ToString()).ToArray();
            var dataPrivateKey = string.Join(",", dataPrivate);
            txtPrivate.Text = dataPrivateKey;

            //Public
            var publicKey = CryptographyProject.EncryptionAlgorithms.KnapsackAlg.J;
            string[] data = publicKey.Select(x => x.ToString()).ToArray();
            var dataFull = string.Join(",", data);
            txtPublic.Text = dataFull;

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            userChangedSmth = false;
            this.GenerateKeys();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (userChangedSmth)
            {
                this.SetUpKeys();
            }
            else
            {
                this.GenerateKeys();
            }

            if (txtPrivate.Text.Length == 0 || txtPublic.Text.Length == 0)
            {
                MessageBox.Show("Private or Public key is empty!");
                return;
            }

            buttonOkPressed = true;

            this.Close();
        }

        private void Knapsack_FormClosing(object sender, FormClosingEventArgs e)
        {
            userChangedSmth = false;
            if (!buttonOkPressed)
            {
                CryptographyProject.EncryptionAlgorithms.KnapsackAlg._p[0] = -1;
                CryptographyProject.EncryptionAlgorithms.KnapsackAlg._j[0] = -1;
                this.mainForm.ClearAlgorithmsViewData();
            }
        }

        private void btnUserChanged_Click(object sender, EventArgs e)
        {
            userChangedSmth = true;
            this.SetUpKeys();
        }
    }
}
