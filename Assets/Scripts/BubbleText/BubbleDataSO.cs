using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "BubbleDialogue/Data", order = 1)]
public class BubbleDataSO : ScriptableObject
{
    public float yOffset;
    public BubbleDataType[] Dialogue;
}
