using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PossibleBonuse
{
    public BattleBonus unitBonus;
    public int bonusMaxCount;
}


[CreateAssetMenu(fileName = "Battle Unit", menuName = "Battle Unit", order = 52)]
public class BattleUnit : ScriptableObject 
{
    [SerializeField]
    private Sprite unitSprite;

    [SerializeField]
    private string unitName;

    [SerializeField]
    private int unitAmount;

    [SerializeField]
    private Vector3 unitCost;

    [SerializeField]
    private int unitHP;

    [SerializeField]
    private float unitRecruitmentTime;

    [SerializeField]
    private int unitSpeed;

    [SerializeField]
    private Vector4 unitParams;

    [SerializeField]
    private List<PossibleBonuse> fullBonuses;

    public Sprite getUnitSprite
    {
        get { return unitSprite; }
    }

    public string getUnitName
    {
        get { return unitName; }
    }

    public int getUnitAmount
    {
        get { return unitAmount; }
    }

    public Vector3 getUnitCost
    {
        get { return unitCost; }
    }

    public int getUnitHP
    {
        get { return unitHP; }
    }

    public float getUnitRecruitTime
    {
        get { return unitRecruitmentTime; }
    }

    public int getUnitSpeed
    {
        get { return unitSpeed; }
    }

    public Vector4 getUnitParams
    {
        get { return unitParams; }
    }

    public List<PossibleBonuse> getFullBonuses
    {
        get { return fullBonuses; }
    }
}
