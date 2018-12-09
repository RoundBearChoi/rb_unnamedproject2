using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerRunningJump : CharacterState {
		public override void InitState () {
			MOVEMENT_DATA.IsJumped = false;
			ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningJump.ToString ();

			if (CONTROL_MECHANISM.IsFacingForward ()) {
				MOVEMENT_DATA.AirMomentum = 3f;
			} else {
				MOVEMENT_DATA.AirMomentum = -3f;
			}
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				jump.JumpUp (JumpForce, true);
				jump.CheckLedgeGrab ();

				if (Mathf.Abs (MANUAL_CONTROL.RIGIDBODY.velocity.y) > 0.0001f) {
					MOVEMENT_DATA.Turn = move.GetTurn ();
					move.AirMove ();
				}
			} else {
				MOVEMENT_DATA.Turn = move.GetTurn ();
				move.AirMove ();
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);
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