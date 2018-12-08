using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerSurpriseUppercut : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.SurpriseUppercut.ToString ();
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {
				//Debug.Log (ANIMATION_DATA.PlayTime);
				if (ANIMATION_DATA.PlayTime >= 0.34f) {

				}

				if (DurationTimePassed ()) {
					attack.DeRegister (characterStateController.controlMechanism.gameObject.name, PlayerState.SurpriseUppercut.ToString ());
					characterStateController.ChangeState ((int) PlayerState.HumanoidIdle);
				}

				attack.UpdateHit ();
			}
		}

		public override void ClearState () {

		}
	}
}