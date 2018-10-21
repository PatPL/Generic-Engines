using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public static class TechNodeEnumWrapper {

		private static Dictionary<TechNode, string> content = null;

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
