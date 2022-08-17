using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BubbleDataType
{
    public string Tag;
    [TextArea (1,3)]
    public string[] text;
}
