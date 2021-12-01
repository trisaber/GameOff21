using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public Collectable[] collectables;

    public Material PickedUp;
    public bool inventory;
    public bool lavaCheck = false;

    public void PickUp(Collectable collected)
    {
        
        foreach (Collectable collectable in collectables)
        {
            if (collected.collectableType==collectable.collectableType)
            {
                
                if(!collectable.picked)
                {
                    collectable.GetComponent<Image>().enabled = true;
                    if (collectable.collectableType==Collectable.collectable.LAVA )
                    {
                        lavaCheck = true;
                    }
                  
                collectable.picked = true;
                Destroy(collected.gameObject);
                }
                else
                {
                    Debug.Log("This item already in inventory");
                }
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
