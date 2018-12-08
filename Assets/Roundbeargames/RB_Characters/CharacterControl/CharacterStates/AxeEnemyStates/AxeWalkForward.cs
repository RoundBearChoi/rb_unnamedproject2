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


        }

        public override void RunFixedUpdate()
        {
            if (ANIMATION_DATA.AnimationNameMatches)
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