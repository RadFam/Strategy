using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitBuilding 
{
    public UnitFactory factoryBase;
    
    //public int maxLevels;
    public int currLevel;
    public int numSlots;
    public int currSlot;
    public int buildNum;

    private int numOfSquads;

    //public List<string> namesOfUnits; // Кого он может производить вообще
    //public List<string> namesOfBonuses;

    public List<string> namesOfActualUnits;
    public List<string> namesOfActualBonuses;
    public List<Sprite> iconsOfActualUnits;
    public List<Sprite> iconsOfActualBonus;

    public List<RealBattleUnit> buildingUnits;

    /*
    public UnitBuilding()
    {
        buildingUnits = new List<RealBattleUnit>();
        
        currLevel = 0;
        currSlot = 0;
        numSlots = factoryBase.getLevelSlots[currLevel];
    }
    */
    public int Squads
    {
        get { return numOfSquads; }
        set { numOfSquads = value; }
    }
    
    public void InitBuilding(int cl)
    {
        buildingUnits = new List<RealBattleUnit>();

        currLevel = cl;
        currSlot = 0;
        numSlots = factoryBase.getLevelSlots[currLevel];

        UpdateMyRealUnits(); // возможны ошибки в самом начале
    }
    

    public void LevelUp()
    {
        currLevel += 1;
        currLevel = Mathf.Max(factoryBase.getMaxLevels, currLevel);
        numSlots = factoryBase.getLevelSlots[currLevel];
        UpdateActualLists();
        UpdateMyRealUnits();
    }

    private void UpdateActualLists()
    {
        namesOfActualUnits.Clear();
        namesOfActualBonuses.Clear();
        iconsOfActualUnits.Clear();
        iconsOfActualBonus.Clear();

        namesOfActualUnits = factoryBase.getFullContent[currLevel].actualUnits;
        namesOfActualBonuses = factoryBase.getFullContent[currLevel].actualBonuses;
        iconsOfActualUnits = factoryBase.getFullContent[currLevel].actualIcons;
        iconsOfActualBonus = factoryBase.getFullContent[currLevel].actualBonusIcons;
    }

    public void UpdateMyRealUnits()
    {
        buildingUnits.Clear();
        buildingUnits = ResourceController.instance.UpdateBuildingUnits(buildNum);
        numOfSquads = buildingUnits.Count;
    }

    public void AddNewBattleUnit(RealBattleUnit RBU)
    {
        buildingUnits.Add(RBU);
        numOfSquads = buildingUnits.Count;
    }

    public void RemoveExistingBattleUnit(string specName)
    {
        RealBattleUnit rbu = buildingUnits.Find(x => x.specialName == specName);
        buildingUnits.Remove(rbu);
        UpdateMyRealUnits();
        //numOfSquads = buildingUnits.Count;
    }

    public string GetSquadName(int num)
    {
        return factoryBase.getFullContent[currLevel].actualUnits[num];
    }

    public string GetBonusName(int num)
    {
        return factoryBase.getFullContent[currLevel].actualBonuses[num];
    }
}
