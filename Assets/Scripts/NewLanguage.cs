using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="New Language")]
public class NewLanguage : ScriptableObject
{
    public string languageName;
    public bool isSelected;

    [TextArea(5, 10)]
    public string firstCall;

    [TextArea(5, 10)]
    public string standUpCall;

    [TextArea(5, 10)]
    public string briefing1;

    [TextArea(5, 10)]
    public string briefing2;

    [TextArea(5, 10)]
    public string briefing3;

    [TextArea(5, 10)]
    public string lavaReCall;

    [TextArea(5, 10)]
    public string noteBook;

    [TextArea(5, 10)]
    public string holocon;

    [TextArea(5, 10)]
    public string sizeler;

    [TextArea(5, 10)]
    public string sensor;

    [TextArea(5, 10)]
    public string ladybug;

}
