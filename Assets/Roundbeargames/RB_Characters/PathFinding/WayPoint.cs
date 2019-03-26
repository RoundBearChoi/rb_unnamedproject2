using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {
    public class WayPoint : MonoBehaviour {
        public PathFindMethod pathFindMethod;
        public float RequiredJumpForce;
        public float AirWalkSpeedMultiplier;
        public string GroundName;
        public List<WayPoint> Neighbors;
        public int KnownDistance = 1000;
        public WayPoint PreviousPoint;
        public PathFinder pathFinder;
        public Color PlayerColor;
        public Color EnemyColor;
        public Color DefaultColor;
        private Renderer renderer;

        [HorizontalGroup ("Split", 0.5f)]
        [Button (ButtonSizes.Large), GUIColor (0.4f, 0.8f, 1)]
        public void CreateWayPointLeft () {
            CreateWayPointPrefab (new Vector3 (-0.6f, 0f, 0f));
        }

        [VerticalGroup ("Split/right")]
        [Button (ButtonSizes.Large), GUIColor (0, 1, 0)]
        public void CreateWayPointRight () {
            CreateWayPointPrefab (new Vector3 (0.6f, 0f, 0f));
        }

        void Start () {
            renderer = GetComponent<Renderer> ();
            TurnOnDefaultColor ();
        }

        void Update () {
            if (pathFinder == null) {
                pathFinder = GameObject.FindObjectOfType<PathFinder> ();
            }

            if (pathFinder.ShowDebugRaycast) {
                foreach (WayPoint w in Neighbors) {
                    Vector3 line = (w.transform.position + pathFinder.GreenLineOffset) - (this.transform.position + pathFinder.GreenLineOffset);

                    if (w.transform.position.x > this.transform.position.x) {
                        Debug.DrawRay (this.transform.position + pathFinder.GreenLineOffset, line, Color.green);
                    } else if (w.transform.position.x < this.transform.position.x) {
                        Debug.DrawRay (this.transform.position + pathFinder.GreenLineOffset * 0.1f, line, Color.blue);
                    }
                }
            }
        }

        void CreateWayPointPrefab (Vector3 offset) {
            if (pathFinder == null) {
                pathFinder = this.gameObject.GetComponentInParent<PathFinder> ();
            }

            string latestName = pathFinder.GetLatestWayPoint ().gameObject.name;
            int latestNum = int.Parse (latestName.Replace ("point - ", ""));

            GameObject w = GameObject.Instantiate (pathFinder.WayPointPrefab) as GameObject;
            w.transform.parent = this.transform.parent.transform;
            w.name = "point";
            w.transform.position = this.transform.position + offset;

            WayPoint wp = w.GetComponent<WayPoint> ();
            wp.pathFindMethod = PathFindMethod.WALK;
            wp.Neighbors = new List<WayPoint> ();
            wp.Neighbors.Add (this);
            wp.GroundName = GroundName;

            //Debug.Log("latest name: " + latestName);
            //Debug.Log("latest num: " + latestNum.ToString());

            wp.gameObject.name = "point - " + (latestNum + 1).ToString ();

            Neighbors.Add (wp);
        }

        public void CalcNeighborDistance () {
            //if (this.gameObject.name.Contains ("48") || this.gameObject.name.Contains ("49")) {
            //    Debug.Log ("checking");
            //}
            foreach (WayPoint n in Neighbors) {
                int distance = 1;
                if (n.pathFindMethod == PathFindMethod.JUMP) {
                    if (n.GroundName != GroundName) {
                        distance = 10;
                    }
                }
                //Debug.Log ("calculating " + this.gameObject.name + " to " + n.gameObject.name);
                if (KnownDistance + distance < n.KnownDistance) {
                    //if (n.gameObject.name.Contains ("48") || n.gameObject.name.Contains ("49")) {
                    //    Debug.Log ("checking");
                    //}
                    n.PreviousPoint = this;
                    n.KnownDistance = KnownDistance + distance;
                    n.CalcNeighborDistance ();
                }
            }
        }

        void OnTriggerEnter (Collider col) {
            TouchDetector det = col.GetComponent<TouchDetector> ();
            if (det != null) {
                if (det.touchDetectorType == TouchDetectorType.WAY_POINT_DETECTOR) {
                    if (det.controlMechanism.moveData != null) {
                        det.controlMechanism.moveData.LastWayPoint = this;
                        if (det.controlMechanism.controlType == ControlType.PLAYER) {
                            TurnOnPlayerColor ();
                        } else if (det.controlMechanism.controlType == ControlType.ENEMY) {
                            TurnOnEnemyColor ();
                        }
                    }
                }
            }
        }

        void OnTriggerExit (Collider col) {
            TouchDetector det = col.GetComponent<TouchDetector> ();
            if (det != null) {
                if (det.touchDetectorType == TouchDetectorType.WAY_POINT_DETECTOR) {
                    TurnOnDefaultColor ();
                    //if (det.controlMechanism.controlType == ControlType.PLAYER) {
                    //Debug.Log ("exited: " + this.gameObject.name);
                    //}
                }
            }
        }

        public void TurnOnPlayerColor () {
            renderer.material.SetColor ("_Color", PlayerColor);
        }

        public void TurnOnEnemyColor () {
            renderer.material.SetColor ("_Color", EnemyColor);
        }

        public void TurnOnDefaultColor () {
            renderer.material.SetColor ("_Color", DefaultColor);
        }
    }
}