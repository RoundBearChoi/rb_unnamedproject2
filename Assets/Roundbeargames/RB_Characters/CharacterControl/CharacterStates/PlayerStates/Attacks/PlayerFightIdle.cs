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
                //Debug.Log(ANIMATION_DATA.PlayTime);
                if (DurationTimePassed())
                {
                    characterStateController.ChangeState((int)PlayerState.HumanoidIdle);
                }
            }
        }

        public override void RunLateUpdate()
        {
            if (TargetHead != null)
            {
                NeutralHeadPos = TargetHead.position - (Vector3.up * 0.25f);
                NeutralHeadPos.z = 0f;

                NeutralNeckPos = Neck.transform.position;
                NeutralNeckPos.z = 0f;

                NeckDir = NeutralNeckPos - NeutralHeadPos;
                NeckDir.Normalize();
                Neck.forward = NeckDir;

                Head.forward = NeckDir;
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
        private Vector3 NeutralHeadPos;
        private Vector3 NeckDir;
    }
}
