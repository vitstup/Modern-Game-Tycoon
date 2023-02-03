using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class OfficePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI officeName;
    [SerializeField] private TextMeshProUGUI square;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI rentPrice;

    [SerializeField] private Image OfficeImg;

    [SerializeField] private Button buyBtn;
    [SerializeField] private Button sellBtn;
    [SerializeField] private Button rentBtn;
    [SerializeField] private Button selectBtn;

    private Office office;

    public void SetInfo(Office office, int id)
    {
        this.office = office;
        officeName.text = Localization.Localize("office." + id);
        OfficeImg.sprite = office.sprite;
        StringBuilder sb = new StringBuilder();
            /// square text
        sb.Append(Localization.Localize("square"));
        sb.Append(" ");
        sb.Append(TextConvertor.ChangeColorAndSize(office.square.ToString(), Color.black, 20));
        sb.Append("m\u00B2");
        square.text = sb.ToString();
        sb.Clear();
            /// Price text
        sb.Append(Localization.Localize("purchprice"));
        sb.Append(" ");
        sb.Append(TextConvertor.ChangeColorAndSize(TextConvertor.moneyText(office.price), Color.red, 20));
        price.text = sb.ToString();
        sb.Clear();
            /// Rent text
        sb.Append(Localization.Localize("rentprice"));
        sb.Append(" ");
        sb.Append(TextConvertor.ChangeColorAndSize(TextConvertor.moneyText(office.rentPrice), Color.red, 20));
        rentPrice.text = sb.ToString();
        sb.Clear();
    }

    public void UpdateBtns(bool selected)
    {
        bool enoughMoney = office.price <= Main.instance.money;
        if (office.state == OfficeState.none)
        {
            if (enoughMoney) ChangeBtnStyle(buyBtn, true, "buy");
            else ChangeBtnStyle(buyBtn, false, "buy");
            ChangeBtnStyle(sellBtn, false, "sell");
            ChangeBtnStyle(rentBtn, true, "rent");
            if (selected) ChangeBtnStyle(selectBtn, false, "selected");
            else ChangeBtnStyle(selectBtn, false, "select");
        }
        else if (office.state == OfficeState.rented)
        {
            if (enoughMoney) ChangeBtnStyle(buyBtn, true, "buy");
            else ChangeBtnStyle(buyBtn, false, "buy");
            ChangeBtnStyle(sellBtn, false, "sell");
            if (selected) { ChangeBtnStyle(selectBtn, false, "selected"); ChangeBtnStyle(rentBtn, false, "rented"); }
            else { ChangeBtnStyle(selectBtn, true, "select"); ChangeBtnStyle(rentBtn, true, "rented"); }
        }
        else if (office.state == OfficeState.bought)
        {
            ChangeBtnStyle(buyBtn, false, "bought");
            ChangeBtnStyle(rentBtn, false, "rent");
            if (selected) { ChangeBtnStyle(selectBtn, false, "selected"); ChangeBtnStyle(sellBtn, false, "sell"); }
            else { ChangeBtnStyle(selectBtn, true, "select"); ChangeBtnStyle(sellBtn, true, "sell"); }
        }
    }

    private void ChangeBtnStyle(Button btn, bool enabled, string text)
    {
        btn.interactable = enabled;
        btn.GetComponentInChildren<TextMeshProUGUI>().text = Localization.Localize(text);
        btn.GetComponent<Image>().color = enabled ? Constans.GreenColor : Constans.GrayBgInactive;
    }

    public void Buy()
    {
        Main.instance.MinusMoney(office.price);
        office.state = OfficeState.bought;
        OfficesUI.instance.UpdatePanels();
    }

    public void Sell()
    {
        Main.instance.AddMoney(office.price);
        office.state = OfficeState.none;
        OfficesUI.instance.UpdatePanels();
    }

    public void Rent()
    {
        if (office.state == OfficeState.rented) office.state = OfficeState.none;
        else office.state = OfficeState.rented;
        OfficesUI.instance.UpdatePanels();
    }

    public void Select()
    {
        OfficeManager.instance.ChangeCurrentOffice(office);
        OfficesUI.instance.UpdatePanels();
    }

}