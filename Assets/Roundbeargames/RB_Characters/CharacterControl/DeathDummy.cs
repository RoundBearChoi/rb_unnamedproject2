using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class DeathDummy : MonoBehaviour {
		public float DeathAnimationIndex;
		public Animator animator;
		public float PlayTime;
		public float Duration;

		void Awake () {
			PlayTime = 0f;
			animator.SetFloat ("DeathAnimationIndex", DeathAnimationIndex);
		}

		void Update () {
			PlayTime += Time.deltaTime;

			if (Duration <= PlayTime) {
				PlayTime = 0f;
				animator.gameObject.SetActive (false);
				animator.gameObject.SetActive (true);
				animator.SetFloat ("DeathAnimationIndex", DeathAnimationIndex);
				animator.Play ("DeathTree");
			}
		}
	}
}