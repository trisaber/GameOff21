using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Language")]
public class NewLanguage : ScriptableObject
{
    [TextArea(5, 10)]
    public string firstCall;

    [TextArea(5, 10)]
    public string standUpCall;

    [TextArea(5, 10)]
    public string lavaReCall;

    [TextArea(5, 10)]
    public string arne;

    [TextArea(5, 10)]
    public string noteBook;
}
