using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class TurnToPlayer : StateComponent {
		public Coroutine TurnRoutine;
		public float TurnDistance;
		public bool IsFinished = false;
		public void UpdateTurn () {
			if (characterState == null) {
				return;
			}

			if (TurnRoutine == null) {
				TurnRoutine = StartCoroutine (_Turn ());
			}
		}

		IEnumerator _Turn () {
			float targetRotation = 0f;

			if (controlMechanism.IsFacingForward ()) {
				targetRotation = 180f;
			} else {
				targetRotation = 0f;
			}

			while (true) {
				//Debug.Log (controlMechanism.transform.rotation.eulerAngles.y);
				controlMechanism.transform.Rotate (Vector3.up * 100f * Time.deltaTime);
				if (Mathf.Abs (controlMechanism.transform.rotation.eulerAngles.y - targetRotation) <= 3f) {
					break;
				}
				yield return new WaitForFixedUpdate ();
			}
			controlMechanism.transform.rotation = Quaternion.Euler (0, targetRotation, 0);
			IsFinished = true;
		}

		public bool IsTurning () {
			if (TurnRoutine == null) {
				return false;
			} else {
				return true;
			}
		}

		public bool StartTurning () {
			if (characterState == null) {
				return false;
			}

			if (characterState.AI_CONTROL.PlayerIsClose (TurnDistance) && !characterState.AI_CONTROL.IsFacingPlayer ()) {
				if (!characterState.AI_CONTROL.PlayerIsDead ()) {
					return true;
				}
			}
			return false;
		}
	}
}