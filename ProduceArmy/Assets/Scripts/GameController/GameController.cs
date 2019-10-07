using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void CreateNewSquadTask(string specname, int num, float productspeed, int buildNum) // For battle Units (!!!)
    {
        ProductTask pt = new ProductTask(specname, num, productspeed, ProductionController.ProductType.squadProd);
        pt.SetUnitInfo(buildNum);

        //Debug.Log("Specname: " + specname);
        //Debug.Log("Number: " + num.ToString());
        //Debug.Log("Productspeed: " + productspeed.ToString());
        //Debug.Log("Production Task: " + pt);

        ProductionController.instance.AddNewProduct(pt);
    }

    public void NewTaskProduced()
    {
        
    }

    // ОБРАБОТКА ДЕЙСТВИЙ С ОТРЯДАМИ ЮНИТОВ

    public void CreateNewBattleUnit(string unitName, int numToProd, int buildNum, ref UnitBuilding build) // Имя боевого юнита, ссылка на здание
    {
        // Подыскиваем подходящий BattleUnit SO
        BattleUnit BU = ResourceController.instance.allBattleUnits.Find(x => x.getUnitName == unitName);

        // Имя подразделения придумываем сами (!!!)
        string specName = ResourceController.instance.GetEteSquadName(unitName);

        RealBattleUnit RBU = new RealBattleUnit(specName); // Здесь в качестве ссылки должно даваться уникальное имя подразделения (!!! не unitName !!!)
        RBU.bu = BU;
        RBU.buildNum = build.buildNum;
        RBU.curAmount = 0;
        RBU.curStatus = ResourceController.SquadStatus.onReqruit;

        build.AddNewBattleUnit(RBU);
        ResourceController.instance.AddNewArmy(RBU);

        // Запускаем в производство это подразделение
        CreateNewSquadTask(specName, Mathf.Min(RBU.bu.getUnitAmount, numToProd), RBU.bu.getUnitRecruitTime, buildNum);
    }

    public void UnderstaffBattleUnit(string origName, int numProd, int buildNum) // Уникальное имя боевого подразделения, сколько юнитов нужно добрать
    {
        int ind = ResourceController.instance.currentArmy.FindIndex(x => x.specialName == origName);
        ResourceController.instance.currentArmy[ind].curStatus = ResourceController.SquadStatus.onReqruit;

        RealBattleUnit RBU = ResourceController.instance.currentArmy.Find(x => x.specialName == origName);
        CreateNewSquadTask(origName, numProd, RBU.bu.getUnitRecruitTime, buildNum);
    }

    public void RemoveOldBattleUnit(string specName)
    {
        // Надо удалить из очереди (если там есть)
        ProductionController.instance.DeleteExistingProduct(specName);

        // Удалить из ресурсов
        ResourceController.instance.DeleteArmy(specName);
    }

    // ОБРАБОТКА ДЕЙСТВИЙ С БОНУСАМИ

    public void CreateBonusForDirectUnit(string bonusName, string squadName, int num, int buildNum)
    {
        BattleBonus BB = null;
        BB = ResourceController.instance.allBattleBonuses.Find(x => x.getBonusName == bonusName);

        if (BB != null)
        {
            float productSpeed = BB.getBonusRecruitTime;
            ProductTask pt = new ProductTask(bonusName, num, productSpeed, ProductionController.ProductType.bonusProd);
            pt.SetBonusInfo(squadName, buildNum);

            ProductionController.instance.AddNewProduct(pt);
        }
    }
}
