using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public abstract class CharacterAnimationData : MonoBehaviour {
		public abstract void StartAnimation (int animationIndex);
		public Animator characterAnimator;
		public string DesignatedAnimation;
		public float PlayTime;

		protected bool IsInTransition;
		protected AnimatorStateInfo animatorStateInfo;
		protected AnimatorClipInfo[] animatorClipInfo;
		public bool AnimationNameMatches = false;

		public bool IsAnimation () {
			if (animatorClipInfo[0].clip.name.Equals (DesignatedAnimation)) {
				AnimationNameMatches = true;
			} else {
				AnimationNameMatches = false;
			}
			return AnimationNameMatches;
		}

		public void StopAnimation () {
			characterAnimator.speed = 0f;
		}

		public void PlayAnimation () {
			characterAnimator.speed = 1f;
		}

		public bool AnimationIsPlaying () {
			if (characterAnimator.speed != 1f) {
				return false;
			} else {
				return true;
			}
		}

		public void UpdateData () {
			animatorStateInfo = characterAnimator.GetCurrentAnimatorStateInfo (0);
			animatorClipInfo = characterAnimator.GetCurrentAnimatorClipInfo (0);

			if (IsAnimation ()) {
				PlayTime = animatorClipInfo[0].clip.length * animatorStateInfo.normalizedTime;
			}
		}
	}
}