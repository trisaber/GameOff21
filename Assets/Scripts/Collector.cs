using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public InventoryUI inventoryUI;
    private Collectable ToPick=null;
    private Protagonist protagonist = null;

    public bool CanPickUpObject()
    {
        return (ToPick != null);
    }

    public void PickUp()
    {
        if (ToPick != null)
        {
            inventoryUI.PickUp(ToPick);
        }
    }

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
    }

}
