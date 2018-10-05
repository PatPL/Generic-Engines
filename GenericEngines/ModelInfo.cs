using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class ModelInfo {

		public double OriginalHeight;
		public double OriginalWidth;
		public double PlumeSizeMultiplier;
		public double PlumePosition;
		public string NodeStackTop;
		public string NodeStackBottom;
		public string NodeStackAttach;
		public string ModelPath;
		public string ThrustTransformName;
		
	}

	public static class ModelList {

		private static readonly List<ModelInfo> models = new List<ModelInfo> () {
			new ModelInfo { //Model.LR91
				OriginalHeight = 1.885,
				OriginalWidth = 0.97,
				PlumeSizeMultiplier = 1.0,
				PlumePosition = 0.8,
				NodeStackTop = "0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1",
				NodeStackBottom = "0.0, -1.1635, 0.0, 0.0, -1.0, 0.0, 1",
				NodeStackAttach = "0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1",
				ModelPath = "RealismOverhaul/Models/LR-91eng",
				ThrustTransformName = "thrustTransform"
			}
		};

		public static ModelInfo Get (Model index) {
			return models[(int) index];
		}

		public static string GetName (Model index) {
			switch (index) {
				default:
				return index.ToString ();
			}
		}

	}

	public enum Model {
		LR91
	}
}
