using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadInfoDialog : MonoBehaviour {

    public Image squadImage;
    public Text squadName;
    public Text squadStatus;
    public Text squadNumber;
    public Text squadHP;
    public Text squadSpeed;
    public Text squadParams;
    public Text squadBonuses;

    public void OnDialogEnable(bool vol)
    {
        gameObject.SetActive(vol);
    }

    public void SetSquadInfo(string squadNm)
    {
        RealBattleUnit RBU = null;
        RBU = ResourceController.instance.currentArmy.Find(x => x.specialName == squadNm);

        if (RBU != null)
        {
            squadImage.sprite = RBU.bu.getUnitSprite;

            squadName.text = squadNm;

            squadStatus.text = "Статус: " + RBU.curStatus.ToString();

            squadNumber.text = "Численность: " + RBU.curAmount.ToString();

            squadHP.text = "HP: " + RBU.bu.getUnitHP.ToString();

            squadSpeed.text = "Скорость: " + RBU.bu.getUnitSpeed.ToString();

            squadParams.text = "LA: " + RBU.bu.getUnitParams.x.ToString() + "  HD: " + RBU.bu.getUnitParams.y.ToString() + "  LD: " + RBU.bu.getUnitParams.z.ToString() + "  HD: " + RBU.bu.getUnitParams.w.ToString();

            // Заполняем список имеющихся бонусов данного отряда
            string bInfo = "";
            for (int i = 0; i < RBU.GetCurrBonuses.Count; ++i)
            {
                bInfo = bInfo + RBU.GetCurrBonuses[i].unitBonus.getBonusName + ": " + RBU.GetCurrBonuses[i].currnetCount.ToString() + "\n";
            }
            squadBonuses.text = bInfo;
        }
    }
}
