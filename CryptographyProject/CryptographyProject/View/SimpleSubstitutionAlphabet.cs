using CryptographyProject.EncryptionAlgorithms;
using CryptographyProject.Helper;
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

namespace CryptographyProject.View
{
    public partial class SimpleSubstitutionAlphabet : Form
    {
        private Main mainForm;
        private bool buttonOkPressed = false;

        public SimpleSubstitutionAlphabet(Main mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void SimpleSubstitutionAlphabet_Load(object sender, EventArgs e)
        {
            lblStandrad.Text = new string(SimpleSubstituionCipher.StandardAlphabet);
            txtEncryptionAlphabet.KeyPress += txtEncryptionAlphabet_TextChanged;
        }

        //Checking for duplicates in the key
        public bool CheckForDuplicates()
        {
            char[] myArray = txtEncryptionAlphabet.Text.ToCharArray();
            char[] newArray = myArray.Distinct().ToArray();

            if (myArray.Length != newArray.Length)
            {
                return true;
            }
            return false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Number of characters error
            if (txtEncryptionAlphabet.Text.Length < Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS ||
                txtEncryptionAlphabet.Text.Length > Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS)
            {
                MessageBox.Show("Alphabet is not valid! You need to insert exactly 26 characters!");
                return;
            }

            //Duplicates error
            if (CheckForDuplicates())
            {
                MessageBox.Show("There are duplicates in the encryption alphabet! Insert a new one!");
                return;
            }

            SimpleSubstituionCipher.EncryptionAlphabetChars = txtEncryptionAlphabet.Text.ToCharArray();
            buttonOkPressed = true;
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

                            //Key must contain only a-z and A-Z chars
                            if (!System.Text.RegularExpressions.Regex.IsMatch(key, @"^[a-zA-Z]+$"))
                            {
                                MessageBox.Show("This textbox accepts only alphabetical characters!");
                                return;
                            }

                            //Key length is not valid
                            if (key.Length < Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS ||
                                key.Length > Constants.SimpleSubstitutionAlgorithm.NUMBER_OF_CHARS)
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

        private void SimpleSubstitutionAlphabet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!buttonOkPressed)
            {
                this.mainForm.ClearAlgorithmsViewData();
            }
        }
    }
}
