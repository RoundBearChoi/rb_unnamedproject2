using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DynamicColliderType {
	DEFAULT,
	JumpOver,
}

namespace roundbeargames {
	public class ColliderController : MonoBehaviour {
		public Animator ColliderAnimator;

		void Awake () {
			ColliderAnimator = this.gameObject.GetComponent<Animator> ();
		}

		public void TriggerDynamicCollider (DynamicColliderType type) {
			ColliderAnimator.SetInteger ("DynamicColliderIndex", (int) type);
		}

		public DynamicColliderType GetCurrentDynamicColliderType () {
			int integer = ColliderAnimator.GetInteger ("DynamicColliderIndex");
			return (DynamicColliderType) integer;
		}
	}
}