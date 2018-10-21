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
	/// <summary>
	/// Has the same functionality as b2v, but it's reversed
	/// </summary>
	public sealed class ReversedBooleanToVisibilityConverter : IValueConverter {
		/// <summary>
		/// Turns a bool into Visibility
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is bool && targetType == typeof (Visibility)) {
				return ((bool) value ? Visibility.Collapsed : Visibility.Visible);
			} else {
				throw new Exception ("ReversedBooleanToVisibilityConverter got an error (Convert)");
			}
		}

		/// <summary>
		/// Turns Visibility into a bool
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
			if (value is Visibility && targetType == typeof (bool)) {
				return ((Visibility) value == Visibility.Visible ? false : true);
			} else {
				throw new Exception ("ReversedBooleanToVisibilityConverter got an error (Convert Back)");
			}
		}
	}
}
