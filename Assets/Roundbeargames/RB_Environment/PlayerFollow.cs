using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {
    public enum PlayerFollowType {
        DEFAULT,
        FOCUS_ON_HEAD,
    }
    public class PlayerFollow : SerializedMonoBehaviour {
        CharacterManager characterManager;
        public Vector3 FollowOffset;
        public PlayerFollowType CurrentFollowType;
        public float Speed;
        public Dictionary<PlayerFollowType, Vector3> FollowDictionary;

        void FixedUpdate () {
            if (characterManager == null) {
                characterManager = ManagerGroup.Instance.GetManager (ManagerType.CHARACTER_MANAGER) as CharacterManager;
            } else {
                this.transform.position = Vector3.Lerp (this.transform.position, characterManager.Player.transform.position + FollowOffset, Time.deltaTime * Speed);
            }
        }

        public void SetFollow (PlayerFollowType offsetType) {
            FollowOffset = FollowDictionary[offsetType];
            CurrentFollowType = offsetType;
        }
    }
}