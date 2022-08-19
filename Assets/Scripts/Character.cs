using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public CharacterSO Char { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }
    public List<Skills> Skills { get; set; }

    public Character(CharacterSO pChar, int pLevel){
        Char = pChar;
        Level = pLevel;
        HP = MaxHP;

        // Assign Character Skills
        Skills = new List<Skills>();
        foreach ( var skill in Char.LearnableSkills){
            if (skill.Level <= Level)
                Skills.Add(new Skills(skill.Skills));
            if (Skills.Count >= 4)
                break;
        }
    }

    public int Attack {
        get { return Mathf.FloorToInt((Char.Damage * Level)/ 100f)+5; }
    }

    //Mathf.FloorToInt((_char.[ATTRIBUTE] * level)/ 100f)+5

    public int MaxHP{
        get {
            return Mathf.FloorToInt((Char.MaxHP * Level)/100f) + 10;
        }
    }
}
