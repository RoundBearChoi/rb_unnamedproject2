using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public abstract class StateComponent : MonoBehaviour {
		public ControlMechanism controlMechanism;
		public CharacterStateController characterStateController;
		public CharacterState characterState;
		public CharacterData characterData;
		public CharacterMovementData movementData;
		public VFXManager vfxManager;

		IEnumerator Start () {
			yield return new WaitForEndOfFrame ();
			controlMechanism = this.gameObject.GetComponentInParent<ControlMechanism> ();
			characterStateController = this.gameObject.GetComponentInParent<CharacterStateController> ();
			characterState = this.gameObject.GetComponentInParent<CharacterState> ();
			characterData = characterStateController.characterData;
			movementData = characterData.characterMovementData;

			vfxManager = ManagerGroup.Instance.GetManager (ManagerType.VFX_MANAGER) as VFXManager;
		}
	}

}