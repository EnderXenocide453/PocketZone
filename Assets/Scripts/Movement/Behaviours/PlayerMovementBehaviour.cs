using Input;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class PlayerMovementBehaviour : MovementBehaviour
    {
        private InputPresenter m_InputPresenter;

        [Inject]
        public void Construct(InputPresenter inputPresenter)
        {
            m_InputPresenter = inputPresenter;

            if (CharacterMovement != null) {
                m_InputPresenter.onMove += Move;
            }
        }

        protected override void Move(Vector2 direction)
        {
            CharacterMovement?.MoveByDirection(direction);
        }

        private void OnDestroy()
        {
            m_InputPresenter.onMove -= Move;
        }
    }
}

