using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GenericEngines {
	public static class ConfirmBox {

		public static bool returnValue;

		public static bool Show (string message) {
			Random rng = new Random ();
			int confirmValue = rng.Next (100000, 999999);

			ConfirmWindow confirmWindow = new ConfirmWindow ();
			confirmWindow.InfoBox.Text = message;
			confirmWindow.ShowInputBox.Text = $"Please enter this number below to confirm: {confirmValue}";
			confirmWindow.confirmValue = confirmValue;

			returnValue = false;
			confirmWindow.ShowDialog ();

			return returnValue;
		}
	}
}
