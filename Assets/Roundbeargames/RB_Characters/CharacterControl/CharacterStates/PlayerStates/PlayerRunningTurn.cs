using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerRunningTurn : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningTurn.ToString();


            if (move.CanInstantTurn(characterStateController.PrevState.GetType()))
            {
                slowDown.SetBaseSpeed(MOVEMENT_DATA.RunSpeed * 0.9f);
            }
            else
            {
                slowDown.SetBaseSpeed(MOVEMENT_DATA.RunSpeed * 0.38f);
            }
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {
                if (CONTROL_MECHANISM.IsFalling())
                {
                    characterStateController.ChangeState((int)PlayerState.FallALoop);
                    return;
                }

                if (!MOVEMENT_DATA.IsGrounded)
                {
                    characterStateController.ChangeState((int)PlayerState.FallALoop);
                }
                slowDown.SlowDownToStop();
                UpdateTurn();
            }
            else
            {
                move.MoveWithoutTurning(slowDown.GetBaseSpeed(), CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        private void UpdateTurn()
        {
            if (DurationTimePassed())
            {
                if (CHARACTER_TRANSFORM.right.x > 0)
                {
                    CHARACTER_TRANSFORM.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else
                {
                    CHARACTER_TRANSFORM.rotation = Quaternion.Euler(0, 0, 0);
                }

                switch (move.GetMoveTransition())
                {
                    case MoveTransitionStates.RUN:
                        characterStateController.ChangeState((int)PlayerState.HumanoidRun);
                        return;
                    case MoveTransitionStates.WALK:
                        characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                        return;
                    case MoveTransitionStates.NONE:
                        characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                        return;
                    case MoveTransitionStates.JUMP:
                        if (MOVEMENT_DATA.Run)
                        {
                            characterStateController.ChangeState((int)PlayerState.RunningJump);
                        }
                        else
                        {
                            characterStateController.ChangeState((int)PlayerState.JumpingUp);
                        }
                        return;
                }
            }
        }
    }
}