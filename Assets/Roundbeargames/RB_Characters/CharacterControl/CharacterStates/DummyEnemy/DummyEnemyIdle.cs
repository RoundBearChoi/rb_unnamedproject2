using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class DummyEnemyIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = DummyEnemyState.HumanoidIdle.ToString();
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {

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
