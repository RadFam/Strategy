using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitImage : MonoBehaviour, IPointerClickHandler
{

    public ProduceDialogOne myControlParent;
    public int myNumber;
    public Sprite mySprite;
    public Sprite defaultSprite;
    public Image myImage;
    public Image selectedFrame;
    public Image statusImage;
    public Text squadName;
    public Text squadStatus;
    public Text squadQuantity;

    [SerializeField]
    private List<Sprite> squadState = new List<Sprite>();
    private ResourceController.SquadStatus myStatus;

    void Start()
    {
        //myNumber = 0;
        //myImage = gameObject.GetComponent<Image>();
        //mySprite = defaultSprite;
        //myImage.sprite = mySprite;
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

    public void SetSquadData(string squad)
    {
        squadName.text = squad;
    }

    public void SetQuantityData(int num)
    {
        squadQuantity.text = num.ToString();
    }

    public void SetStatusData()
    { 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        
        if (eventData.button == PointerEventData.InputButton.Left && clickCount == 1)
        {
            myControlParent.OnImageUnitClick(myNumber);
        }
        else if (eventData.button == PointerEventData.InputButton.Left && clickCount == 2)
        {
            myControlParent.OnImageUnitDoubleClick(myNumber);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (myStatus == ResourceController.SquadStatus.onFree)
            {
                myControlParent.OnImageUnitRightClick(myNumber);
            }
        }
    }

    public void SelectedState(bool state)
    {
        selectedFrame.gameObject.SetActive(state);
    }

    public void SetStatus(ResourceController.SquadStatus sq)
    {
        myStatus = sq;
        statusImage.sprite = squadState[(int)myStatus];
    }
    
    /*
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            myControlParent.OnImageUnitClick(myNumber);
        }
        if (Input.GetMouseButtonUp(1))
        {
            myControlParent.OnImageUnitRightClick(myNumber);
        }
    }
    */
}
