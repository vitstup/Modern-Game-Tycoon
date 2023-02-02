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

    private ShopItemScript[] shopItems;

    private void Awake()
    {
        instance = this;
        SetShopPanels();
    }

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
        TimeManager.instance.ChangeRunStatus(RunStatus.building);
        BuildingManager.instance.Build(building);
    }

    public void OpenShopPanel()
    {
        ShopPanel.SetActive(true);
        TimeManager.instance.ChangeRunStatus(RunStatus.stoped);
    }

    public void UpdateInfo()
    {
        // update happines
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].UpdatePrice();
        }
    }
}