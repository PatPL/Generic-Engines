using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GenericEngines {
	public static class PrimitiveTypeExtensions {

		public static string Compact (this string input) {
			string output = input;

			string[] lines = output.Split ('\n');
			output = "";

			foreach (string i in lines) {
				string tmp = i.Trim (new char[] { ' ', '\t', '\r' });

				if (tmp != "") {
					output += $"{tmp}\n";
				}


			}

			return output;

		}

		public static string Str (this double input, int accuracy = -1) {
			if (accuracy < 0) {
				return input.ToString (CultureInfo.InvariantCulture);
			} else {
				return (((long) Math.Round (input * Math.Pow (10, accuracy))) / Math.Pow (10, accuracy)).ToString (CultureInfo.InvariantCulture);
			}
		}
	}
}
