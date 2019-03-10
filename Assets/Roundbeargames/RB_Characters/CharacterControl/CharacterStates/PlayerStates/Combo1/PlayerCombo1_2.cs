using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class PlayerCombo1_2 : CharacterState {
        public override void InitState () {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo1_2.ToString ();

            float turn = move.GetTurn ();

            //CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.RIGHT_FOOT_FIRE].gameObject.SetActive (true);

            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (true);

            move.InstMoveForward (0.135f, turn);

            comboTransition.Reset ();
        }

        public override void RunFixedUpdate () {
            if (ANIMATION_DATA.AnimationNameMatches) {

            } else {
                move.MoveForward (MOVEMENT_DATA.RunSpeed * 0.25f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }

        public override void RunFrameUpdate () {
            if (UpdateAnimation ()) {
                //Debug.Log (ANIMATION_DATA.PlayTime);

                if (comboTransition.GoNext (ANIMATION_DATA.PlayTime, ATTACK_DATA.AttackA)) {
                    if (MOVEMENT_DATA.MoveUp) {
                        characterStateController.ChangeState ((int) PlayerState.PlayerCombo2_3);
                        return;
                    } else {
                        characterStateController.ChangeState ((int) PlayerState.PlayerCombo1_3_Uppercut);
                        return;
                    }
                }

                if (DurationTimePassed ()) {
                    characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
                    return;
                }

                attack.UpdateHit (TouchDetectorType.ATTACK_LEFT_FIST, ref attack.Target);
            }
        }

        public override void RunLateUpdate () {

        }

        public override void ClearState () {
            attack.DeRegister (characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo1_2.ToString ());
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive (false);
            //CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.RIGHT_FOOT_FIRE].gameObject.SetActive (false);
        }
    }
}