using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProducingMods : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public ModificationProdDialog myControlParent;
    public CostTooltipOne myToolTip;

    public Image mainImage;
    public Sprite mySprite;
    public string myName;
    public int myNum;

    public void SetData(Sprite spr, string name, int currNum)
    {
        mySprite = spr;
        myName = name;
        myNum = currNum;

        mainImage.sprite = mySprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            myControlParent.OnProducingImageClick(myNum, myName);
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            myControlParent.OnProducingImageRightClick(myNum, myName);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 val = myControlParent.GetModificationProdData(myName, myNum);
        myToolTip.OnShowTooltip(val);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myToolTip.OnCloseTooltip();
    }
}
