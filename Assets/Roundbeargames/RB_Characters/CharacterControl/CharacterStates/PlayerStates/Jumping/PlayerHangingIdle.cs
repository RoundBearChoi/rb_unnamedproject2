using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerHangingIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.HangingIdle.ToString();
            MOVEMENT_DATA.AirMomentum = 0f;
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (MOVEMENT_DATA.MoveUp && MOVEMENT_DATA.Jump)
                {
                    characterStateController.ChangeState((int)PlayerState.BracedHangToCrouch);
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