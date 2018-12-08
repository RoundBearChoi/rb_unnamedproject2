using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class WayPoint : MonoBehaviour {
		public string GroundName;
		public List<WayPoint> Neighbors;
		public int KnownDistance = 1000;
		public WayPoint PreviousPoint;
		public PathFinder pathFinder;
		public Color PlayerColor;
		public Color EnemyColor;
		public Color DefaultColor;
		private Renderer renderer;

		void Start () {
			renderer = GetComponent<Renderer> ();
			TurnOnDefaultColor ();
		}

		public void CalcNeighborDistance () {
			foreach (WayPoint n in Neighbors) {
				//Debug.Log ("calculating " + this.gameObject.name + " to " + n.gameObject.name);
				if (KnownDistance + 1 < n.KnownDistance) {
					n.PreviousPoint = this;
					n.KnownDistance = KnownDistance + 1;
					//Debug.Log ("new path found! to " + n.gameObject.name);
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