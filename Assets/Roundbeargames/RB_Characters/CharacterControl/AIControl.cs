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
        public List<WayPoint> TargetPath;
        CharacterManager characterManager;
        Vector3 PlayerDir;
        public bool IsFacingPath;

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

        public void UpdateStartPath () {
            if (TargetPath.Count == 0) {
                return;
            }

            if (TargetPath.Count == 1) {
                if (IsFacing (TargetPath[TargetPath.Count - 1].transform.position)) {
                    IsFacingPath = true;
                } else {
                    IsFacingPath = false;
                }
                return;
            }

            WayPoint first = TargetPath[TargetPath.Count - 1];
            WayPoint second = TargetPath[TargetPath.Count - 2];

            Vector3 pathDir = second.transform.position - first.transform.position;
            pathDir.Normalize ();

            //Debug.Log("first waypoint dir: " + pathDir);

            Vector3 orientation = first.transform.position - this.transform.position;
            orientation.Normalize ();

            //Debug.Log("orientation: " + orientation);

            if (orientation.x * pathDir.x < 0f) {
                if (IsFacing (TargetPath[TargetPath.Count - 2].transform.position)) {
                    IsFacingPath = true;
                } else {
                    IsFacingPath = false;
                }

                TargetPath.RemoveAt (TargetPath.Count - 1);
            } else {
                if (IsFacing (TargetPath[TargetPath.Count - 1].transform.position)) {
                    IsFacingPath = true;
                } else {
                    IsFacingPath = false;
                }
            }
        }

        public PathFindMethod GetPathFindMethod () {
            if (TargetPath == null) {
                return PathFindMethod.NONE;
            }

            if (TargetPath.Count == 0) {
                return PathFindMethod.NONE;
            }

            if (TargetPath[TargetPath.Count - 1].pathFindMethod == PathFindMethod.JUMP) {
                return PathFindMethod.JUMP;
            }

            if (!IsFacingPath) {
                return PathFindMethod.TURN;
            } else {
                if (TargetPath[TargetPath.Count - 1].pathFindMethod == PathFindMethod.WALK) {
                    return PathFindMethod.WALK;
                }
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
                            return;
                        }
                    }
                }
            }
        }
    }
}