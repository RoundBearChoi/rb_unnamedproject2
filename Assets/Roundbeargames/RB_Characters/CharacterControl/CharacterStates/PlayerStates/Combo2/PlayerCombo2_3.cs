using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerCombo2_3 : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo2_3.ToString ();

			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (true);

			Vector3 footPos = new Vector3 (CONTROL_MECHANISM.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.x, CONTROL_MECHANISM.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.y + 0.1f, 0f);
			VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.GROUND_SHOCK, footPos);
			VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.GROUND_SMOKE, footPos);

			//CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive (false);
			//CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive (true);

			//comboTransition.Reset ();

			attack.AttackAnimationMotionTriggered = false;
			CONTROL_MECHANISM.RIGIDBODY.AddForce (Vector3.up * 200f);
		}

		public override void RunFixedUpdate () {
			if (ANIMATION_DATA.AnimationNameMatches) {
				if (ANIMATION_DATA.PlayTime < 0.6f) {
					if (!MOVEMENT_DATA.IsGrounded) {
						move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.65f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
					}
				}

				//hangtime
				if (ANIMATION_DATA.PlayTime > 0.48f) {
					if (!MOVEMENT_DATA.IsGrounded) {
						if (ANIMATION_DATA.AnimationIsPlaying ()) {
							ANIMATION_DATA.StopAnimation ();
						}
					}

					if (!ANIMATION_DATA.AnimationIsPlaying ()) {
						if (MOVEMENT_DATA.IsGrounded) {
							ANIMATION_DATA.PlayAnimation ();
						}
					}
				}

				//show effect - motion, cam shake
				if (ANIMATION_DATA.PlayTime > 0.515f) {
					if (MOVEMENT_DATA.IsGrounded) {
						if (!attack.AttackAnimationMotionTriggered) {
							attack.AttackAnimationMotionTriggered = true;
							GameObject sm = VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.MOTION_STRAIGHT_ATTACK, CONTROL_MECHANISM.transform.position);
							if (!CONTROL_MECHANISM.IsFacingForward ()) {
								sm.transform.rotation = Quaternion.Euler (0, 180, 0);
							} else {
								sm.transform.rotation = Quaternion.Euler (0, 0, 0);
							}
							CAMERA_MANAGER.ShakeCamera (0.3f);
						}
					}
				}

				//show effect - ground effect
				if (ANIMATION_DATA.PlayTime > 0.55f) {
					if (attack.AttackAnimationMotionTriggered && !GroundEffectShown) {
						GroundEffectShown = true;
						GameObject nt = VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.NITRONIC, CONTROL_MECHANISM.transform.position);
						if (!CONTROL_MECHANISM.IsFacingForward ()) {
							nt.transform.rotation = Quaternion.Euler (0, 180, 0);
						} else {
							nt.transform.rotation = Quaternion.Euler (0, 0, 0);
						}
					}
				}
			} else {
				move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.7f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);

				if (DurationTimePassed ()) {
					if (MOVEMENT_DATA.IsGrounded) {
						characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
						return;
					}
				}

				attack.UpdateHit (TouchDetectorType.ATTACK_RIGHT_FIST, ref attack.Target);
			}
		}

		public override void RunLateUpdate () {

		}

		public override void ClearState () {
			attack.DeRegister (characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo2_3.ToString ());
			CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
			GroundEffectShown = false;
		}

		bool GroundEffectShown;
	}
}