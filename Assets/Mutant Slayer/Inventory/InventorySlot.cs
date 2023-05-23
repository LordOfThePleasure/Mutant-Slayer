using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int amount;

    public Image image;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private GameObject removeButton;

    [SerializeField] private Image buttonImage;
    public bool selected;

    public void SetItem(Item i, int amount = 1)
    {
        item = i;

        if (i == null)
        {
            image.gameObject.SetActive(false);
            Deselect();
            amountText.text = "";

            return;
        }

        image.gameObject.SetActive(true);

        image.sprite = i.image;
        this.amount = amount;
        amountText.text = this.amount.ToString();

        if (!item.ableToStuck || amount == 0)
        {
            amountText.text = "";
        }
    }

    public void ChangeAmount(int amount)
    {
        this.amount += amount;

        if (this.amount <= 0)
        {
            SetItem(null);
        }

        amountText.text = this.amount.ToString();
    }

    public void SetBulletsAmount(int amount, int maxAmount)
    {
        this.amount = amount;
        amountText.text = amount.ToString() + "/" + maxAmount.ToString();
    }

    // used by button on slot object
    public void Select()
    {
        if (item == null)
            return;

        Inventory.instance.DeselectSlots();

        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, 0, buttonImage.color.a);
        selected = true;

        if (item.type == ItemType.Weapon)
        {
            Inventory.instance.playerShooting.EquipWeaponFromSlot(this);
        }
        else
        {
            Inventory.instance.playerShooting.UnequipWeapon();
        }

        removeButton.SetActive(true);
    }

    public void Deselect()
    {
        if (buttonImage == null)
            return;
        if (item != null && item.type == ItemType.Weapon)
            amountText.text = "";

        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, 1, buttonImage.color.a);
        selected = false;

        removeButton.SetActive(false);
    }

    public void RemoveButton()
    {
        if (selected && item.type == ItemType.Weapon)
            Inventory.instance.playerShooting.UnequipWeapon();
        SetItem(null);
    }
}
