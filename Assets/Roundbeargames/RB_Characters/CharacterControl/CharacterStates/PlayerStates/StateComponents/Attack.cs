using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class Attack : StateComponent {
        public ControlMechanism Target;
        public float AttackStartTime;
        public float AttackEndTime;

        public bool AttackAnimationMotionTriggered;

        public bool UpdateHit (TouchDetectorType touchDetectorType, ref ControlMechanism target) {
            if (IsWithinAttackTime ()) {
                TouchDetector detector = characterData.GetTouchDetector (touchDetectorType);
                Touchable touchable = GetFirstTouch (detector, TouchableType.CHARACTER);
                if (touchable != null) {
                    touchable.controlMechanism.characterStateController.TakeHit (controlMechanism, characterData.characterAnimationData.DesignatedAnimation);
                    target = touchable.controlMechanism;
                    return true;
                }
            }
            return false;
        }

        Touchable GetFirstTouch (TouchDetector targetTouchDetector, TouchableType touchableType) {
            if (targetTouchDetector == null) {
                return null;
            }

            if (targetTouchDetector.TouchablesDictionary.ContainsKey (touchableType)) {
                if (targetTouchDetector.TouchablesDictionary[touchableType].Count > 0) {
                    foreach (Touchable touchable in targetTouchDetector.TouchablesDictionary[TouchableType.CHARACTER]) {
                        return touchable;
                    }
                }
            }

            return null;
        }

        public bool IsWithinAttackTime () {
            CharacterAnimationData animationData = characterData.characterAnimationData;

            if (AttackStartTime != 0f && AttackEndTime != 0f) {
                if (animationData.PlayTime != 0f) {
                    if (animationData.PlayTime > AttackStartTime)
                        if (animationData.PlayTime < AttackEndTime) {
                            return true;
                        }
                }
            }
            return false;
        }

        public void DeRegister (string hitter, string move) {
            CharacterManager cm = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
            cm.Deregister (hitter, move);
        }
    }
}