using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ControlType
{
    PLAYER,
    ENEMY,
    NONE,
}

public enum BodyPart
{
    RIGHT_HAND,
    RIGHT_FOOT,
}

public enum BodyTrail
{
    RIGHT_HAND,
    BODY,
}

namespace roundbeargames
{

    public abstract class ControlMechanism : SerializedMonoBehaviour
    {
        public ControlType controlType = ControlType.NONE;
        public CharacterStateController characterStateController;
        public CharacterMovementData moveData;
        public CharacterAttackData attackData;
        public Dictionary<string, int> CollidedObjects;
        private Vector3 CenterPos;
        private Rigidbody rigidbody;
        public Rigidbody RIGIDBODY
        {
            get
            {
                if (this.rigidbody == null)
                {
                    rigidbody = this.gameObject.GetComponent<Rigidbody>();
                }
                return rigidbody;
            }
        }

        public List<ColliderController> colliderControllers;
        public Dictionary<BodyPart, Transform> BodyPartDictionary;
        public Dictionary<BodyTrail, GameObject> BodyTrailDictionary;

        public void FindCharacterStateController()
        {
            characterStateController = this.gameObject.GetComponentInChildren<CharacterStateController>();
            characterStateController.controlMechanism = this;
        }

        public void CenterPosition()
        {
            if (this.transform.position.z >= 0.01f || this.transform.position.z <= -0.1f)
            {
                CenterPos.x = this.transform.position.x;
                CenterPos.y = this.transform.position.y;
                CenterPos.z = 0f;
                this.transform.position = Vector3.Lerp(this.transform.position, CenterPos, Time.deltaTime * 20f);
            }
        }

        public bool IsFacingForward()
        {
            if (this.transform.right.x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFacing(Vector3 pos)
        {
            Vector3 dir = pos - this.transform.position;
            dir.Normalize();
            if (IsFacingForward() && dir.x > 0f)
            {
                return true;
            }
            else if (!IsFacingForward() && dir.x < 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TriggerColliderControl(DynamicColliderType type)
        {
            foreach (ColliderController c in colliderControllers)
            {
                c.TriggerDynamicCollider(type);
            }
        }

        public void ClearVelocity()
        {
            RIGIDBODY.velocity = Vector3.zero;
            RIGIDBODY.angularVelocity = Vector3.zero;
            RIGIDBODY.drag = 0f;
            RIGIDBODY.angularDrag = 0f;
        }

        void OnCollisionEnter(Collision col)
        {
            //Debug.Log ("collision: " + col.gameObject.name);
            foreach (ContactPoint con in col.contacts)
            {
                float yAngle = Mathf.Abs(con.normal.y - 1f);
                if (yAngle < 0.0001f)
                {
                    moveData.IsGrounded = true;
                    moveData.GroundName = col.gameObject.name;

                    if (CollidedObjects.ContainsKey(col.gameObject.name))
                    {
                        CollidedObjects[col.gameObject.name] += 1;
                    }
                    else
                    {
                        CollidedObjects.Add(col.gameObject.name, 1);
                    }
                    break;
                }
            }

        }

        void OnCollisionExit(Collision col)
        {
            if (CollidedObjects.ContainsKey(col.gameObject.name))
            {
                if (CollidedObjects[col.gameObject.name] == 1)
                {
                    CollidedObjects.Remove(col.gameObject.name);
                }
                else
                {
                    CollidedObjects[col.gameObject.name] -= 1;
                }
            }

            if (CollidedObjects.Count == 0)
            {
                moveData.IsGrounded = false;
            }
        }
    }
}