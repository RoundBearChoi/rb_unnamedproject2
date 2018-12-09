using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class AxeJumpingUp : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeJumpingUp.ToString ();
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {

			}
		}

		public override void ClearState () {

		}
	}
}