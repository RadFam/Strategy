using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct UnitInfo
{
    public int factoryOwner;
}

public struct BonusInfo
{
    public string squadOwner;
    public int factoryOwner;
}

public struct BuildingInfo
{ 
}

public class ProductTask
{
    public string productName;
    public int productUnits;
    public float productUnitTime;
    public float elapsedTime;
    public ProductionController.ProductType prodType;
    public UnitInfo unitInfo;
    public BonusInfo bonusInfo;
    public BuildingInfo buildingInfo;

    public ProductTask(string name, int units, float unittime, ProductionController.ProductType pt)
    {
        elapsedTime = 0.0f;
        productName = name;
        productUnits = units;
        productUnitTime = unittime;
        prodType = pt;

        unitInfo.factoryOwner = -1;

        bonusInfo.squadOwner = "";
        bonusInfo.factoryOwner = -1;
    }

    public void SetUnitInfo(int val)
    {
        unitInfo.factoryOwner = val;
    }

    public void SetBonusInfo(string squadName, int val)
    {
        bonusInfo.squadOwner = squadName;
        bonusInfo.factoryOwner = val;
    }

    public bool AddTime(float vol)
    {
        elapsedTime += vol;
        if (elapsedTime >= productUnits * productUnitTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int AbortProduction()
    { 
        return (int)(elapsedTime / productUnitTime);
    }

    public float GetPercentage()
    {
        return (elapsedTime / (productUnits * productUnitTime));
    }
}
