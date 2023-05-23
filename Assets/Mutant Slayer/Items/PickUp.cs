using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Item item;
    public int amount;

    public void Set(Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
        spriteRenderer.sprite = item.image;
    }
}
