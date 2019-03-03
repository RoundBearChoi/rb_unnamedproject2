using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class PlayerIdle : CharacterState {
        public override void InitState () {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.HumanoidIdle.ToString ();
            CONTROL_MECHANISM.ClearVelocity ();
        }

        public override void RunFixedUpdate () {
            MOVEMENT_DATA.Turn = move.GetTurn ();

            if (ANIMATION_DATA.AnimationNameMatches) {
                if (CONTROL_MECHANISM.IsFalling ()) {
                    characterStateController.ChangeState ((int) PlayerState.FallALoop);
                    return;
                }

                if (MOVEMENT_DATA.Turn != CHARACTER_TRANSFORM.rotation.eulerAngles.y) {
                    CHARACTER_TRANSFORM.rotation = Quaternion.Euler (0, MOVEMENT_DATA.Turn, 0);
                }
            }
        }

        public override void RunFrameUpdate () {
            if (UpdateAnimation ()) {
                if (!StartMoving ()) {
                    CheckCrouch ();
                    CheckAttack ();
                }
            }
        }

        public override void RunLateUpdate () {

        }

        public override void ClearState () {

        }

        bool StartMoving () {
            switch (move.GetMoveTransition ()) {
                case MoveTransitionStates.RUN:
                    characterStateController.ChangeState ((int) PlayerState.HumanoidRun);
                    return true;
                case MoveTransitionStates.WALK:
                    characterStateController.ChangeState ((int) PlayerState.HumanoidWalk);
                    return true;
                case MoveTransitionStates.JUMP:
                    if (Mathf.Abs (CONTROL_MECHANISM.RIGIDBODY.velocity.y) < 0.025f) {
                        characterStateController.ChangeState ((int) PlayerState.JumpingUp);
                    }
                    return true;
            }
            return false;
        }

        void CheckCrouch () {
            if (MOVEMENT_DATA.MoveDown) {
                characterStateController.ChangeState ((int) PlayerState.CrouchIdle);
            }
        }

        void CheckAttack () {
            if (ATTACK_DATA == null) {
                return;
            }

            if (ATTACK_DATA.AttackA) {
                //characterStateController.ChangeState((int)PlayerState.Jab_R_1);
                characterStateController.ChangeState ((int) PlayerState.PlayerCombo1_1);
            }
        }
    }
}