using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitImage_3 : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ProduceDialogOne myControlParent;
    public CostTooltipOne myToolTip;
    public string myName;
    public int myNum;
    public Sprite mySprite;
    public Image busyMaskImage;
    public Image shadowImage;
    public Image myImage;

    public bool isProducing;
    public bool availability;

    public void SetBonusData(string nm, int num, Sprite spr)
    {
        myNum = num;
        myName = nm;
        mySprite = spr;
        myImage.sprite = spr;
    }

    public void AvailableIcon(bool val)
    {
        availability = val;
        if (val)
        {
            shadowImage.gameObject.SetActive(false);
            myImage.sprite = mySprite;
        }
        else 
        {
            shadowImage.gameObject.SetActive(true);
        }
    }

    public void SetIsProducting(bool val)
    {
        isProducing = val;
        busyMaskImage.gameObject.SetActive(val);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (availability)
            { 
                // если бонус доступен для данного отряда
                myControlParent.OnImageBonusClick(myNum);
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!isProducing && availability) // Если не занято и бонус доступен для данного отряда
            {
                myControlParent.OnImageBonusRightClick(myNum);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isProducing && availability) // Если не занято и бонус доступен для данного отряда
        {
            Vector3 val = myControlParent.GetIconThreeData(myName);
            myToolTip.OnShowTooltip(val); 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isProducing && availability) // Если не занято и бонус доступен для данного отряда
        {
            myToolTip.OnCloseTooltip(); 
        }
    }
}
