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

				if (Mathf.Abs (MANUAL_CONTROL.RIGIDBODY.velocity.y) > 0.0001f) {
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