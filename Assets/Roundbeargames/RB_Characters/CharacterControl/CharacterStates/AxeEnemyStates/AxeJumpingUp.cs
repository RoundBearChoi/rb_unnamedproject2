using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class AxeJumpingUp : CharacterState {
		public override void InitState () {
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
						characterStateController.ChangeState ((int) AxeEnemyState.AxeFallingToLanding);
						return;
					}

					if (AI_CONTROL.transform.position.y > AI_CONTROL.GetNextWayPoint ().transform.position.y) {
						if (Mathf.Abs (AI_CONTROL.transform.position.x - AI_CONTROL.GetNextWayPoint ().transform.position.x) < 1f) {
							move.MoveForward (MOVEMENT_DATA.WalkSpeed, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
						}
					}
				}
			}
		}

		public override void ClearState () {

		}
	}
}