using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class CharacterDeath : CharacterState
    {
        string ParameterString = "DeathAnimationIndex";

        public override void InitState()
        {
            ANIMATION_DATA.characterAnimator.runtimeAnimatorController = characterStateController.DeathAnimator;
            CONTROL_MECHANISM.RIGIDBODY.useGravity = true;

            if (characterStateController.DeathCause.Contains("Jab"))
            {
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 0f);
            }
            else if (characterStateController.DeathCause.Contains("Uppercut"))
            {
                CONTROL_MECHANISM.ClearVelocity();
                CONTROL_MECHANISM.RIGIDBODY.AddForce(Vector3.up * 250f);

                CAMERA_MANAGER.ShakeCamera(0.4f);
                Transform rightHand = CHARACTER_MANAGER.Player.BodyPartDictionary[BodyPart.RIGHT_HAND];
                VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.SPARK, rightHand.position);
                VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.FLARE, rightHand.position);
                VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.BLOOD, rightHand.position);
                VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.DISTORTION, rightHand.position);

                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 1f);
            }
            else if (characterStateController.DeathCause.Contains("Axe"))
            {
                CAMERA_MANAGER.gameCam.SetOffset(CameraOffsetType.ZOOM_ON_PLAYER_DEATH_RIGHT_SIDE);
                Time.timeScale = 0.35f;
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 2f);
            }
            else
            {
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 2f);
            }
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {

        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        public void Revive()
        {
            ANIMATION_DATA.characterAnimator.runtimeAnimatorController = characterStateController.OriginalAnimator;
            CONTROL_MECHANISM.RIGIDBODY.useGravity = true;

            if (CONTROL_MECHANISM.controlType == ControlType.ENEMY)
            {
                characterStateController.DeathCause = string.Empty;
                characterStateController.DeathBringer = string.Empty;
                characterStateController.characterData.hitRegister.RegisteredHits.Clear();
                characterStateController.ChangeState((int)AxeEnemyState.AxeIdle);
            }
        }
    }
}