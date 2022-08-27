using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Character
{
    [SerializeField] CharacterSO charSO;
    [SerializeField] int level;
    [SerializeField] int str;
    [SerializeField] int defense;
    [SerializeField] int energyPoint;

    public CharacterSO Char { get{
        return charSO;
    } }
    public int Level { get{
        return level;
    } }

    public int Exp {get; set;}
    public int HP { get; set; }
    public List<Skills> Skills { get; set; }

    public void Init(){
        HP = MaxHP;

        // Assign Character Skills
        Skills = new List<Skills>();
        foreach ( var skill in Char.LearnableSkills){
            if (skill.Level <= Level)
                Skills.Add(new Skills(skill.Skills));
            if (Skills.Count >= CharacterSO.MaxNumOfMoves)
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
        if (Skills.Count > CharacterSO.MaxNumOfMoves)
            return;

        Skills.Add(new Skills(skillToLearn.Skills));
    }

    public int Attack {
        get { return Mathf.FloorToInt((Char.Strength * Level)/ 100f)+5; }
    }

    //Mathf.FloorToInt((_char.[ATTRIBUTE] * level)/ 100f)+5

    public int MaxHP{
        get {
            return Mathf.FloorToInt((Char.MaxHP * Level)/100f) + 10;
        }
    }

    public DamageDetails TakeDamage(Skills skill, Character attacker){
        var damageDetails = new DamageDetails(){
            Fainted = false
        };

        float modifiers = Random.Range(0.85f, 1f);
        float a = (1*attacker.Level + 10)/ 250f;
        float d = a * skill.Skill.Power * ((float) attacker.Attack) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0){
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Skills GetRandomSkill(){
        int r = Random.Range(0, Skills.Count);
        return Skills[r];
    }
}

public class DamageDetails{
    public bool Fainted {get; set;}
}
