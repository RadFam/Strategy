using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionController : MonoBehaviour {

    public enum ProductType { squadProd, bonusProd, buildingProd, buildingUpgrade, upgradeProd };

    public ViewController viewControl; // А может, тут сделать через события, хз...
    
    private List<ProductTask> productsOnWork = new List<ProductTask>();
    private bool checkTaskState;
    private bool notstopProduction = true;

    public static ProductionController instance = null;
    
    // Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        
        productsOnWork.Clear();
        checkTaskState = false;

        if (instance == null)
        {
            instance = this;
        }
	}

    public void AddNewProduct(ProductTask pt)
    {
        productsOnWork.Add(pt);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (notstopProduction)
        {
            for (int i = productsOnWork.Count - 1; i >= 0; --i)
            {
                checkTaskState = productsOnWork[i].AddTime(Time.deltaTime);
                if (checkTaskState)
                {
                    if (productsOnWork[i].prodType == ProductType.squadProd)
                    {
                        ResourceController.instance.AddUnitsToArmy(productsOnWork[i].productName, productsOnWork[i].productUnits); // Это временная мера, потому что еще будут бонусы и постройка/обновление зданий      
                        viewControl.UpdateDialogOne(productsOnWork[i].unitInfo.factoryOwner);
                    }
                    else if (productsOnWork[i].prodType == ProductType.bonusProd)
                    {
                        if (productsOnWork[i].bonusInfo.squadOwner != "") // Бонус уже приписан определенному юниту
                        {
                            ResourceController.instance.AddNewBonusToArmy(productsOnWork[i].productName, productsOnWork[i].bonusInfo.squadOwner, productsOnWork[i].productUnits);
                            viewControl.UpdateModificationDialog(productsOnWork[i].bonusInfo.factoryOwner);
                        }
                        else
                        {
                            ResourceController.instance.AddNewBonusToSupply(productsOnWork[i].productName, productsOnWork[i].productUnits, productsOnWork[i].bonusInfo.factoryOwner);
                            viewControl.UpdateModificationDialog(productsOnWork[i].bonusInfo.factoryOwner);
                        }
                    }
                    else if (productsOnWork[i].prodType == ProductType.buildingProd)
                    {
                    }
                    else if (productsOnWork[i].prodType == ProductType.buildingUpgrade)
                    {
                    }
                    else if (productsOnWork[i].prodType == ProductType.upgradeProd)
                    {
                    }

                    productsOnWork.RemoveAt(i);
                }
            }
        }
	}

    public float GetSpecialProductPercentage(string productName, out int units) // Battle Units
    {
        float ans = 0.0f;
        units = 0;

        //Debug.Log("productName: " + productName);
        ProductTask pt = productsOnWork.Find(x => x.productName == productName);
        if (pt != null)
        {
            ans = pt.GetPercentage();
            units = pt.productUnits;
        }

        return ans;
    }

    public float GetSpecialProductPercentage(string bonusName, string squadName, out int units) // Bonuses of Units
    {
        float ans = 0.0f;
        units = 0;

        ProductTask pt = productsOnWork.Find(x => x.prodType == ProductType.bonusProd && x.productName == bonusName && x.bonusInfo.squadOwner == squadName);
        if (pt != null)
        {
            ans = pt.GetPercentage();
            units = pt.productUnits;
        }

        return ans;
    }

    public float GetSpecialProductPercentage(string bonusName, int factoryNum, out int units) // Bonuses of Factory
    {
        float ans = 0.0f;
        units = 0;

        ProductTask pt = productsOnWork.Find(x => x.prodType == ProductType.bonusProd && x.productName == bonusName && x.bonusInfo.factoryOwner == factoryNum);
        if (pt != null)
        {
            ans = pt.GetPercentage();
            units = pt.productUnits;
        }

        return ans;
    }

    public void DeleteExistingProduct(string prName)
    { 
        // Узнаем, есть, ли продукт с таким именем
        notstopProduction = false;

        ProductTask PT = null;
        PT = productsOnWork.Find(x => x.productName == prName);
        if (PT != null)
        {
            int ind = productsOnWork.FindIndex(x => x.productName == prName);
            productsOnWork.RemoveAt(ind);
        }

        notstopProduction = true;
    }

    public void DeleteExistingProduct_Mods(string prName, int factoryOwner)
    {
        notstopProduction = false;

        ProductTask PT = null;
        PT = productsOnWork.Find(x => x.productName == prName && x.bonusInfo.factoryOwner == factoryOwner);
        if (PT != null)
        {
            int ind = productsOnWork.FindIndex(x => x.productName == prName && x.bonusInfo.factoryOwner == factoryOwner);
            productsOnWork.RemoveAt(ind);
        }

        notstopProduction = true;
    }
}
