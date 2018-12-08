using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class AIControl : ControlMechanism
    {
        [SerializeField] float PlayerDistance;
        public List<WayPoint> TargetPath;
        CharacterManager characterManager;
        Vector3 PlayerDir;

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
        }

        private void CheckPlayerDistance()
        {
            if (characterManager == null)
            {
                characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
            }
            else
            {
                PlayerDistance = Vector3.Distance(this.transform.position, characterManager.Player.transform.position);
            }
        }

        public bool PlayerIsDead()
        {
            if (characterManager == null)
            {
                characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
                return false;
            }
            else
            {
                if (characterManager.Player.characterStateController.CurrentState.GetType() == typeof(CharacterDeath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool PlayerIsClose(float distance)
        {
            CheckPlayerDistance();
            if (PlayerDistance < distance && PlayerDistance != 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFacingPlayer()
        {
            if (characterManager == null)
            {
                return false;
            }

            PlayerDir = characterManager.Player.transform.position - this.transform.position;
            PlayerDir.Normalize();

            if (this.transform.right.x * PlayerDir.x > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public WayPoint GetLastPlayerWayPoint()
        {
            if (characterManager == null)
            {
                characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
            }

            return characterManager.Player.moveData.LastWayPoint;
        }

        public void FindPathToPlayer()
        {
            TargetPath.Clear();
            if (moveData.LastWayPoint != null)
            {
                if (GetLastPlayerWayPoint() != null)
                {
                    List<WayPoint> newPath = characterStateController.CurrentState.move.FindClosestPathTo(moveData.LastWayPoint, GetLastPlayerWayPoint());
                    foreach (WayPoint w in newPath)
                    {
                        TargetPath.Add(w);
                    }
                }
            }
        }
    }
}