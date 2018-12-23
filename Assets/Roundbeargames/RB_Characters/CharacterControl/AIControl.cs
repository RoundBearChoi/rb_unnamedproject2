using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathFindMethod {
    NONE,
    WALK,
    TURN,
    JUMP,
}

namespace roundbeargames {
    public class AIControl : ControlMechanism {
        [SerializeField] float PlayerDistance;
        public int PathUpdateCount;
        public List<WayPoint> TargetPath;
        CharacterManager characterManager;
        Vector3 PlayerDir;
        //public bool IsFacingPath;

        void Awake () {
            FindCharacterStateController ();
        }

        void Update () {
            if (moveData == null) {
                moveData = characterStateController.characterData.characterMovementData;
            }
        }

        private void CheckPlayerDistance () {
            if (characterManager == null) {
                characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
            } else {
                PlayerDistance = Vector3.Distance (this.transform.position, characterManager.Player.transform.position);
            }
        }

        public bool PlayerIsDead () {
            if (characterManager == null) {
                characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
                return false;
            } else {
                if (characterManager.Player.characterStateController.CurrentState.GetType () == typeof (CharacterDeath)) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public bool PlayerIsOnSameGround () {
            if (characterManager == null) {
                characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
                return false;
            } else {
                if (characterManager.Player.characterStateController.characterData.characterMovementData.GroundName.Equals (moveData.GroundName)) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public bool IsDead () {
            if (characterStateController.CurrentState.GetType () == typeof (CharacterDeath)) {
                return true;
            } else {
                return false;
            }
        }

        public bool PlayerIsClose (float distance) {
            CheckPlayerDistance ();
            if (PlayerDistance < distance && PlayerDistance != 0f) {
                return true;
            } else {
                return false;
            }
        }

        public bool IsFacingPlayer () {
            if (characterManager == null) {
                return false;
            }

            PlayerDir = characterManager.Player.transform.position - this.transform.position;
            PlayerDir.Normalize ();

            if (this.transform.right.x * PlayerDir.x > 0f) {
                return true;
            } else {
                return false;
            }
        }

        public WayPoint GetLastPlayerWayPoint () {
            if (characterManager == null) {
                characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
            }

            return characterManager.Player.moveData.LastWayPoint;
        }

        public float GetRequiredJumpForce () {
            for (int i = TargetPath.Count - 1; i >= 0; i--) {
                if (TargetPath[i].pathFindMethod == PathFindMethod.JUMP) {
                    return TargetPath[i].RequiredJumpForce;
                }
            }
            return 400f;
        }

        public void FindPathToPlayer () {
            TargetPath.Clear ();
            if (moveData.LastWayPoint != null) {
                if (GetLastPlayerWayPoint () != null) {
                    List<WayPoint> newPath = characterStateController.CurrentState.move.FindClosestPathTo (moveData.LastWayPoint, GetLastPlayerWayPoint ());
                    foreach (WayPoint w in newPath) {
                        TargetPath.Add (w);
                    }
                }
            }
        }

        public void InitStartPath () {
            if (TargetPath == null) {
                return;
            }

            if (TargetPath.Count <= 1) {
                return;
            }

            WayPoint first = GetNextWayPoint ();
            WayPoint second = TargetPath[TargetPath.Count - 2];

            Vector3 pathDir = second.transform.position - first.transform.position;
            pathDir.Normalize ();

            Vector3 orientation = first.transform.position - this.transform.position;
            orientation.Normalize ();

            if (orientation.x * pathDir.x < 0f) {
                TargetPath.RemoveAt (TargetPath.Count - 1);
                InitStartPath ();
            }
        }

        public PathFindMethod GetPathFindMethod () {
            if (TargetPath == null) {
                return PathFindMethod.NONE;
            }

            if (TargetPath.Count == 0) {
                return PathFindMethod.NONE;
            }

            if (GetNextWayPoint ().pathFindMethod == PathFindMethod.JUMP) {
                if (!IsFacing (GetNextWayPoint ().transform.position)) {
                    return PathFindMethod.TURN;
                }

                if (!GetNextWayPoint ().GroundName.Equals (moveData.GroundName)) {
                    if (this.transform.position.y < GetNextWayPoint ().transform.position.y) {
                        return PathFindMethod.JUMP;
                    }
                }
            }

            if (!IsFacing (GetNextWayPoint ().transform.position)) {
                return PathFindMethod.TURN;

                /*if (moveData.GroundName.Equals(GetNextWayPoint().GroundName))
                {
                    return PathFindMethod.TURN;
                }
                else if (GetNextWayPoint().transform.position.y < this.transform.position.y)
                {
                    return PathFindMethod.TURN;
                }*/
            } else {
                return PathFindMethod.WALK;

                /*if (GetNextWayPoint().pathFindMethod == PathFindMethod.WALK)
                {
                    return PathFindMethod.WALK;
                }
                else
                {
                    if (GetNextWayPoint().GroundName.Equals(moveData.GroundName))
                    {
                        return PathFindMethod.WALK;
                    }
                }*/
            }

            return PathFindMethod.NONE;
        }

        public void UpdatePathStatus () {
            if (TargetPath.Count == 0) {
                return;
            }

            List<TouchDetector> listDet = characterStateController.characterData.GetTouchDetector (TouchDetectorType.WAY_POINT_DETECTOR);
            foreach (TouchDetector d in listDet) {
                if (d.TouchablesDictionary.ContainsKey (TouchableType.WAYPOINT)) {
                    foreach (Touchable touchable in d.TouchablesDictionary[TouchableType.WAYPOINT]) {
                        if (touchable.gameObject.GetComponent<WayPoint> () == TargetPath[TargetPath.Count - 1]) {
                            TargetPath.RemoveAt (TargetPath.Count - 1);
                            PathUpdateCount++;
                            //Debug.Log("updating path: " + PathUpdateCount.ToString());
                            return;
                        }
                    }
                }
            }
        }

        public WayPoint GetNextWayPoint () {
            if (TargetPath == null) {
                return null;
            }

            if (TargetPath.Count == 0) {
                return null;
            }

            return TargetPath[TargetPath.Count - 1];
        }
    }
}