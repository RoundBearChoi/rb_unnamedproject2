﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerFallingIdle : CharacterState {
		public override void InitState () {
			ANIMATION_DATA.DesignatedAnimation = PlayerState.FallALoop.ToString ();
		}

		public override void RunFixedUpdate () {
			MOVEMENT_DATA.Turn = move.GetTurn ();
			move.AirMove ();

			if (MOVEMENT_DATA.IsGrounded) {
				CONTROL_MECHANISM.ClearVelocity ();
				characterStateController.ChangeState ((int) PlayerState.FallingToLanding);
			}

			if (ANIMATION_DATA.AnimationNameMatches) {

				if (jump.IsGrabbingLedge ()) {
					characterStateController.ChangeState ((int) PlayerState.HangingIdle);
					CONTROL_MECHANISM.RIGIDBODY.velocity = Vector3.zero;
					CONTROL_MECHANISM.RIGIDBODY.angularVelocity = Vector3.zero;
					CONTROL_MECHANISM.RIGIDBODY.useGravity = false;
				}
			}
		}

		public override void RunFrameUpdate () {
			if (UpdateAnimation ()) {

			}
		}

		private bool RollAfterRunningJump () {
			if (characterStateController.PrevState.GetType () != typeof (PlayerRunningJump)) {
				return false;
			}

			switch (move.GetMoveTransition ()) {
				case MoveTransitionStates.JUMP:
					if (MOVEMENT_DATA.MoveForward || MOVEMENT_DATA.MoveBack) {
						characterStateController.ChangeState ((int) PlayerState.StandToRoll);
						return true;
					}
					break;
				case MoveTransitionStates.NONE:
					break;
				case MoveTransitionStates.RUN:
				case MoveTransitionStates.WALK:
					characterStateController.ChangeState ((int) PlayerState.StandToRoll);
					return true;
			}

			return false;
		}

		public override void ClearState () {
			if (characterStateController.PrevState.GetType () != typeof (PlayerRunningJump)) {
				MOVEMENT_DATA.AirMomentum = 0f;
			}
		}
	}
}