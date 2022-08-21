using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] int letterPerSecond;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<TMP_Text> actionTexts;
    [SerializeField] List<TMP_Text> moveTexts;

    [SerializeField] TMP_Text manaCost;
    [SerializeField] TMP_Text damageAmount;

    public void SetDialogue(string dialogue){
        dialogueText.text = dialogue;
    }

    public IEnumerator TypeDialogue(string dialogue){
        // animate dialogue
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSecond);
        }
    }

    public void EnableDialogueText(bool enabled){
        dialogueText.enabled = enabled;
    }

    public void EnableActionSelector (bool enabled){
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector (bool enabled){
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }
}
