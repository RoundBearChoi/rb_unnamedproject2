using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class PlayerAnimationData : CharacterAnimationData {
		public override void StartAnimation (int animationIndex) {
			characterAnimator.SetInteger (PlayerStateIndexHash, animationIndex);
		}

		int PlayerStateIndexHash = Animator.StringToHash ("PlayerStateIndex");
	}
}