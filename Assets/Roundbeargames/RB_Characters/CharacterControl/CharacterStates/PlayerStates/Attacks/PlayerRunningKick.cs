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
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }
    }
}
