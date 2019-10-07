using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public ProduceDialogOne pdOne;
    public ModificationProdDialog pdMod;
    public GameController GameCtrl;

    private bool showDialogOne;
    private bool showDialogMod;

    public bool DlgOne
    {
        get { return showDialogOne; }
        set { showDialogOne = value; }
    }

    public bool DlgMod
    {
        get { return showDialogMod; }
        set { showDialogMod = value; }
    }
    
    // Use this for initialization
	void Start () {
		showDialogOne = false;
        showDialogMod = false;
	}

    public void OnSHowButtonClick()
    {
        showDialogOne = !showDialogOne;
        pdOne.gameObject.SetActive(showDialogOne);
        pdOne.Active = showDialogOne;
    }

    public void OnShowDialogOne(int bn)
    {
        //showDialogOne = !showDialogOne;
        //pdOne.gameObject.SetActive(showDialogOne);
        //pdOne.Active = showDialogOne;
        pdOne.OnOpenClose(true);
        pdOne.UpdateStatic(bn);
        showDialogOne = true;
    }

    public void OnShowDialogMod(int bn)
    {
        // Тут должна быть инициализация вызова диалога производства бонусов-модификатов
        // ......
        // ......
        showDialogMod = true;
    }

    public void UpdateDialogOne(int bn)
    {
        if ((showDialogOne) && (pdOne.CBN == bn))
        {
            pdOne.UpdateStatic(bn); // Номер здания
        }
    }

    public void UpdateModificationDialog(int bn)
    {
        if ((showDialogMod) && (pdMod.CBN == bn))
        {
            pdMod.StaticUpdate(bn);
        }
    }

}
