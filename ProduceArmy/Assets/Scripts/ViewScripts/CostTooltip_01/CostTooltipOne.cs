using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostTooltipOne : MonoBehaviour {

    public Text goldVal;
    public Text oreVal;
    public Text crstVal;

    private int gV = 0;
    private int oV = 0;
    private int cV = 0;
    private bool active = false;
    private Vector2 addCoords;

    [SerializeField]
    private Camera uiCamera;

    public void OnShowTooltip(Vector3 costInfo)
    {
        gameObject.SetActive(true);
        gV = (int)costInfo.x;
        oV = (int)costInfo.y;
        cV = (int)costInfo.z;

        goldVal.text = gV.ToString();
        oreVal.text = oV.ToString();
        crstVal.text = cV.ToString();

        active = true;

        RectTransform rtr = gameObject.transform.GetComponent<RectTransform>();
        addCoords = new Vector3(rtr.rect.width / 3 + 5, rtr.rect.height / 3 + 5);

        Update();
    }

    public void OnCloseTooltip()
    {
        active = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
            transform.localPosition = localPoint + addCoords;
    }
}
