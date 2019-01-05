using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerCrouchSneakLeft : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.CrouchedSneakingLeft.ToString();
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
                UpdateCrouchWalk();
            }
            else
            {
                MOVEMENT_DATA.Turn = move.GetTurn();
                move.MoveForward(MOVEMENT_DATA.CrouchSpeed, MOVEMENT_DATA.Turn);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {

            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        private void UpdateCrouchWalk()
        {
            switch (move.GetMoveTransition())
            {
                case MoveTransitionStates.RUN:
                case MoveTransitionStates.WALK:
                    if (!MOVEMENT_DATA.MoveDown)
                    {
                        characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                    }
                    MOVEMENT_DATA.Turn = move.GetTurn();
                    move.MoveForward(MOVEMENT_DATA.CrouchSpeed, MOVEMENT_DATA.Turn);
                    return;
                case MoveTransitionStates.JUMP:
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                    return;
                case MoveTransitionStates.NONE:
                    characterStateController.ChangeState((int)PlayerState.CrouchIdle);
                    return;
            }
        }
    }
}