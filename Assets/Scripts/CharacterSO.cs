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

    [SerializeField] int damage;
    [SerializeField] int maxHP;

    [SerializeField] List<LearnableSkills> learnableSkills;


    public string CharName {get { return charName; }}
    public int Damage{get { return damage; }}
    public int MaxHP{get { return maxHP; }}
    public Faction Faction{get { return faction; }}
    public List<LearnableSkills> LearnableSkills{ get { return learnableSkills; }}
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
