using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public InventoryUI inventoryUI;
    private Collectable ToPick=null;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>()!=null)
        {
        ToPick = other.gameObject.GetComponent<Collectable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ToPick = null;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1")&&ToPick!=null)
        {
            inventoryUI.PickUp(ToPick);
        }
    }

}
