using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AxeWalkForward : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeWalkForward.ToString();

            AI_CONTROL.TargetPath.Clear();

            if (MOVEMENT_DATA.LastWayPoint != null)
            {
                if (AI_CONTROL.GetLastPlayerWayPoint() != null)
                {
                    List<WayPoint> newPath = move.FindClosestPathTo(MOVEMENT_DATA.LastWayPoint, AI_CONTROL.GetLastPlayerWayPoint());
                    foreach (WayPoint w in newPath)
                    {
                        AI_CONTROL.TargetPath.Add(w);
                    }
                }
            }
        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
            {
                if (AI_CONTROL.IsFacingPlayer())
                {
                    if (!AI_CONTROL.PlayerIsDead())
                    {
                        MOVEMENT_DATA.Turn = move.GetTurn();
                        move.MoveForward(MOVEMENT_DATA.WalkSpeed, MOVEMENT_DATA.Turn);
                    }
                    else
                    {
                        characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
                    }
                }
                else
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
                }
            }
        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (findPlayer.TriggerAttack())
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeAttackDownward);
                }
            }
        }

        public override void ClearState()
        {

        }
    }
}