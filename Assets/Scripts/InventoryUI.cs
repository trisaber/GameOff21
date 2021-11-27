using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public Collectable[] collectables;

    public Material PickedUp;

    public void PickUp(Collectable collected)
    {
        
        foreach (Collectable collectable in collectables)
        {
            if (collected.collectableType==collectable.collectableType)
            {
                
                if(!collectable.picked)
                {
                    collectable.GetComponentInChildren<MeshRenderer>().material = PickedUp;

                  
                collectable.picked = true;
                Destroy(collected.gameObject);
                }
                else
                {
                    Debug.Log("This item already in inventory");
                }
            }
            if(inventoryCheck())
            {
                Debug.Log("All items are picked");
            }
        }     
    }
    public bool inventoryCheck()  // inventory control for endgame 
    {
        foreach (Collectable collectable in collectables)
        {
            if (collectable.picked == false) return false;
        }
        return true;
    }
}
