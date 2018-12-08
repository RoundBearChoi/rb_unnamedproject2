using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class CharacterDataComponent : MonoBehaviour {

		public CharacterStateController CHARACTER_STATE_CONTROLLER {
			get {
				if (characterStateController == null) {
					characterStateController = this.gameObject.GetComponentInParent<CharacterStateController> ();
				}
				return characterStateController;
			}
		}

		private CharacterStateController characterStateController;
	}

}