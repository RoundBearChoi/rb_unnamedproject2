using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class Jump : StateComponent {
        public float JumpTime;
        public Ledge GrabbedLedge;

        public bool IsJumpTime () {
            if (characterData.characterAnimationData.PlayTime >= JumpTime) {
                if (movementData.IsGrounded) {
                    return true;
                }
            }
            return false;
        }

        public void JumpUp (float JumpForce, bool showJumpSmoke) {
            if (!movementData.IsJumped) {
                if (IsJumpTime ()) {
                    if (showJumpSmoke) {
                        ShowJumpSmoke ();
                    }
                    controlMechanism.RIGIDBODY.AddForce (Vector3.up * JumpForce);
                    movementData.IsJumped = true;
                }
            }
        }

        void ShowJumpSmoke () {
            Vector3 footPos = new Vector3 (controlMechanism.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.x, controlMechanism.BodyPartDictionary[BodyPart.RIGHT_FOOT].position.y + 0.1f, 0f);
            vfxManager.ShowSimpleEffect (SimpleEffectType.GROUND_SHOCK, footPos);
            vfxManager.ShowSimpleEffect (SimpleEffectType.GROUND_SMOKE, footPos);
            controlMechanism.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive (false);
            controlMechanism.BodyTrailDictionary[BodyTrail.BODY].gameObject.SetActive (true);
        }

        public bool GrabLedge () {
            if (IsGrabbingLedge ()) {
                //Debug.Log(GrabbedLedge.gameObject.name);
                controlMechanism.RIGIDBODY.useGravity = false;
                controlMechanism.RIGIDBODY.MovePosition (GrabbedLedge.transform.position + GrabbedLedge.GrabPosition);
                controlMechanism.ClearVelocity ();
                return true;
            }
            return false;
        }

        public bool IsGrabbingLedge () {
            if (characterData == null) {
                return false;
            }

            if (characterData.characterMovementData.MoveUp == true) {
                TouchDetector t = characterStateController.characterData.GetTouchDetector (TouchDetectorType.TOP_FRONT);
                if (t != null) {
                    if (t.TouchablesDictionary.ContainsKey (TouchableType.LEDGE)) {
                        if (t.TouchablesDictionary[TouchableType.LEDGE].Count > 0) {
                            foreach (Touchable touchable in t.TouchablesDictionary[TouchableType.LEDGE]) {
                                GrabbedLedge = touchable.gameObject.GetComponent<Ledge> ();
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