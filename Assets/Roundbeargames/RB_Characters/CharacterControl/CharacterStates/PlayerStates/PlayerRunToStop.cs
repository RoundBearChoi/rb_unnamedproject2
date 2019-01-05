using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerRunToStop : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.RunToStop.ToString();
            slowDown.SetBaseSpeed(MOVEMENT_DATA.RunSpeed * 0.9f);
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
                UpdateRunToStop();
            }
            else
            {
                move.MoveForward(slowDown.GetBaseSpeed(), CHARACTER_TRANSFORM.rotation.eulerAngles.y);
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (ATTACK_DATA != null)
                {
                    if (ATTACK_DATA.AttackA)
                    {
                        characterStateController.ChangeState((int)PlayerState.SurpriseUppercut);
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

        private void UpdateRunToStop()
        {
            //Debug.Log (PlayTime);

            if (!DurationTimePassed())
            {
                return;
            }

            float turn = move.GetTurn();
            CHARACTER_TRANSFORM.rotation = Quaternion.Euler(0, turn, 0);

            switch (move.GetMoveTransition())
            {
                case MoveTransitionStates.RUN:
                    characterStateController.ChangeState((int)PlayerState.HumanoidRun);
                    break;
                case MoveTransitionStates.WALK:
                    characterStateController.ChangeState((int)PlayerState.HumanoidWalk);
                    break;
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
                    break;
            }
        }
    }
}