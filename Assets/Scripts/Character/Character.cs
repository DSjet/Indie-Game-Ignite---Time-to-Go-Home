using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public CharacterSO charSO;
    public int level;
    public int str;
    public int defense;
    public int energyPoint;
    public GameObject HPHUD;

    public CharacterSO Char { get{
        return charSO;
    } }
    public int Level { get{
        return level;
    } }

    public int Exp {get; set;}
    public int HP;
    public List<Skills> Skilliard;

    public void Init(){
        HP = charSO.MaxHP;

        // Assign Character Skills
        Skilliard = new List<Skills>();
        foreach ( var skill in Char.LearnableSkills){
            if (skill.Level <= Level)
                Skilliard.Add(new Skills(skill.Skills));
            if (Skilliard.Count >= CharacterSO.MaxNumOfMoves)
                break;
        }

        Exp = Char.GetExpForLevel(level);
    }

    public bool CheckForLevelUp(){
        if (Exp > Char.GetExpForLevel(level + 1)){
            ++level;
            return true;
        }
        return false;
    }

    public LearnableSkills GetLearnableSkillsMoveAtCurrLevel(){
        return Char.LearnableSkills.Where(x => x.Level == level).FirstOrDefault();
    }

    public void LearnSkill(LearnableSkills skillToLearn){
        if (Skilliard.Count > CharacterSO.MaxNumOfMoves)
            return;

        Skilliard.Add(new Skills(skillToLearn.Skills));
    }

    public int Attack {
        get { return Mathf.FloorToInt((Char.Strength * Level)/ 100f)+5;
         }
    }

    //Mathf.FloorToInt((_char.[ATTRIBUTE] * level)/ 100f)+5

    public int MaxHP{
        get {
            return Mathf.FloorToInt((Char.MaxHP * Level)/100f) + 10;
        }
    }

    public DamageDetails TakeDamage(Skills skill, Character attacker, bool isDecelerate){
        var damageDetails = new DamageDetails(){
            Fainted = false
        };

        float modifiers = Random.Range(0.85f, 1f);
        float a = (1*attacker.Level + 10)/ 250f;
        float d = a * skill.Skill.Power * ((float) attacker.Attack) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);
        if(isDecelerate) damage = Mathf.FloorToInt(damage/2);

        HP -= damage;
        if (HP <= 0){
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Skills GetRandomSkill(){
        int r = Random.Range(0, Skilliard.Count);
        return Skilliard[r];
    }

    public void UpdateHPUD(){
        HPHUD.GetComponent<Slider>().value = HP;
    }
}

public class DamageDetails{
    public bool Fainted {get; set;}
}
