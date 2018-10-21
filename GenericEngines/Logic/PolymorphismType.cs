using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	/// <summary>
	/// Class with polymorphism attributes
	/// </summary>
	public static class PolymorphismType {

		private static readonly string[] names = new string[] {
			"Single",
			"Multimode Master",
			"Multimode Slave",
			"Multiconfig Master",
			"Multiconfig Slave"
		};
		
		public static string GetName (Polymorphism index) {
			return names[(int) index];
		}
		
	}

	public enum Polymorphism {
		Single,
		MultiModeMaster,
		MultiModeSlave,
		MultiConfigMaster,
		MultiConfigSlave
	}
}
