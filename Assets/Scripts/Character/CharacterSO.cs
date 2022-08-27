using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] string charName;

    [SerializeField] Sprite CharSprite;
    [SerializeField] Faction faction;

    // Base Stats
    [SerializeField] int strength;
    [SerializeField] int defense;
    [SerializeField] int energyPoint;
    [SerializeField] int maxHP;
    [SerializeField] int expYield;
    [SerializeField] List<LearnableSkills> learnableSkills;

    public static int MaxNumOfMoves{ get; set; } = 4;

    public int GetExpForLevel(int Level){
        return Level * Level * Level;
    }

    public string CharName {get { return charName; }}

    // Base Stats
    public int Strength{get { return strength; }}
    public int Defense{get { return defense; }}
    public int EnergyPoint{get { return energyPoint;}}
    public int MaxHP{get { return maxHP; }}
    public Faction Faction{get { return faction; }}
    public List<LearnableSkills> LearnableSkills{ get { return learnableSkills; }}

    public int ExpYield => expYield;
}

[System.Serializable]
public class LearnableSkills{
    [SerializeField] SkillsSO skills;
    [SerializeField] int level;

    public SkillsSO Skills{
        get {
            return skills;
        }
    }
    public int Level{
        get {
            return level;
        }
    }
}


public enum Faction{
    Friendly,
    Hostile
}
