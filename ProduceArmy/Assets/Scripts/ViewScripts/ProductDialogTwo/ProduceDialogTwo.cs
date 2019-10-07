using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProduceDialogTwo : MonoBehaviour {

    public ViewController upViewController;
    public ProduceDialogOne upParent;

    public delegate void FuncWhatToDo(int value);
    public FuncWhatToDo ftd;

    private int maxUnits;
    private int buildUnits;
    public Text staticLabel;
    public Text dynamicLabel;
    public Slider slider;

    private bool isActive;

    public bool Active
    {
        get { return isActive; }
        set { isActive = value; }
    }

    // Use this for initialization
	void Start () 
    {
        ResetFunc();
	}

    public void SetUnits(int vol)
    {
        maxUnits = vol;
        staticLabel.text = "MAX: " + maxUnits.ToString();
    }

    void Update()
    {
        OnSliderUpdate();
    }

    public void OnSliderUpdate()
    {
        buildUnits = (int)(slider.value * maxUnits);
        dynamicLabel.text = buildUnits.ToString();
    }

    public void OnProduceButtonClick()
    {
        // Надо передать сведения о том, сколько юнитов нужно начать строить
        if (buildUnits != 0)
        {
            //upParent.GetDataFromDlg2(buildUnits);
            ftd(buildUnits);
        }

        ResetFunc();
        gameObject.SetActive(false);
    }

    void ResetFunc()
    {
        buildUnits = 0;
        slider.value = 0.0f;
        ftd = null;
    }
}
