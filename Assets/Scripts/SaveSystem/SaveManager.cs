using Behaviours;
using Loot;
using System;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using System.IO;

public class SaveManager : MonoBehaviour
{
    const string Path = "Saves/save.json";

    [SerializeField] private BaseCharacter m_Player;
    private Inventory m_Inventory;
    private LootFabric m_LootFabric;

    [Inject]
    public void Construct(Inventory inventory, LootFabric lootFabric)
    {
        m_Inventory = inventory;
        m_LootFabric = lootFabric;
    }

    [ContextMenu("save")]
    public void Save()
    {
        var lootInfo = new LootInfo[m_Inventory.StoredItems.Length];
        Array.Copy(m_Inventory.StoredItems, lootInfo, lootInfo.Length);

        var playerInfo = m_Player.CharacterSaveInfo;

        SaveInfo data = new SaveInfo(lootInfo, playerInfo);

        if (!File.Exists(Path)) {
            File.Create(Path);
        }

        File.WriteAllText(
            Path, 
            JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            ));
    }

    [ContextMenu("load")]
    public void Load()
    {
        if (!File.Exists(Path))
            return;

        SaveInfo data = JsonConvert.DeserializeObject<SaveInfo>(File.ReadAllText(Path));
        m_Player.CharacterSaveInfo = data.PlayerInfo;

        m_Inventory.SetData(data.GetLootInfo(m_LootFabric));
    }

    private struct SaveInfo
    {
        public (int id, int count)[] LootInfo;
        public CharacterSaveInfo PlayerInfo;

        public SaveInfo(LootInfo[] loot, CharacterSaveInfo playerInfo)
        {
            LootInfo = new (int id, int count)[loot.Length];

            for (int i = 0; i < loot.Length; i++) {
                LootInfo[i] = (loot[i].ID, loot[i].Count);
            }

            PlayerInfo = playerInfo;
        }

        public LootInfo[] GetLootInfo(LootFabric lootFabric)
        {
            LootInfo[] loot = new LootInfo[LootInfo.Length];

            for(int i = 0;i < LootInfo.Length; i++) {
                loot[i] = lootFabric.GetItem(LootInfo[i].id, LootInfo[i].count);
            }

            return loot;
        }
    }
}
