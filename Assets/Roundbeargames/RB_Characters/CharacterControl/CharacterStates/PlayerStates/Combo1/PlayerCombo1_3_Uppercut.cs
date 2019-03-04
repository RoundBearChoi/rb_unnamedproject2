using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerCombo1_3_Uppercut : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.PlayerCombo1_3_Uppercut.ToString();

            //float turn = move.GetTurn ();
            //move.InstMoveForward (0.25f, turn);

            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive(false);
            CONTROL_MECHANISM.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive(true);
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {

            }
            else
            {
                if (characterStateController.PrevState.GetType() == typeof(PlayerCombo1_2))
                {
                    move.MoveForward(MOVEMENT_DATA.RunSpeed * 0.95f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
                }
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);
                if (ANIMATION_DATA.PlayTime < 0.25f)
                {
                    move.MoveForward(MOVEMENT_DATA.RunSpeed * 0.6f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);

                }

                if (DurationTimePassed())
                {
                    if (attack.Target == null)
                    {
                        characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                        return;
                    }
                    else
                    {
                        characterStateController.ChangeState((int)PlayerState.FightIdle);
                        return;
                    }
                }

                attack.UpdateHit(TouchDetectorType.ATTACK_RIGHT_FIST, ref attack.Target);
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, PlayerState.PlayerCombo1_3_Uppercut.ToString());
        }
    }
}