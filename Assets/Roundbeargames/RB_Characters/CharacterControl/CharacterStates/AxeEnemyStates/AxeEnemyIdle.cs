using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class AxeEnemyIdle : CharacterState {
        public override void InitState () {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeIdle.ToString ();

            if (pathFinder == null) {
                pathFinder = GameObject.FindObjectOfType<PathFinder> ();
            }
        }

        public override void RunFixedUpdate () {

        }

        public override void ClearState () {

        }

        public override void RunFrameUpdate () {
            if (UpdateAnimation ()) {
                if (findPlayer.TriggerAttack ()) {
                    characterStateController.ChangeState ((int) AxeEnemyState.AxeAttackDownward);
                    return;
                }

                if (ChasePlayer ()) {
                    AI_CONTROL.FindPathToPlayer ();

                    switch (AI_CONTROL.GetPathFindMethod ()) {
                        case PathFindMethod.NONE:
                            break;
                        case PathFindMethod.WALK:
                            characterStateController.ChangeState ((int) AxeEnemyState.AxeWalkForward);
                            return;
                        case PathFindMethod.TURN:
                            characterStateController.ChangeState ((int) AxeEnemyState.StandingTurnToRight90);
                            return;
                    }
                }
            }
        }

        private PathFinder pathFinder;

        bool ChasePlayer () {
            if (AI_CONTROL.GetLastPlayerWayPoint () != null) {
                if (AI_CONTROL.PlayerIsClose (10f)) {
                    if (!AI_CONTROL.PlayerIsDead ()) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}