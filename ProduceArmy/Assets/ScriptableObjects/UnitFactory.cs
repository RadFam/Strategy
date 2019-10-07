using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Runtime.Serialization;
//using HelpDetails;

[System.Serializable]
public struct ActualContent
{
    public List<string> actualUnits;
    public List<string> actualBonuses;
    public List<Sprite> actualIcons;
    public List<Sprite> actualBonusIcons;
}

[CreateAssetMenu(fileName = "Unit Factory", menuName = "Unit Factory", order = 54)]
public class UnitFactory : ScriptableObject
{
    [SerializeField]
    private int maxLevels;

    [SerializeField]
    private List<int> levelSlots;
    
    [SerializeField]
    private List<Sprite> factorySprite;

    [SerializeField]
    private List<string> factoryName;

    [SerializeField]
    private int primaryCost;

    [SerializeField]
    private List<int> updateCost;

    [SerializeField]
    private float primaryConstructTime;

    [SerializeField]
    private List<float> updateConstructTime;

    [SerializeField]
    private List<ActualContent> fullContent;

    public List<Sprite> getFactorySprite
    {
        get { return factorySprite; }
    }

    public List<string> getFactoryName
    {
        get { return factoryName; }
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

    public List<int> getLevelSlots
    {
        get { return levelSlots; }
    }

    public List<float> getUpdateConstructTime
    {
        get { return updateConstructTime; }
    }

    public int getMaxLevels
    {
        get { return maxLevels; }
    }

    public List<ActualContent> getFullContent
    {
        get { return fullContent; }
    }
}
