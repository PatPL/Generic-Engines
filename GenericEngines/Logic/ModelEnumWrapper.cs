using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GenericEngines {
	/// <summary>
	/// Contains all info needed to display model correctly in ComboBox
	/// </summary>
	public class ModelEnumWrapper {

		public Model model { get; set; }
		public string modelName { get; set; }
		public string toolTip { get; set; }
		public string type { get; set; }

		private static ListCollectionView content = null;

		/// <summary>
		/// Get the ListCollectionView with all models
		/// </summary>
		public static ListCollectionView Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static ListCollectionView GenerateContent () {
			List<ModelEnumWrapper> output = new List<ModelEnumWrapper> ();

			foreach (Model i in Enum.GetValues (typeof (Model))) {
				output.Add (new	ModelEnumWrapper (i, ModelList.GetName (i), ModelList.GetPreviewImagePath (i), ModelList.GetType (i)));
			}

			output.Sort (delegate (ModelEnumWrapper a, ModelEnumWrapper b) {
				return string.Compare (a.modelName, b.modelName);
			});

			ListCollectionView realOutput = new ListCollectionView (output);
			realOutput.GroupDescriptions.Add (new PropertyGroupDescription ("type"));

			content = realOutput;
			return realOutput;
		}

		private ModelEnumWrapper (
			Model _model,
			string _modelName,
			string _toolTip,
			string _type
		) {
			model = _model;
			modelName = _modelName;
			toolTip = _toolTip;
			type = _type;
		}

	}
}
