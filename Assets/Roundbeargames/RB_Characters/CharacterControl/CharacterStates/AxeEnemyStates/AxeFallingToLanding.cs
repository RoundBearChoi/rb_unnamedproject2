using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class AxeFallingToLanding : CharacterState {
		public override void InitState () {
			AI_CONTROL.TargetPath.Clear ();
			ANIMATION_DATA.DesignatedAnimation = AxeEnemyState.AxeFallingToLanding.ToString ();
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				if (DurationTimePassed ()) {
					characterStateController.ChangeState ((int) AxeEnemyState.AxeIdle);
				}
			}
		}

		public override void ClearState () {

		}
	}
}