using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerRunningJump : CharacterState
    {
        public override void InitState()
        {
            MOVEMENT_DATA.IsJumped = false;
            ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningJump.ToString();

            MOVEMENT_DATA.AirMomentum = 3f;
        }

        public override void RunFixedUpdate()
        {
            MOVEMENT_DATA.Turn = move.GetTurn();
            move.AirMove();

            if (ANIMATION_DATA.AnimationNameMatches)
            {
                jump.JumpUp(JumpForce, true);


                if (jump.GrabLedge())
                {
                    characterStateController.ChangeState((int)PlayerState.HangingIdle);
                    return;
                }

                if (ANIMATION_DATA.PlayTime > 0.1f)
                {
                    if (MOVEMENT_DATA.IsGrounded || move.IsGoingToLand())
                    {
                        CONTROL_MECHANISM.ClearVelocity();
                        characterStateController.ChangeState((int)PlayerState.FallingToLanding);
                        return;
                    }
                }
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);
                if (DurationTimePassed())
                {
                    if (MOVEMENT_DATA.IsGrounded)
                    {
                        CONTROL_MECHANISM.ClearVelocity();
                        characterStateController.ChangeState((int)PlayerState.FallingToLanding);
                        return;
                    }
                    else
                    {
                        characterStateController.ChangeState((int)PlayerState.FallALoop);
                        return;
                    }
                }
            }
        }

        public override void ClearState()
        {

        }

        [SerializeField] float JumpForce;
    }
}