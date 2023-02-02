using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class ShopItemScript : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private Image bg;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI price;

    [SerializeField] private TextMeshProUGUI happinesValue;
    [SerializeField] private TextMeshProUGUI happinesText;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image happinesImage;

    private Building building;


    private void Awake()
    {
        bg = GetComponent<Image>();
    }

    public void SetInfo(Building building, Sprite sprite, int itemId)
    {
        this.building = building;
        itemName.text = Localization.Localize("shp." + itemId);
        price.text = TextConvertor.moneyText(building.GetPrice());
        happinesValue.text = building.happiness.ToString();
        itemImage.sprite = sprite;
    }

    public void UpdatePrice()
    {
        price.text = TextConvertor.moneyText(building.GetPrice());
    }

    public void Build()
    {
        Unpointed();
        ShopUI.instance.Build(building);

    }

    public void OnPointerEnter(PointerEventData eventData) => Pointed();

    public void OnPointerExit(PointerEventData eventData) => Unpointed();

    private void Pointed()
    {
        bg.enabled = true;
        itemName.color = Color.black;
        happinesValue.color = Color.black;
        happinesImage.color = Color.black;
        happinesText.color = Constans.BlackTextInactive;

        itemImage.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    private void Unpointed()
    {
        bg.enabled = false;
        itemName.color = Color.white;
        happinesValue.color = Color.white;
        happinesImage.color = Color.white;
        happinesText.color = Constans.WhiteTextInactive;

        itemImage.transform.localScale = Vector3.one;
    }
}