using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerFallingToLanding : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.FallingToLanding.ToString ();
			//MANUAL_CONTROL.RIGIDBODY.velocity = Vector3.zero;
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				if (DurationTimePassed ()) {
					float turn = move.GetTurn ();
					CHARACTER_TRANSFORM.rotation = Quaternion.Euler (0, turn, 0);

					switch (move.GetMoveTransition ()) {
						case MoveTransitionStates.RUN:
							characterStateController.ChangeState ((int) PlayerState.HumanoidRun);
							break;
						case MoveTransitionStates.WALK:
							characterStateController.ChangeState ((int) PlayerState.HumanoidWalk);
							break;
						case MoveTransitionStates.NONE:
							characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
							break;
						case MoveTransitionStates.JUMP:
							characterStateController.ChangeState ((int) PlayerState.JumpingUp);
							break;
					}
				}
			}
		}

		public override void ClearState () {
			MOVEMENT_DATA.AirMomentum = 0f;
		}
	}
}