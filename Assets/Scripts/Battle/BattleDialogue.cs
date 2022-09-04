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
    [SerializeField] List<Button> buttonsMove;

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

    public void UpdateMoveSelection(){
        int selected = -1;
        for (int i = 0; i < buttonsMove.Count; ++i){
            if (buttonsMove[i].GetComponent<ButtonOnHover>().onHover){
                selected = i;
                break;
            }
        }
        BattleHandler bt = GameObject.FindObjectOfType<BattleHandler>();
        bt.currentMove = selected;
        if(selected != -1){
            manaCost.text = $"Time Cost: {bt.currentCharacter.Skilliard[selected].Skill.ManaCost}";
            damageAmount.text = bt.currentCharacter.Skilliard[selected].Skill.Power.ToString();
        }else{
            manaCost.text = "";
            damageAmount.text = "";
        }        
    }

    public void SetSkillNames(List<Skills> skills){
        for (int i = 0; i < moveTexts.Count; i++){
            if ( i < skills.Count){
                buttonsMove[i].interactable = true;
                moveTexts[i].text = skills[i].Skill.SkillName;
            }else{
                moveTexts[i].text = "";
                buttonsMove[i].interactable = false;
            } 
        }
    }
}
