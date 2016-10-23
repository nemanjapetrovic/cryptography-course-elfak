using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyProject.Logic;

namespace CryptographyProject
{
    public partial class Main : Form
    {
        //Resource data  
        private ResourceManager mResourceManager;

        //My data
        private Folders mFolders;
        private MainController mMainController;


        //Constructor
        public Main()
        {
            InitializeComponent();

            //Initialize the resource data            
            mResourceManager = new ResourceManager(typeof(CryptographyProject.Properties.Resources));

            //Initialize my data
            mFolders = new Folders();
            mMainController = new MainController();

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
                    mFolders.InputFolder = dialogFolder.SelectedPath;
                    txtInputFolder.Text = mFolders.InputFolder;
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
                    mFolders.OutputFolder = dialogFolder.SelectedPath;
                    txtOutputFolder.Text = mFolders.OutputFolder;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Choose encryption algorithm
        private void listAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Main button - start/stop the program
        private void btnMain_Click(object sender, EventArgs e)
        {

        }

        //Save the new settings data
        private void SaveSettings()
        {
            //True/false if the folders data is saved
            Properties.Settings.Default["Folders"] =
                chSaveLocation.Checked.ToString();

            //Save the data
            Properties.Settings.Default["InputFolder"] = (chSaveLocation.Checked)
                ? mFolders.InputFolder
                : string.Empty;
            Properties.Settings.Default["OutputFolder"] = (chSaveLocation.Checked)
                ? mFolders.OutputFolder
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
            chSaveLocation.CheckedChanged -= chSaveLocation_CheckedChanged;
            chSaveLocation.Checked =
                bool.Parse(Properties.Settings.Default["Folders"].ToString());
            chSaveLocation.CheckedChanged += chSaveLocation_CheckedChanged;

            if (!chSaveLocation.Checked)
            {
                return;
            }

            mFolders.InputFolder = Properties.Settings.Default["InputFolder"].ToString();
            txtInputFolder.Text = mFolders.InputFolder;

            mFolders.OutputFolder = Properties.Settings.Default["OutputFolder"].ToString();
            txtOutputFolder.Text = mFolders.OutputFolder;
        }

        //Folder save
        //Save/NotSave the locations
        private void chSaveLocation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                mFolders.InputFolder = txtInputFolder.Text;
                mFolders.OutputFolder = txtOutputFolder.Text;
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
            catch (Exception ex)
            {
                this.ResetSettings();
            }
        }
    }
}
