using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class AxeWalkForward : CharacterState {
        public override void InitState () {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeWalkForward.ToString ();
        }

        public override void RunFixedUpdate () {
            if (ANIMATION_DATA.AnimationNameMatches) {
                if (!MOVEMENT_DATA.IsGrounded) {
                    characterStateController.ChangeState ((int) AxeEnemyState.AxeFallingIdle);
                }

                if (!AI_CONTROL.PlayerIsDead ()) {
                    MOVEMENT_DATA.Turn = move.GetTurn ();
                    move.MoveForward (MOVEMENT_DATA.WalkSpeed, MOVEMENT_DATA.Turn);
                } else {
                    characterStateController.ChangeState ((int) AxeEnemyState.AxeIdle);
                }
            }
        }

        public override void RunFrameUpdate () {
            if (UpdateAnimation ()) {
                if (findPlayer.TriggerAttack ()) {
                    characterStateController.ChangeState ((int) AxeEnemyState.AxeAttackDownward);
                    return;
                }

                AI_CONTROL.UpdatePathStatus ();
                AI_CONTROL.UpdateStartPath ();

                if (AI_CONTROL.TargetPath.Count == 0) {
                    characterStateController.ChangeState ((int) AxeEnemyState.AxeIdle);
                    return;
                }

                switch (AI_CONTROL.GetPathFindMethod ()) {
                    case PathFindMethod.NONE:
                        //characterStateController.ChangeState ((int) AxeEnemyState.AxeIdle);
                        return;
                    case PathFindMethod.WALK:
                        characterStateController.ChangeState ((int) AxeEnemyState.AxeWalkForward);
                        return;
                    case PathFindMethod.TURN:
                        characterStateController.ChangeState ((int) AxeEnemyState.StandingTurnToRight90);
                        return;
                    case PathFindMethod.JUMP:
                        //Debug.Log ("time to jump");
                        characterStateController.ChangeState ((int) AxeEnemyState.AxeJumpingUp);
                        return;
                }

            }
        }

        public override void ClearState () {

        }
    }
}