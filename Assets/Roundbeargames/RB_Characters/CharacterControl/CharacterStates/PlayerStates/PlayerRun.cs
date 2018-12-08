using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerRun : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.HumanoidRun.ToString ();
			RunStopBuffer = 0f;
			RunStop = null;

			if (characterStateController.PrevState.GetType () != typeof (PlayerRunningTurn)) {
				MOVEMENT_DATA.Turn = move.GetTurn ();
				if (MOVEMENT_DATA.Turn != CHARACTER_TRANSFORM.rotation.eulerAngles.y) {
					CHARACTER_TRANSFORM.rotation = Quaternion.Euler (0, MOVEMENT_DATA.Turn, 0);
				}
			}

		}

		public override void RunFixedUpdate () {
			MOVEMENT_DATA.Turn = move.GetTurn ();

			if (ANIMATION_DATA.AnimationNameMatches) {
				move.CheckFall ();
				UpdateRun ();
			} else {
				move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.9f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {

			}
		}

		public override void ClearState () {

		}

		void UpdateRun () {
			switch (move.GetMoveTransition ()) {
				case MoveTransitionStates.RUN:
				case MoveTransitionStates.WALK:
					if (MOVEMENT_DATA.Turn != CHARACTER_TRANSFORM.rotation.eulerAngles.y) {
						characterStateController.ChangeState ((int) PlayerState.RunningTurn);
						break;
					} else {
						move.MoveForward (MOVEMENT_DATA.RunSpeed, MOVEMENT_DATA.Turn);
					}
					break;
				case MoveTransitionStates.JUMP:
					characterStateController.ChangeState ((int) PlayerState.RunningJump);
					break;
				case MoveTransitionStates.NONE:
					if (RunStop == null) {
						RunStop = StartCoroutine (_StopRunning ());
					} else {
						if (MOVEMENT_DATA.MoveDown) {
							characterStateController.ChangeState ((int) PlayerState.RunningSlide);
						} else if (MOVEMENT_DATA.MoveUp) {
							characterStateController.ChangeState ((int) PlayerState.JumpOver);
						} else {
							move.MoveForward (MOVEMENT_DATA.RunSpeed, MOVEMENT_DATA.Turn);
						}
					}
					break;
			}

			if (RunStopBuffer >= 0.05f) {
				characterStateController.ChangeState ((int) PlayerState.RunToStop);
			}
		}

		public float RunStopBuffer;
		private Coroutine RunStop;

		IEnumerator _StopRunning () {
			while (true) {
				RunStopBuffer += Time.deltaTime;
				if (RunStopBuffer >= 0.05f) {
					break;
				}
				yield return new WaitForFixedUpdate ();
			}
		}
	}
}