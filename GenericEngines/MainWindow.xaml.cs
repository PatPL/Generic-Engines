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
using System.Globalization;
using System.ComponentModel;

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		object lastMouseDownObject;

		bool isEdited = false;
		DataGrid mainDataGrid;
		DataGrid currentFuelRatioGrid;
		List<FuelRatioElement> currentFuelRatioList;

		private string _currentFile = null;
		string CurrentFile {
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

		List<FuelRatioElement> CurrentFuelRatios {
			get {
				if (currentFuelRatioGrid != null) {
					return (List<FuelRatioElement>) currentFuelRatioGrid.ItemsSource;
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

					mainDataGrid.CommitEdit ();
					mainDataGrid.CancelEdit ();

					mainDataGrid.ItemsSource = null;
					mainDataGrid.ItemsSource = value;
					RefreshEngines ();
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			}
		}

		private void RefreshEngines () {
			if (mainDataGrid != null) {

				mainDataGrid.CommitEdit ();
				mainDataGrid.CancelEdit ();

				ICollectionView view = CollectionViewSource.GetDefaultView (mainDataGrid.ItemsSource);
				if (view != null) {
					view.SortDescriptions.Clear ();
					foreach (DataGridColumn column in mainDataGrid.Columns) {
						column.SortDirection = null;
					}
				}

				EnsureEnginePolymorphismConsistency ();

				mainDataGrid.Items.Refresh ();
			} else {
				throw new NullReferenceException ("mainDataGrid is null");
			}
		}

		private void RegisterMouseDown (object sender, MouseButtonEventArgs e) {
			lastMouseDownObject = sender;
		}

		private void DuplicateButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				mainDataGrid.CommitEdit ();
				mainDataGrid.CancelEdit ();

				foreach (Engine i in mainDataGrid.SelectedItems) {
					//Creates a copy
					Engines.Add (Serializer.Deserialize (Serializer.Serialize (i), out int _));
				}

				mainDataGrid.UnselectAll ();

				RefreshEngines ();
			}
		}

		private void AddButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				mainDataGrid.CommitEdit ();
				mainDataGrid.CancelEdit ();

				Engine newEngine = new Engine ();

				/* Changed Setting.AvoidCollisionOnNewEngine's function to export only
				if (Settings.GetBool (Setting.AvoidCollisionOnNewEngine)) {
					string defaultName = newEngine.Name;
					int counter = 1;

					while (Engines.Exists (x => x.Name == newEngine.Name)) {
						newEngine.Name = $"{defaultName} {counter++}";
					}
				}
				*/

				Engines.Add (newEngine);
				RefreshEngines ();
			}
		}

		private void RemoveButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (mainDataGrid.SelectedIndex != -1) {
					if (ConfirmBox.Show ($"You are about to delete\n{mainDataGrid.SelectedItems.Count} item(s). Are you sure?")) {

						mainDataGrid.CommitEdit ();
						mainDataGrid.CancelEdit ();

						foreach (Engine i in mainDataGrid.SelectedItems) {
							Engines.Remove (i);
						}

						RefreshEngines ();
					}
				}
			}
		}

		private void SaveButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (CurrentFile == null) {
					SaveasButton_MouseUp (null, null);
				} else {
					if (ConfirmBox.Show ($"You are about to overwrite the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (CurrentFile))} file. Are you sure?")) {
						SaveEnginesToFile (CurrentFile);
					}
				}
			}
		}

		private void SaveasButton_MouseUp (object sender, MouseButtonEventArgs e) {
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
					CurrentFile = fileDialog.FileName;
					SaveEnginesToFile (CurrentFile);
				} else {

				}
			}
		}

		private void OpenButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if ((CurrentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (CurrentFile))} file will be lost! Are you sure you want to open other file?")) {
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
						CurrentFile = fileDialog.FileName;
						ReadEnginesFromFile (CurrentFile);
					} else {

					}
				}

				RefreshEngines ();

			}
		}

		private void ExportButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (Engines.Count > 0) {
					Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog ();
					if (!Directory.Exists (Settings.Get (Setting.DefaultExportDirectory))) {
						Directory.CreateDirectory (Settings.Get (Setting.DefaultExportDirectory));
					}
					fileDialog.InitialDirectory = Settings.Get (Setting.DefaultExportDirectory);
					fileDialog.FileName = CurrentFile == null ? "Unnamed Engine Configs" : System.IO.Path.GetFileNameWithoutExtension (CurrentFile);
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

		private void NewButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if ((CurrentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (CurrentFile))} file will be lost! Are you sure you want to open empty file?")) {
					CurrentFile = null;

					Engines = new List<Engine> ();

					/*
					List<Engine> newEngines = new List<Engine> ();
					for (int i = 0; i < 28; ++i) {
						newEngines.Add (new Engine {
							Active = true,
							Name = $"Plume test {((Plume) i).ToString ()}",
							Width = 0.4,
							Height = 1.4,
							ModelID = Model.Thruster,
							PlumeID = (Plume) i
						});
					}
					Engines = newEngines;
					*/
				}
			}
		}

		private void HelpButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				new About ().ShowDialog ();
			}
		}

		private void SettingsButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				new SettingsWindow ().ShowDialog ();
				foreach (Engine i in Engines) {
					i.NotifyEveryProperty ();
				}
			}
		}

		private void MainDataGrid_Loaded (object sender, RoutedEventArgs e) {
			mainDataGrid = ((DataGrid) sender);
			mainDataGrid.ItemsSource = new List<Engine> ();
			RefreshEngines ();
		}

		private void MainDataGrid_KeyUp (object sender, KeyEventArgs e) {
			if (e.Key == Key.Delete && !isEdited) {
				RemoveButton_MouseUp (null, null);
			}
		}

		private void MainDataGrid_BeginningEdit (object sender, DataGridBeginningEditEventArgs e) {
			isEdited = true;

			MainDataGrid_SetCurrentGDList (sender, e);

			if (e.Column.Header != null && e.Column.Header.ToString () == "Propellants") {
				currentFuelRatioList = ((Engine) e.Row.Item).PropellantRatio;
			}
		}

		private void MainDataGrid_CellEditEnding (object sender, DataGridCellEditEndingEventArgs e) {
			isEdited = false;

			GD_Unload (sender, e);

			if (/*e.Column.SortMemberPath == "EngineName"*/ true) { //Auto resizing seems nice on every column. I'll leave this for now
				e.Column.Width = new DataGridLength (0);
				e.Column.Width = new DataGridLength (0, DataGridLengthUnitType.Auto);
			}

			if ((string) e.Column.Header == "Polymorphism") {
				EnsureEnginePolymorphismConsistency ();
				((Engine) e.Row.Item).NotifyPropertyChanged ("PolyType");
			}
		}

		List<Engine> FixDuplicateID (List<Engine> input, out bool foundDuplicate) {
			List<Engine> output = new List<Engine> ();
			foundDuplicate = false;

			foreach (Engine i in input) {
				Engine copy = Serializer.Deserialize (Serializer.Serialize (i), out int _);
				string originalName = copy.Name;
				int counter = 1;

				while (output.Exists (x => x.Name == copy.Name)) {
					copy.Name = $"{originalName} {counter++}";
					foundDuplicate |= true;
				}

				output.Add (copy);
			}

			return output;
		}

		void ExportEnginesToFile (string path) {
			try {
				int exportedEnginesCount = 0;
				if (Settings.GetBool (Setting.AvoidCollisionOnNewEngine)) {
					File.WriteAllText (path, Exporter.ConvertEngineListToConfig (FixDuplicateID (Engines, out bool foundDuplicate), out exportedEnginesCount));

					if (foundDuplicate) {
						MessageBox.Show ($"Warning! ID duplicates detected. Exporter fixed it, but ID duplicates might cause engines to disappear in game, if one of the duplicates gets removed, or added. Please, try to avoid duplicating IDs. If possible, change IDs and reexport the engines.", "Warning");
					}
				} else {
					File.WriteAllText (path, Exporter.ConvertEngineListToConfig (Engines, out exportedEnginesCount));
				}

				string pathDirectory = new FileInfo (path).Directory.FullName;

				File.WriteAllBytes ($"{pathDirectory}/PlumeScaleFixer.dll", Properties.Resources.GenericEnginesPlumeScaleFixer);
				File.WriteAllText ($"{pathDirectory}/GEAllTankDefinition.cfg", AllTankDefinition.Get);
				MessageBox.Show ($"{exportedEnginesCount} engines succesfully exported to {path}", "Success");
			} catch (Exception e) {
				App.SaveExceptionToFile (e);
				MessageBox.Show ($"Something went wrong while exporting engines to {path}. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
			}
		}

		void SaveEnginesToFile (string path) {
			try {
				FileStream file = new FileStream (path, FileMode.OpenOrCreate, FileAccess.Write);
				file.SetLength (0);

				byte[] serializedEngine;
				foreach (Engine i in Engines) {

					//serializedEngine = i.Serialize ();
					serializedEngine = Serializer.Serialize (i);

					file.Write (serializedEngine, 0, serializedEngine.Length);
				}

				file.Close ();
				MessageBox.Show ($"{Engines.Count} engines succesfully saved to {path}", "Success");
			} catch (Exception e) {
				App.SaveExceptionToFile (e);
				MessageBox.Show ($"Something went wrong while saving engines to {path}. Try to choose different location. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
			}
		}

		void ReadEnginesFromFile (string path, bool append = false) {
			try {
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
				file.Close ();

				Engines = newEngines;
			} catch (Exception e) {
				App.SaveExceptionToFile (e);
				MessageBox.Show ($"Something went wrong while reading engines from {path}. Your .enl file might be corrupt. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
			}
		}

		private void PropellantCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (FuelType)).Cast<FuelType> ();
		}

		private void PropellentDataGrid_Loaded (object sender, RoutedEventArgs e) {
			currentFuelRatioGrid = ((DataGrid) sender);
			currentFuelRatioGrid.ItemsSource = currentFuelRatioList;
			((Grid) currentFuelRatioGrid.Parent).UpdateLayout ();
		}

		// Generic Datagrid input (GD)
		// I can't believe this actually works :D
		//
		// Requirements:
		// The datagrid has to be in the <DataGridTemplateColumn.CellEditingTemplate>
		// The datagrid has to be in the Grid
		// 
		// Datagrid events:
		// Loaded="GD_Loaded"
		// 
		// Button events:
		// MouseUp="addGD_MouseUp"
		// MouseUp="removeGD_MouseUp"
		// 

		private DataGrid CurrentGD = null;
		private List<object> CurrentGDList;
		private Type CurrentGDType;
		private readonly Dictionary<string, Type> GDListTypes = new Dictionary<string, Type> {
			{ "Propellants", typeof (FuelRatioElement) },
			{ "Tank", typeof (FuelRatioElement) },
			{ "Thrust Curve", typeof (DoubleTuple) }
		};

		private void MainDataGrid_SetCurrentGDList (object sender, DataGridBeginningEditEventArgs e) {
			if (e.Column.Header != null) {
				switch (e.Column.Header.ToString ()) {
					case "Propellants":
					CurrentGDList = ((Engine) e.Row.Item).PropellantRatio.ToList<object> ();
					break;
					case "Tank":
					CurrentGDList = ((Engine) e.Row.Item).TanksContents.ToList<object> ();
					break;
					case "Thrust Curve":
					CurrentGDList = ((Engine) e.Row.Item).ThrustCurve.ToList<object> ();
					break;
					default:
					return;
				}

				CurrentGDType = GDListTypes[e.Column.Header.ToString ()];
			}
		}

		private void GD_Loaded (object sender, RoutedEventArgs e) {
			//Sets current DataGrid
			CurrentGD = (DataGrid) sender;

			//Set ItemsSource
			CurrentGD.ItemsSource = CurrentGDList;

			//Grid sizing is bugged, needs a refresh
			((Grid) CurrentGD.Parent).UpdateLayout ();
		}

		private void GD_Unload (object sender, DataGridCellEditEndingEventArgs e) {
			if (CurrentGD != null) {


				CurrentGD.CommitEdit ();
				CurrentGD.CancelEdit ();

				if (e.Column.Header != null) {
					switch (e.Column.Header.ToString ()) {
						case "Propellants":
						((Engine) (e.Row.Item)).PropellantRatio = new List<FuelRatioElement> ();

						foreach (object i in CurrentGDList) {
							((Engine) (e.Row.Item)).PropellantRatio.Add ((FuelRatioElement) i);
						}

						break;
						case "Tank":
						((Engine) (e.Row.Item)).TanksContents = new List<FuelRatioElement> ();

						foreach (object i in CurrentGDList) {
							((Engine) (e.Row.Item)).TanksContents.Add ((FuelRatioElement) i);
						}

						break;
						case "Thrust Curve":
						((Engine) e.Row.Item).ThrustCurve = new List<DoubleTuple> ();

						foreach (object i in CurrentGDList) {
							((Engine) (e.Row.Item)).ThrustCurve.Add ((DoubleTuple) i);
						}

						break;
						default:
						return;
					}

					CurrentGDType = GDListTypes[e.Column.Header.ToString ()];
				}

				((Engine) e.Row.Item).NotifyEveryProperty ();
				CurrentGD = null;
			}
		}

		private void AddGD_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				CurrentGD.CommitEdit ();
				CurrentGD.CancelEdit ();

				CurrentGDList.Add (Activator.CreateInstance (CurrentGDType));
				CurrentGD.Items.Refresh ();
			}
		}

		private void RemoveGD_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {

				CurrentGD.CommitEdit ();
				CurrentGD.CancelEdit ();

				if (CurrentGD.SelectedIndex != -1) {
					foreach (object i in CurrentGD.SelectedItems) {
						CurrentGDList.Remove (i);
					}

					CurrentGD.Items.Refresh ();
				}
			}
		}

		// /Generic Datagrid input (GD)

		private void AddPropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				currentFuelRatioGrid.CommitEdit ();
				currentFuelRatioGrid.CancelEdit ();

				CurrentFuelRatios.Add (new FuelRatioElement ());
				currentFuelRatioGrid.Items.Refresh ();
			}
		}

		private void RemovePropellantButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (currentFuelRatioGrid.SelectedIndex != -1) {
					foreach (FuelRatioElement i in currentFuelRatioGrid.SelectedItems) {
						CurrentFuelRatios.Remove (i);
					}

					currentFuelRatioGrid.Items.Refresh ();
				}
			}
		}

		private void AppendButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				try {
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
						foreach (string i in fileDialog.FileNames) {
							ReadEnginesFromFile (i, true);
						}
					} else {
						return;
					}

					MessageBox.Show ($"Engines succesfully appended to {CurrentFile}", "Success");
					
					RefreshEngines ();

				} catch (Exception ex) {
					// readEnginesFromFile handles file errors one by one
					App.SaveExceptionToFile (ex);
					MessageBox.Show ($"Something went wrong while appending engines. One or more of the .enl files might be corrupt. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
				}
			}
		}

		private void MainWindow_Closing (object sender, System.ComponentModel.CancelEventArgs e) {
			if ((CurrentFile == null && Engines.Count == 0) || ConfirmBox.Show ($"All unsaved changes to the {String.Format ("\"{0}\"", System.IO.Path.GetFileName (CurrentFile))} file will be lost! Are you sure you want to close Generic Engines?")) {

			} else {
				e.Cancel = true;
			}
		}

		private void PlumeCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (Plume)).Cast<Plume> ();
		}

		private void EngineTypeCombo_Loaded (object sender, RoutedEventArgs e) {
			((ComboBox) sender).ItemsSource = Enum.GetValues (typeof (EngineType)).Cast<EngineType> ();
		}

		private void TechComboBox_PreviewKeyUp (object sender, KeyEventArgs e) {
			ComboBox combo = (ComboBox) sender;

			string tmp = combo.Text;

			combo.IsDropDownOpen = true;
			combo.ItemsSource = TechNodeEnumWrapper.Get.Where (x => TechNodeList.GetName (x.Key).ToLower ().Contains (combo.Text.ToLower ()));

			//For some reason combobox likes to lose its contents
			combo.Text = tmp;

			if ( //This is literally the worst thing i've ever written but it fixes majority of the problems
				((TextBox) combo.Template.FindName ("PART_EditableTextBox", combo)).SelectionStart == 0 &&
				((TextBox) combo.Template.FindName ("PART_EditableTextBox", combo)).SelectionLength == 0 &&
				e.Key != Key.Home &&
				e.Key != Key.Left &&
				e.Key != Key.Back &&
				e.Key != Key.PageUp
			) {
				((TextBox) combo.Template.FindName ("PART_EditableTextBox", combo)).Select (tmp.Length, 0);
			}
		}

		private void SortThrustCurve_MouseUp (object sender, MouseButtonEventArgs e) {
			List<DoubleTuple> tupleList = new List<DoubleTuple> ();

			CurrentGD.CommitEdit ();
			CurrentGD.CancelEdit ();

			foreach (object i in CurrentGDList) {
				tupleList.Add ((DoubleTuple) i);
			}

			tupleList.Sort (delegate (DoubleTuple a, DoubleTuple b) {
				//Will sort descending
				if (a.Item1 > b.Item1) {
					return -1;
				} else if (a.Item1 < b.Item1) {
					return 1;
				} else {
					return 0;
				}
			});

			CurrentGDList = tupleList.ToList<object> ();
			CurrentGD.ItemsSource = CurrentGDList;
			CurrentGD.Items.Refresh ();

		}

		private void TanksVolumeInput_PrewiewKeyDown (object sender, KeyEventArgs e) {
			//I have no idea why it's necessary, but the property doesn't update properly without it.
			((TextBox) sender).GetBindingExpression (TextBox.TextProperty).UpdateSource ();
			//Other inputs look literally the same and they work without manual updating ¯\_(ツ)_/¯
		}

		private void MasterIDComboBox_Loaded (object sender, RoutedEventArgs e) {
			MasterIDComboBox_OpenDropbox (sender, null);
		}

		private void MasterIDComboBox_OpenDropbox (object sender, EventArgs e) {
			Engine currentEngine = (Engine) mainDataGrid.CurrentItem;
			ComboBox combo = (ComboBox) sender;

			List<Engine> IDs = new List<Engine> { new Engine () { Name = "" } };
			switch (currentEngine.PolyType) {
				case Polymorphism.MultiModeSlave:
				foreach (Engine i in Engines) {
					if (!i.Active) {
						continue;
					}

					if (i.PolyType != Polymorphism.MultiModeMaster) {
						continue;
					}

					IDs.Add (i);
				}
				break;
				case Polymorphism.MultiConfigSlave:
				foreach (Engine i in Engines) {
					if (!i.Active) {
						continue;
					}

					if (i.PolyType != Polymorphism.MultiConfigMaster) {
						continue;
					}

					IDs.Add (i);
				}
				break;
				default:

				break;
			}

			if (!IDs.Exists (x => x.Name == currentEngine.MasterEngineName)) {
				currentEngine.MasterEngineName = "";
			}

			combo.ItemsSource = IDs;
			combo.Items.Refresh ();

			combo.SelectedItem = IDs.Find (x => x.Name == currentEngine.MasterEngineName);
		}

		/// <summary>
		/// Fixes Polymorphism config errors and alerts the user if error was found.
		/// </summary>
		public void EnsureEnginePolymorphismConsistency () {
			HashSet<string> LinkedMultiModeMasters = new HashSet<string> ();
			List<Engine> EnginesWithErrors = new List<Engine> ();

			foreach (Engine i in Engines) {
				if (!i.Active) {
					continue;
				}

				if (i.PolyType == Polymorphism.MultiModeSlave && i.MasterEngineName != "") {
					if (Engines.Exists (x => x.Active && x.PolyType == Polymorphism.MultiModeMaster && x.Name == i.MasterEngineName)) {
						if (LinkedMultiModeMasters.Contains (i.MasterEngineName)) {
							EnginesWithErrors.Add (i);
							i.MasterEngineName = "";
						} else {
							LinkedMultiModeMasters.Add (i.MasterEngineName);
						}
					} else {
						EnginesWithErrors.Add (i);
						i.MasterEngineName = "";
					}
				}

				if (i.PolyType == Polymorphism.MultiConfigSlave && i.MasterEngineName != "") {
					if (Engines.Exists (x => x.Active && x.PolyType == Polymorphism.MultiConfigMaster && x.Name == i.MasterEngineName)) {
						
					} else {
						EnginesWithErrors.Add (i);
						i.MasterEngineName = "";
					}
				}
			}

			if (EnginesWithErrors.Count > 0) {
				string tmp = "";

				foreach (Engine i in EnginesWithErrors) {
					tmp += $"{i.Name}, ";
					i.NotifyPropertyChanged ("PolyLabel");
				}

				tmp = tmp.Substring (0, tmp.Length - 2);

				MessageBox.Show ($"Inconsistencies found in following engines: {tmp}. Their MasterEngineName has been set to empty. You might want to recheck their Polymorphism settings", "Warning");
			}
		}
	}
}
