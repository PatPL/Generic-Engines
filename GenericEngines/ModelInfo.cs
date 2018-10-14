using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class ModelInfo {

		public double OriginalHeight;
		public double OriginalWidth; //Bell width
		public double OriginalBaseWidth; //Engine base width
		public double PlumeSizeMultiplier;
		public double PlumePosition;
		public double NodeStackTop;
		public double NodeStackBottom;
		public string ModelPath;
		public string TextureDefinitions;
		public string ThrustTransformName;
		public string GimbalTransformName;
		public string[] HiddenMuObjects;
		
	}

	public static class ModelList {

		private static readonly List<ModelInfo> models = new List<ModelInfo> () {
			new ModelInfo { //Model.LR91
				OriginalHeight = 1.885,
				OriginalWidth = 0.9635,
				OriginalBaseWidth = 0.88,
				PlumeSizeMultiplier = 1.0,
				PlumePosition = 0.8,
				NodeStackTop = 0.7215,
				NodeStackBottom = -1.1635,
				ModelPath = "RealismOverhaul/Models/LR-91eng",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "thrustTransform",
				HiddenMuObjects = null
			}, new ModelInfo { //Model.AJ10
				OriginalHeight = 0.654,
				OriginalWidth = 0.285,
				OriginalBaseWidth = 0.3945,
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
				GimbalTransformName = "thrustTransform",
				HiddenMuObjects = new string[] {
					"Cylinder_002"
				}
			}, new ModelInfo { //Model.RS25
				OriginalHeight = 1.5,
				OriginalWidth = 0.865,
				OriginalBaseWidth = 0.985,
				PlumeSizeMultiplier = 0.85,
				PlumePosition = -0.8,
				NodeStackTop = -0.025,
				NodeStackBottom = -1.525,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/KS-25",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Nozzle",
				HiddenMuObjects = new string[] {
					"Size2A"
				}
			}, new ModelInfo { //Model.Thruster
				OriginalHeight = 0.3055,
				OriginalWidth = 0.12,
				OriginalBaseWidth = 0.221,
				PlumeSizeMultiplier = 0.11,
				PlumePosition = -0.04,
				NodeStackTop = 0.0495,
				NodeStackBottom = -0.256,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/LV-1B",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Gimbal",
				HiddenMuObjects = null
			}
		};

		/*
		new ModelInfo { //Model.
				OriginalHeight = ,
				OriginalWidth = ,
				OriginalBaseWidth = ,
				PlumeSizeMultiplier = ,
				PlumePosition = ,
				NodeStackTop = ,
				NodeStackBottom = ,
				ModelPath = "",
				TextureDefinitions = "",
				ThrustTransformName = "",
				GimbalTransformName = "",
				HiddenMuObjects = null
			}
		*/

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
		AJ10,
		RS25,
		Thruster,
	}
}
