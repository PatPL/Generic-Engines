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
using System.Windows.Shapes;

namespace GenericEngines {
	/// <summary>
	/// Logika interakcji dla klasy ConfirmWindow.xaml
	/// </summary>
	
	public partial class ConfirmWindow : Window {

		public int confirmValue;

		public ConfirmWindow () {
			InitializeComponent ();

			InputBox.Focus ();
		}

		private void InputBox_KeyUp (object sender, KeyEventArgs e) {
			ConfirmButton.IsEnabled = ((TextBox) sender).Text == confirmValue.ToString ();
		}

		private void ConfirmButton_MouseUp (object sender, MouseButtonEventArgs e) {
			ConfirmBox.returnValue = true;
			Close ();
		}

		private void CancelButton_MouseUp (object sender, MouseButtonEventArgs e) {
			Close ();
		}

		private void Window_KeyUp (object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter && ConfirmButton.IsEnabled) {
				ConfirmButton_MouseUp (null, null);
			} else if (e.Key == Key.Escape) {
				CancelButton_MouseUp (null, null);
			}
		}
	}
}
