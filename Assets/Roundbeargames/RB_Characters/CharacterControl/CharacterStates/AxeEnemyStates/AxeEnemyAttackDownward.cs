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
                attack.UpdateHit(TouchDetectorType.ATTACK_AXE);

                //Debug.Log(ANIMATION_DATA.PlayTime);

                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
                    attack.DeRegister(characterStateController.controlMechanism.gameObject.name, AxeEnemyState.AxeAttackDownward.ToString());
                }
            }
        }
    }
}