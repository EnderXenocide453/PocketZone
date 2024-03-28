using Input;
using Zenject;

namespace Behaviours
{
    public class PlayerAttackBehaviour : BaseAttackBehaviour
    {
        private InputPresenter m_InputPresenter;

        [Inject]
        public void Construct(InputPresenter inputPresenter)
        {
            m_InputPresenter = inputPresenter;
            m_InputPresenter.OnAttackBegun += OnAttackBegun;
            m_InputPresenter.OnAttackEnds += OnAttackEnds;
        }

        private void OnAttackEnds()
        {
            CharacterAttack.SetAttackPossibility(false);
        }

        private void OnAttackBegun()
        {
            if (!enabled) return;

            CharacterAttack.SetAttackPossibility(true);
            CharacterAttack.TryAttack();
        }
    }
}

