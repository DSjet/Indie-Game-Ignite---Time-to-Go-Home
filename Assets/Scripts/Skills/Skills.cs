using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public SkillsSO Skill { get; set; }
    public int ManaCost { get; set; }

    public Skills (SkillsSO pSkill){
        Skill = pSkill;
        ManaCost = pSkill.ManaCost;
    }
}
