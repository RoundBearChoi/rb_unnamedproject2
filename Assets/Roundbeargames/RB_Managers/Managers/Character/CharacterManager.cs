using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
    public class CharacterManager : Manager {
        public List<ControlMechanism> ListEnemies;
        public ControlMechanism Player;

        void Start () {
            FindExistingCharacters ();
        }

        void FindExistingCharacters () {
            ControlMechanism[] controlMechanisms = GameObject.FindObjectsOfType<ControlMechanism> ();
            foreach (ControlMechanism c in controlMechanisms) {
                if (c.controlType == ControlType.ENEMY) {
                    ListEnemies.Add (c);
                } else if (c.controlType == ControlType.PLAYER) {
                    Player = c;
                }
            }
        }

        public void Deregister (string hitter, string move) {
            foreach (ControlMechanism c in ListEnemies) {
                c.characterStateController.characterData.hitRegister.DeRegister (hitter, move);
            }
            Player.characterStateController.characterData.hitRegister.DeRegister (hitter, move);
        }
    }
}