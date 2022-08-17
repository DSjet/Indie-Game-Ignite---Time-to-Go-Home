using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]
public class CharacterSO : ScriptableObject
{
    [SerializeField] string charName;
    [SerializeField] int level;

    [SerializeField] int damage;

    [SerializeField] int maxHP;
    [SerializeField] Faction faction;


    public string CharName {get { return charName; }}
    public int Level{get { return level; }}
    public int Damage{get { return damage; }}
    public int MaxHP{get { return maxHP; }}
    public Faction Faction{get { return faction; }}
}

public enum Faction{
    Friendly,
    Hostile
}
