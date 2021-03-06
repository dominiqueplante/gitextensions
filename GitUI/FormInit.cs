﻿using System;
using System.IO;
using System.Windows.Forms;
using GitCommands;
using GitCommands.Repository;
using ResourceManager.Translation;

namespace GitUI
{
    public partial class FormInit : GitExtensionsForm
    {
        private readonly TranslationString _chooseDirectory =
            new TranslationString("Please choose a directory.");

        private readonly TranslationString _chooseDirectoryCaption =
            new TranslationString("Choose directory");

        private readonly TranslationString _chooseDirectoryNotFile =
            new TranslationString("Cannot initialize a new repository on a file.\nPlease choose a directory.");

        private readonly TranslationString _chooseDirectoryNotFileCaption =
            new TranslationString("Error");

        private readonly TranslationString _initMsgBoxCaption =
            new TranslationString("Create new repository");

        

        public FormInit(string dir)
        {
            InitializeComponent();
            Translate();
            Directory.Text = dir;
        }

        public FormInit()
        {
            InitializeComponent();
            Translate();

            if (!Settings.Module.IsValidWorkingDirectory)
                Directory.Text = Settings.WorkingDir;
        }

        private void DirectoryDropDown(object sender, EventArgs e)
        {
            Directory.DataSource = Repositories.RepositoryHistory.Repositories;
            Directory.DisplayMember = "Path";
        }

        private void InitClick(object sender, EventArgs e)
        {
            string trimmedDirectoryText = Directory.Text.Trim();
            if (string.IsNullOrEmpty(trimmedDirectoryText))
            {
                MessageBox.Show(this, _chooseDirectory.Text, _chooseDirectoryCaption.Text);
                return;
            }

            if (File.Exists(trimmedDirectoryText))
            {
                MessageBox.Show(this, _chooseDirectoryNotFile.Text,_chooseDirectoryNotFileCaption.Text);
                return;
            }

            Settings.WorkingDir = trimmedDirectoryText;

            if (!System.IO.Directory.Exists(Settings.WorkingDir))
                System.IO.Directory.CreateDirectory(Settings.WorkingDir);

            MessageBox.Show(this, Settings.Module.Init(Central.Checked, Central.Checked), _initMsgBoxCaption.Text);

            Repositories.AddMostRecentRepository(trimmedDirectoryText);

            Close();
        }

        private void BrowseClick(object sender, EventArgs e)
        {
            var browseDialog = new FolderBrowserDialog();

            if (browseDialog.ShowDialog(this) == DialogResult.OK)
                Directory.Text = browseDialog.SelectedPath;
        }
    }
}