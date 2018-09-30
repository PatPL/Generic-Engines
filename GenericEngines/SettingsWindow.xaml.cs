﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace GenericEngines {
	public partial class SettingsWindow : Window {

		// Use settings.snippet
		// sett [Tab + Tab]
		
		public bool AdvConfirmBox {
			get {
				return bool.Parse (Settings.Get (Setting.AdvConfirmBox));
			}
			set {
				Settings.Set (Setting.AdvConfirmBox, value.ToString ());
			}
		}

		public string DefaultSaveDirectory {
			get {
				return Settings.Get (Setting.DefaultSaveDirectory);
			}
			set {
				Settings.Set (Setting.DefaultSaveDirectory, value);
			}
		}


		public string DefaultExportDirectory {
			get {
				return Settings.Get (Setting.DefaultExportDirectory);
			}
			set {
				Settings.Set (Setting.DefaultExportDirectory, value);
			}
		}

		// /Settings

		object lastMouseDownObject;
		private void registerMouseDown (object sender, MouseButtonEventArgs e) {
			lastMouseDownObject = sender;
		}

		public SettingsWindow () {
			InitializeComponent ();

			this.DataContext = this;
		}

		private void defaultSaveDirectory_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog ();

				System.Windows.Forms.DialogResult result = folderDialog.ShowDialog ();

				if (result == System.Windows.Forms.DialogResult.OK) {
					DefaultSaveDirectory = folderDialog.SelectedPath;
					DefaultSaveDirectoryTextBox.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void defaultExportDirectory_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog ();

				System.Windows.Forms.DialogResult result = folderDialog.ShowDialog ();

				if (result == System.Windows.Forms.DialogResult.OK) {
					DefaultExportDirectory = folderDialog.SelectedPath;
					DefaultExportDirectoryTextBox.Text = folderDialog.SelectedPath;
				}
			}
		}

		private void steamDirectory_MouseUp (object sender, MouseButtonEventArgs e) {

			string x86PFDir = $"{Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86)}\\Steam\\steamapps\\common\\Kerbal Space Program\\GameData\\GenericEngines\\";
			
			if (Directory.Exists (x86PFDir)) {
				DefaultExportDirectory = x86PFDir;
				DefaultExportDirectoryTextBox.Text = x86PFDir;
				MessageBox.Show ($"Default engine config export set to: {x86PFDir}");
			} else {
				MessageBox.Show ($"Steam KSP not found in default directory: {x86PFDir}");
			}
		}
	}
}