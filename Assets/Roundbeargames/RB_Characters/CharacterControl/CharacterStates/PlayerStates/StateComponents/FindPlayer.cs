using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class FindPlayer : StateComponent {
		public float AttackTriggerDistance;
		public bool TriggerAttack () {
			if (characterState == null) {
				return false;
			}

			if (characterState.AI_CONTROL.PlayerIsClose (AttackTriggerDistance) && characterState.AI_CONTROL.IsFacingPlayer ()) {
				if (!characterState.AI_CONTROL.PlayerIsDead ()) {
					return true;
				}
			}

			return false;
		}
	}
}