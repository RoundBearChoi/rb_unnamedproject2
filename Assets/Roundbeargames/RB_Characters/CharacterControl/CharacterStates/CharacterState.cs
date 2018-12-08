using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    HumanoidIdle,
    HumanoidWalk,
    HumanoidRun,
    RunToStop,
    RunningTurn,
    JumpingUp,
    FallALoop,
    FallingToLanding,
    HangingIdle,
    BracedHangToCrouch,
    JustClimbed,
    CrouchIdle,
    Jab_R_1,
    SurpriseUppercut,
    RunningSlide,
    RunningJump,
    JumpOver,
    CrouchedSneakingLeft,
    StandToRoll,
}

public enum AxeEnemyState
{
    AxeIdle,
    AxeAttackDownward,
    StandingTurnToRight90,
    AxeWalkForward,
}

namespace roundbeargames
{
    public abstract class CharacterState : MonoBehaviour
    {
        public CharacterStateController characterStateController;
        public abstract void RunFrameUpdate();
        public abstract void RunFixedUpdate();
        public abstract void ClearState();
        public abstract void InitState();

        //Behaviors
        public Move move;
        public SlowDown slowDown;
        public Attack attack;
        public Jump jump;
        public TurnToPlayer turnToPlayer;
        public FindPlayer findPlayer;

        //Common Variables
        private ControlMechanism controlMechanism;
        private ManualControl manualControl;
        private AIControl aiControl;
        private CharacterManager characterManager;
        private VFXManager vfxManager;
        private CameraManager cameraManager;
        public float Duration;

        public bool DurationTimePassed()
        {
            if (!ANIMATION_DATA.IsAnimation())
            {
                return false;
            }

            if (Duration == 0f)
            {
                return false;
            }
            else if (ANIMATION_DATA.PlayTime >= Duration)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAnimation()
        {
            ANIMATION_DATA.UpdateData();
            if (!ANIMATION_DATA.IsAnimation())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateDeath()
        {
            if (characterStateController.characterData.hitRegister.RegisteredHits.Count == 0)
            {
                return;
            }

            if (characterStateController.NonKillable == false)
            {
                foreach (KeyValuePair<string, List<string>> data in characterStateController.characterData.hitRegister.RegisteredHits)
                {
                    characterStateController.DeathCause = data.Value[0];
                    characterStateController.DeathBringer = data.Key;
                    break;
                }

                characterStateController.ChangeState(999);
            }
        }

        public CharacterData CHARACTER_DATA
        {
            get { return characterStateController.characterData; }
        }

        public CharacterMovementData MOVEMENT_DATA
        {
            get { return characterStateController.characterData.characterMovementData; }
        }

        public CharacterAnimationData ANIMATION_DATA
        {
            get { return characterStateController.characterData.characterAnimationData; }
        }

        public CharacterAttackData ATTACK_DATA
        {
            get { return characterStateController.characterData.characterAttackData; }
        }

        public Transform CHARACTER_TRANSFORM
        {
            get { return characterStateController.controlMechanism.transform; }
        }

        public ControlMechanism CONTROL_MECHANISM
        {
            get
            {
                if (controlMechanism == null)
                {
                    controlMechanism = this.gameObject.GetComponentInParent<ControlMechanism>();
                }
                return controlMechanism;
            }
        }

        public ManualControl MANUAL_CONTROL
        {
            get
            {
                if (manualControl == null)
                {
                    manualControl = characterStateController.controlMechanism as ManualControl;
                }
                return manualControl;
            }
        }

        public AIControl AI_CONTROL
        {
            get
            {
                if (aiControl == null)
                {
                    aiControl = characterStateController.controlMechanism as AIControl;
                }
                return aiControl;
            }
        }

        public CharacterManager CHARACTER_MANAGER
        {
            get
            {
                if (characterManager == null)
                {
                    characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
                }
                return characterManager;
            }
        }

        public VFXManager VFX_MANAGER
        {
            get
            {
                if (vfxManager == null)
                {
                    vfxManager = ManagerGroup.Instance.GetManager(ManagerType.VFX_MANAGER) as VFXManager;
                }
                return vfxManager;
            }
        }

        public CameraManager CAMERA_MANAGER
        {
            get
            {
                if (cameraManager == null)
                {
                    cameraManager = ManagerGroup.Instance.GetManager(ManagerType.CAMERA_MANAGER) as CameraManager;
                }
                return cameraManager;
            }
        }
    }
}