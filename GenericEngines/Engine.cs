using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericEngines {
	public class Engine {
		public bool Selected { get; set; }
		public bool Active { get; set; }
		public string Name { get; set; }

		public Engine (bool _Selected, bool _Active, string _Name) {
			Selected = _Selected;
			Active = _Active;
			Name = _Name;
		}
	}
}
