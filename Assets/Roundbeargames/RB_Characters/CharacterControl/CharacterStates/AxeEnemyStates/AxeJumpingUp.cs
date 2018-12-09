using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class AxeJumpingUp : CharacterState {
		public override void InitState () {
			//AI_CONTROL.TargetPath.Clear ();
			MOVEMENT_DATA.IsJumped = false;
			ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeJumpingUp.ToString ();
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				jump.JumpUp (400f, false);
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				if (ANIMATION_DATA.PlayTime > jump.JumpTime + 0.5f) {
					if (MOVEMENT_DATA.IsGrounded) {
						//Debug.Log ("landed");
					}
				}
			}
		}

		public override void ClearState () {

		}
	}
}