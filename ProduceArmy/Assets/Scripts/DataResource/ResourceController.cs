using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct squadsNamePattern
{
    public BattleUnit bu;
    public string sqPattern;
    public int eteNum;
}

public class ResourceController : MonoBehaviour 
{
    public enum SquadStatus {onFree, onReqruit, onUpgrade, onMarch, onBattle, onDie};
    
    public List<BattleUnit> allBattleUnits;
    public List<BattleBonus> allBattleBonuses;
    public List<squadsNamePattern> allUnitNominat;

    [SerializeField]
    public List<UnitBuilding> currentUnitBuildings = new List<UnitBuilding>();
    [SerializeField]
    public List<BonusBuilding> currentModsBuildings = new List<BonusBuilding>();

    public List<RealBattleUnit> currentArmy = new List<RealBattleUnit>();
    public List<RealBattleBonus> currentBonuses = new List<RealBattleBonus>(); // То, что дожидается своей очереди, пока их приобретут

    public static ResourceController instance = null;
    
    // Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        currentArmy.Clear();
        currentBonuses.Clear();

        if (instance == null)
        {
            instance = this;

            // Temporary
            for (int i = 0; i < currentUnitBuildings.Count; ++i )
            {
                currentUnitBuildings[i].InitBuilding(i);
            }
        }
	}

    // ОТРАБОТКА ДЕЙСТВИЙ С ВОЙСКАМИ 

    public void AddNewArmy(RealBattleUnit rbu)
    {
        currentArmy.Add(rbu);
    }

    public void AddUnitsToArmy(string armyNum, int num)
    {
        // Тут надо разобраться - открывать новое подразделение, или добавлять произведенные войска к текущему подразделению
        RealBattleUnit RBU = null;
        RBU = currentArmy.Find(x => x.specialName == armyNum);
        if (RBU == null) // Принципиально новое подразделение появилось
        {
            // .................
        }
        else // Таковое уже есть
        {
            int ind = currentArmy.FindIndex(x => x.specialName == armyNum);
            currentArmy[ind].AddUnits(num);
            currentArmy[ind].curStatus = ResourceController.SquadStatus.onFree;

            int indd = currentUnitBuildings.FindIndex(x => x.buildNum == currentArmy[ind].buildNum);
            currentUnitBuildings[indd].UpdateMyRealUnits();
        }
    }

    public void DeleteArmy(string armyName)
    {
        RealBattleUnit RBU = null;
        RBU = currentArmy.Find(x => x.specialName == armyName);
        if (RBU != null)
        {
            int buildNum = RBU.buildNum;
            int ind = currentUnitBuildings.FindIndex(x => x.buildNum == buildNum);
            int ind2 = currentArmy.FindIndex(x => x.specialName == armyName);
            currentArmy.RemoveAt(ind2);
            //currentArmy.Remove(RBU);
            currentUnitBuildings[ind].RemoveExistingBattleUnit(armyName);
        }
    }

    // ОТРАБОТКА ДЕЙСТВИЙ С БОНУСАМИ

    public void AddNewBonus(RealBattleBonus rbb)
    {
        currentBonuses.Add(rbb);
    }

    public void AddNewBonusToArmy(string bonusName, string squadName, int num)
    {
        int ind = currentArmy.FindIndex(x => x.specialName == squadName);
        if (ind > -1)
        {
            int ind2 = allBattleBonuses.FindIndex(x => x.getBonusName == bonusName);
            currentArmy[ind].AddBonusToUnit(allBattleBonuses[ind2], num);
        }
    }

    public void AddNewBonusToSupply(string bonusName, int bonusVol, int buildingNum)
    {
        // Зададим запрос на производство нового бонуса из очереди на фабрике
        int ind = currentModsBuildings.FindIndex(x => x.buildNum == buildingNum);
        if (ind >= 0)
        {
            currentModsBuildings[ind].CheckForNewProducingBonus();
        }

        // Проверяем, есть ли на складе бонусы с похожим содержанием
        ind = -1;
        ind = currentBonuses.FindIndex(x => x.bb.getBonusName == bonusName);
        if (ind >= 0)
        {
            currentBonuses[ind].AddNewUnits(bonusVol);
        }
        else // Если нет, то создаем их
        {
            RealBattleBonus RBB = new RealBattleBonus(bonusVol, "");
            currentBonuses.Add(RBB);
        }
    }

    // ВСПОМОГАТЕЛЬНЫЕ ФУНКЦИИ ПО ЗАПРОСАМ ИЗВНЕ

    public List<RealBattleUnit> UpdateBuildingUnits(int buildingNum)
    {
        List<RealBattleUnit> returnList = new List<RealBattleUnit>();

        returnList = currentArmy.FindAll(x => x.buildNum == buildingNum);

        return returnList;
    }

    public int GetStateArmyCount(string name)
    {
        int num = allBattleUnits.FindIndex(x => x.getUnitName == name);
        return allBattleUnits[num].getUnitAmount;
    }

    public Vector3 GetStateArmyCost(string name)
    { 
        int num = allBattleUnits.FindIndex(x => x.getUnitName == name);
        return allBattleUnits[num].getUnitCost;
    }

    public int GetStateBonusCount(string name)
    {
        int num = allBattleBonuses.FindIndex(x => x.getBonusName == name);
        return allBattleBonuses[num].getBonusAmount;
    }

    public Vector3 GetStateBonusCost(string name)
    {
        int num = allBattleBonuses.FindIndex(x => x.getBonusName == name);
        return allBattleBonuses[num].getBonusCost;
    }

    public string GetEteSquadName(string battleUnitClassName)
    {
        int ind = 0;
        ind = allUnitNominat.FindIndex(x => x.bu.getUnitName == battleUnitClassName);

        string nm = "";
        nm = allUnitNominat[ind].sqPattern + allUnitNominat[ind].eteNum.ToString();
        squadsNamePattern SNP = allUnitNominat[ind];
        SNP.eteNum = SNP.eteNum + 1;
        allUnitNominat[ind] = SNP;
        return nm;
    }
}
