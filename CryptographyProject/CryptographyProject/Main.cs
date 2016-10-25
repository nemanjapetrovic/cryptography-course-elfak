using System;
using System.Drawing;
using System.Windows.Forms;
using CryptographyProject.Controller;
using CryptographyProject.Model;

namespace CryptographyProject
{
    public partial class Main : Form
    {
        //My data
        private MainController mMainController;

        //Constructor
        public Main()
        {
            InitializeComponent();

            //Initialize my data
            mMainController = new MainController();
            mMainController.DataModel.Folders = new Folders();

            //Load the settings
            try
            {
                this.LoadSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Input folder set
        private void btnChangeInputFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dialogFolder.ShowDialog() == DialogResult.OK)
                {
                    mMainController.DataModel.Folders.InputFolder = dialogFolder.SelectedPath;
                    txtInputFolder.Text = mMainController.DataModel.Folders.InputFolder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Output folder set
        private void btnChangeOutputFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dialogFolder.ShowDialog() == DialogResult.OK)
                {
                    mMainController.DataModel.Folders.OutputFolder = dialogFolder.SelectedPath;
                    txtOutputFolder.Text = mMainController.DataModel.Folders.OutputFolder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Choose encryption algorithm
        private void listAlgorithms_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue != CheckState.Checked)
            {
                return;
            }

            var selectedItems = listAlgorithms.CheckedIndices;
            if (selectedItems.Count > 0)
            {
                listAlgorithms.SetItemChecked(selectedItems[0], false);
            }
            mMainController.DataModel.AlgorithmIndex = listAlgorithms.SelectedIndex;
        }

        //This function will enable controls if status = true, else it will disable all the controls
        private void EnableControls(Control con,bool status = false)
        {
            foreach (Control c in con.Controls)
            {
                EnableControls(c,status);
            }
            con.Enabled = status;
        }

        //StartingTheProcess of coding/decoding
        //View changes
        private void Started()
        {
            //Model
            mMainController.DataModel.CoderStarted = true;
            //Design            
            btnMain.Text = "S T O P";
            btnMain.ForeColor = Color.FromArgb(0x00FF0000);
            //Enable all controls = false
            EnableControls(this);
            //Only enable the important one
            btnMain.Enabled = true;
            btnMain.Parent.Enabled = true;
            logView.Enabled = true;
            logView.Parent.Enabled = true;
        }

        //Ending the process of coding/decoding
        //View changes
        private void Ended()
        {
            //Model
            mMainController.DataModel.CoderStarted = false;
            //Design
            btnMain.Text = "S T A R T";
            btnMain.ForeColor = Color.FromArgb(0x0000FF00);
            //Enable all controls
            EnableControls(this, true);
        }

        //Main button - start/stop the program
        private void btnMain_Click(object sender, EventArgs e)
        {
            if (mMainController.DataModel.CoderStarted)
            {
                //Stop the coder
                this.Ended();
                mMainController.StopTheProcess();
            }
            else
            {
                //Start the coder
                this.Started();
                mMainController.StartTheProcess();
            }
        }

        //Save the new settings data
        private void SaveSettings()
        {
            //True/false if the folders data is saved
            Properties.Settings.Default["Folders"] =
                chSaveLocation.Checked.ToString();

            //Save the data
            Properties.Settings.Default["InputFolder"] = (chSaveLocation.Checked)
                ? mMainController.DataModel.Folders.InputFolder
                : string.Empty;
            Properties.Settings.Default["OutputFolder"] = (chSaveLocation.Checked)
                ? mMainController.DataModel.Folders.OutputFolder
                : string.Empty;

            Properties.Settings.Default.Save();
        }

        //Reset the settings when error occured
        private void ResetSettings()
        {
            //Reset
            chSaveLocation.CheckedChanged -= chSaveLocation_CheckedChanged;
            chSaveLocation.Checked = false;
            chSaveLocation.CheckedChanged += chSaveLocation_CheckedChanged;

            //True/false if the folders data is saved
            Properties.Settings.Default["Folders"] =
                chSaveLocation.Checked.ToString();

            //Save the data
            Properties.Settings.Default["InputFolder"] = string.Empty;
            Properties.Settings.Default["OutputFolder"] = string.Empty;

            Properties.Settings.Default.Save();
        }

        //Load the settings and set the input and output folder fields
        private void LoadSettings()
        {
            //Load threads number settings
            mMainController.DataModel.ThreadsNumber = Int32.Parse(Properties.Settings.Default["ThreadsNumber"].ToString());
            threadsNumber.Value = mMainController.DataModel.ThreadsNumber;

            //Load folders data
            chSaveLocation.CheckedChanged -= chSaveLocation_CheckedChanged;
            chSaveLocation.Checked =
                bool.Parse(Properties.Settings.Default["Folders"].ToString());
            chSaveLocation.CheckedChanged += chSaveLocation_CheckedChanged;

            if (!chSaveLocation.Checked)
            {
                return;
            }

            mMainController.DataModel.Folders.InputFolder = Properties.Settings.Default["InputFolder"].ToString();
            txtInputFolder.Text = mMainController.DataModel.Folders.InputFolder;

            mMainController.DataModel.Folders.OutputFolder = Properties.Settings.Default["OutputFolder"].ToString();
            txtOutputFolder.Text = mMainController.DataModel.Folders.OutputFolder;
        }

        //Folder save
        //Save/NotSave the locations
        private void chSaveLocation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mMainController.DataModel.Folders.InputFolder = txtInputFolder.Text;
                mMainController.DataModel.Folders.OutputFolder = txtOutputFolder.Text;
                this.SaveSettings();
            }
            catch (Exception ex)
            {
                this.ResetSettings();
                MessageBox.Show(ex.Message);
            }
        }

        //Exit
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.SaveSettings();
            }
            catch (Exception)
            {
                this.ResetSettings();
            }
        }

        //Change threads number
        private void threadsNumber_ValueChanged(object sender, EventArgs e)
        {
            mMainController.DataModel.ThreadsNumber = (int)threadsNumber.Value;
            Properties.Settings.Default["ThreadsNumber"] = mMainController.DataModel.ThreadsNumber;
            Properties.Settings.Default.Save();
        }


    }
}
