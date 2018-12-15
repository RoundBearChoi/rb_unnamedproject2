using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerRunningTurn : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningTurn.ToString ();
			slowDown.SetBaseSpeed (MOVEMENT_DATA.RunSpeed * 0.9f);
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				if (CONTROL_MECHANISM.IsFalling ()) {
					characterStateController.ChangeState ((int) PlayerState.FallALoop);
					return;
				}

				if (!MOVEMENT_DATA.IsGrounded) {
					characterStateController.ChangeState ((int) PlayerState.FallALoop);
				}
				slowDown.SlowDownToStop ();
				UpdateTurn ();
			} else {
				move.MoveWithoutTurning (slowDown.GetBaseSpeed (), CHARACTER_TRANSFORM.rotation.eulerAngles.y);
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);
			}
		}

		public override void ClearState () {

		}

		private void UpdateTurn () {
			if (DurationTimePassed ()) {
				if (CHARACTER_TRANSFORM.right.x > 0) {
					CHARACTER_TRANSFORM.rotation = Quaternion.Euler (0, 180f, 0);
				} else {
					CHARACTER_TRANSFORM.rotation = Quaternion.Euler (0, 0, 0);
				}

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
						//characterStateController.ChangeState ((int) PlayerState.JumpingUp);
						break;
				}
			}
		}
	}
}