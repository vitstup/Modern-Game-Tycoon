using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PublisherUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite activeImg;
    [SerializeField] private Sprite inactiveImg;

    [SerializeField] private TextMeshProUGUI publisherName;
    [SerializeField] private TextMeshProUGUI auditoryText;
    [SerializeField] private TextMeshProUGUI paymentText;

    private Publisher publisher;

    public void SetInfo(Publisher publisher, GameProject game)
    {
        if (publisher == null) { gameObject.SetActive(false); return; }

        gameObject.SetActive(true);

        publisherName.text = publisher.company;

        auditoryText.text = Localization.Localize("audience") + ": " + TextConvertor.ChangeColor(TextConvertor.numText(publisher.auditory), Color.black);

        int payment = (int)publisher.GetPayment(game.size);
        paymentText.text = Localization.Localize("payment") + ": " + TextConvertor.ChangeColor(TextConvertor.moneyText(payment), Constans.GreenColor);

        this.publisher = publisher;
    }

    public void SelectPublisher()
    {
        PublishersUI.instance.PublisherChoosed(publisher);
    }

    

    private void Handled()
    {
        img.sprite = activeImg;
    }

    public void NotHandled()
    {
        img.sprite = inactiveImg;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Handled();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        NotHandled();
    }
}