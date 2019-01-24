using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerRunningSlide : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.RunningSlide.ToString();
            slowDown.SetBaseSpeed(MOVEMENT_DATA.RunSpeed);
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

                slowDown.SlowDownToStop();
            }
            else
            {
                move.MoveForward(MOVEMENT_DATA.RunSpeed * 1.125f, CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }
        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);

                if (DurationTimePassed())
                {
                    if (MOVEMENT_DATA.MoveDown)
                    {
                        characterStateController.ChangeState((int)PlayerState.CrouchIdle);
                        return;
                    }

                    switch (move.GetMoveTransition())
                    {
                        case MoveTransitionStates.WALK:
                            if (move.MoveDirectionMatches())
                            {
                                characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                            }
                            else
                            {
                                characterStateController.ChangeState((int)PlayerState.RunningTurn);
                            }
                            return;
                        case MoveTransitionStates.RUN:
                            if (move.MoveDirectionMatches())
                            {
                                characterStateController.ChangeState((int)PlayerState.HumanoidRun);
                            }
                            else
                            {
                                characterStateController.ChangeState((int)PlayerState.RunningTurn);
                            }
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
                        case MoveTransitionStates.NONE:
                            characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                            return;
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
    }
}