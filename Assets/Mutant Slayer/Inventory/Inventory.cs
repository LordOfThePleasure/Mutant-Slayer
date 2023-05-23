using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    [SerializeField] private int slotsAmount;
    [SerializeField] private GameObject slotPrefub;
    [SerializeField] private Transform layout;
    public PlayerShooting playerShooting;

    public List<InventorySlot> slots = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < slotsAmount; i++)
        {
            GameObject slot = Instantiate(slotPrefub, layout);
            slot.name = "slot " + i.ToString();
            slots.Add(slot.GetComponent<InventorySlot>());
        }
    }

    public bool AddItem(Item item, int amount)
    {
        bool successfullyAdded = TryAddItemToStack(item, amount);
        if (!successfullyAdded) successfullyAdded = TryAddItemToEmptySlot(item, amount);

        return successfullyAdded;
    }

    private bool TryAddItemToEmptySlot(Item item, int amount = 1)
    {
        foreach (var s in slots)
        {
            if (s.item == null)
            {
                s.SetItem(item, amount);
                return true;
            }
        }

        print("Inventory is full!");
        return false;
    }

    private bool TryAddItemToStack(Item item, int amount = 1)
    {
        if (!item.ableToStuck)
        {
            return false;
        }

        InventorySlot slot = slots.FirstOrDefault(x => x.item != null && x.item.name == item.name);
        if (slot != null)
        {
            slot.ChangeAmount(amount);
            return true;
        }
        return false;
    }

    public void ClearInventory()
    {
        foreach (var slot in slots)
        {
            slot.SetItem(null);
        }
    }

    public void DeselectSlots()
    {
        foreach (var slot in slots)
        {
            slot.Deselect();
        }
    }

    public InventorySlot FindSlotWithAmmo()
    {
        return slots.FirstOrDefault(x => x.item != null && x.item.type == ItemType.Ammo);
    }
}

[System.Serializable]
public class ItemWithAmount
{
    public Item item;
    public int amount;
}
