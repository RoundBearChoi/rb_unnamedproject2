using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerRunningKick : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningKick.ToString();

            CONTROL_MECHANISM.RIGIDBODY.AddForce(Vector3.up * 120f);
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {
                move.MoveForward(MOVEMENT_DATA.RunSpeed * 1.125f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
            else
            {
                move.MoveForward(MOVEMENT_DATA.RunSpeed * 1.13f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log(ANIMATION_DATA.PlayTime);

                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)PlayerState.HumanoidRun);
                    return;
                }
            }

            attack.UpdateHit(TouchDetectorType.ATTACK_RIGHT_FOOT, ref attack.Target);
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, PlayerState.RunningKick.ToString());
        }
    }
}
