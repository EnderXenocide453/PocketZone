using UnityEngine;

namespace Movement
{
    public abstract class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_CharacterMovement;
        public CharacterMovement CharacterMovement
        {
            get => m_CharacterMovement;
            set
            {
                m_CharacterMovement = value;
            }
        }

        protected abstract void Move(Vector2 direction);
    }
}

