using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class GameUI : MonoBehaviour
    {
        public KeyboardPress keyboardPress;
        public UISprite PostProcChecker;
        private CharacterManager characterManager;
        private CameraManager cameraManager;

        void Start()
        {
            characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
            cameraManager = ManagerGroup.Instance.GetManager(ManagerType.CAMERA_MANAGER) as CameraManager;
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

        public void OnClickTogglePostProcessing()
        {
            if (PostProcChecker.enabled)
            {
                PostProcChecker.enabled = false;
            }
            else
            {
                PostProcChecker.enabled = true;
            }

            ProcPostProc();
        }

        public void ProcPostProc()
        {
            if (PostProcChecker.enabled)
            {
                cameraManager.gameCam.PostProc.enabled = true;
            }
            else
            {
                cameraManager.gameCam.PostProc.enabled = false;
            }
        }
    }
}
