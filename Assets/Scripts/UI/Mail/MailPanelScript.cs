using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailPanelScript : MonoBehaviour
{
    [SerializeField] private Image Bg;
    [SerializeField] private Sprite incactiveBg;
    [SerializeField] private Sprite activeBg;

    [SerializeField] private TextMeshProUGUI sender;
    [SerializeField] private TextMeshProUGUI theme;
    [SerializeField] private TextMeshProUGUI date;

    [SerializeField] private Image favouriteImg;
    [SerializeField] private Sprite incactiveFavourite;
    [SerializeField] private Sprite activeFavourite;

    private Mail mail;

    public void SetInfo(Mail mail)
    {
        Bg.sprite = mail.wasOpened ? activeBg : incactiveBg;

        sender.text = mail.sender;
        theme.text = mail.GetTheme();
        date.text = mail.GetDate();

        this.mail = mail;

        UpdateFavourite();
    }

    public void SetFavourite()
    {
        mail.isFavourite = !mail.isFavourite;
        UpdateFavourite();
    }

    public void DeleteMail()
    {
        MailManager.instance.mails.Remove(mail);
        MailUI.instance.UpdatePanels();
    }

    public void OpenMail()
    {
        mail.wasOpened = true;
        MailUI.instance.OpenMail(mail);
    }

    private void UpdateFavourite()
    {
        favouriteImg.sprite = mail.isFavourite ? activeFavourite : incactiveFavourite;
    }
}