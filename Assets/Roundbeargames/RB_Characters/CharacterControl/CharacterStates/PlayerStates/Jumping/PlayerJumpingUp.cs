using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerJumpingUp : CharacterState {
		public override void InitState () {
			MOVEMENT_DATA.IsJumped = false;
			ANIMATION_DATA.DesignatedAnimation = PlayerState.JumpingUp.ToString ();
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				jump.JumpUp (JumpForce, true);
				jump.CheckLedgeGrab ();

				if (ANIMATION_DATA.PlayTime > 0.5f) {
					if (MOVEMENT_DATA.IsGrounded || move.IsGoingToLand ()) {
						characterStateController.ChangeState ((int) PlayerState.FallingToLanding);
					}
				}

				if (Mathf.Abs (MANUAL_CONTROL.RIGIDBODY.velocity.y) > 0.25f) {
					MOVEMENT_DATA.Turn = move.GetTurn ();
					move.AirMove ();
				}
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				if (DurationTimePassed ()) {
					characterStateController.ChangeState ((int) PlayerState.FallALoop);
				}
			}
		}

		public override void ClearState () {

		}

		[SerializeField] float JumpForce;

	}
}