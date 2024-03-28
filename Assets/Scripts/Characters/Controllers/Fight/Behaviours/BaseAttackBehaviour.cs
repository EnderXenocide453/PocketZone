using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
    public abstract class BaseAttackBehaviour : MonoBehaviour
    {
        [SerializeField] CharacterAttack m_CharacterAttack;
        private Transform m_CurrentTarget;

        public CharacterAttack CharacterAttack
        {
            get => m_CharacterAttack;
            set
            {
                m_CharacterAttack = value;
            }
        }
        public Transform CurrentTarget => m_CurrentTarget;

        public void SetTarget(Transform target)
        {
            m_CurrentTarget = target;
            m_CharacterAttack.SetTarget(target);
        }

        private void OnDisable()
        {
            CharacterAttack.SetAttackPossibility(false);
        }
    }
}

