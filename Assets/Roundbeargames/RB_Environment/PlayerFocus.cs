using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerFocus : MonoBehaviour {
		[SerializeField] string PlayerNeckObjectName;
		[SerializeField] Transform PlayerNeck;
		CharacterManager characterManager;

		void FixedUpdate () {
			if (characterManager == null) {
				characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
			} else {
				if (PlayerNeck == null) {
					PlayerNeck = FindInHierarchy (characterManager.Player.transform, PlayerNeckObjectName);
				} else {
					this.transform.position = PlayerNeck.position;
				}
			}
		}

		public Transform FindInHierarchy (Transform root, string name) {
			foreach (Transform child in root) {
				//Debug.Log (child.gameObject.name);
				if (child.gameObject.name == name) {
					return child;
				}

				Transform result = FindInHierarchy (child, name);
				if (result != null) {
					return result;
				}
			}

			return null;
		}
	}
}