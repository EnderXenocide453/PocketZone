using Behaviours;
using Loot;
using System;
using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private BaseCharacter m_Player;
    private Inventory m_Inventory;

    private LootInfo[] m_LootInfo;
    private CharacterSaveInfo m_PlayerInfo;

    [Inject]
    public void Construct(Inventory inventory)
    {
        m_Inventory = inventory;
    }

    [ContextMenu("save")]
    public void Save()
    {
        m_LootInfo = new LootInfo[m_Inventory.StoredItems.Length];
        Array.Copy(m_Inventory.StoredItems, m_LootInfo, m_LootInfo.Length);

        m_PlayerInfo = m_Player.CharacterSaveInfo;
    }

    [ContextMenu("load")]
    public void Load()
    {
        m_Inventory.SetData(m_LootInfo);

        m_Player.CharacterSaveInfo = m_PlayerInfo;
    }
}
