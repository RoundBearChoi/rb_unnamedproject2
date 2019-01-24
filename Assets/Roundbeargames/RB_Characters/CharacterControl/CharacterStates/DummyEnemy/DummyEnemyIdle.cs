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
            SlidingPlayerDetector = CHARACTER_DATA.GetTouchDetector(TouchDetectorType.CHARACTER_DETECTOR_SLIDER_FORESIGHT);
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (Stomp)
                {
                    if (StompPlayer())
                    {
                        characterStateController.ChangeState((int)DummyEnemyState.StompingQuick);
                        return;
                    }
                }
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        TouchDetector SlidingPlayerDetector;
        public bool Stomp;

        private bool StompPlayer()
        {
            if (SlidingPlayerDetector.TouchablesDictionary.ContainsKey(TouchableType.CHARACTER))
            {
                List<Touchable> touchables = SlidingPlayerDetector.TouchablesDictionary[TouchableType.CHARACTER];

                foreach (Touchable t in touchables)
                {
                    if (t.controlMechanism.controlType == ControlType.PLAYER)
                    {
                        if (t.controlMechanism.characterStateController.CurrentState.GetType() == typeof(PlayerRunningSlide))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
