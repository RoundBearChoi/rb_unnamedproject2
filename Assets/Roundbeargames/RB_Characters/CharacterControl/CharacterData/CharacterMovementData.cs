using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class CharacterMovementData : MonoBehaviour {
		public bool MoveForward = false;
		public bool MoveBack = false;
		public bool MoveUp = false;
		public bool MoveDown = false;
		public bool Run = false;
		public bool Jump = false;
		public bool IsJumped = false;
		public float Turn;
		public float WalkSpeed;
		public float CrouchSpeed;
		public float AirMomentum;
		public float RunSpeed;
		public bool IsGrounded;
		public string GroundName;
		public WayPoint LastWayPoint;
	}
}