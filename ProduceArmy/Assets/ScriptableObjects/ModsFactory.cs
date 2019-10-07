using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BonusContent
{
    public List<BattleBonus> actualAttackBonuses;
    public List<BattleBonus> actualDefenceBonuses;
    
    //public List<string> actualAttackBonuses;
    //public List<string> actualDefenceBonuses;
    //public List<Sprite> actualABIcons;
    //public List<Sprite> actualDBIcons;
}

[CreateAssetMenu(fileName = "Mods Factory", menuName = "Mods Factory", order = 55)]
public class ModsFactory : ScriptableObject {

    [SerializeField]
    private int maxLevels;

    [SerializeField]
    private List<Sprite> modsFactorySprite;

    [SerializeField]
    private List<string> modsFactoryName;

    [SerializeField]
    private int primaryCost;

    [SerializeField]
    private List<int> updateCost;

    [SerializeField]
    private float primaryConstructTime;

    [SerializeField]
    private List<float> updateConstructTime;

    [SerializeField]
    private List<BonusContent> fullContent;

    public List<Sprite> getFactorySprite
    {
        get { return modsFactorySprite; }
    }

    public List<string> getFactoryName
    {
        get { return modsFactoryName; }
    }

    public int getPrimaryCost
    {
        get { return primaryCost; }
    }

    public float getPrimaryConstructTime
    {
        get { return primaryConstructTime; }
    }

    public List<int> getUpdateCost
    {
        get { return updateCost; }
    }

    public List<float> getUpdateConstructTime
    {
        get { return updateConstructTime; }
    }

    public int getMaxLevels
    {
        get { return maxLevels; }
    }

    public List<BonusContent> getFullContent
    {
        get { return fullContent; }
    }
}
