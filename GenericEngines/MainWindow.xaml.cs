using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		bool isEdited = false;

		public MainWindow () {
			InitializeComponent ();
		}

		DataGrid mainDataGrid;
		List<Engine> Engines {
			get {
				if (mainDataGrid != null) {
					return (List<Engine>) mainDataGrid.ItemsSource;
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			} set {
				if (mainDataGrid != null) {
					mainDataGrid.ItemsSource = value;
					mainDataGrid.Items.Refresh ();
				} else {
					throw new NullReferenceException ("mainDataGrid is null");
				}
			}
		}

		private void addButton_MouseUp (object sender, MouseButtonEventArgs e) {
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
	}
}
