using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModificationProdDialog : MonoBehaviour 
{
    public ViewController upViewController;
    public ProduceDialogTwo pdTwo;
    public YesNoDialog ynDlg;
    
    public AttackMods aMods;
    public DefenceMods dMods;
    public ProducingMods pMods;
    public Transform aParent;
    public Transform dParent;
    public Transform pParent;

    public Slider productionSlider;
    public Text productCount;

    private bool isActive = false;
    private int attackClicked = 0;
    private int defenceClicked = 0;
    private int producingClicked = 0;
    private string currentBonusSelected = "";

    private BonusBuilding currFactory;
    private int currFactoryNum;

    private List<AttackMods> attackElems = new List<AttackMods>();
    private List<DefenceMods> defenceElems = new List<DefenceMods>();
    private List<ProducingMods> produceElems = new List<ProducingMods>();

    private float updatePeriod = 0.5f;
    private float updateTime = 0.0f;
    //private bool sessionOn = false; 

    public int CBN
    {
        get { return currFactoryNum; }
    }

    public void SetCurrentBuilding(int buildNum)
    {
        currFactory = ResourceController.instance.currentModsBuildings.Find(x => x.buildNum == buildNum);
        currFactoryNum = buildNum;
    }

    public void OnAttackImageClick(int num, string nm) // подсветка рамкой, если просто выделена
    {
        attackClicked = num;
        currentBonusSelected = nm;
    }

    public void OnAttackImageDoubleClick(int num, string nm) // вывод информации по бонусу
    {
        attackClicked = num;
        currentBonusSelected = nm;
    }

    public void OnAttackImageRightClick(int num, string nm) // вызов диалога для производства
    {
        attackClicked = num;
        currentBonusSelected = nm;

        int val = ResourceController.instance.GetStateBonusCount(currentBonusSelected);

        pdTwo.gameObject.SetActive(true);
        pdTwo.ftd = GetDataFromDlg2;
        pdTwo.SetUnits(val);
    }

    public void OnDefenceImageClick(int num, string nm)
    {
        defenceClicked = num;
        currentBonusSelected = nm;
    }

    public void OnDefenceImageDoubleClick(int num, string nm)
    {
        defenceClicked = num;
        currentBonusSelected = nm;
    }

    public void OnDefenceImageRightClick(int num, string nm)
    {
        defenceClicked = num;

        int val = ResourceController.instance.GetStateBonusCount(nm);

        pdTwo.gameObject.SetActive(true);
        pdTwo.ftd = GetDataFromDlg2;
        pdTwo.SetUnits(val);
    }

    public void OnProducingImageClick(int num, string nm)
    {
        producingClicked = num;
    }

    public void OnProducingImageRightClick(int num, string nm)
    {
        producingClicked = num;
        ynDlg.OnEnableDisable(true);
        ynDlg.ftd = ConfirmDeleteSquadButton;
    }

    public void SelectAttackFrame(int num)
    {
        foreach (AttackMods am in attackElems)
        {
            if (am.myNum == num)
            {
                am.SetFrameActive(true);
            }
            else
            {
                am.SetFrameActive(false);
            }
        }
    }

    public void SelectDefenceFrame(int num)
    {
        foreach (DefenceMods dm in defenceElems)
        {
            if (dm.myNum == num)
            {
                dm.SetFrameActive(true);
            }
            else
            {
                dm.SetFrameActive(false);
            }
        }
    }

    public Vector3 GetModificationData(string modName)
    {
        Vector3 ans = new Vector3(0.0f, 0.0f, 0.0f);
        ans = ResourceController.instance.GetStateBonusCost(modName);
        return ans;
    }

    public Vector3 GetModificationProdData(string modName, int num)
    {
        Vector3 ans = new Vector3(0.0f, 0.0f, 0.0f);
        ans = ResourceController.instance.GetStateBonusCost(modName);
        ans = ans * currFactory.producingBonuses[num].val;
        return ans;
    }

    public void GetDataFromDlg2(int num)
    {
        if (num != 0)
        {
            // Создаем продукт
            // И ставим его в очередь на производство
            ResourceController.instance.currentModsBuildings.Find(x => x.buildNum == currFactory.buildNum).AddProducingBonus(currentBonusSelected, num);

            StaticUpdate(currFactory.buildNum);
        }
    }

    public void ConfirmDeleteSquadButton()
    {
        // Просто удалаяем бонус из очереди
        currFactory.RemoveProducingBonus(producingClicked);
        StaticUpdate(currFactory.buildNum);
    }

    public void StaticUpdate(int buildNum)
    {
        ClearInnerData();

        currFactory = ResourceController.instance.currentModsBuildings.Find(x => x.buildNum == buildNum);
        currFactoryNum = buildNum;
        currentBonusSelected = currFactory.attackBonuses[0].getBonusName;

        // attack bonuses
        for (int i = 0; i < currFactory.attackBonuses.Count; ++i)
        {
            AttackMods obj = Instantiate(aMods);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(aParent);
            obj.SetData(currFactory.attackBonuses[i].getBonusSprite, currFactory.attackBonuses[i].getBonusName, i);
            attackElems.Add(obj);
        }

        // defence bonuses
        for (int i = 0; i < currFactory.defenceBonuses.Count; ++i)
        {
            DefenceMods obj = Instantiate(dMods);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(dParent);
            obj.SetData(currFactory.defenceBonuses[i].getBonusSprite, currFactory.defenceBonuses[i].getBonusName, i);
            defenceElems.Add(obj);
        }

        for (int i = 0; i < currFactory.producingBonuses.Count; ++i)
        {
            ProducingMods obj = Instantiate(pMods);
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(pParent);
            obj.SetData(currFactory.producingBonuses[i].prodBonus.getBonusSprite, currFactory.producingBonuses[i].prodBonus.getBonusName, i);
            produceElems.Add(obj);
        }
    }

    public void DynamicUpdate()
    {
        float ans = 0.0f;
        int outUnits = 0;

        if (produceElems.Count > 0)
        {
            ans = ProductionController.instance.GetSpecialProductPercentage(currFactory.producingBonuses[0].prodBonus.getBonusName, currFactory.buildNum, out outUnits);
            
            productionSlider.value = ans;
            if (ans != 0.0f)
            {
                int result = (int)(ans * outUnits);
                productCount.text = result.ToString();
            }
            else
            {
                productCount.text = "";
            }
        }
    }

    public void ClearInnerData()
    { 
        attackClicked = 0;
        defenceClicked = 0;
        producingClicked = 0;
        currentBonusSelected = "";
        currFactoryNum = -1;

        currFactory = null;
        for (int i = 0; i < attackElems.Count; ++i)
        {
            Destroy(attackElems[i].gameObject);
        }
        for (int i = 0; i < defenceElems.Count; ++i)
        {
            Destroy(defenceElems[i].gameObject);
        }
        for (int i = 0; i < produceElems.Count; ++i)
        {
            Destroy(produceElems[i].gameObject);
        }
        attackElems.Clear();
        defenceElems.Clear();
        produceElems.Clear();

        updateTime = 0.0f;
    }
    public void OnExit()
    {
        ClearInnerData();
        upViewController.DlgMod = false;
        gameObject.SetActive(false);
    }
}
