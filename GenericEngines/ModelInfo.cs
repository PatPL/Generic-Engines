using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class ModelInfo {

		public double OriginalHeight;
		public double OriginalWidth;
		public string NodeStackTop;
		public string NodeStackBottom;
		public string NodeStackAttach;
		public string ModelPath;
		
	}

	public static class ModelList {

		private static readonly List<ModelInfo> models = new List<ModelInfo> () {
			new ModelInfo {
				OriginalHeight = 1.9,
				OriginalWidth = 1.0,
				NodeStackTop = "0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1",
				NodeStackBottom = "0.0, -1.1635, 0.0, 0.0, -1.0, 0.0, 1",
				NodeStackAttach = "0.0, 0.7215, 0.0, 0.0, 1.0, 0.0, 1",
				ModelPath = "RealismOverhaul/Models/LR-91eng"
			}
		};

		public static ModelInfo Get (Model index) {
			return models[(int) index];
		}

	}

	public enum Model {
		LR91
	}
}
