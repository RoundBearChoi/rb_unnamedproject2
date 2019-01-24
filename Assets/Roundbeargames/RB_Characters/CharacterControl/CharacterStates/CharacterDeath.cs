using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class CharacterDeath : CharacterState
    {
        public override void InitState()
        {
            SetDefaultLocalPos();
            ANIMATION_DATA.characterAnimator.runtimeAnimatorController = characterStateController.DeathAnimator;
            CONTROL_MECHANISM.RIGIDBODY.useGravity = true;
            SpinKickReactionTriggered = false;

            if (characterStateController.DeathCause.Contains("Jab"))
            {
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 0f);
            }
            else if (characterStateController.DeathCause.Contains("Uppercut"))
            {
                CONTROL_MECHANISM.ClearVelocity();
                CONTROL_MECHANISM.RIGIDBODY.AddForce(Vector3.up * 300f);

                ShowHitEffect(BodyPart.RIGHT_HAND);
                CAMERA_MANAGER.ShakeCamera(0.4f);

                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 1f);
            }
            else if (characterStateController.DeathCause.Contains("Axe"))
            {
                CAMERA_MANAGER.gameCam.SetOffset(CameraOffsetType.ZOOM_ON_PLAYER_DEATH_RIGHT_SIDE);
                Time.timeScale = 0.35f;
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 2f);
            }
            else if (characterStateController.DeathCause.Contains("Collateral"))
            {
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 0f);
            }
            else if (characterStateController.DeathCause.Contains("RunningKick"))
            {
                ShowHitEffect(BodyPart.RIGHT_FOOT);
                CAMERA_MANAGER.ShakeCamera(0.4f);
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 4f);
            }
            else
            {
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 2f);
            }

            if (collateral != null)
            {
                collateral.CollateralAIs.Clear();
            }
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (collateral != null)
            {
                if (collateral.DetectionRoutine != null)
                {
                    if (collateral.TriggerCollateralDamage())
                    {
                        //Debug.Log("has triggered collateral death");
                    }
                }
            }
        }

        public override void RunLateUpdate()
        {

        }

        public override void ClearState()
        {

        }

        string ParameterString = "DeathAnimationIndex";
        bool SpinKickReactionTriggered;
        bool DefaultLocalPosIsSet = false;
        Vector3 DefaultLocalPos;

        public void SetDefaultLocalPos()
        {
            if (!DefaultLocalPosIsSet)
            {
                DefaultLocalPos = ANIMATION_DATA.characterAnimator.transform.localPosition;
                DefaultLocalPosIsSet = true;
            }
        }

        public void Revive()
        {
            ANIMATION_DATA.characterAnimator.transform.localPosition = DefaultLocalPos;
            ANIMATION_DATA.characterAnimator.applyRootMotion = false;

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

        public void ProcSpinKickReaction()
        {
            if (!SpinKickReactionTriggered)
            {
                //Debug.Log("spinkick reaction triggered");
                ShowHitEffect(BodyPart.RIGHT_FOOT);
                CAMERA_MANAGER.ShakeCamera(0.4f);

                SpinKickReactionTriggered = true;
                ANIMATION_DATA.characterAnimator.applyRootMotion = true;
                ANIMATION_DATA.characterAnimator.runtimeAnimatorController = null;
                ANIMATION_DATA.characterAnimator.runtimeAnimatorController = characterStateController.DeathAnimator;
                ANIMATION_DATA.characterAnimator.SetFloat(ParameterString, 3f);

                collateral.StartDetectionTimer(0.5f);
                //Calculating = true;
            }
        }

        //bool Calculating = false;
        //float CollateralTime = 0f;

        void ShowHitEffect(BodyPart bodypart)
        {
            Transform part = CHARACTER_MANAGER.Player.BodyPartDictionary[bodypart];
            VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.SPARK, part.position);
            VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.FLARE, part.position);
            VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.BLOOD, part.position);
            VFX_MANAGER.ShowSimpleEffect(SimpleEffectType.DISTORTION, part.position);
        }
    }
}