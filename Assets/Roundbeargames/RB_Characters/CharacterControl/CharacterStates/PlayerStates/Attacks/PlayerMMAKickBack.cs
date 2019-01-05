using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerMMAKickBack : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.mmaKick_back.ToString();
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
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                    return;
                }

                if (attack.UpdateHit(TouchDetectorType.ATTACK_RIGHT_FOOT, ref attack.Target))
                {
                    //Debug.Log("mma kick hit");
                }
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {
            attack.DeRegister(characterStateController.controlMechanism.gameObject.name, PlayerState.mmaKick_back.ToString());
        }
    }
}

