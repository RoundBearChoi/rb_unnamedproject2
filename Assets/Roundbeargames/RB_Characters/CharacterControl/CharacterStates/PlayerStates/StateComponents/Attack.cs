using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class Attack : StateComponent {
		public float AttackStartTime;
		public float AttackEndTime;
		public bool ShakeCamera = false;
		public void UpdateHit () {
			if (IsWithinAttackTime ()) {
				List<TouchDetector> detectors = characterData.GetTouchDetector (TouchDetectorType.ATTACK);
				Touchable touchable = GetFirstTouch (detectors, TouchableType.CHARACTER);
				if (touchable != null) {
					touchable.controlMechanism.characterStateController.TakeHit (controlMechanism, characterData.characterAnimationData.DesignatedAnimation);
				}
			}
		}

		Touchable GetFirstTouch (List<TouchDetector> targetTouchDetector, TouchableType touchableType) {
			if (targetTouchDetector == null) {
				return null;
			}

			foreach (TouchDetector detector in targetTouchDetector) {
				if (detector.TouchablesDictionary.ContainsKey (touchableType)) {
					if (detector.TouchablesDictionary[touchableType].Count > 0) {
						foreach (Touchable touchable in detector.TouchablesDictionary[TouchableType.CHARACTER]) {
							return touchable;
						}
					}
				}
			}

			return null;
		}

		public bool IsWithinAttackTime () {
			if (this.gameObject.name.Contains ("Jab")) {
				//Debug.Log (animationData.PlayTime);
			}

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