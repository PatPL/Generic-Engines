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

	public class Command : ICommand {
		
		private readonly Action action;
		
		#pragma warning disable CS0067
		public event EventHandler CanExecuteChanged;
		#pragma warning restore CS0067

		public Command (Action _action) {
			action = _action;
		}

		public bool CanExecute (object parameter) {
			return true;
		}

		public void Execute (object parameter) {
			action ();
		}
	}

	public partial class MainWindow : Window {
		
		public ICommand New_Command => new Command (() => { NewButton_MouseUp (null, null); });
		public ICommand Open_Command => new Command (() => { OpenButton_MouseUp (null, null); });
		public ICommand Append_Command => new Command (() => { AppendButton_MouseUp (null, null); });
		public ICommand Save_Command => new Command (() => { SaveButton_MouseUp (null, null); });
		public ICommand SaveAs_Command => new Command (() => { SaveasButton_MouseUp (null, null); });
		public ICommand Validate_Command => new Command (() => { ValidateButton_MouseUp (null, null); });
		public ICommand Export_Command => new Command (() => { ExportButton_MouseUp (null, null); });
		public ICommand Duplicate_Command => new Command (() => { DuplicateButton_MouseUp (null, null); });
		public ICommand Add_Command => new Command (() => { AddButton_MouseUp (null, null); });
		public ICommand Remove_Command => new Command (() => { RemoveButton_MouseUp (null, null); });
		public ICommand Settings_Command => new Command (() => { SettingsButton_MouseUp (null, null); });
		public ICommand About_Command => new Command (() => { HelpButton_MouseUp (null, null); });

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
			this.DataContext = this;
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

				List<(int, Engine)> toDuplicate = new List<(int, Engine)> ();

				foreach (Engine i in mainDataGrid.SelectedItems) {
					toDuplicate.Add ((i.UID, Serializer.Deserialize (Serializer.Serialize (i), out int _)));
				}

				toDuplicate.Sort (delegate ((int, Engine) a, (int, Engine) b) {
					//Will sort descending
					if (a.Item1 > b.Item1) {
						return 1;
					} else if (a.Item1 < b.Item1) {
						return -1;
					} else {
						return 0;
					}
				});

				foreach ((int, Engine) i in toDuplicate) {
					Engines.Add (i.Item2);
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
						(int savedEngines, ReturnStatus status) = EngineUtility.SaveEnginesToFile (Engines, CurrentFile);
						switch (status) {
							case ReturnStatus.Success:
							MessageBox.Show ($"{savedEngines} engines succesfully saved to {CurrentFile}", "Success");
							break;
							case ReturnStatus.Error:
							MessageBox.Show ($"Something went wrong while saving engines to {CurrentFile}. Try to choose a different location. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
							break;
						}
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
					(int savedEngines, ReturnStatus status) = EngineUtility.SaveEnginesToFile (Engines, CurrentFile);
					switch (status) {
						case ReturnStatus.Success:
						MessageBox.Show ($"{savedEngines} engines succesfully saved to {CurrentFile}", "Success");
						break;
						case ReturnStatus.Error:
						MessageBox.Show ($"Something went wrong while saving engines to {CurrentFile}. Try to choose a different location. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
						break;
					}
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
						(List<Engine> newEngines, ReturnStatus status) = EngineUtility.ReadEnginesFromFile (fileDialog.FileName);

						switch (status) {
							case ReturnStatus.Success:
							CurrentFile = fileDialog.FileName;
							Engines = newEngines;
							RefreshEngines ();
							break;
							case ReturnStatus.Error:
							MessageBox.Show ($"Something went wrong while reading the file. Your .enl file might be corrupted. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
							break;
						}
					}
				}
			}
		}

		private void ValidateButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				List<string> errors = EngineUtility.Validate (Engines);

				if (errors.Count == 0) {
					MessageBox.Show ("No inconsistencies found in current engine list", $"{errors.Count} errors found");
				} else {
					ListMessageBox.Show ("Following errors found:", errors);
					//MessageBox.Show ($"Following errors found:\n{String.Join ("\n", errors.ToArray ())}", $"{errors.Count} errors found");
				}
			}
		}

		private void ExportButton_MouseUp (object sender, MouseButtonEventArgs e) {
			if (sender == null || lastMouseDownObject == sender) {
				if (Engines.Count > 0) {
					if (EngineUtility.Validate (Engines).Count > 0) {
						MessageBox.Show ("Validation errors found in current list. Fix all errors and try again", "Error");
						return;
					}

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
						(int exportedEngineCount, ReturnStatus status) = EngineUtility.ExportEnginesToFile (Engines, fileDialog.FileName);
						switch (status) {
							case ReturnStatus.Success:
							MessageBox.Show ($"{exportedEngineCount} engines succesfully exported to {fileDialog.FileName}", "Success");
							break;
							case ReturnStatus.Error:
							MessageBox.Show ($"Something went wrong while exporting engines to {fileDialog.FileName}. All changes were reverted. If you're exporting directly to KSP folder, KSP shouldn't be running. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
							break;
						}
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
			/*
			if ((string) e.Column.Header == "Polymorphism") {
				((Engine) e.Row.Item).NotifyPropertyChanged ("PolyType");
			}
			*/
			
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

					CurrentGD.CommitEdit ();
					CurrentGD.CancelEdit ();

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

				bool errors = false;
				if (result != null && result == true) {
					foreach (string i in fileDialog.FileNames) {
						if (EngineUtility.AppendEnginesToList (i, Engines) == ReturnStatus.Error) {
							errors = true;
							MessageBox.Show ($"Something went wrong while reading engines from {i}. Your .enl file might be corrupt. More info about this error saved to {App.otherErrorLogLocation}", "Warning");
						}
					}

					RefreshEngines ();

					if (!errors) {
						MessageBox.Show ($"All files appended successfully", "Success");
					}
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
				IDs.AddRange (Engines.FindAll (x => x.Active && x.PolyType == Polymorphism.MultiModeMaster));
				break;
				case Polymorphism.MultiConfigSlave:
				IDs.AddRange (Engines.FindAll (x => x.Active && x.PolyType == Polymorphism.MultiConfigMaster));
				break;
			}

			combo.ItemsSource = IDs;
			combo.Items.Refresh ();

			Engine master = IDs.Find (x => x.Name == currentEngine.MasterEngineName);
			if (master is null) {
				currentEngine.MasterEngineName = "";
			} else {
				combo.SelectedItem = master;
			}
		}

		private void Window_KeyUp (object sender, KeyEventArgs e) {
			
			if (e.Key == Key.RightCtrl && mainDataGrid.Items.Count != 0) {
				mainDataGrid.Focus ();

				if (mainDataGrid.CurrentCell.IsValid) {
					DataGridCellInfo tmp = mainDataGrid.CurrentCell;
					mainDataGrid.CurrentCell = new DataGridCellInfo (mainDataGrid.Items[0], mainDataGrid.Columns[0]);
					mainDataGrid.CurrentCell = tmp;
					//For whatever reason this must be like this
					//Thanks microsoft
				} else {
					mainDataGrid.CurrentCell = new DataGridCellInfo (mainDataGrid.Items[0], mainDataGrid.Columns[0]);
				}
			}

			if (e.Key == Key.Space && !isEdited && mainDataGrid.CurrentCell.IsValid) {
				mainDataGrid.BeginEdit ();

				KeyEventArgs tabPressEventArgs = new KeyEventArgs (Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab) { RoutedEvent = Keyboard.KeyDownEvent };
				InputManager.Current.ProcessInput (tabPressEventArgs);
			}
			
			if (e.Key == Key.Enter) {
				mainDataGrid.CommitEdit ();
			}
		}

		private void MainDataGrid_PreviewKeyDown (object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter || e.Key == Key.Return) {
				e.Handled = true;
			}
		}
	}
}
