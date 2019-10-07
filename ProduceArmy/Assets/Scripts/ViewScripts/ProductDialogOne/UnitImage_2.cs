using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitImage_2 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ProduceDialogOne myControlParent;
    public CostTooltipOne myToolTip;
    public int myNumber;
    public Sprite mySprite;
    public Image myImage;
	
    // Use this for initialization
	void Start () {
		
	}

    public void SetNumber(int num)
    {
        myNumber = num;
    }

    public void SetSprite(Sprite spr)
    {
        mySprite = spr;
        myImage.sprite = spr;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            myControlParent.OnImageUnitClick2(myNumber);
        else if (eventData.button == PointerEventData.InputButton.Right)
            myControlParent.OnImageUnitRightClick2(myNumber);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 val = myControlParent.GetIconTwoData(myNumber);
        myToolTip.OnShowTooltip(val);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myToolTip.OnCloseTooltip();
    }
}
