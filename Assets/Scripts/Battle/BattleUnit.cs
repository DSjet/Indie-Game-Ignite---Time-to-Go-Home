using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] CharacterSO character;
    [SerializeField] int level;
    [SerializeField] bool isFriendlyUnit;
    
    public Character Char{ get; set; }

    public void Setup(){
        Char = new Character(character, level);
        if (isFriendlyUnit){
            // friendly unit setup
        } else {
            // hostile unit setup
        }
    }
}
