using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputPresenter : MonoBehaviour
    {
        private PlayerControls m_PlayerControls;
        private bool m_IsMove;

        private InputAction m_MoveAction;

        public event Action<Vector2> onMove;
        public event Action OnAttackBegun;
        public event Action OnAttackEnds;
        public event Action OnInventoryAction;

        private void Awake()
        {
            m_PlayerControls = new PlayerControls();
            m_PlayerControls.Enable();

            BindActions();
        }

        private void FixedUpdate()
        {
            if (m_IsMove)
                onMove?.Invoke(m_MoveAction.ReadValue<Vector2>());
        }

        #region InputActionsBinding
        private void BindActions()
        {
            m_MoveAction = m_PlayerControls.ActionMap.MoveAction;
            m_MoveAction.started += OnMoveActionStarted;
            m_MoveAction.canceled += OnMoveActionCanceled;

            m_PlayerControls.ActionMap.AttackAction.started += OnAttackActionStarted;
            m_PlayerControls.ActionMap.AttackAction.canceled += OnAttackActionCanceled;

            m_PlayerControls.ActionMap.InventoryAction.started += OnInventoryActionExecute;
        }

        private void OnInventoryActionExecute(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnInventoryAction?.Invoke();
        }

        private void OnAttackActionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnAttackEnds?.Invoke();
        }

        private void OnAttackActionStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnAttackBegun?.Invoke();
        }

        private void OnMoveActionCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_IsMove = false;
        }

        private void OnMoveActionStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_IsMove = true;
        }
        #endregion
    }
}

