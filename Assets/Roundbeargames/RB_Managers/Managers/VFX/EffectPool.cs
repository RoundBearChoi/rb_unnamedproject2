using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimpleEffectType
{
    SPARK,
    FLARE,
    BLOOD,
    GROUND_SHOCK,
    DISTORTION,
    GROUND_SMOKE,
}

namespace roundbeargames
{
    public abstract class EffectPool : MonoBehaviour
    {
        [SerializeField] SimpleEffectType simpleEffectType;
        [SerializeField] GameObject EffectPrefab;
        [SerializeField] List<GameObject> Showing;
        [SerializeField] List<GameObject> Pool;
        [SerializeField] float Duration;

        public void ShowEffect(Vector3 pos)
        {
            GameObject effect;
            if (Pool.Count == 0)
            {
                effect = Instantiate(EffectPrefab) as GameObject;
            }
            else
            {
                effect = Pool[0];
                Pool.RemoveAt(0);
            }
            //effect.transform.localPosition = Vector3.zero;
            effect.transform.position = new Vector3(pos.x, pos.y, pos.z);
            effect.SetActive(true);
            Showing.Add(effect);

            StartCoroutine(_TurnOff(Duration, effect));
        }

        IEnumerator _TurnOff(float seconds, GameObject obj)
        {
            yield return new WaitForSeconds(seconds);
            Showing.Remove(obj);
            Pool.Add(obj);
            obj.SetActive(false);
        }

    }
}