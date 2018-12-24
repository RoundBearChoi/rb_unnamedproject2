using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerFollow : MonoBehaviour
    {
        CharacterManager characterManager;
        public Vector3 Offset;
        public float Speed;

        void FixedUpdate()
        {
            if (characterManager == null)
            {
                characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
            }
            else
            {
                this.transform.position = Vector3.Lerp(this.transform.position, characterManager.Player.transform.position + Offset, Time.deltaTime * Speed);
            }
        }
    }
}