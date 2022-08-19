using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
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

    // string charName;
    // int charLevel;

    // int damage;

    // int maxHP;
    // int currentHP;

    // void Awake(){
    //     charName = character.CharName;
    //     // charLevel = character.Level;
    //     damage = character.Damage;
    //     maxHP = character.MaxHP;
    //     currentHP = maxHP;
    // }

    // public bool TakeDamage(int dmg){
    //     currentHP -= dmg;

    //     if (currentHP <= 0)
    //         return true;
    //     else
    //         return false;
    // }

    // public void Heal(int amount){
    //     currentHP += amount;
    //     if (currentHP > maxHP)
    //         currentHP = maxHP;
    // }

    // public string CharName{get { return charName; }}
    // public int CharLevel{get { return charLevel; }}
    // public int Damage{get { return damage; }}
    // public int MaxHP{get { return maxHP; }}
    // public int CurrentHP{get { return currentHP; }}
}
