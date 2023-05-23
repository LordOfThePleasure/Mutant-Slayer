using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Inventory inventory;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {            
            PickUp pickUp = collision.GetComponent<PickUp>();

            if (inventory.AddItem(pickUp.item, pickUp.amount))
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
