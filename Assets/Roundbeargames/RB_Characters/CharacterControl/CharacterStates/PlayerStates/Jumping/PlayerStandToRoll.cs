using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerStandToRoll : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.StandToRoll.ToString();
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {
                move.SimpleMove(MOVEMENT_DATA.AirMomentum * 0.5f, true);
            }
            else
            {
                move.SimpleMove(MOVEMENT_DATA.AirMomentum * 0.85f, true);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log(ANIMATION_DATA.PlayTime);
                if (ANIMATION_DATA.PlayTime >= FallCheck)
                {
                    if (!MOVEMENT_DATA.IsGrounded)
                    {
                        characterStateController.ChangeState((int)PlayerState.FallALoop);
                    }
                }

                if (DurationTimePassed())
                {
                    if (MOVEMENT_DATA.MoveDown)
                    {
                        characterStateController.ChangeState((int)PlayerState.CrouchIdle);
                        return;
                    }

                    switch (move.GetMoveTransition())
                    {
                        case MoveTransitionStates.JUMP:
                            if (MOVEMENT_DATA.MoveForward || MOVEMENT_DATA.MoveBack)
                            {
                                if (MOVEMENT_DATA.Run)
                                {
                                    characterStateController.ChangeState((int)PlayerState.HumanoidRun);
                                }
                                else
                                {
                                    characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                                }
                            }
                            else
                            {
                                characterStateController.ChangeState((int)PlayerState.JumpingUp);
                            }
                            return;
                        case MoveTransitionStates.NONE:
                            characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
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
                        case MoveTransitionStates.WALK:
                            characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                            if (move.MoveDirectionMatches())
                            {
                                characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                            }
                            else
                            {
                                characterStateController.ChangeState((int)PlayerState.RunningTurn);
                            }
                            return;
                    }
                }
            }
        }

        public override void ClearState()
        {
            MOVEMENT_DATA.AirMomentum = 0f;
            CONTROL_MECHANISM.ClearVelocity();
        }

        public float FallCheck;
    }
}