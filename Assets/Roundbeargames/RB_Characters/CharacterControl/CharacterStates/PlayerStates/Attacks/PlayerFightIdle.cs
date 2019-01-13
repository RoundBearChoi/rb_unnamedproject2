using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames
{
    public class PlayerFightIdle : CharacterState
    {
        public override void InitState()
        {
            ANIMATION_DATA.DesignatedAnimation = PlayerState.FightIdle.ToString();
            GetTargetHead();
        }

        public override void RunFixedUpdate()
        {

        }

        public override void RunFrameUpdate()
        {
            if (UpdateAnimation())
            {
                if (ATTACK_DATA.AttackA)
                {
                    characterStateController.ChangeState((int)PlayerState.mmaKick_back);
                    return;
                }

                //Debug.Log(ANIMATION_DATA.PlayTime);
                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                    return;
                }
            }
        }

        public override void RunLateUpdate()
        {
            if (TargetHead != null)
            {
                NeutralTargetPos = TargetHead.position - (Vector3.up * 0.25f);
                NeutralTargetPos.z = 0f;

                NeutralNeckPos = Neck.transform.position;
                NeutralNeckPos.z = 0f;

                NeckDir = NeutralNeckPos - NeutralTargetPos;

                //Debug.Log(NeckDir);

                if (CONTROL_MECHANISM.IsFacingForward())
                {
                    NeckDir += new Vector3(-3f, 0f, 0f);
                }
                else
                {
                    NeckDir += new Vector3(3f, 0f, 0f);
                }

                Neck.transform.LookAt(Neck.transform.position + NeckDir, Vector3.up);
                Head.transform.LookAt(Neck.transform.position + NeckDir, Vector3.up);
                //Neck.forward = NeckDir;
                //Head.forward = NeckDir;
            }
        }

        public override void ClearState()
        {

        }

        private void GetTargetHead()
        {
            TargetControlMech = null;
            if (characterStateController.PrevState.attack != null)
            {
                if (characterStateController.PrevState.attack.Target != null)
                {
                    TargetControlMech = characterStateController.PrevState.attack.Target;
                }
            }

            if (TargetControlMech != null)
            {
                List<Touchable> touchables = TargetControlMech.characterStateController.characterData.GetTouchable(TouchableType.CHARACTER);

                foreach (Touchable t in touchables)
                {
                    if (t.gameObject.name.Contains("Head"))
                    {
                        TargetHead = t.transform;
                        break;
                    }
                }
            }
        }

        public Transform Neck;
        public Transform Head;
        public ControlMechanism TargetControlMech;
        public Transform TargetHead;
        private Vector3 NeutralNeckPos;
        private Vector3 NeutralTargetPos;
        private Vector3 NeckDir;
    }
}
