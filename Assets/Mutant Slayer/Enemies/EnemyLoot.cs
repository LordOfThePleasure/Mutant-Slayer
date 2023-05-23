using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] protected Item item;
    [SerializeField] protected int amount;

    public void DropLoot()
    {
        ObjectPooler.instance.Spawn("PickUp", transform.position, Quaternion.identity).GetComponent<PickUp>().Set(item, amount);
    }
}
