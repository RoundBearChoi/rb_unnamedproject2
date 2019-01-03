using System.Collections;
using System.Collections.Generic;
using Cinemachine.PostFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {
    public enum CameraOffsetType {
        DEFAULT,
        ZOOM_ON_PLAYER_DEATH_RIGHT_SIDE,
        ZOOM_ON_PLAYER_DEATH_LEFT_SIDE,
    }
    public class GameCam : SerializedMonoBehaviour {
        public UnityEngine.PostProcessing.PostProcessingBehaviour PostProc;
        public Cinemachine.CinemachineVirtualCamera cinemachineVirtualCam;
        public Cinemachine.CinemachineTransposer cinemachineTransposer;
        public Dictionary<CameraOffsetType, Vector3> CamOffsets;
        private Coroutine OffsetChangeRoutine;
        private Vector3 velocity;
        //private float CurrentLerpTime = 0f;
        //private float LerpTime = 1f;
        public CinemachinePostFX postFX;

        void Start () {
            PostProc = this.gameObject.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour> ();
            Cinemachine.CinemachineComponentBase[] arrPipeline = cinemachineVirtualCam.GetComponentPipeline ();
            foreach (Cinemachine.CinemachineComponentBase c in arrPipeline) {
                if (c.GetType () == typeof (Cinemachine.CinemachineTransposer)) {
                    cinemachineTransposer = c as Cinemachine.CinemachineTransposer;
                    break;
                }
            }

            SetOffset (CameraOffsetType.DEFAULT);
        }

        public void SetOffset (CameraOffsetType offsetType) {
            //cinemachineTransposer.m_FollowOffset = CamOffsets[offsetType];
            OffsetChangeRoutine = StartCoroutine (_ProcChangeOffset (CamOffsets[offsetType]));

            if (offsetType == CameraOffsetType.ZOOM_ON_PLAYER_DEATH_LEFT_SIDE) {
                postFX.m_FocusOffset = 0.3f;

            } else if (offsetType == CameraOffsetType.ZOOM_ON_PLAYER_DEATH_RIGHT_SIDE) {
                postFX.m_FocusOffset = 0.3f;
            }
        }

        IEnumerator _ProcChangeOffset (Vector3 targetOffset) {
            //CurrentLerpTime = 0f;

            while (true) {
                if (Mathf.Abs (Vector3.Magnitude (cinemachineTransposer.m_FollowOffset - targetOffset)) < 0.01f) {
                    //Debug.Log("offset change complete");
                    cinemachineTransposer.m_FollowOffset = targetOffset;
                    yield break;
                }

                /*
                CurrentLerpTime += Time.deltaTime;
                float t = CurrentLerpTime / LerpTime;
                t = 1f - Mathf.Cos (t * Mathf.PI * 0.5f);
                if (t >= 1f) {
                    t = 1f;
                }
                cinemachineTransposer.m_FollowOffset = Vector3.Lerp (cinemachineTransposer.m_FollowOffset, targetOffset, t);
                */

                cinemachineTransposer.m_FollowOffset = Vector3.SmoothDamp (cinemachineTransposer.m_FollowOffset, targetOffset, ref velocity, 0.2f);
                yield return new WaitForEndOfFrame ();
            }
        }
    }
}