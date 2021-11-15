using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum collectable
    {
        CollectabeA,
        CollectabeB,
        CollectabeC,
        CollectabeD
    };
    public collectable collectableType = collectable.CollectabeA;
    public bool picked=false;


}
