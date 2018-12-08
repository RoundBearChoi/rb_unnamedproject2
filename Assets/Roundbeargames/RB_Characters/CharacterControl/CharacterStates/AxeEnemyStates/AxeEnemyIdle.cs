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

            if (pathFinder == null)
            {
                pathFinder = GameObject.FindObjectOfType<PathFinder>();
            }
        }

        public override void RunFixedUpdate()
        {

        }

        public override void ClearState()
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

                if (turnToPlayer.StartTurning())
                {
                    characterStateController.ChangeState((int)AxeEnemyState.StandingTurnToRight90);
                    return;
                }

                if (ChasePlayer())
                {

                }
            }
        }

        private PathFinder pathFinder;

        bool ChasePlayer()
        {
            if (AI_CONTROL.IsFacingPlayer())
            {
                if (AI_CONTROL.GetLastPlayerWayPoint() != null)
                {
                    if (AI_CONTROL.PlayerIsClose(10f))
                    {
                        if (!AI_CONTROL.PlayerIsDead())
                        {
                            characterStateController.ChangeState((int)AxeEnemyState.AxeWalkForward);
                        }

                    }
                }
            }
            return false;
        }
    }
}