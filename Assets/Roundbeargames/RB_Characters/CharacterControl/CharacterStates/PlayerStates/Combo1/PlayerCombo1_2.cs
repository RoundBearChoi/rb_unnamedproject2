using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerCombo1_2 : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo1_2.ToString();

            float turn = move.GetTurn();
            move.InstMoveForward(0.135f, turn);

            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive(false);
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive(true);
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {

            }
            else
            {
                move.MoveForward(MOVEMENT_DATA.RunSpeed * 0.25f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);
                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                    return;
                }

                if (ANIMATION_DATA.PlayTime > 0.45f)
                {
                    if (ATTACK_DATA.AttackA)
                    {
                        characterStateController.ChangeState((int)PlayerState.PlayerCombo1_3_Uppercut);
                        return;
                    }
                }

                attack.UpdateHit(TouchDetectorType.ATTACK_LEFT_FIST, ref attack.Target);
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo1_2.ToString());
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BACK].gameObject.SetActive(false);
        }
    }
}