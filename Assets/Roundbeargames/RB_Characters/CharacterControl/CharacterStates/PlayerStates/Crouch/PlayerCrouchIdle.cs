using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerCrouchIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.CrouchIdle.ToString();
            //MANUAL_CONTROL.RIGIDBODY.velocity = Vector3.zero;
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (MOVEMENT_DATA.MoveDown)
                {
                    CheckCrouchWalk();
                }
                else
                {
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                }
            }
        }

        public override void ClearState()
        {

        }

        void CheckCrouchWalk()
        {
            switch (move.GetMoveTransition())
            {
                case MoveTransitionStates.RUN:
                case MoveTransitionStates.WALK:
                    characterStateController.ChangeState((int)PlayerState.CrouchedSneakingLeft);
                    break;
                case MoveTransitionStates.JUMP:
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                    break;
                case MoveTransitionStates.NONE:
                    break;
            }
        }
    }
}