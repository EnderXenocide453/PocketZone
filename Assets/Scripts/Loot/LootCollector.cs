using UnityEngine;
using Zenject;

namespace Loot
{
    public class LootCollector : MonoBehaviour
    {
        private Inventory m_Inventory;

        [Inject]
        public void Constructor(Inventory inventory)
        {
            m_Inventory = inventory;
        }

        public void AddLoot(LootInfo loot)
        {
            m_Inventory.AddLoot(loot);
        }
    }
}

