using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class SlowDown : StateComponent
    {
        public float FootDownTime;
        public float SlowDownRate;
        public float BreakMultiplier;
        private float BaseSpeed;

        public void SetBaseSpeed(float speed)
        {
            BaseSpeed = speed;
        }

        public float GetBaseSpeed()
        {
            return BaseSpeed;
        }

        public void SlowDownToStop()
        {
            if (characterData == null || controlMechanism == null)
            {
                return;
            }

            CharacterAnimationData animationData = characterData.characterAnimationData;
            Transform characterTransform = controlMechanism.transform;

            if (BaseSpeed > 0f)
            {
                if (FootDownTime != 0f)
                {
                    if (animationData.PlayTime <= FootDownTime)
                    {
                        BaseSpeed -= Time.deltaTime * SlowDownRate;
                    }
                    else
                    {
                        BaseSpeed -= Time.deltaTime * SlowDownRate * BreakMultiplier;
                    }
                }
                else
                {
                    BaseSpeed -= Time.deltaTime * SlowDownRate;
                }

                if (characterData.CanMoveThrough(TouchDetectorType.FRONT))
                {
                    characterTransform.Translate(Vector3.right * BaseSpeed * Time.deltaTime);
                }
            }
        }
    }
}