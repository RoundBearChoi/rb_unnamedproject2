using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class UIManager : Manager
    {
        public GameUI gameUI;

        void Start()
        {
            gameUI = FindObjectOfType<GameUI>();
        }
    }
}