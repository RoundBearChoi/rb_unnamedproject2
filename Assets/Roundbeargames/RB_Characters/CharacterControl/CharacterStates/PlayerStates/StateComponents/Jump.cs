using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class Jump : StateComponent
    {
        public float JumpTime;

        public bool IsJumpTime()
        {
            if (characterData.characterAnimationData.PlayTime >= JumpTime)
            {
                if (movementData.IsGrounded)
                {
                    return true;
                }
            }
            return false;
        }

        public void JumpUp(float JumpForce, bool showJumpSmoke)
        {
            if (!movementData.IsJumped)
            {
                if (IsJumpTime())
                {
                    if (showJumpSmoke)
                    {
                        ShowJumpSmoke();
                    }
                    controlMechanism.RIGIDBODY.AddForce(Vector3.up * JumpForce);
                    movementData.IsJumped = true;
                }
            }
        }

        void ShowJumpSmoke()
        {
            Vector3 footPos = new Vector3(controlMechanism.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.x, controlMechanism.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.y + 0.1f, 0f);
            vfxManager.ShowSimpleEffect(SimpleEffectType.GROUND_SHOCK, footPos);
            vfxManager.ShowSimpleEffect(SimpleEffectType.GROUND_SMOKE, footPos);
            controlMechanism.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive(false);
            controlMechanism.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive(true);
        }

        public void CheckLedgeGrab()
        {
            if (IsGrabbingLedge())
            {
                controlMechanism.RIGIDBODY.velocity = Vector3.zero;
                controlMechanism.RIGIDBODY.angularVelocity = Vector3.zero;
                controlMechanism.RIGIDBODY.useGravity = false;
                characterStateController.ChangeState((int)PlayerState.HangingIdle);
            }
        }

        public bool IsGrabbingLedge()
        {
            if (characterData == null)
            {
                return false;
            }

            if (characterData.characterMovementData.MoveUp == true)
            {
                List<TouchDetector> tList = characterStateController.characterData.GetTouchDetector(TouchDetectorType.TOP_FRONT);
                if (tList != null)
                {
                    foreach (TouchDetector d in tList)
                    {
                        if (d.TouchablesDictionary.ContainsKey(TouchableType.LEDGE))
                        {
                            if (d.TouchablesDictionary[TouchableType.LEDGE].Count > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}