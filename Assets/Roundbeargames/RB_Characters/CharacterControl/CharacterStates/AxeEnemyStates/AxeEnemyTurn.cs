using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AxeEnemyTurn : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.StandingTurnToRight90.ToString();
            turnToPlayer.IsFinished = false;
            turnToPlayer.TurnRoutine = null;
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (turnToPlayer.IsFinished)
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
                }
                else
                {
                    turnToPlayer.UpdateTurn();
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