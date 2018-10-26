using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Contains all info needed to display model correctly in ComboBox
	/// </summary>
	public static class TechNodeEnumWrapper {

		private static Dictionary<TechNode, string> content = null;

		/// <summary>
		/// Get the Dictionary with tech node names
		/// </summary>
		public static Dictionary<TechNode, string> Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static Dictionary<TechNode, string> GenerateContent () {
			Dictionary<TechNode, string> output = new Dictionary<TechNode, string> ();

			foreach (TechNode i in Enum.GetValues (typeof (TechNode))) {
				output.Add (i, TechNodeList.GetName (i));
			}

			content = output;
			return output;
		}

	}
}
