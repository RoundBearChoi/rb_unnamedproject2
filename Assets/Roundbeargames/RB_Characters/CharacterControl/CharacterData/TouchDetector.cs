using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum TouchDetectorType {
	FRONT,
	BACK,
	TOP_FRONT,
	ATTACK,
	GROUND_ROLL,
	WAY_POINT_DETECTOR,
}

namespace roundbeargames {
	public class TouchDetector : SerializedMonoBehaviour {
		public TouchDetectorType touchDetectorType;
		public bool IsTouching = false;
		public Dictionary<TouchableType, List<Touchable>> TouchablesDictionary;
		public List<GameObject> GeneralObjects;
		private int Counter = 0;

		public ControlMechanism controlMechanism;

		void Start () {
			controlMechanism = this.gameObject.GetComponentInParent<ControlMechanism> ();
		}

		void OnTriggerEnter (Collider col) {
			Counter++;
			RegisterTouch (col);
		}

		void OnTriggerExit (Collider col) {
			Counter--;
			DeregisterTouch (col);
		}

		void Update () {
			if (Counter > 0) {
				if (!IsTouching) {
					IsTouching = true;
				}
			} else {
				if (IsTouching) {
					IsTouching = false;
				}
			}
		}

		void RegisterTouch (Collider col) {
			Touchable touchable = col.gameObject.GetComponent<Touchable> ();
			if (touchable != null) {
				//Debug.Log ("registering ledgegrab touch");
				if (!TouchablesDictionary.ContainsKey (touchable.touchableType)) {
					List<Touchable> newList = new List<Touchable> ();
					newList.Add (touchable);
					TouchablesDictionary.Add (touchable.touchableType, newList);
				} else {
					if (!TouchablesDictionary[touchable.touchableType].Contains (touchable)) {
						TouchablesDictionary[touchable.touchableType].Add (touchable);
					}
				}
			} else {
				if (col.GetComponent<TouchDetector> () == null) {
					if (!GeneralObjects.Contains (col.gameObject)) {
						GeneralObjects.Add (col.gameObject);
					}
				}
			}
		}

		void DeregisterTouch (Collider col) {
			Touchable touchable = col.gameObject.GetComponent<Touchable> ();
			if (touchable != null) {
				if (TouchablesDictionary.ContainsKey (touchable.touchableType)) {
					if (TouchablesDictionary[touchable.touchableType].Contains (touchable)) {
						TouchablesDictionary[touchable.touchableType].Remove (touchable);
					}
				}

				if (TouchablesDictionary[touchable.touchableType].Count == 0) {
					TouchablesDictionary.Remove (touchable.touchableType);
				}
			} else {
				if (col.GetComponent<TouchDetector> () == null) {
					if (GeneralObjects.Contains (col.gameObject)) {
						GeneralObjects.Remove (col.gameObject);
					}
				}
			}
		}
	}
}