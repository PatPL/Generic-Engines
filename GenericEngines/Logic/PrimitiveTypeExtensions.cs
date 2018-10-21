using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GenericEngines {
	/// <summary>
	/// Extensions of primitive C# data types
	/// </summary>
	public static class PrimitiveTypeExtensions {
		
		/// <summary>
		/// This returns the input string with trimmed whitespace, and removed empty lines
		/// </summary>
		/// <param name="input">The string to be compacted</param>
		/// <returns></returns>
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

		/// <summary>
		/// Same as double.ToString (), but uses period instead of comma, and you can set how many digits after the period you want (Max).
		/// </summary>
		/// <param name="input">The double to be turned into a string</param>
		/// <param name="accuracy">How many digits after period do you want (Max)</param>
		/// <returns></returns>
		public static string Str (this double input, int accuracy = -1) {
			if (accuracy < 0) {
				return input.ToString (CultureInfo.InvariantCulture);
			} else {
				return (((long) Math.Round (input * Math.Pow (10, accuracy))) / Math.Pow (10, accuracy)).ToString (CultureInfo.InvariantCulture);
			}
		}
	}
}
