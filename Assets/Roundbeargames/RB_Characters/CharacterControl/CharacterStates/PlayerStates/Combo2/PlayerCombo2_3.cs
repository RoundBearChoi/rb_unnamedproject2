using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerCombo2_3 : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo2_3.ToString ();

			//float turn = move.GetTurn ();
			//move.InstMoveForward (0.135f, turn);

			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (true);

			CONTROL_MECHANISM.RIGIDBODY.AddForce (Vector3.up * 200f);
			//comboTransition.Reset ();
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				if (ANIMATION_DATA.PlayTime < 0.6f) {
					if (!MOVEMENT_DATA.IsGrounded) {
						move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.65f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
					}
				}
			} else {
				move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.7f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				Debug.Log (ANIMATION_DATA.PlayTime);

				if (DurationTimePassed ()) {
					characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
					return;
				}

				attack.UpdateHit (TouchDetectorType.ATTACK_RIGHT_FIST, ref attack.Target);
			}
		}

		public override void RunLateUpdate () {

		}

		public override void ClearState () {
			attack.DeRegister (characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo2_3.ToString ());
			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
		}
	}
}