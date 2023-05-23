using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {            
            PickUp pickUp = collision.GetComponent<PickUp>();

            if (Inventory.instance.AddItem(pickUp.item, pickUp.amount))
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
