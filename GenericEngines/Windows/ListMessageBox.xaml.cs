using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
	/// Logika interakcji dla klasy ListMessageBox.xaml
	/// </summary>
	public partial class ListMessageBox : Window, INotifyPropertyChanged {

		public string DisplayedText { get; set; }
		public List<string> DisplayedList { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public static void Show (string text, List<string> list) {
			ListMessageBox t = new ListMessageBox {
				DisplayedText = text,
				DisplayedList = list
			};

			t.NotifyEveryProperty ();
			t.ShowDialog ();
		}

		public ListMessageBox () {
			this.DataContext = this;
			InitializeComponent ();
		}

		private void CloseWindow_Click (object sender, RoutedEventArgs e) {
			this.Close ();
		}

		/// <summary>
		/// Update the property in UI
		/// </summary>
		/// <param name="name">The property to be updated</param>
		public void NotifyPropertyChanged (string name) {
			PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));
		}

		/// <summary>
		/// Updates EVERY property of this.
		/// </summary>
		public void NotifyEveryProperty () {
			foreach (PropertyInfo i in typeof (ListMessageBox).GetProperties ()) {
				NotifyPropertyChanged (i.Name);
			}
		}
	}
}
