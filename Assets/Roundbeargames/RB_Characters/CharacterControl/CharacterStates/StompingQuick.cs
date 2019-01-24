using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class StompingQuick : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = DummyEnemyState.StompingQuick.ToString();
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log(ANIMATION_DATA.PlayTime);

                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)DummyEnemyState.HumanoidIdle);
                    return;
                }

                attack.UpdateHit(TouchDetectorType.ATTACK_RIGHT_FOOT, ref attack.Target);
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, DummyEnemyState.StompingQuick.ToString());
        }
    }
}
