using UnityEngine;

namespace Movement
{
    public abstract class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_CharacterMovement;
        private Transform m_CurrentTarget;

        public CharacterMovement CharacterMovement
        {
            get => m_CharacterMovement;
            set
            {
                m_CharacterMovement = value;
            }
        }
        public Transform CurrentTarget => m_CurrentTarget;

        public virtual void SetTarget(Transform target)
        {
            m_CurrentTarget = target;
        }

        protected abstract void Move(Vector2 direction);
    }
}

