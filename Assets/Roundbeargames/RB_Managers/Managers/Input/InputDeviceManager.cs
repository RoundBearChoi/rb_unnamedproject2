using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class InputDeviceManager : Manager
    {
        [SerializeField]
        RB_VirtualInput virtualInputPrefab;
        private RB_VirtualInput virtualInput;
        public RB_VirtualInput VIRTUAL_INPUT
        {
            get
            {
                if (virtualInput == null)
                {
                    RB_VirtualInput[] old = GameObject.FindObjectsOfType<RB_VirtualInput>();
                    foreach (RB_VirtualInput v in old)
                    {
                        Destroy(v.gameObject);
                    }

                    GameObject obj = Instantiate(virtualInputPrefab.gameObject) as GameObject;
                    virtualInput = obj.GetComponent<RB_VirtualInput>();
                }
                return virtualInput;
            }
        }
    }
}