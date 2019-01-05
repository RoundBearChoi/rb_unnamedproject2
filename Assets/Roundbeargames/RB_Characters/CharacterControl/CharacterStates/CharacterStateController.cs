using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames
{
    public class CharacterStateController : SerializedMonoBehaviour
    {
        public Dictionary<int, CharacterState> StatesDictionary;
        public bool NonKillable;
        public CharacterState CurrentState;
        public CharacterState PrevState;
        public CharacterData characterData;
        public ControlMechanism controlMechanism;
        public RuntimeAnimatorController OriginalAnimator;
        public RuntimeAnimatorController DeathAnimator;
        public string DeathCause;
        public string DeathBringer;

        void Start()
        {
            foreach (KeyValuePair<int, CharacterState> d in StatesDictionary)
            {
                d.Value.characterStateController = this;
            }

            CurrentState = StatesDictionary[0];
            characterData = this.gameObject.GetComponentInChildren<CharacterData>();

            characterData.FindDatas();
            CurrentState.InitState();
        }

        void FixedUpdate()
        {
            CurrentState.RunFixedUpdate();
        }

        void Update()
        {
            if (CurrentState.GetType() != typeof(CharacterDeath))
            {
                CurrentState.UpdateDeath();
            }

            CurrentState.RunFrameUpdate();
        }

        void LateUpdate()
        {
            CurrentState.RunLateUpdate();
        }

        public void ChangeState(int stateIndex)
        {
            CurrentState.ClearState();

            PrevState = CurrentState;
            CurrentState = StatesDictionary[stateIndex];

            CurrentState.ANIMATION_DATA.PlayTime = 0f;
            CurrentState.ANIMATION_DATA.StartAnimation(stateIndex);

            if (CurrentState.attack != null)
            {
                CurrentState.attack.Target = null;
            }

            CurrentState.InitState();
        }

        public bool TakeHit(ControlMechanism hitter, string move)
        {
            return characterData.hitRegister.Register(hitter.gameObject.name, move);
        }
    }
}