using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class FrameManager : Manager
    {
        void Start()
        {
            Application.targetFrameRate = 72;
            Application.runInBackground = true;
            //Time.timeScale = 1f;
        }
    }
}