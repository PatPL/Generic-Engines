﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		object lastMouseDownObject;

		bool isEdited = false;
		DataGrid mainDataGrid;
		DataGrid currentFuelRatioGrid;
		FuelRatioList currentFuelRatioList;

		private string _currentFile = null;
		string currentFile {
			get {
				return _currentFile;
			}
			set {
				_currentFile = value;
				Title = $"Generic Engines | {_currentFile}";
			}
		}

		public MainWindow () {
			InitializeComponent ();
		}

		FuelRatioList currentFuelRatios {
			get {
				if (currentFuelRatioGrid != null) {
					return (FuelRatioList) currentFuelRatioGrid.ItemsSource;
				} else {
					throw new NullReferenceException ("currentFuelRatioGrid is null");
				}
			}
			set {
				if (currentFuelRatioGrid != null) {
					currentFuelRatioGrid.ItemsSource = null;
					currentFuelRatioGrid.ItemsSource = value;
					currentFuelRatioGrid.Items.Refresh ();
				} else {
					throw new NullReferenceException ("currentFuelRatioGrid is null");
				}
			}
		}

		List<Engine> Engines {
			get {
				if (mainDataGrid != null) {
					return (List<Engine>) mainDataGrid.ItemsSource;
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			}
			set {
				if (mainDataGrid != null) {
					mainDataGrid.ItemsSource = null;
					mainDataGrid.ItemsSource = value;
					mainDataGrid.Items.Refresh ();
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			}
		}

		private void registerMouseDown (object sender, MouseButtonEventArgs e) {
			lastMouseDownObject = sender;
		}

		private void addButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				mainDataGrid.CommitEdit ();
				mainDataGrid.CancelEdit ();

				Engines.Add (new Engine ());
				mainDataGrid.Items.Refresh ();
			}
		}

		private void removeButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (mainDataGrid.SelectedIndex != -1) {
					if (ConfirmBox.Show ($"You are about to delete\n{mainDataGrid.SelectedItems.Count} item(s). Are you sure?")) {

						mainDataGrid.CommitEdit ();
						mainDataGrid.CancelEdit ();

						foreach (Engine i in mainDataGrid.SelectedItems) {
							Engines.Remove (i);
						}

						mainDataGrid.Items.Refresh ();
					}
				}
			}
		}

		private void saveButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (currentFile == null) {
					saveasButton_MouseUp (null, null);
				} else {
					if (ConfirmBox.Show ($"You are about to overwrite the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file. Are you sure?")) {
						saveEnginesToFile (currentFile);
					}
				}
			}
		}

		private void saveasButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog ();
				if (!Directory.Exists (Settings.Get (Setting.DefaultSaveDirectory))) {
					Directory.CreateDirectory (Settings.Get (Setting.DefaultSaveDirectory));
				}
				fileDialog.InitialDirectory = Settings.Get (Setting.DefaultSaveDirectory);
				fileDialog.FileName = "Unnamed Engine List";
				fileDialog.DefaultExt = ".enl";
				fileDialog.Filter = "Engine Lists|*.enl";

				bool? result = fileDialog.ShowDialog ();

				if (result != null && result == true) {
					currentFile = fileDialog.FileName;
					saveEnginesToFile (currentFile);
				} else {

				}
			}
		}

		private void openButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if ((currentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file will be lost! Are you sure you want to open other file?")) {
					Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog ();
					if (!Directory.Exists (Settings.Get (Setting.DefaultSaveDirectory))) {
						Directory.CreateDirectory (Settings.Get (Setting.DefaultSaveDirectory));
					}
					fileDialog.InitialDirectory = Settings.Get (Setting.DefaultSaveDirectory);
					fileDialog.FileName = "";
					fileDialog.DefaultExt = ".enl";
					fileDialog.Filter = "Engine Lists|*.enl";

					bool? result = fileDialog.ShowDialog ();

					if (result != null && result == true) {
						currentFile = fileDialog.FileName;
						readEnginesFromFile (currentFile);
					} else {

					}
				}
			}
		}

		private void exportButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (Engines.Count > 0) {
					Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog ();
					if (!Directory.Exists (Settings.Get (Setting.DefaultExportDirectory))) {
						Directory.CreateDirectory (Settings.Get (Setting.DefaultExportDirectory));
					}
					fileDialog.InitialDirectory = Settings.Get (Setting.DefaultExportDirectory);
					fileDialog.FileName = currentFile == null ? "Unnamed Engine Configs" : System.IO.Path.GetFileNameWithoutExtension (currentFile);
					fileDialog.DefaultExt = ".cfg";
					fileDialog.Filter = "Engine Configs|*.cfg";

					bool? result = fileDialog.ShowDialog ();

					if (result != null && result == true) {
						ExportEnginesToFile (fileDialog.FileName);
					} else {

					}
				}
			}
		}

		private void newButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if ((currentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file will be lost! Are you sure you want to open empty file?")) {
					currentFile = null;

					Engines = new List<Engine> ();

					/*
					List<Engine> newEngines = new List<Engine> ();
					for (int i = 0; i < 28; ++i) {
						newEngines.Add (new Engine {
							Active = true,
							Name = $"Plume test {((Plume) i).ToString ()}",
							Width = 0.4,
							Height = 1.4,
							PlumeID = (Plume) i
						});
					}
					Engines = newEngines;
					*/
				}
			}
		}

		private void helpButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				new About ().ShowDialog ();
			}
		}

		private void settingsButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				new SettingsWindow ().ShowDialog ();
			}
		}

		private void mainDataGrid_Loaded (object sender, RoutedEventArgs e) {
			mainDataGrid = ((DataGrid) sender);
			mainDataGrid.ItemsSource = new List<Engine> ();
			mainDataGrid.Items.Refresh ();
		}

		private void mainDataGrid_KeyUp (object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete && !isEdited) {
				removeButton_MouseUp (null, null);
			}
		}

		private void mainDataGrid_BeginningEdit (object sender, DataGridBeginningEditEventArgs e) {
			isEdited = true;

			if (e.Column.Header != null && e.Column.Header.ToString () == "Propellants") {
				currentFuelRatioList = ((Engine) e.Row.Item).PropellantRatio;
			}
		}

		private void mainDataGrid_CellEditEnding (object sender, DataGridCellEditEndingEventArgs e) {
			isEdited = false;
		}

		void ExportEnginesToFile (string path) {
			File.WriteAllText (path, Exporter.ConvertEngineListToConfig (Engines));

			string pathDirectory = new FileInfo (path).Directory.FullName;

			File.WriteAllBytes ($"{pathDirectory}/PlumeScaleFixer.dll", Properties.Resources.GenericEnginesPlumeScaleFixer);
		}

		void saveEnginesToFile (string path) {
			FileStream file = new FileStream (path, FileMode.OpenOrCreate, FileAccess.Write);
			file.SetLength (0);

			byte[] serializedEngine;
			foreach (Engine i in Engines) {

				//serializedEngine = i.Serialize ();
				serializedEngine = Serializer.Serialize (i);

				file.Write (serializedEngine, 0, serializedEngine.Length);
			}

			file.Close ();
		}

		void readEnginesFromFile (string path, bool append = false) {
			FileStream file = new FileStream (path, FileMode.Open, FileAccess.Read);

			if (!append) {
				Engines.Clear ();
			}
			List<Engine> newEngines = (append ? Engines : new List<Engine> ());

			byte[] data = new byte[file.Length];
			file.Read (data, 0, (int) file.Length);

			int offset = 0;

			while (offset < data.Length) {

				//newEngines.Add (Engine.Deserialize (data, out int addedOffset, offset));
				newEngines.Add (Serializer.Deserialize (data, out int addedOffset, offset));

				offset += addedOffset;
			}

			Engines = newEngines;
			file.Close ();
		}

		private void propellantCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (FuelType)).Cast<FuelType> ();
		}

		private void propellentDataGrid_Loaded (object sender, RoutedEventArgs e) {
			currentFuelRatioGrid = ((DataGrid) sender);
			currentFuelRatioGrid.ItemsSource = currentFuelRatioList;
			((Grid) currentFuelRatioGrid.Parent).UpdateLayout ();
		}

		private void propellentDataGrid_KeyUp (object sender, KeyEventArgs e) {

		}

		private void propellentDataGrid_CellEditEnding (object sender, DataGridCellEditEndingEventArgs e) {

		}

		private void propellentDataGrid_BeginningEdit (object sender, DataGridBeginningEditEventArgs e) {

		}

		private void addPropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				currentFuelRatioGrid.CommitEdit ();
				currentFuelRatioGrid.CancelEdit ();

				currentFuelRatios.Add (new FuelRatioElement ());
				currentFuelRatioGrid.Items.Refresh ();
			}
		}

		private void removePropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (currentFuelRatioGrid.SelectedIndex != -1) {
					foreach (FuelRatioElement i in currentFuelRatioGrid.SelectedItems) {
						currentFuelRatios.Remove (i);
					}

					currentFuelRatioGrid.Items.Refresh ();
				}
			}
		}

		private void appendButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog ();
				if (!Directory.Exists (Settings.Get (Setting.DefaultSaveDirectory))) {
					Directory.CreateDirectory (Settings.Get (Setting.DefaultSaveDirectory));
				}
				fileDialog.InitialDirectory = Settings.Get (Setting.DefaultSaveDirectory);
				fileDialog.FileName = "";
				fileDialog.DefaultExt = ".enl";
				fileDialog.Filter = "Engine Lists|*.enl";
				fileDialog.Multiselect = true;

				bool? result = fileDialog.ShowDialog ();

				if (result != null && result == true) {
					foreach (string i in fileDialog.FileNames)
					readEnginesFromFile (i, true);
				} else {

				}
			}
		}

		private void mainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) {
			if ((currentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file will be lost! Are you sure you want to close Generic Engines?")) {

			} else {
				e.Cancel = true;
			}
		}

		private void modelCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (Model)).Cast<Model> ();
		}

		private void plumeCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (Plume)).Cast<Plume> ();
		}
	}
}
