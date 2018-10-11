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
		public double NodeStackTop;
		public double NodeStackBottom;
		public string ModelPath;
		public string TextureDefinitions;
		public string ThrustTransformName;
		public string[] HiddenMuObjects;
		
	}

	public static class ModelList {

		private static readonly List<ModelInfo> models = new List<ModelInfo> () {
			new ModelInfo { //Model.LR91
				OriginalHeight = 1.885,
				OriginalWidth = 0.97,
				PlumeSizeMultiplier = 1.0,
				PlumePosition = 0.8,
				NodeStackTop = 0.7215,
				NodeStackBottom = -1.1635,
				ModelPath = "RealismOverhaul/Models/LR-91eng",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				HiddenMuObjects = null
			}, new ModelInfo { //Model.AJ10
				OriginalHeight = 0.654,
				OriginalWidth = 0.285,
				PlumeSizeMultiplier = 0.295,
				PlumePosition = -0.09,
				NodeStackTop = 0.33,
				NodeStackBottom = -0.324,
				ModelPath = "SXT/Parts/Rocketry/Engine/Vanguard/model",
				TextureDefinitions = @"
					texture = fairing , Squad/Parts/Engine/liquidEngineLV-T45/model002
					texture = model000 , Squad/Parts/Engine/liquidEngineLV-T45/model000
					texture = model001 , Squad/Parts/Engine/liquidEngineLV-T45/model001
				",
				ThrustTransformName = "thrustTransform",
				HiddenMuObjects = new string[] {
					"Cylinder_002"
				}
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
		LR91,
		AJ10
	}
}
