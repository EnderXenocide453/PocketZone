using Input;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterMovement m_CharacterMovement;
        private InputPresenter m_InputPresenter;

        [Inject]
        public void Construct(InputPresenter inputPresenter)
        {
            m_InputPresenter = inputPresenter;

            if (m_CharacterMovement != null) {
                m_InputPresenter.onMove += Move;
            }
        }

        public void SetMovement(CharacterMovement characterMovement)
        {
            m_CharacterMovement = characterMovement;
        }

        private void Move(Vector2 direction)
        {
            m_CharacterMovement?.MoveByDirection(direction);
        }

        private void OnDestroy()
        {
            m_InputPresenter.onMove -= Move;
        }
    }
}

