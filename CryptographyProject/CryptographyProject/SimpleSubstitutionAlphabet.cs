using CryptographyProject.EncryptionAlgorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptographyProject
{
    public partial class SimpleSubstitutionAlphabet : Form
    {
        public SimpleSubstitutionAlphabet()
        {
            InitializeComponent();
        }

        private void SimpleSubstitutionAlphabet_Load(object sender, EventArgs e)
        {
            lblStandrad.Text = new string(SimpleSubstituionCipher.StandardAlphabet);
            txtEncryptionAlphabet.KeyPress += txtEncryptionAlphabet_TextChanged;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtEncryptionAlphabet.Text.Length < SimpleSubstituionCipher.NUMBER_OF_CHARS || txtEncryptionAlphabet.Text.Length > SimpleSubstituionCipher.NUMBER_OF_CHARS)
            {
                MessageBox.Show("Alphabet is not valid! You need to insert exactly 26 characters!");
                return;
            }

            SimpleSubstituionCipher.EncryptionAlphabetChars = txtEncryptionAlphabet.Text.ToCharArray();
            this.Close();
        }

        private void txtEncryptionAlphabet_TextChanged(object sender, KeyPressEventArgs e)
        {
            Char pressedKey = e.KeyChar;
            if (Char.IsLetter(pressedKey) || e.KeyChar == '\b')
            {
                // Allow input.
                e.Handled = false;
            }
            else
            {
                // Stop the character from being entered into the control since not a letter, nor backspace
                e.Handled = true;
            }

            e.KeyChar = Char.ToUpper(e.KeyChar);
            if (txtEncryptionAlphabet.Text.Contains(e.KeyChar.ToString().ToUpper()))
            {
                MessageBox.Show("You already inserted this character!");
                txtEncryptionAlphabet.Text.Remove(txtEncryptionAlphabet.Text.Length - 1);
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var dialog = openFileDialog.OpenFile())
                    {
                        using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                        {
                            string key = sr.ReadLine().ToUpper();
                            if (!System.Text.RegularExpressions.Regex.IsMatch(key, @"^[a-zA-Z]+$"))
                            {
                                MessageBox.Show("This textbox accepts only alphabetical characters!");
                                return;
                            }

                            if (key.Distinct().Count() < SimpleSubstituionCipher.NUMBER_OF_CHARS)
                            {
                                MessageBox.Show("You already inserted this character!");
                                return;
                            }

                            if(key.Length < SimpleSubstituionCipher.NUMBER_OF_CHARS || key.Length > SimpleSubstituionCipher.NUMBER_OF_CHARS)
                            {
                                MessageBox.Show("The key is not valid length!");
                                return;
                            }
                            txtEncryptionAlphabet.Text = key;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
