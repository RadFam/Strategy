using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackMods : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ModificationProdDialog myControlParent;
    public CostTooltipOne myToolTip;

    public Image mainImage;
    public Image frameImage;
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

    public void SetFrameActive(bool val)
    {
        frameImage.gameObject.SetActive(val);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (eventData.button == PointerEventData.InputButton.Left && clickCount == 1)
        {
            myControlParent.OnAttackImageClick(myNum, myName);
        }
        else if (eventData.button == PointerEventData.InputButton.Left && clickCount == 2)
        {
            myControlParent.OnAttackImageDoubleClick(myNum, myName);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            myControlParent.OnAttackImageRightClick(myNum, myName);
        }

        myControlParent.SelectAttackFrame(myNum);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 val = myControlParent.GetModificationData(myName);
        myToolTip.OnShowTooltip(val);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myToolTip.OnCloseTooltip();
    }
}
