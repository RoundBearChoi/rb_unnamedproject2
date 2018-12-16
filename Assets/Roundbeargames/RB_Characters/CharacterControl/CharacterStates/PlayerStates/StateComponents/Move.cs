using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveTransitionStates
{
    NONE,
    WALK,
    RUN,
    JUMP
}

namespace roundbeargames
{
    public class Move : StateComponent
    {
        public float AirMaxSpeed;
        public float AirSpeedGain;
        private float AirMomentumDecreaseMultiplier;
        private PathFinder pathFinder;

        public MoveTransitionStates GetMoveTransition()
        {
            if (characterData == null)
            {
                return MoveTransitionStates.NONE;
            }

            CharacterMovementData moveData = characterData.characterMovementData;

            if (moveData == null)
            {
                return MoveTransitionStates.NONE;
            }

            if (moveData.Jump)
            {
                return MoveTransitionStates.JUMP;
            }

            if (moveData.MoveForward)
            {
                if (moveData.Run)
                {
                    return MoveTransitionStates.RUN;
                }
                else
                {
                    return MoveTransitionStates.WALK;
                }
            }

            if (moveData.MoveBack)
            {
                if (moveData.Run)
                {
                    return MoveTransitionStates.RUN;
                }
                else
                {
                    return MoveTransitionStates.WALK;
                }
            }

            return MoveTransitionStates.NONE;
        }

        public float GetTurn()
        {
            if (controlMechanism == null)
            {
                return 0;
            }

            if (characterData == null)
            {
                return 0;
            }

            CharacterMovementData movementData = characterData.characterMovementData;
            Transform characterTransform = controlMechanism.transform;

            if (controlMechanism.IsFacingForward())
            {
                if (movementData.MoveBack && !movementData.MoveForward)
                {
                    return 180;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (!movementData.MoveBack && movementData.MoveForward)
                {
                    return 0;
                }
                else
                {
                    return 180;
                }
            }
        }

        public void MoveForward(float speed, float yAngle)
        {
            if (controlMechanism == null)
            {
                return;
            }

            Transform characterTransform = controlMechanism.transform;

            characterTransform.rotation = Quaternion.Euler(0, yAngle, 0);

            if (characterData.CanMoveThrough(TouchDetectorType.FRONT))
            {
                characterTransform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        public void SimpleMove(float Speed, bool goForward)
        {
            if (controlMechanism == null)
            {
                return;
            }

            Transform characterTransform = controlMechanism.transform;

            if (goForward)
            {
                if (characterData.CanMoveThrough(TouchDetectorType.FRONT))
                {
                    characterTransform.Translate(Vector3.right * Speed * Time.deltaTime);
                }
            }
            else
            {
                if (characterData.CanMoveThrough(TouchDetectorType.BACK))
                {
                    characterTransform.Translate(-Vector3.right * Speed * Time.deltaTime);
                }
            }
        }

        public void MoveWithoutTurning(float Speed, float turn)
        {
            if (controlMechanism == null)
            {
                return;
            }

            Transform characterTransform = controlMechanism.transform;

            if (characterData.characterMovementData.MoveForward || characterData.characterMovementData.MoveBack)
            {
                if (characterTransform.rotation.eulerAngles.y != turn)
                {
                    SimpleMove(Speed, false);
                }
                else
                {
                    SimpleMove(Speed, true);
                }
            }
        }

        public void AirMove()
        {
            if (controlMechanism == null)
            {
                return;
            }

            if (!characterData.CanMoveThrough(TouchDetectorType.FRONT))
            {
                return;
            }

            Transform characterTransform = controlMechanism.transform;

            if (!movementData.MoveForward && !movementData.MoveBack)
            {
                movementData.AirMomentum = Mathf.Lerp(movementData.AirMomentum, 0f, Time.deltaTime * AirSpeedGain * 0.2f);
            }
            else if (MoveDirectionMatches())
            {
                if (characterData.CanMoveThrough(TouchDetectorType.FRONT))
                {
                    if (movementData.AirMomentum >= AirMaxSpeed)
                    {
                        movementData.AirMomentum = Mathf.Lerp(movementData.AirMomentum, AirMaxSpeed, Time.deltaTime * AirSpeedGain * 0.5f);
                    }
                    else if (movementData.AirMomentum < (AirMaxSpeed))
                    {
                        movementData.AirMomentum += Time.deltaTime * AirSpeedGain;
                    }
                }
            }
            else if (!MoveDirectionMatches())
            {
                if (characterData.CanMoveThrough(TouchDetectorType.BACK))
                {
                    if (movementData.AirMomentum <= AirMaxSpeed * -1f)
                    {
                        movementData.AirMomentum = Mathf.Lerp(movementData.AirMomentum, AirMaxSpeed * -1f, Time.deltaTime * AirSpeedGain * 0.5f);
                    }
                    else if (movementData.AirMomentum > (AirMaxSpeed * -1f))
                    {
                        movementData.AirMomentum -= Time.deltaTime * AirSpeedGain * 0.5f;
                    }
                }
            }

            SimpleMove(movementData.AirMomentum, true);
        }

        public bool MoveDirectionMatches()
        {
            if (movementData.MoveForward && controlMechanism.IsFacingForward())
            {
                return true;
            }
            else if (movementData.MoveBack && !controlMechanism.IsFacingForward())
            {
                return true;
            }

            return false;
        }

        public List<WayPoint> FindClosestPathTo(WayPoint from, WayPoint to)
        {
            if (pathFinder == null)
            {
                pathFinder = GameObject.FindObjectOfType<PathFinder>();
            }
            return pathFinder.FindPath(from, to);
        }

        public bool IsGoingToLand()
        {
            if (controlMechanism.RIGIDBODY.velocity.y > 0.1f)
            {
                return false;
            }

            if (characterData.IsTouchingGeneralObject(TouchDetectorType.GROUND_ROLL))
            {
                movementData.IsGrounded = true;
                return true;
            }

            return false;
        }

        public bool CanInstantTurn(System.Type stateType)
        {
            if (stateType == typeof(PlayerRunningSlide) ||
                            stateType == typeof(PlayerJumpOver) ||
                            stateType == typeof(PlayerStandToRoll))
            {
                return false;
            }
            return true;
        }
    }
}