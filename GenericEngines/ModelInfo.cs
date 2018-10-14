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
				OriginalBaseWidth = 0.892,
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
				OriginalBaseWidth = 0.395,
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
				OriginalBaseWidth = 0.989,
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
				OriginalBaseWidth = 0.222,
				PlumeSizeMultiplier = 0.11,
				PlumePosition = -0.04,
				NodeStackTop = 0.0495,
				NodeStackBottom = -0.256,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/LV-1B",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Gimbal",
				HiddenMuObjects = null
			}, new ModelInfo { //Model.Aestus
				OriginalHeight = 0.393,
				OriginalWidth = 0.234,
				OriginalBaseWidth = 0.616,
				PlumeSizeMultiplier = 0.225,
				PlumePosition = -0.06,
				NodeStackTop = 0.0,
				NodeStackBottom = -0.393,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/48-7S",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Obj_Gimbal",
				HiddenMuObjects = new string[] {
					"Size2A",
					"node_fairing_collider"
				}
			}, new ModelInfo { //Model.IonThruster
				OriginalHeight = 0.3935,
				OriginalWidth = 0.459,
				OriginalBaseWidth = 0.627,
				PlumeSizeMultiplier = 0.42,
				PlumePosition = 0,
				NodeStackTop = 0.1965,
				NodeStackBottom = -0.197,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/IonEngine",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "thrustTransform",
				HiddenMuObjects = new string[] {
					"Size1B",
					"fairing"
				}
			}, new ModelInfo { //Model.F1
				OriginalHeight = 4.48,
				OriginalWidth = 1.802,
				OriginalBaseWidth = 3.78,
				PlumeSizeMultiplier = 1.6,
				PlumePosition = -0.7,
				NodeStackTop = 1.49,
				NodeStackBottom = -2.99,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/KR-2L",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Nozzle",
				HiddenMuObjects = new string[] {
					"fairing"
				}
			}, new ModelInfo { //Model.RD0105T
				OriginalHeight = 0.727,
				OriginalWidth = 0.445,
				OriginalBaseWidth = 0.989,
				PlumeSizeMultiplier = 0.4,
				PlumePosition = -0.12,
				NodeStackTop = 0.195,
				NodeStackBottom = -0.532,
				ModelPath = "VenStockRevamp/Squad/Parts/Propulsion/LV900",
				TextureDefinitions = "",
				ThrustTransformName = "thrustTransform",
				GimbalTransformName = "Obj_Gimbal",
				HiddenMuObjects = new string[] {
					"Size2B",
					"fairing",
					"Hoses"
				}
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
		Aestus,
		IonThruster,
		F1,
		RD0105T
	}
}
