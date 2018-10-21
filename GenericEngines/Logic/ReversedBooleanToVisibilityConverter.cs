using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace GenericEngines {
	public sealed class ReversedBooleanToVisibilityConverter : IValueConverter {
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is bool && targetType == typeof (Visibility)) {
				return ((bool) value ? Visibility.Collapsed : Visibility.Visible);
			} else {
				throw new Exception ("ReversedBooleanToVisibilityConverter got an error (Convert)");
			}
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is Visibility && targetType == typeof (bool)) {
				return ((Visibility) value == Visibility.Visible ? false : true);
			} else {
				throw new Exception ("ReversedBooleanToVisibilityConverter got an error (Convert Back)");
			}
		}
	}
}
