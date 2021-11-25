using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryUI : MonoBehaviour
{
    public Collectable[] collectables;
    public GameObject PASPanel;
    public Material PickedUp;

    public void PickUp(Collectable collected)
    {
        
        foreach (Collectable collectable in collectables)
        {
            if (collected.collectableType==collectable.collectableType)
            {
                
                if(!collectable.picked)
                {
                collectable.GetComponent<MeshRenderer>().material = PickedUp;
                collectable.picked = true;
                Destroy(collected.gameObject);
                    if (collectable.collectableType==Collectable.collectable.CollectabeA)
                    {
                        PASPanel.SetActive(true);
                    }
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
