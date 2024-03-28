using System;
using UnityEngine;

namespace Triggers
{
    public class SenseTrigger : InteractionTrigger
    {
        public Action<Collider2D> onTargetEnter;
        public Action<Collider2D> onTargetExit;

        protected override void OnEnter(Collider2D collision)
        {
            onTargetEnter?.Invoke(collision);
        }

        protected override void OnExit(Collider2D collision)
        {
            onTargetExit?.Invoke(collision);
        }
    }
}

