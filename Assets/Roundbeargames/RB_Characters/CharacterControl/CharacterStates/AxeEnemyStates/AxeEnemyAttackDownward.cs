using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AxeEnemyAttackDownward : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeAttackDownward.ToString();
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
                //normal hit
                attack.UpdateHit();

                //Debug.Log(ANIMATION_DATA.PlayTime);

                //too close still dies
                if (ANIMATION_DATA.PlayTime > DeathByTooCloseTime)
                {
                    if (ANIMATION_DATA.PlayTime < attack.AttackEndTime)
                    {
                        if (CHARACTER_DATA.IsTouchingPlayer(TouchDetectorType.FRONT))
                        {
                            if (!AI_CONTROL.PlayerIsDead())
                            {
                                if (!CHARACTER_MANAGER.Player.characterStateController.NonKillable)
                                {
                                    CHARACTER_MANAGER.Player.characterStateController.ChangeState(999);
                                }
                            }
                        }
                    }

                }

                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
                    attack.DeRegister(characterStateController.controlMechanism.gameObject.name, AxeEnemyState.AxeAttackDownward.ToString());
                }
            }
        }

        public float DeathByTooCloseTime;
    }
}