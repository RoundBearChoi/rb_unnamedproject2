using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchableType {
	LEDGE,
	CHARACTER,
	WAYPOINT,
}

namespace roundbeargames {
	public class Touchable : MonoBehaviour {
		public TouchableType touchableType;
		public ControlMechanism controlMechanism;

		void Start () {
			controlMechanism = this.gameObject.GetComponentInParent<ControlMechanism> ();
		}
	}
}