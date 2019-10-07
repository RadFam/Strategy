using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct myBonuse
{
    public BattleBonus unitBonus;
    public int currnetCount;
}

public class RealBattleUnit
{
    public BattleUnit bu;
    public int curAmount;
    public int buildNum;
    public string specialName;
    public ResourceController.SquadStatus curStatus;
    private List<myBonuse> currBonuses;

    public List<myBonuse> GetCurrBonuses
    {
        get { return currBonuses; }
    }

    public RealBattleUnit(string squadName)
    {
        curAmount = 0;
        currBonuses = new List<myBonuse>(); // В принципе, должно хватить
        specialName = squadName;
        curStatus = ResourceController.SquadStatus.onFree;
    }

    public int NeedUnits()
    {
        return bu.getUnitAmount - curAmount;
    }

    public void AddUnits(int add)
    {
        curAmount += add;
        curAmount = Mathf.Min(curAmount, bu.getUnitAmount);
    }

    public void SubtractUnits(int subtract)
    {
        curAmount -= subtract;
        curAmount = Mathf.Max(curAmount, 0);
    }

    public List<string> GetAvailableBonuses()
    {
        List<string> answer = new List<string>();

        for (int i = 0; i < bu.getFullBonuses.Count; ++i)
        {
            answer.Add(bu.getFullBonuses[i].unitBonus.getBonusName);
        }

        return answer;
    }

    public bool AddBonusToUnit(BattleBonus BB, int bonusNum)
    {
        // Check if we can add this bonus to unit
        int chkInd = -1;
        int maxVol = 0;
        for (int i = 0; i < bu.getFullBonuses.Count; ++i )
        {
            if (bu.getFullBonuses[i].unitBonus == BB)
            {
                chkInd = i;
                maxVol = bu.getFullBonuses[i].bonusMaxCount;
                break;
            }
        }
        if (chkInd == -1)
        {
            return false;
        }
        
        int ind = -1;
        for (int i = 0; i < currBonuses.Count; ++i)
        {
            if (currBonuses[i].unitBonus == BB)
            {
                ind = i;
                break;
            }
        }
        if (ind != -1)
        {
            myBonuse mb = new myBonuse();
            mb.unitBonus = BB;
            mb.currnetCount = Mathf.Max(currBonuses[ind].currnetCount + bonusNum, maxVol);
            currBonuses[ind] = mb;
        }
        else
        {
            myBonuse mb = new myBonuse();
            mb.unitBonus = BB;
            mb.currnetCount = Mathf.Max(bonusNum, maxVol); ;
            currBonuses.Add(mb);
        }

        return true;
    }

    public bool SubtractBonusFromUnit(BattleBonus BB)
    {
        int chkInd = -1;
        int minVol = 0;

        for (int i = 0; i < bu.getFullBonuses.Count; ++i)
        {
            if (bu.getFullBonuses[i].unitBonus == BB)
            {
                chkInd = i;
                break;
            }
        }
        if (chkInd == -1)
        {
            return false;
        }

        minVol = currBonuses[chkInd].currnetCount - 1;
        if (minVol > 0)
        {
            myBonuse mb = new myBonuse();
            mb.unitBonus = BB;
            mb.currnetCount = minVol;
            currBonuses[chkInd] = mb;
        }
        else
        {
            currBonuses.RemoveAt(chkInd);
        }


        return true;
    }

    public int NeedBonus(string bonusNm)
    {
        int ans = 0;

        int ind_1 = bu.getFullBonuses.FindIndex(x => x.unitBonus.getBonusName == bonusNm);

        if (ind_1 == -1) // такого бонуса не предусмотрено для подобных юнитов
        {
            return 0;
        }

        int ind_2 = currBonuses.FindIndex(x => x.unitBonus.getBonusName == bonusNm);

        if (ind_2 == -1)
        {
            return bu.getFullBonuses[ind_1].unitBonus.getBonusAmount;
        }

        ans = Mathf.Min(bu.getFullBonuses[ind_1].unitBonus.getBonusAmount, currBonuses[ind_2].unitBonus.getBonusAmount);

        return ans;
    }
}
