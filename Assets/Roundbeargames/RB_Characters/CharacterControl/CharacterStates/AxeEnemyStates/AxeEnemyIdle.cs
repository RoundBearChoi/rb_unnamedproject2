using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AxeEnemyIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeIdle.ToString();
            AI_CONTROL.TargetPath.Clear();
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (findPlayer.TriggerAttack())
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeAttackDownward);
                    return;
                }

                if (ChasePlayer())
                {
                    if (AI_CONTROL.TargetPath.Count == 0)
                    {
                        AI_CONTROL.FindPathToPlayer();
                    }

                    //AI_CONTROL.UpdateStartPath ();
                    AI_CONTROL.InitStartPath();
                    AI_CONTROL.UpdatePathStatus();

                    switch (AI_CONTROL.GetPathFindMethod())
                    {
                        case PathFindMethod.NONE:
                            if (!AI_CONTROL.IsFacingPlayer())
                            {
                                characterStateController.ChangeState((int)AxeEnemyState.StandingTurnToRight90);
                            }
                            return;
                        case PathFindMethod.WALK:
                            characterStateController.ChangeState((int)AxeEnemyState.AxeWalkForward);
                            return;
                        case PathFindMethod.TURN:
                            characterStateController.ChangeState((int)AxeEnemyState.StandingTurnToRight90);
                            return;
                        case PathFindMethod.JUMP:
                            characterStateController.ChangeState((int)AxeEnemyState.AxeJumpingUp);
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

        bool ChasePlayer()
        {
            if (AI_CONTROL.GetLastPlayerWayPoint() != null)
            {
                if (AI_CONTROL.PlayerIsClose(20f))
                {
                    if (!AI_CONTROL.PlayerIsDead())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}