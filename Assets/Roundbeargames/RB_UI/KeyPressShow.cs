using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public enum KeyPressType
    {
        NONE,
        W,
        A,
        S,
        D,
        SHIFT,
        ENTER,
        SPACE,
    }

    public class KeyPressShow : MonoBehaviour
    {
        public UISprite ColoredSprite;
        public KeyPressType keyPressType;

        public void ShowSprite(bool show)
        {
            ColoredSprite.gameObject.SetActive(show);
        }
    }
}
