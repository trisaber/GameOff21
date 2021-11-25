using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum collectable   //rename types as names of objects
    {
        CollectabeA,
        CollectabeB,
        CollectabeC,
        CollectabeD
    };
    public collectable collectableType = collectable.CollectabeA;
    public bool picked=false;


}
