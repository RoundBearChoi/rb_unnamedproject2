using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerJustClimbed : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.JustClimbed.ToString ();
			CHARACTER_TRANSFORM.Translate (ClimbDistance);
			CONTROL_MECHANISM.RIGIDBODY.useGravity = true;
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				characterStateController.ChangeState ((int) PlayerState.CrouchIdle);
			}
		}

		public override void ClearState () {

		}

		public Vector3 ClimbDistance;
	}
}