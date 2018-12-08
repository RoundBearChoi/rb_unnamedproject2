using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerRunningSlide : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningSlide.ToString ();
			slowDown.SetBaseSpeed (MOVEMENT_DATA.RunSpeed);
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				move.CheckFall ();
				slowDown.SlowDownToStop ();

				if (DurationTimePassed ()) {
					MOVEMENT_DATA.Turn = move.GetTurn ();
					if (MOVEMENT_DATA.MoveDown) {
						characterStateController.ChangeState ((int) PlayerState.CrouchIdle);
					} else {
						switch (move.GetMoveTransition ()) {
							case MoveTransitionStates.RUN:
								characterStateController.ChangeState ((int) PlayerState.HumanoidRun);
								break;
							case MoveTransitionStates.WALK:
								characterStateController.ChangeState ((int) PlayerState.HumanoidWalk);
								break;
							case MoveTransitionStates.JUMP:
								break;
							case MoveTransitionStates.NONE:
								characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
								break;
						}
					}
				}
			} else {
				move.MoveForward (MOVEMENT_DATA.RunSpeed * 1.125f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
			}
		}
		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);
			}
		}

		public override void ClearState () {

		}
	}
}