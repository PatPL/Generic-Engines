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
	public partial class SettingsWindow : Window {

		// Use settings.snippet
		// sett [Tab + Tab]
		
		public bool AdvConfirmBox {
			get {
				return bool.Parse (Settings.Get ("AdvConfirmBox"));
			}
			set {
				Settings.Set ("AdvConfirmBox", value.ToString ());
			}
		}

		public SettingsWindow () {
			InitializeComponent ();

			this.DataContext = this;
		}
	}
}
