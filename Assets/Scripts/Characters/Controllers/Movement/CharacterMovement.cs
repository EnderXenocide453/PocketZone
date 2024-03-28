using UnityEngine;

namespace Movement
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        /// <summary>
        /// Изменяет итоговое значение скорости
        /// </summary>
        private const float SpeedMultiplier = 0.01f;

        [SerializeField] private float m_Speed;
        public float Speed => m_Speed * SpeedMultiplier;

        public abstract void MoveByDirection(Vector2 direction);
    }
}

