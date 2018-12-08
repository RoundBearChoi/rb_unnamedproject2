using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class ManualControl : ControlMechanism
    {
        InputDeviceManager inputDeviceManager;
        VirtualInput virtualInput;
        //public Dictionary<string, int> CollidedObjects;
        public float FallMultiplier;
        public float LowJumpMultiplier;

        void Awake()
        {
            FindCharacterStateController();
        }

        void Update()
        {
            if (moveData == null)
            {
                moveData = characterStateController.characterData.characterMovementData;
            }
            else
            {
                UpdateControl();
            }

            if (attackData == null)
            {
                attackData = characterStateController.characterData.characterAttackData;
            }

            CenterPosition();
        }

        void FixedUpdate()
        {
            if (characterStateController.CurrentState.GetType() != typeof(PlayerJumpOver))
            {
                if (RIGIDBODY.velocity.y < 0f)
                {
                    RIGIDBODY.velocity += Vector3.up * Physics.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
                }
                else if (RIGIDBODY.velocity.y > 0f && moveData.Jump == false)
                {
                    RIGIDBODY.velocity += Vector3.up * Physics.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
                    //Debug.Log ("short jump");
                }
            }
        }

        void FindInputDevice()
        {
            inputDeviceManager = ManagerGroup.Instance.GetManager(ManagerType.INPUT_DEVICE_MANAGER) as InputDeviceManager;
            virtualInput = inputDeviceManager.VIRTUAL_INPUT;
        }

        void UpdateControl()
        {
            if (virtualInput == null)
            {
                FindInputDevice();
            }

            if (virtualInput.ToggleMoveForward)
            {
                moveData.MoveForward = true;
            }
            else
            {
                moveData.MoveForward = false;
            }
            if (virtualInput.ToggleMoveBack)
            {
                moveData.MoveBack = true;
            }
            else
            {
                moveData.MoveBack = false;
            }
            if (virtualInput.ToggleMoveUp)
            {
                moveData.MoveUp = true;
            }
            else
            {
                moveData.MoveUp = false;
            }
            if (virtualInput.ToggleMoveDown)
            {
                moveData.MoveDown = true;
            }
            else
            {
                moveData.MoveDown = false;
            }
            if (virtualInput.ToggleRun)
            {
                moveData.Run = true;
            }
            else
            {
                moveData.Run = false;
            }
            if (virtualInput.Jump)
            {
                moveData.Jump = true;
            }
            else
            {
                moveData.Jump = false;
            }
            if (virtualInput.AttackA)
            {
                attackData.AttackA = true;
            }
            else
            {
                attackData.AttackA = false;
            }
        }
    }
}