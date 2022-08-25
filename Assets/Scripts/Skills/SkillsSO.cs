using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Character/Create new Skill")]
public class SkillsSO : ScriptableObject
{
    [SerializeField] string skillName;

    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int manaCost;
    
    public string SkillName {
        get {
            return skillName;
        }
    }
    public int Power {
        get {
            return power;
        }
    }
    public int Accuracy{
        get {
            return accuracy;
        }
    }
    public int ManaCost{
        get {
            return manaCost;
        }
    }
}
