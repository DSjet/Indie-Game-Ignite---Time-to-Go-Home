using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;

    public void SetDialogue(string dialogue){
        dialogueText.text = dialogue;
    }
}
