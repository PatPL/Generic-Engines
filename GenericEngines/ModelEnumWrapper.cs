using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class ModelEnumWrapper {

		public Model model { get; set; }
		public string modelName { get; set; }
		public string toolTip { get; set; }

		private static List<ModelEnumWrapper> content = null;

		public static List<ModelEnumWrapper> Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static List<ModelEnumWrapper> GenerateContent () {
			List<ModelEnumWrapper> output = new List<ModelEnumWrapper> ();

			foreach (Model i in Enum.GetValues (typeof (Model))) {
				output.Add (new	ModelEnumWrapper (i, ModelList.GetName (i), ModelList.GetTooltip (i)));
			}

			content = output;
			return output;
		}

		private ModelEnumWrapper (
			Model _model,
			string _modelName,
			string _toolTip
		) {
			model = _model;
			modelName = _modelName;
			toolTip = _toolTip;
		}

	}
}
