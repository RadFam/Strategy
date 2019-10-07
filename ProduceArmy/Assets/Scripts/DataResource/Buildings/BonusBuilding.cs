using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ProdMods
{
    public BattleBonus prodBonus;
    public int val;
    public string specName;
}

[System.Serializable]
public class BonusBuilding
{
    public ModsFactory modsBase;

    public int currLevel;
    public int numSlotsAttack;
    public int numSlotsDefence;
    public int buildNum;

    public List<BattleBonus> attackBonuses;
    public List<BattleBonus> defenceBonuses;
    public List<ProdMods> producingBonuses;

    public void InitBuilding(int cl)
    {
        attackBonuses = new List<BattleBonus>();
        defenceBonuses = new List<BattleBonus>();
        producingBonuses = new List<ProdMods>();

        currLevel = cl;

        UpdateMyRealBonuses();
    }

    public void UpdateMyRealBonuses()
    {
        attackBonuses.Clear();
        defenceBonuses.Clear();

        attackBonuses = modsBase.getFullContent[currLevel].actualAttackBonuses;
        defenceBonuses = modsBase.getFullContent[currLevel].actualDefenceBonuses;

        numSlotsAttack = attackBonuses.Count;
        numSlotsDefence = defenceBonuses.Count;
    }

    public void LevelUp()
    {
        currLevel += 1;
        currLevel = Mathf.Max(modsBase.getMaxLevels, currLevel);
        UpdateMyRealBonuses();
    }

    public void RemoveProducingBonus(int num)
    {
        if (num >= 1)
        {
            if (producingBonuses.Count >= 2)
            {
                producingBonuses.RemoveAt(num);
            }
        }
        if (num == 0) // удаляем производимый бонус и убираем из списка продукт
        {
            ProductionController.instance.DeleteExistingProduct_Mods(producingBonuses[0].specName, buildNum);
            producingBonuses.RemoveAt(num);
        }
    }

    public void AddProducingBonus(string name, int val)
    {
        BattleBonus bb = ResourceController.instance.allBattleBonuses.Find(x => x.getBonusName == name);
        ProdMods PM = new ProdMods();
        PM.prodBonus = bb;
        PM.val = val;
        //PM.specName = "";
        PM.specName = bb.getBonusName;
        producingBonuses.Add(PM);
        if (producingBonuses.Count == 1) // Только началось заполнение
        {
            // Составляем задачу и обращаемся к Production Controller
            MakeNewProduct();
        }
    }

    public void CheckForNewProducingBonus()
    {
        producingBonuses.RemoveAt(0);
        if (producingBonuses.Count > 0)
        {
            MakeNewProduct();
        }
    }

    public void MakeNewProduct()
    {
        //string prodName = ResourceController.instance.GetEteModificationName(producingBonuses[0].prodBonus.getBonusName);
        string prodName = producingBonuses[0].prodBonus.getBonusName;
        int val = producingBonuses[0].val;
        float productspeed = producingBonuses[0].prodBonus.getBonusRecruitTime;

        ProdMods PM = producingBonuses[0];
        PM.specName = prodName;
        producingBonuses[0] = PM;

        ProductTask pt = new ProductTask(prodName, val, productspeed, ProductionController.ProductType.bonusProd);
        pt.SetBonusInfo("", buildNum);
        ProductionController.instance.AddNewProduct(pt);
    }
}
