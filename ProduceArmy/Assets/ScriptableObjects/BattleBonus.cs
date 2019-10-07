using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle Bonus", menuName = "Battle Bonus", order = 53)]
public class BattleBonus : ScriptableObject
{

    [SerializeField]
    private Sprite bonusSprite;

    [SerializeField]
    private string bonusName;

    [SerializeField]
    private int bonusAmount;

    [SerializeField]
    private Vector3 bonusCost;

    [SerializeField]
    private float bonusRecruitmentTime;

    public Sprite getBonusSprite
    {
        get { return bonusSprite; }
    }

    public string getBonusName
    {
        get { return bonusName; }
    }

    public int getBonusAmount
    {
        get { return bonusAmount; }
    }

    public Vector3 getBonusCost
    {
        get { return bonusCost; }
    }

    public float getBonusRecruitTime
    {
        get { return bonusRecruitmentTime; }
    }
}
