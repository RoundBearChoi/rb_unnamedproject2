using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class VirtualInput : MonoBehaviour {
        public bool ToggleMoveForward = false;
        public bool ToggleMoveBack = false;
        public bool ToggleMoveUp = false;
        public bool ToggleMoveDown = false;
        public bool ToggleRun = false;
        public bool Jump = false;
        public bool AttackA = false;

        void Update () {
            if (Input.GetKey ("a")) {
                ToggleMoveBack = true;
            } else {
                ToggleMoveBack = false;
            }

            if (Input.GetKey ("d")) {
                ToggleMoveForward = true;
            } else {
                ToggleMoveForward = false;
            }

            if (Input.GetKey ("w")) {
                ToggleMoveUp = true;
            } else {
                ToggleMoveUp = false;
            }

            if (Input.GetKey ("s")) {
                ToggleMoveDown = true;
            } else {
                ToggleMoveDown = false;
            }

            if (Input.GetKey ("left shift") || Input.GetKey ("right shift")) {
                ToggleRun = true;
            } else if (!Input.GetKey ("left shift") || !Input.GetKey ("right shift")) {
                ToggleRun = false;
            }

            if (Input.GetKey ("space")) {
                Jump = true;
            } else {
                Jump = false;
            }

            if (Input.GetKey ("return")) {
                AttackA = true;
            } else {
                AttackA = false;
            }
        }
    }
}