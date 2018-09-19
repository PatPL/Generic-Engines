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
		public MainWindow () {
			InitializeComponent ();

			
		}

		private void addButton_MouseDown (object sender, MouseButtonEventArgs e) {

			Application.Current.Shutdown ();
		}

		private void removeButton_MouseDown (object sender, MouseButtonEventArgs e) {

		}

		private void saveButton_MouseDown (object sender, MouseButtonEventArgs e) {

		}

		private void mainDataGrid_Loaded (object sender, RoutedEventArgs e) {
			((DataGrid) sender).ItemsSource = GenerateEngines ();
		}

		private static List<Engine> GenerateEngines () {
			List<Engine> output = new List<Engine> ();

			output.Add (new Engine (
				true,
				false,
				"Test 1"
			));

			output.Add (new Engine (
				true,
				true,
				"Test two"
			));

			output.Add (new Engine (
				false,
				false,
				"Test THREE"
			));

			output.Add (new Engine (
				false,
				true,
				"Test4"
			));

			output.Add (new Engine (
				true,
				false,
				"Test%"
			));

			return output;
		}
	}
}
