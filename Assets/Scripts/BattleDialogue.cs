using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] int letterPerSecond;
    [SerializeField] Color highlightedColor;
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

    public void UpdateActionSelection(int selectedAction){
        for (int i = 0; i <actionTexts.Count; ++i){
            if ( i == selectedAction){
                actionTexts[i].color = highlightedColor;
            } else {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Skills skill){
        for (int i = 0; i < actionTexts.Count; ++i){
            if ( i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }

        manaCost.text = $"Mana Cost: {skill.ManaCost}/ {skill.Skill.ManaCost}";
        damageAmount.text = skill.Skill.Power.ToString();
    }

    public void SetMoveNames(List<Skills> skills){
        for (int i = 0; i < moveTexts.Count; ++i){
            if ( i < skills.Count)
                moveTexts[i].text = skills[i].Skill.SkillName;
            else 
                moveTexts[i].text = "-";
        }
    }
}
