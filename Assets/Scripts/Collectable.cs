using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum collectable   //rename types as names of objects
    {
        LAVA,
        Notebook,
        holocon,
        sizeler,
        sensor,
        ladybug
    };
    public collectable collectableType;
    public bool picked=false;

}
