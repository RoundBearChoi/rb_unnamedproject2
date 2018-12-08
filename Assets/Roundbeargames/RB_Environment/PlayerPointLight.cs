using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerPointLight : MonoBehaviour {
		CharacterManager characterManager;

		[SerializeField]
		Vector3 Offset;

		void FixedUpdate () {
			if (characterManager == null) {
				characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
			} else {
				this.transform.position = characterManager.Player.transform.position + Offset;
			}
		}
	}
}