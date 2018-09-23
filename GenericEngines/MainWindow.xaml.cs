using System;
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

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		bool isEdited = false;
		DataGrid mainDataGrid;

		private string _currentFile = null;
		string currentFile {
			get {
				return _currentFile;
			} set {
				_currentFile = value;
				Title = $"Generic Engines | {_currentFile}";
			}
		}

		public MainWindow () {
			InitializeComponent ();
		}

		List<Engine> Engines {
			get {
				if (mainDataGrid != null) {
					return (List<Engine>) mainDataGrid.ItemsSource;
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			} set {
				if (mainDataGrid != null) {
					mainDataGrid.ItemsSource = null;
					mainDataGrid.ItemsSource = value;
					mainDataGrid.Items.Refresh ();
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			}
		}

		private void addButton_MouseUp (object sender, MouseButtonEventArgs e) {
			mainDataGrid.CommitEdit ();
			mainDataGrid.CancelEdit ();

			Engines.Add (new Engine ());
			mainDataGrid.Items.Refresh ();
		}

		private void removeButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (mainDataGrid.SelectedIndex != -1) {
				if (ConfirmBox.Show ($"You are about to delete\n{mainDataGrid.SelectedItems.Count} item(s). Are you sure?")) {

					foreach (Engine i in mainDataGrid.SelectedItems) {
						Engines.Remove (i);
					}

					mainDataGrid.Items.Refresh ();
				}
			}
		}

		private void saveButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (currentFile == null) {
				saveasButton_MouseUp (null, null);
			} else {
				if (ConfirmBox.Show ($"You are about to overwrite the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file. Are you sure?")) {
					saveEnginesToFile (currentFile);
				}
			}
		}

		private void saveasButton_MouseUp (object sender, MouseButtonEventArgs e) {
			Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog ();
			if (!Directory.Exists ($"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\")) {
				Directory.CreateDirectory ($"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\");
			}
			fileDialog.InitialDirectory = $"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\";
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

		private void openButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if ((currentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (currentFile))} file will be lost! Are you sure you want to open other file?")) {
				Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog ();
				if (!Directory.Exists ($"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\")) {
					Directory.CreateDirectory ($"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\");
				}
				fileDialog.InitialDirectory = $"{Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments)}\\Generic Engines\\Saves\\";
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

		private void exportButton_MouseUp (object sender, MouseButtonEventArgs e) {
			
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
		}

		private void mainDataGrid_CellEditEnding (object sender, DataGridCellEditEndingEventArgs e) {
			isEdited = false;
		}

		void saveEnginesToFile (string path) {
			FileStream file = new FileStream (path, FileMode.OpenOrCreate, FileAccess.Write);
			file.SetLength (0);

			byte[] serializedEngine;
			foreach (Engine i in Engines) {
				serializedEngine = i.Serialize ();
				file.Write (serializedEngine, 0, serializedEngine.Length);
			}

			file.Close ();
		}

		void readEnginesFromFile (string path) {
			FileStream file = new FileStream (path, FileMode.Open, FileAccess.Read);

			Engines.Clear ();
			List<Engine> newEngines = new List<Engine> ();

			byte[] data = new byte[file.Length];
			file.Read (data, 0, (int) file.Length);

			int offset = 0;

			while (offset < data.Length) {
				newEngines.Add (Engine.Deserialize (data, out int addedOffset, offset));
				offset += addedOffset;
			}

			Engines = newEngines;
			file.Close ();
		}

		private void propellantCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (FuelType)).Cast<FuelType> ();
		}

		private void propellentDataGrid_Loaded (object sender, RoutedEventArgs e) {

		}

		private void propellentDataGrid_KeyUp (object sender, KeyEventArgs e) {

		}

		private void propellentDataGrid_CellEditEnding (object sender, DataGridCellEditEndingEventArgs e) {

		}

		private void propellentDataGrid_BeginningEdit (object sender, DataGridBeginningEditEventArgs e) {

		}

		private void addPropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {

		}

		private void removePropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {

		}
	}
}
