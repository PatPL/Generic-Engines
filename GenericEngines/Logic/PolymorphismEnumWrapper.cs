using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Contains all info needed to display Polymorphism types correctly in ComboBox
	/// </summary>
	class PolymorphismEnumWrapper {

		private static Dictionary<Polymorphism, string> content = null;

		/// <summary>
		/// Get the Dictionary with Polymorphism type names
		/// </summary>
		public static Dictionary<Polymorphism, string> Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static Dictionary<Polymorphism, string> GenerateContent () {
			Dictionary<Polymorphism, string> output = new Dictionary<Polymorphism, string> ();

			foreach (Polymorphism i in Enum.GetValues (typeof (Polymorphism))) {
				output.Add (i, PolymorphismType.GetName (i));
			}

			content = output;
			return output;
		}

	}
}
