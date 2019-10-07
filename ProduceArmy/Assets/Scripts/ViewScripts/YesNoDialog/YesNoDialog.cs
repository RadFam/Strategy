using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesNoDialog : MonoBehaviour {

	public delegate void FuncToDo();
    public FuncToDo ftd;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnableDisable(bool val)
    {
        if (!val)
        {
            ftd = null;
        }
        gameObject.SetActive(val);
    }

    public void OnNoClick()
    {
        OnEnableDisable(false);
    }

    public void OnYesClick()
    {
        ftd();
        OnEnableDisable(false);
    }
}
