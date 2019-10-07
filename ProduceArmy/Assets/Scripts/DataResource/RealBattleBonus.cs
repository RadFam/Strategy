using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattleBonus
{
    public BattleBonus bb;
    public int currAmount;
    public string squadOwner;

    public RealBattleBonus(int initNum, string squad)
    {
        currAmount = initNum;
        squadOwner = squad;
    }

    public void AddNewUnits(int val) // VAL can be negative
    {
        currAmount += val;
    }
}
