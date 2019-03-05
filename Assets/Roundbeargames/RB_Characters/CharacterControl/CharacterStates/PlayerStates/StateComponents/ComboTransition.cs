using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class ComboTransition : StateComponent {
		public float PressTime;
		public float TransitionTime;
		public bool AttackUnpressed;
		public bool AttackPressed;
		public bool GoNext (float animationTime, bool attackA) {
			if (!attackA && !AttackUnpressed) {
				AttackUnpressed = true;
			}

			if (AttackUnpressed) {
				if (PressTime < animationTime) {
					if (attackA) {
						AttackPressed = true;
					}
				}
			}

			if (AttackPressed) {
				if (TransitionTime < animationTime) {
					return true;
				}
			}

			return false;
		}

		public void Reset () {
			AttackPressed = false;
			AttackUnpressed = false;
		}
	}
}