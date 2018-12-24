using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class GameCam : MonoBehaviour
    {
        public UnityEngine.PostProcessing.PostProcessingBehaviour PostProc;

        void Start()
        {
            PostProc = this.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();
        }
    }
}