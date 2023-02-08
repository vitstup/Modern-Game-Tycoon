using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI instance;

    [SerializeField] private ShopItemScript shopItemPrefab;
    [SerializeField] private Transform content;

    [SerializeField] private Building[] buildings;
    [SerializeField] private Sprite[] buildingSprites;

    [SerializeField] private GameObject ShopPanel;

    [SerializeField] private TextMeshProUGUI happinessText;

    [SerializeField] private GameObject autoFurniturePanel;
    [SerializeField] private TextMeshProUGUI officeName;
    [SerializeField] private TextMeshProUGUI expenses;

    private ShopItemScript[] shopItems;

    private void Awake()
    {
        instance = this;
    }

    private void Start() => SetShopPanels();

    private void SetShopPanels()
    {
        if (buildings.Length != buildingSprites.Length) Debug.Log("Wrong Count of building sprites");
        shopItems = new ShopItemScript[buildings.Length];
        for (int i = 0; i < buildings.Length; i++)
        {
            shopItems[i] = Instantiate(shopItemPrefab, content);
            shopItems[i].SetInfo(buildings[i], buildingSprites[i], i);
        }
    }

    public void Build(Building building)
    {
        ShopPanel.SetActive(false);
        autoFurniturePanel.SetActive(false);
        TimeManager.instance.ChangeRunStatus(RunStatus.building);
        TimeManager.instance.ChangeSpeed(1);
        BuildingManager.instance.Build(building);
    }

    public void OpenShopPanel()
    {
        ShopPanel.SetActive(true);
        TimeManager.instance.ChangeRunStatus(RunStatus.stoped);
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        BuildingManager.instance.CalculateHappiness();
        happinessText.text = Localization.Localize("happiness") + ": " + TextConvertor.percentText(BuildingManager.instance.happiness);
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].UpdatePrice();
        }
    }

    public void UpdateAutoFurnishInfo()
    {
        officeName.text = Localization.Localize("office." + OfficeManager.instance.currentOffice);
        expenses.text = Localization.Localize("expenses") + ": " + TextConvertor.moneyText(GetAutoFurnitureExpenses());
    }

    public int GetAutoFurnitureExpenses()
    {
        int expenses = 0;
        var furniture = OfficeManager.instance.GetCurrentOffice().autoFurniture; /// getting future furniture price
        var buildings = furniture.GetComponentsInChildren<Building>();
        foreach (Building building in buildings)
        {
            if (building is Table) (building as Table).SetModernPc();
            expenses += building.GetPrice();
        }
        buildings = BuildingManager.instance.itemsParent.GetComponentsInChildren<Building>(); /// getting current furniture price
        foreach (Building building in buildings)
        {
            expenses -= building.GetPrice() / 2;
        }
        return expenses;
    }
}