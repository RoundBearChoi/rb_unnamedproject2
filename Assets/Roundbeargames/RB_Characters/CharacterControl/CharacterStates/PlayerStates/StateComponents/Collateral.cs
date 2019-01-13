using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class Collateral : StateComponent
    {
        public Coroutine DetectionRoutine;
        bool StopDetecting = false;
        public List<AIControl> CollateralAIs;
        public bool TriggerCollateralDamage()
        {
            if (StopDetecting)
            {
                return false;
            }

            if (DetectionRoutine == null)
            {
                return false;
            }

            if (characterData.TouchDetectors.ContainsKey(TouchDetectorType.CHARACTER_DETECTOR_BODY))
            {
                TouchDetector d = characterData.TouchDetectors[TouchDetectorType.CHARACTER_DETECTOR_BODY];
                if (d.IsTouching)
                {
                    if (d.TouchablesDictionary.ContainsKey(TouchableType.CHARACTER))
                    {
                        List<Touchable> touchables = d.TouchablesDictionary[TouchableType.CHARACTER];
                        foreach (Touchable touchable in touchables)
                        {
                            if (touchable.gameObject.name.Contains("Enemy"))
                            {
                                //Debug.Log("touch: " + touchable.gameObject.name);
                                AIControl aiControl = touchable.controlMechanism as AIControl;
                                if (!CollateralAIs.Contains(aiControl))
                                {
                                    CollateralAIs.Add(aiControl);
                                    aiControl.characterStateController.ForceDeath = true;
                                    aiControl.characterStateController.DeathCause = "Collateral";

                                    if (controlMechanism.IsFacingForward())
                                    {
                                        aiControl.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                                    }
                                    else
                                    {
                                        aiControl.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                                    }

                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }

                    }
                }
            }
            return false;
        }

        public void StartDetectionTimer(float timer)
        {
            if (DetectionRoutine == null && StopDetecting == false)
            {
                DetectionRoutine = StartCoroutine(_StartDetectingTimer(timer));
            }
        }

        IEnumerator _StartDetectingTimer(float timer)
        {
            //Debug.Log("detection started");
            yield return new WaitForSeconds(timer);
            StopDetecting = true;
            //Debug.Log("detection ended");
        }
    }
}
