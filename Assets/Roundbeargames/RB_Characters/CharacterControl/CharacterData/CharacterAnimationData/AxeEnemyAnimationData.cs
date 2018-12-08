using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class AxeEnemyAnimationData : CharacterAnimationData {
		public override void StartAnimation (int animationIndex) {
			characterAnimator.SetInteger (AxeEnemyStateIndex, animationIndex);
		}

		int AxeEnemyStateIndex = Animator.StringToHash ("AxeEnemyStateIndex");
	}
}