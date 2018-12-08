using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class CharacterDeath : CharacterState {
		string ParameterString = "DeathAnimationIndex";

		public override void InitState () {
			ANIMATION_DATA.characterAnimator.runtimeAnimatorController = characterStateController.DeathAnimator;

			if (characterStateController.DeathCause.Contains ("Jab")) {
				ANIMATION_DATA.characterAnimator.SetFloat (ParameterString, 0f);
			} else if (characterStateController.DeathCause.Contains ("Uppercut")) {
				CAMERA_MANAGER.ShakeCamera (0.4f);
				Transform rightHand = CHARACTER_MANAGER.Player.BodyPartDictionary[BodyPart.RIGHT_HAND];
				VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.SPARK, rightHand.position);
				VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.FLARE, rightHand.position);
				VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.BLOOD, rightHand.position);
				VFX_MANAGER.ShowSimpleEffect (SimpleEffectType.DISTORTION, rightHand.position);

				ANIMATION_DATA.characterAnimator.SetFloat (ParameterString, 1f);
			} else if (characterStateController.DeathCause.Contains ("Axe")) {
				ANIMATION_DATA.characterAnimator.SetFloat (ParameterString, 2f);
			}
		}

		public override void RunFixedUpdate () {

		}

		public override void RunFrameUpdate () {

		}

		public override void ClearState () {

		}
	}
}