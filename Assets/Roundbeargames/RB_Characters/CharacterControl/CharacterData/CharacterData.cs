using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {

    public class CharacterData : SerializedMonoBehaviour {
        public CharacterMovementData characterMovementData;
        public CharacterAnimationData characterAnimationData;
        public CharacterAttackData characterAttackData;
        public ControlMechanism controlMechanism;
        public HitRegister hitRegister;
        public Dictionary<TouchDetectorType, List<TouchDetector>> TouchDetectors;

        public List<TouchDetector> GetTouchDetector (TouchDetectorType touchDetectorType) {
            if (TouchDetectors.ContainsKey (touchDetectorType)) {
                return TouchDetectors[touchDetectorType];
            } else {
                return null;
            }
        }

        public bool IsTouchingGeneralObject (TouchDetectorType touchDetectorType) {
            if (TouchDetectors.ContainsKey (touchDetectorType)) {
                foreach (TouchDetector t in TouchDetectors[touchDetectorType]) {
                    if (t.IsTouching) {
                        if (t.GeneralObjects.Count > 0) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void FindDatas () {
            if (controlMechanism == null) {
                controlMechanism = this.gameObject.GetComponentInParent<ControlMechanism> ();
            }

            if (characterMovementData == null) {
                characterMovementData = this.gameObject.GetComponentInChildren<CharacterMovementData> ();
            }

            if (characterAnimationData == null) {
                characterAnimationData = this.gameObject.GetComponentInChildren<CharacterAnimationData> ();
            }

            if (characterAttackData == null) {
                characterAttackData = this.gameObject.GetComponentInChildren<CharacterAttackData> ();
            }

            if (hitRegister == null) {
                hitRegister = this.gameObject.GetComponentInChildren<HitRegister> ();
            }

            TouchDetector[] detectors = controlMechanism.gameObject.GetComponentsInChildren<TouchDetector> ();

            foreach (TouchDetector d in detectors) {
                if (!TouchDetectors.ContainsKey (d.touchDetectorType)) {
                    List<TouchDetector> newList = new List<TouchDetector> ();
                    newList.Add (d);
                    TouchDetectors.Add (d.touchDetectorType, newList);
                } else {
                    if (!TouchDetectors[d.touchDetectorType].Contains (d)) {
                        TouchDetectors[d.touchDetectorType].Add (d);
                    }
                }
            }
        }
    }
}