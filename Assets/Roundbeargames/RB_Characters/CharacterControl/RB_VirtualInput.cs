using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class RB_VirtualInput : MonoBehaviour
    {
        public bool ToggleMoveForward = false;
        public bool ToggleMoveBack = false;
        public bool ToggleMoveUp = false;
        public bool ToggleMoveDown = false;
        public bool ToggleRun = false;
        public bool Jump = false;
        public bool AttackA = false;
        private UIManager uiManager;

        void Start()
        {
            uiManager = ManagerGroup.Instance.GetManager(ManagerType.UI_MANAGER) as UIManager;
        }

        void Update()
        {
            if (Input.GetKey("a"))
            {
                ToggleMoveBack = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.A).ShowSprite(true);
            }
            else
            {
                ToggleMoveBack = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.A).ShowSprite(false);
            }

            if (Input.GetKey("d"))
            {
                ToggleMoveForward = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.D).ShowSprite(true);
            }
            else
            {
                ToggleMoveForward = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.D).ShowSprite(false);
            }

            if (Input.GetKey("w"))
            {
                ToggleMoveUp = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.W).ShowSprite(true);
            }
            else
            {
                ToggleMoveUp = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.W).ShowSprite(false);
            }

            if (Input.GetKey("s"))
            {
                ToggleMoveDown = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.S).ShowSprite(true);
            }
            else
            {
                ToggleMoveDown = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.S).ShowSprite(false);
            }

            if (Input.GetKey("left shift") || Input.GetKey("right shift"))
            {
                ToggleRun = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.SHIFT).ShowSprite(true);
            }
            else if (!Input.GetKey("left shift") || !Input.GetKey("right shift"))
            {
                ToggleRun = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.SHIFT).ShowSprite(false);
            }

            if (Input.GetKey("space"))
            {
                Jump = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.SPACE).ShowSprite(true);
            }
            else
            {
                Jump = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.SPACE).ShowSprite(false);
            }

            if (Input.GetKey("return"))
            {
                AttackA = true;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.ENTER).ShowSprite(true);
            }
            else
            {
                AttackA = false;
                uiManager.gameUI.keyboardPress.GetKeyPressShow(KeyPressType.ENTER).ShowSprite(false);
            }
        }
    }
}