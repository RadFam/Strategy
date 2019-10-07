using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceDialogOne : MonoBehaviour {

    public ViewController upViewController;
    public ProduceDialogTwo pdTwo;
    public YesNoDialog ynDlg;
    public SquadInfoDialog sqID;
    
    //public Image unitImagePref;
    public UnitImage unitImagePref;
    public UnitImage_2 unitImagePref_2;
    public UnitImage_3 unitImagePref_3;
    public Transform uipParent;
    public Transform uipParent_2;
    public Transform uipParent_3;

    public Image bonusImagePref;
    public Transform bipParent;

    public Slider productionSlider;
    public Text productCount;

    public Sprite emptyUnitSprite;

    private bool isActive = false;
    private int unitClicked = 0;
    private int unitClicked_2 = 0;
    private int bonusClicked = 0;
    private bool bonusFocus = false;
    private UnitBuilding currBuild;
    private int currBuildNum;

    private List<UnitImage> squadElements = new List<UnitImage>();
    private List<UnitImage_2> squadElements_2 = new List<UnitImage_2>();
    private List<UnitImage_3> squadElements_3 = new List<UnitImage_3>();

    private List<string> avialableBounses = new List<string>();

    private float updatePeriod = 0.5f;
    private float updateTime = 0.0f;
    private bool sessionOn = false;

    public int CBN
    {
        get { return currBuildNum; }
    }

    // Temporary function
    public void SetCurrentBuilding(int buildNum)
    {
        currBuild = ResourceController.instance.currentUnitBuildings.Find(x => x.buildNum == buildNum);
        currBuildNum = buildNum;
    }

    public bool Active
    {
        get { return isActive; }
        set { isActive = value; }
    }
    
    // Use this for initialization
	void Start () {
		
	}

    public void OnImageUnitClick(int num)
    {
        unitClicked = num;
        BonusOfUnitUpdate();
        bonusFocus = false;
        Debug.Log("Picked out unit slot: " + num.ToString());
        for (int i = 0; i < squadElements.Count; ++i)
        {
            if (i != num)
            {
                squadElements[i].SelectedState(false);
            }
            else
            {
                squadElements[i].SelectedState(true);
            }
        }
    }

    public void OnImageUnitDoubleClick(int num)
    {
        if (unitClicked < currBuild.Squads)
        {
            sqID.OnDialogEnable(true);
            sqID.SetSquadInfo(currBuild.buildingUnits[unitClicked].specialName);
        }
    }

    public void OnImageUnitRightClick(int num)
    {
        bonusFocus = false;

        unitClicked = num;
        BonusOfUnitUpdate();
        // Если в слоте уже есть какие-то войска (ОБЯЗАТЕЛЬНО ЭТО ПРОВЕРИТЬ!)

        if (unitClicked < currBuild.Squads)
        {
            pdTwo.gameObject.SetActive(true);
            pdTwo.ftd = GetDataFromDlg2;
            pdTwo.SetUnits(currBuild.buildingUnits[unitClicked].NeedUnits());
        }
    }

    public void OnImageUnitClick2(int num)
    { 
        // появляется инфа по данному виду юнитов // пока это не нужно
        unitClicked_2 = num;
    }

    public void OnImageUnitRightClick2(int num)
    {
        unitClicked_2 = num;
        //Debug.Log("Need to open by right click");
        //Debug.Log("unitClicked: " + unitClicked.ToString() + "  currBuild.Squads: " + currBuild.Squads.ToString() + "  currBuild.numSlots: " + currBuild.numSlots.ToString());

        if ((unitClicked >= currBuild.Squads) && (unitClicked < currBuild.numSlots))
        {
            string unitNm = currBuild.GetSquadName(num);
            int fullVol = ResourceController.instance.GetStateArmyCount(unitNm);
            // Запускаем окно 2
            pdTwo.gameObject.SetActive(true);
            pdTwo.ftd = GetDataFromDlg2;
            pdTwo.SetUnits(fullVol);
        }
    }

    public void OnButtonReqruitClick()
    {
        bonusFocus = false;

        Debug.Log("Reqruit button is clicked");

        if (unitClicked < currBuild.Squads)
        {
            pdTwo.gameObject.SetActive(true);
            pdTwo.ftd = GetDataFromDlg2;
            pdTwo.SetUnits(currBuild.buildingUnits[unitClicked].NeedUnits());
        }
    }

    public void OnImageBonusClick(int num)
    {
        bonusClicked = num;
        bonusFocus = true;
        Debug.Log("Picked out bonus slot: " + num.ToString());
    }

    public void OnImageBonusRightClick(int num)
    {
        bonusClicked = num;
        bonusFocus = true;
        Debug.Log("Picked out bonus slot: " + num.ToString());

        if (unitClicked < currBuild.Squads) // До этого мы выделили уже существующий отряд
        {
            int val = currBuild.buildingUnits[unitClicked].NeedBonus(currBuild.namesOfActualBonuses[bonusClicked]);
            
            pdTwo.gameObject.SetActive(true);
            pdTwo.ftd = GetDataFromDlg2;
            pdTwo.SetUnits(val);
        }
    }

    void Update()
    { 
        // Обновляем вид диалогового окна в зависимости от того, в каком состоянии сейчас находится процесс производства
        if (isActive)
        {
            updateTime += Time.deltaTime;
            if (updateTime >= updatePeriod)
            {
                UpdateDynamic();
                updateTime = 0.0f;
            }
        }
    }

    //public void UpdateStatic(UnitBuilding UB)
    public void UpdateStatic(int buildNum)
    {
        ClearInnerData();
        
        currBuild = ResourceController.instance.currentUnitBuildings.Find(x => x.buildNum == buildNum);
        currBuildNum = buildNum;
        // currBuild = UB; // Вообще-то, тут еще надо предусмотреть возможность затирания массивов картинок
        // Debug.Log("currBuild.buildingUnits.Count " + currBuild.buildingUnits.Count.ToString());
        for (int i = 0; i < currBuild.buildingUnits.Count; ++i)
        {
            UnitImage img = Instantiate(unitImagePref);
            img.gameObject.SetActive(true);
            img.transform.SetParent(uipParent, false);
            img.SetNumber(i);
            img.SetSprite(currBuild.buildingUnits[i].bu.getUnitSprite); // Вроде так 
            img.SetSquadData(currBuild.buildingUnits[i].specialName);
            img.SetQuantityData(currBuild.buildingUnits[i].curAmount);
            img.SetStatus(currBuild.buildingUnits[i].curStatus);
            squadElements.Add(img);
        }
        for (int i = currBuild.buildingUnits.Count; i < currBuild.numSlots; ++i)
        {
            UnitImage img = Instantiate(unitImagePref);
            img.gameObject.SetActive(true);
            img.transform.SetParent(uipParent, false);
            img.SetNumber(i);
            img.SetSprite(emptyUnitSprite); // Вроде так
            squadElements.Add(img);
        }

        for (int i = 0; i < currBuild.iconsOfActualUnits.Count; ++i)
        {
            UnitImage_2 img = Instantiate(unitImagePref_2);
            img.gameObject.SetActive(true);
            img.transform.SetParent(uipParent_2, false);
            img.SetNumber(i);
            img.SetSprite(currBuild.iconsOfActualUnits[i]);
            squadElements_2.Add(img);
        }

        for (int i = 0; i < currBuild.iconsOfActualBonus.Count; ++i)
        {
            UnitImage_3 img = Instantiate(unitImagePref_3);
            img.gameObject.SetActive(true);
            img.transform.SetParent(uipParent_3, false);
            img.SetBonusData(currBuild.namesOfActualBonuses[i], i, currBuild.iconsOfActualBonus[i]);
            img.AvailableIcon(true);
            squadElements_3.Add(img);
        }

        BonusOfUnitUpdate();

        /*
        for (int i = 0; i < currBuild.namesOfActualBonuses.Count; ++i)
        {
            Image img = Instantiate(bonusImagePref);
            img.transform.parent = bipParent;
        }
        */

        if (!sessionOn)
        {
            squadElements[0].SelectedState(true);
        }
        sessionOn = true;
    }

    public void UpdateDynamic()
    {
        float ans = 0.0f;
        int outUnits = 0;
        if (bonusFocus)
        {
            // смотрим данные по производству выделенного бонуса
            ans = ProductionController.instance.GetSpecialProductPercentage(currBuild.namesOfActualBonuses[bonusClicked], currBuild.buildingUnits[unitClicked].specialName, out outUnits);
        }
        else
        {
            // смотрим данные по производству выделенного юнита
            if (unitClicked < currBuild.buildingUnits.Count)
            {
                ans = ProductionController.instance.GetSpecialProductPercentage(currBuild.buildingUnits[unitClicked].specialName, out outUnits);
                squadElements[unitClicked].SetStatus(currBuild.buildingUnits[unitClicked].curStatus);
                squadElements[unitClicked].SetQuantityData(currBuild.buildingUnits[unitClicked].curAmount);
            }
        }
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

    // Прорисовываем бонусы, доступные для выделенного конкретного боевого отряда
    private void BonusOfUnitUpdate()
    {
        avialableBounses.Clear();

        if (unitClicked < currBuild.Squads)
        {
            avialableBounses = currBuild.buildingUnits[unitClicked].GetAvailableBonuses();
        }

        for (int i = 0; i < squadElements_3.Count; ++i)
        {
            int ind = avialableBounses.FindIndex(x => x == squadElements_3[i].myName);
            if (ind != -1)
            {
                squadElements_3[i].AvailableIcon(false);
            }
            else
            {
                squadElements_3[i].AvailableIcon(true);
            }
        }
    }

    private void BonusOfUnitDynamicUpdate()
    {
        string unitNm = currBuild.namesOfActualUnits[unitClicked];
        string bonusNm = "";
        int units = 0;
        float ans = 0.0f;

        for (int i = 0; i < squadElements_3.Count; ++i)
        {
            bonusNm = squadElements_3[i].myName;
            int ind = avialableBounses.FindIndex(x => x == bonusNm);
            if (ind != -1)
            { 
                // Ставим иконке статус занята или нет
                ans = ProductionController.instance.GetSpecialProductPercentage(bonusNm, unitNm, out units);
                if (ans != 0.0f)
                {
                    squadElements_3[i].SetIsProducting(true);
                    if ((bonusFocus) && i == bonusClicked)
                    {
                        productionSlider.value = ans;
                        int result = (int)(ans * units);
                        productCount.text = result.ToString();
                    }
                }
                else
                {
                    squadElements_3[i].SetIsProducting(false);
                }
            }
        }
    }

    public void GetDataFromDlg2(int num)
    {
        if (!bonusFocus) // Задание на постройку армии
        {
            if ((unitClicked < currBuild.buildingUnits.Count) && (unitClicked < currBuild.numSlots)) // Дособираем отряд
            {
                upViewController.GameCtrl.UnderstaffBattleUnit(currBuild.buildingUnits[unitClicked].specialName, num, currBuildNum);
            }
            if ((unitClicked >= currBuild.buildingUnits.Count) && (unitClicked < currBuild.numSlots)) // Создаем новый отряд
            {
                string unitNm = currBuild.GetSquadName(unitClicked_2);
                upViewController.GameCtrl.CreateNewBattleUnit(unitNm, num, currBuildNum, ref currBuild);
            }
        }
        if (bonusFocus) // Задание на постройку бонуса для отряда
        {
            upViewController.GameCtrl.CreateBonusForDirectUnit(currBuild.namesOfActualBonuses[bonusClicked], currBuild.buildingUnits[unitClicked].specialName, num, currBuildNum);
        }

        UpdateStatic(currBuild.buildNum);
    }

    public Vector3 GetIconTwoData(int num)
    {
        return ResourceController.instance.GetStateArmyCost(currBuild.namesOfActualUnits[num]);
    }

    public Vector3 GetIconThreeData(string name)
    {
        return ResourceController.instance.GetStateBonusCost(name);
    }

    public void OnOpenClose(bool value)
    {
        isActive = value;
        if (value)
        {
            // Open window
            gameObject.SetActive(true);
        }
        else
        {
            // Delete all unit images
            ClearInnerData();
            upViewController.DlgOne = false;
            // Close window
            gameObject.SetActive(false);
        }
    }

    private void ClearInnerData()
    {
        unitClicked = 0;
        unitClicked_2 = 0;
        bonusClicked = 0;
        bonusFocus = false;
        currBuildNum = -1;
        
        currBuild = null;
        for (int i = 0; i < squadElements.Count; ++i)
        {
            Destroy(squadElements[i].gameObject);
        }
        for (int i = 0; i < squadElements_2.Count; ++i)
        {
            Destroy(squadElements_2[i].gameObject);
        }
        for (int i = 0; i < squadElements_3.Count; ++i)
        {
            Destroy(squadElements_3[i].gameObject);
        }
        squadElements.Clear();
        squadElements_2.Clear();
        squadElements_3.Clear();
        avialableBounses.Clear();

        updateTime = 0.0f;
        sessionOn = false;
    }

    public void OnDeleteSquadClick()
    {
        ynDlg.OnEnableDisable(true);
        ynDlg.ftd = ConfirmDeleteSquadButton;
    }

    public void ConfirmDeleteSquadButton()
    {
        upViewController.GameCtrl.RemoveOldBattleUnit(currBuild.buildingUnits[unitClicked].specialName);
        int num = currBuild.buildNum;
        UpdateStatic(num);
    }
}
