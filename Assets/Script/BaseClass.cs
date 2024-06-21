using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseClass
{
    public enum genders
    {
        Male,
        Female
    }

    [Header("Description")]
    public string playerName;
    public int currentLevel;
    public int currentExp;

    [Header("Stats")]
    public int cur_health;
    public int max_health;
    public int cur_mana;
    public int max_mana;

    
    [Header("Gender")]
    public genders Gender;

    [Header("Stats")]
    public int strength;
    public int endurance;
    public int agility;
    public int wisdom;
    public int intelligence;

    [Header("Skills")]
    public int statPoints;
    public int skillPoints;
    //List of Current Skills
}
