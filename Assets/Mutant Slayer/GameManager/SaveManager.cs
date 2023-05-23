using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private List<Item> allItems;
    [SerializeField] private ItemWithAmount[] defaultItems;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 defaultPosition;
    [SerializeField] private Health playerHealth;

    [SerializeField] private EnemySpawner spawner;

    private class SlotSaver
    {
        public string itemName;
        public int amount;

        public SlotSaver(string itemName, int amount)
        {
            this.itemName = itemName;
            this.amount = amount;
        }
    }

    private class ObjectSaver
    {
        public string savingTag;
        public Vector2 position;

        public ObjectSaver(string savingTag, Vector2 position)
        {
            this.savingTag = savingTag;
            this.position = position;
        }
    }
    
    private void SaveInventory()
    {
        List<SlotSaver> slotSavers = new();
        foreach (var slot in Inventory.instance.slots)
        {
            if (slot.item == null)
                continue;

            slotSavers.Add(new SlotSaver(slot.item.name, slot.amount));
        }

        SaveGame.Save("Slots", slotSavers);
    }

    private void LoadInventory()
    {
        Inventory.instance.ClearInventory();

        if (!SaveGame.Exists("Slots"))
        {
            foreach (var item in defaultItems)
            {
                Inventory.instance.AddItem(item.item, item.amount);
            }
            return;
        }

        List<SlotSaver> slotLoaders = SaveGame.Load<List<SlotSaver>>("Slots");

        foreach (var slot in slotLoaders)
        {
            Inventory.instance.AddItem(allItems.FirstOrDefault(x => x.name == slot.itemName), slot.amount);
        }
    }

    private void SaveObjects()
    {
        SavableObject[] savableObjects = FindObjectsOfType<SavableObject>();

        List<ObjectSaver> savingList = new();

        foreach (var obj in savableObjects)
        {
            savingList.Add(new ObjectSaver(obj.savingTag, obj.transform.position));
        }

        SaveGame.Save("Objects", savingList);
    }

    private void LoadObjects()
    {
        foreach (var item in FindObjectsOfType<SavableObject>())
        {
            item.gameObject.SetActive(false);
        }

        if (!SaveGame.Exists("Objects"))
        {
            spawner.DefaultSpawn();
            return;
        }

        List<ObjectSaver> loadingList = SaveGame.Load<List<ObjectSaver>>("Objects");

        foreach (var obj in loadingList)
        {
            ObjectPooler.instance.Spawn(obj.savingTag, obj.position, Quaternion.identity);
        }
    }

    private void SavePlayer()
    {
        SaveGame.Save("PlayerPosition", playerTransform.position);
        SaveGame.Save("PlayerHealth", playerHealth.health);
    }

    private void LoadPlayer()
    {
        if (!SaveGame.Exists("PlayerPosition"))
        {
            playerTransform.position = defaultPosition;
            playerHealth.Reload();
            return;
        }


        playerTransform.position = SaveGame.Load<Vector2>("PlayerPosition");
        playerHealth.health = SaveGame.Load<int>("PlayerHealth");
    }

    public void DeleteAllSaves()
    {
        SaveGame.DeleteAll();
    }

    public void Save()
    {
        SavePlayer();
        SaveObjects();
        SaveInventory();
    }

    public void Load()
    {
        LoadPlayer();
        LoadObjects();
        LoadInventory();
        Inventory.instance.slots[0].Select();
    }
}
