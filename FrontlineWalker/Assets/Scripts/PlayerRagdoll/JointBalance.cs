using UnityEngine;

namespace PlayerRagdoll
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JointBalance : MonoBehaviour
    {
        public float targetRotation;
        public float force;
        private Rigidbody2D _rb;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rb.MoveRotation(Mathf.LerpAngle(_rb.rotation, targetRotation, force));
        }
    }
}