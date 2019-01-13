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
        private SaveManager saveManager;
        private RB_SceneManager rbSceneManager;

        IEnumerator Start()
        {
            characterManager = ManagerGroup.Instance.GetManager(ManagerType.CHARACTER_MANAGER) as CharacterManager;
            cameraManager = ManagerGroup.Instance.GetManager(ManagerType.CAMERA_MANAGER) as CameraManager;
            saveManager = ManagerGroup.Instance.GetManager(ManagerType.SAVE_MANAGER) as SaveManager;
            rbSceneManager = ManagerGroup.Instance.GetManager(ManagerType.SCENE_MANAGER) as RB_SceneManager;

            yield return new WaitForEndOfFrame();

            saveManager.Load();
            cameraManager.gameCam.PostProc.enabled = saveManager.CurrentData.ShowPostProc;
            PostProcChecker.enabled = saveManager.CurrentData.ShowPostProc;
            ProcPostProc();
        }

        public void OnClickRestartGame()
        {
            //Debug.Log("restarting game");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("TestScene");
            rbSceneManager.RestartScene();
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
                saveManager.CurrentData.ShowPostProc = false;
            }
            else
            {
                PostProcChecker.enabled = true;
                saveManager.CurrentData.ShowPostProc = true;
            }

            saveManager.Save();
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

        public void OnClickQuitGame()
        {
            Application.Quit();
        }
    }
}