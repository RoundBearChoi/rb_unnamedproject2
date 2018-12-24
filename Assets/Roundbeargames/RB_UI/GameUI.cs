using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class GameUI : MonoBehaviour
    {
        public KeyboardPress keyboardPress;
        private CharacterManager characterManager;

        void Start()
        {
            characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
        }

        public void OnClickRestartGame()
        {
            //Debug.Log("restarting game");
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene");
        }

        public void OnClickReviveEnemy()
        {
            if (characterManager.ListEnemies[0].characterStateController.CurrentState.GetType() == typeof(CharacterDeath))
            {
                //Debug.Log("reviving enemy");
                CharacterDeath deathState = characterManager.ListEnemies[0].characterStateController.CurrentState as CharacterDeath;
                deathState.Revive();
            }
        }
    }
}
