using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerStandToRoll : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.StandToRoll.ToString ();
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				move.SimpleMove (MOVEMENT_DATA.AirMomentum * 0.5f, CONTROL_MECHANISM.IsFacingForward ());
			} else {
				move.SimpleMove (MOVEMENT_DATA.AirMomentum * 0.85f, CONTROL_MECHANISM.IsFacingForward ());
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);

				if (DurationTimePassed ()) {
					if (MOVEMENT_DATA.MoveDown) {
						characterStateController.ChangeState ((int) PlayerState.CrouchIdle);
						return;
					}

					switch (move.GetMoveTransition ()) {
						case MoveTransitionStates.JUMP:
							if (MOVEMENT_DATA.MoveForward || MOVEMENT_DATA.MoveBack) {
								if (MOVEMENT_DATA.Run) {
									characterStateController.ChangeState ((int) PlayerState.HumanoidRun);
								} else {
									characterStateController.ChangeState ((int) PlayerState.HumanoidWalk);
								}
							}
							break;
						case MoveTransitionStates.NONE:
							characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
							break;
						case MoveTransitionStates.RUN:
							characterStateController.ChangeState ((int) PlayerState.HumanoidRun);
							break;
						case MoveTransitionStates.WALK:
							characterStateController.ChangeState ((int) PlayerState.HumanoidWalk);
							break;
					}
				}
			}
		}

		public override void ClearState () {
			MOVEMENT_DATA.AirMomentum = 0f;
			CONTROL_MECHANISM.ClearVelocity ();
		}
	}
}