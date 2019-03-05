using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class PlayerCombo1_1 : CharacterState {
        public override void InitState () {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo1_1.ToString ();

            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (true);

            comboTransition.Reset ();
        }

        public override void RunFixedUpdate () {
            if (ANIMATION_DATA.AnimationNameMatches) {

            } else {
                if (characterStateController.PrevState.GetType () == typeof (PlayerIdle)) {
                    move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.45f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
                }
            }
        }

        public override void RunFrameUpdate () {
            if (UpdateAnimation ()) {
                //Debug.Log (ANIMATION_DATA.PlayTime);

                if (comboTransition.GoNext (ANIMATION_DATA.PlayTime, ATTACK_DATA.AttackA)) {
                    characterStateController.ChangeState ((int) PlayerState.PlayerCombo1_2);
                    return;
                }

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
            attack.DeRegister (characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo1_1.ToString ());
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
        }
    }
}