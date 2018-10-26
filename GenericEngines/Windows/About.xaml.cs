using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class About : Window {

		public string Version {
			get {
				return $"Generic Engines v{AppInfo.Version}";
			}
		}
		public string SerializerVersion {
			get {
				return $"Serializer v{AppInfo.SerializerVersion}";
			}
		}

		public About () {
			InitializeComponent ();

			this.DataContext = this;
		}

		private void OpenWebpage (object sender, System.Windows.Navigation.RequestNavigateEventArgs e) {
			Process.Start (new ProcessStartInfo (e.Uri.AbsoluteUri));
			//this.Close ();
		}
	}
}
