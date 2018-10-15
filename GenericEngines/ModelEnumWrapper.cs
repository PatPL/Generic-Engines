using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	class ModelEnumWrapper {

		private static Dictionary<Model, string> content = null;

		public static Dictionary<Model, string> Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static Dictionary<Model, string> GenerateContent () {
			Dictionary<Model, string> output = new Dictionary<Model, string> ();

			foreach (Model i in Enum.GetValues (typeof (Model))) {
				output.Add (i, ModelList.GetName (i));
			}

			content = output;
			return output;
		}

	}
}
