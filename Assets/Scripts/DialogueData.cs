using UnityEngine;

[System.Serializable]
public class DialogueData
{
    public string Name;

    [TextArea(1,3)]
    public string[] Text;
}
