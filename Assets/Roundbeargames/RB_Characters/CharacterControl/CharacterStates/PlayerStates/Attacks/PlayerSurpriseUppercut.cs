using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerSurpriseUppercut : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.SurpriseUppercut.ToString();
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                //Debug.Log (ANIMATION_DATA.PlayTime);
                if (ANIMATION_DATA.PlayTime >= 0.34f)
                {

                }

                if (DurationTimePassed())
                {

                    if (attack.Target == null)
                    {
                        characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                        return;
                    }
                    else
                    {
                        characterStateController.ChangeState((int)PlayerState.FightIdle);
                        return;
                    }

                }

                attack.UpdateHit(TouchDetectorType.ATTACK_RIGHT_FIST, ref attack.Target);
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, PlayerState.SurpriseUppercut.ToString());
        }
    }
}