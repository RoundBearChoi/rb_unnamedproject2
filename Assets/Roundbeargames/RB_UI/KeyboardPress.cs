using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace roundbeargames
{
    public class KeyboardPress : SerializedMonoBehaviour
    {
        public Dictionary<KeyPressType, KeyPressShow> KeyPresses;

        void Start()
        {
            KeyPressShow[] keys = this.gameObject.GetComponentsInChildren<KeyPressShow>();

            foreach (KeyPressShow k in keys)
            {
                KeyPresses.Add(k.keyPressType, k);
            }
        }

        public KeyPressShow GetKeyPressShow(KeyPressType keyPressType)
        {
            if (KeyPresses.ContainsKey(keyPressType))
            {
                return KeyPresses[keyPressType];
            }
            else
            {
                return null;
            }
        }
    }
}
