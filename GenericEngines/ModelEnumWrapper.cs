﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GenericEngines {
	public class ModelEnumWrapper {

		public Model model { get; set; }
		public string modelName { get; set; }
		public string toolTip { get; set; }
		public string type { get; set; }

		private static ListCollectionView content = null;

		public static ListCollectionView Get {
			get {
				return content ?? GenerateContent ();
			}
		}

		private static ListCollectionView GenerateContent () {
			List<ModelEnumWrapper> output = new List<ModelEnumWrapper> ();

			foreach (Model i in Enum.GetValues (typeof (Model))) {
				output.Add (new	ModelEnumWrapper (i, ModelList.GetName (i), ModelList.GetTooltip (i), ModelList.GetType (i)));
			}

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
