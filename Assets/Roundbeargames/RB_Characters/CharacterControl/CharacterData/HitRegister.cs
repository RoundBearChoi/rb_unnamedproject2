using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {
	public class HitRegister : SerializedMonoBehaviour {
		void Start () {
			RegisteredHits.Clear ();
		}

		public Dictionary<string, List<string>> RegisteredHits;

		public bool Register (string hitter, string move) {
			if (RegisteredHits.ContainsKey (hitter)) {
				if (RegisteredHits[hitter].Contains (move)) {
					return false;
				} else {
					RegisteredHits[hitter].Add (move);
					return true;
				}
			} else {
				List<string> moves = new List<string> ();
				moves.Add (move);
				RegisteredHits.Add (hitter, moves);
				return true;
			}
		}

		public void DeRegister (string hitter, string move) {
			if (RegisteredHits.ContainsKey (hitter)) {
				if (RegisteredHits[hitter].Contains (move)) {
					RegisteredHits[hitter].Remove (move);
				}
			}
		}

		public bool IsHit () {
			if (RegisteredHits.Count > 0) {
				return true;
			} else {
				return false;
			}
		}
	}
}