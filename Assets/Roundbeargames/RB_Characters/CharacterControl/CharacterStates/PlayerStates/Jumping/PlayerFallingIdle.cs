using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerFallingIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.FallALoop.ToString();
        }

        public override void RunFixedUpdate()
        {
            MOVEMENT_DATA.Turn = move.GetTurn();
            move.AirMove();

            if (MOVEMENT_DATA.IsGrounded)
            {
                if (RollAfterJump())
                {
                    characterStateController.ChangeState((int)PlayerState.StandToRoll);
                    return;
                }
                else
                {
                    CONTROL_MECHANISM.ClearVelocity();
                    characterStateController.ChangeState((int)PlayerState.FallingToLanding);
                    return;
                }

            }

            if (ANIMATION_DATA.AnimationNameMatches)
            {
                if (jump.GrabLedge())
                {
                    characterStateController.ChangeState((int)PlayerState.HangingIdle);
                    return;
                }
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {

            }
        }

        private bool RollAfterJump()
        {
            if (MOVEMENT_DATA.AirMomentum < 3.6f)
            {
                return false;
            }

            if (CONTROL_MECHANISM.IsFacingForward())
            {
                if (MOVEMENT_DATA.MoveForward)
                {
                    return true;
                }
            }
            else if (!CONTROL_MECHANISM.IsFacingForward())
            {
                if (MOVEMENT_DATA.MoveBack)
                {
                    return true;
                }
            }
            return false;
        }

        public override void ClearState()
        {

        }
    }
}