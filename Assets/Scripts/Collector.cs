using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public Collectable ToPick { get; private set; } = null;
    private Protagonist protagonist = null;

    public bool CanPickUpObject()
    {
        return (ToPick != null);
    }

    public bool isLadybug()
    {
        return (ToPick != null && ToPick.collectableType == Collectable.collectable.ladybug);
    }

    public void PickUp()
    {
        if (isLadybug())
        {
            ToPick.picked = true;
        }
        else if (ToPick != null)
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
