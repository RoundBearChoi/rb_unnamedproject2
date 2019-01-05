using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AxeJumpingUp : CharacterState
    {
        public override void InitState()
        {
            MOVEMENT_DATA.IsJumped = false;
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeJumpingUp.ToString();
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {
                //Debug.Log("jump force: " + AI_CONTROL.GetRequiredJumpForce().ToString());
                jump.JumpUp(AI_CONTROL.GetRequiredJumpForce(), false);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (ANIMATION_DATA.PlayTime > jump.JumpTime + 0.5f)
                {
                    if (MOVEMENT_DATA.IsGrounded)
                    {
                        characterStateController.ChangeState((int)AxeEnemyState.AxeFallingToLanding);
                        return;
                    }

                    if (AI_CONTROL.transform.position.y > AI_CONTROL.GetNextWayPoint().transform.position.y)
                    {
                        if (!IsPastWayPoint())
                        {
                            move.MoveForward(MOVEMENT_DATA.WalkSpeed * AI_CONTROL.GetNextWayPoint().AirWalkSpeedMultiplier, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
                        }
                    }
                }
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        bool IsPastWayPoint()
        {
            if (CONTROL_MECHANISM.IsFacingForward())
            {
                if (CONTROL_MECHANISM.transform.position.x > AI_CONTROL.GetNextWayPoint().transform.position.x)
                {
                    return true;
                }
            }
            else
            {
                if (CONTROL_MECHANISM.transform.position.x < AI_CONTROL.GetNextWayPoint().transform.position.x)
                {
                    return true;
                }
            }
            return false;
        }
    }
}