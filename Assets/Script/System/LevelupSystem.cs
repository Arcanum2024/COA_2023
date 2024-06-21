using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelupSystem : MonoBehaviour
{

    public int currentLevel;
    public int baseExp = 20;
    public int currentExp;

    public int ExpForNextLevel;
    public int ExpDifferenceToNextLevel;
    public int TotalExpDifference;

    public float fillAmount;
    public float reverseFillAmount;

    public int statPoints;
    public int skillPoints;


    void Start()
    {
        InvokeRepeating("AddExp", 1f, 1f);
    }

    public void AddExp()
    {
        CalculateLevel(5);
    }

    void CalculateLevel(int amount)
    {
        currentExp += amount;

        int temp_cur_level = (int)Mathf.Sqrt(currentExp/baseExp) + 1;

        if(currentLevel != temp_cur_level)
        {
            currentLevel = temp_cur_level;
        }

        ExpForNextLevel = baseExp * currentLevel * currentLevel;
        ExpDifferenceToNextLevel = ExpForNextLevel - currentExp;
        TotalExpDifference = ExpForNextLevel - (baseExp * (currentLevel-1) * (currentLevel-1));

        fillAmount = (float)ExpDifferenceToNextLevel / (float)TotalExpDifference;
        reverseFillAmount = 1 - fillAmount;

        statPoints = 5 * (currentLevel - 1);
        skillPoints = 15 * (currentLevel - 1);
    }
}
