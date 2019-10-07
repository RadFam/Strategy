using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnBuildClick : MonoBehaviour {

    public Camera cam;
    public int buildingNum;
    public ViewController VC;

    private bool isClicked = false;
    private RaycastHit hit;
    private Ray ray;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonUp(0) && (!isClicked))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // Видимо, пока временная мера
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject == this.gameObject)
                    {
                        isClicked = true;
                        //Debug.Log(hit.transform.name);
                        VC.OnShowDialogOne(buildingNum);
                        StartCoroutine(ReloadCoroutine());
                    }
                }
            }
        }
	}

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        isClicked = false;
    }
}
