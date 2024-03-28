using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundCharacterMovement : CharacterMovement
    {
        private Rigidbody2D m_Body;

        void Start()
        {
            m_Body = GetComponent<Rigidbody2D>();
        }

        public override void MoveByDirection(Vector2 direction)
        {
            m_Body.AddForce(direction * Speed);
        }
    }
}

