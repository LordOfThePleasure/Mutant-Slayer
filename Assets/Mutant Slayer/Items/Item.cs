using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Ammo }

[CreateAssetMenu(fileName = "New item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public bool ableToStuck;

    public ItemType type;
    public GameObject obj;
}
