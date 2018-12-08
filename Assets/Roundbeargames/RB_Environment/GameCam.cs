using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class GameCam : MonoBehaviour {
		public PlayerFollow playerFollow;
		public PlayerFocus PlayerFocus;
		CharacterManager characterManager;
		public Vector3 DistanceFromPlayer;
		public float Speed;

		void FixedUpdate () {
			if (characterManager == null) {
				characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
			} else {
				this.transform.position = Vector3.Lerp (this.transform.position, characterManager.Player.transform.position + DistanceFromPlayer, Time.deltaTime * Speed);
				this.transform.LookAt (playerFollow.transform.position);
			}
		}
	}
}