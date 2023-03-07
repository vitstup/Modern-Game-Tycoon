using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailUI : MonoBehaviour
{
    public static MailUI instance;

    [SerializeField] private GameObject mailCanvas;

    [SerializeField] private MailPanelScript mailPanelPrefab;
    [SerializeField] private Transform mailsContent;

    [SerializeField] private List<MailPanelScript> mailPanels = new List<MailPanelScript>();


    [SerializeField] private TextMeshProUGUI adressText;

    [SerializeField] private GameObject mailPanel;
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI companyText;
    [SerializeField] private TextMeshProUGUI themeText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI mailText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePanels()
    {
        var mails = new List<Mail>(MailManager.instance.mails);
        mails.Reverse();
        
        for (int i = 0; i < mails.Count; i++)
        {
            if (i >= mailPanels.Count) mailPanels.Add(Instantiate(mailPanelPrefab, mailsContent));
            mailPanels[i].gameObject.SetActive(true);
            mailPanels[i].SetInfo(mails[i]);
        }
        for (int i = mails.Count; i < mailPanels.Count; i++)
        {
            mailPanels[i].gameObject.SetActive(false);
        }
    }

    public void OpenMail(Mail mail)
    {
        mailPanel.SetActive(true);
        senderText.text = mail.sender;
        companyText.text = ""; // change to companyName
        themeText.text = mail.GetTheme();
        dateText.text = mail.GetDate();
        mailText.text = mail.GetMessage(0); // change "0" to current language

        adressText.text = GetMailAdress(mail);
    }

    public void ReturnToMainPage()
    {
        mailPanel.SetActive(false);
        UpdatePanels();
        adressText.text = GetBaseAdress();
    }

    public void OpenMailCanvas()
    {
        mailCanvas.SetActive(true);
        TimeManager.instance.NecessaryPause(true);
        ReturnToMainPage();
    }

    public void DeleteAllMails()
    {
        MailManager.instance.mails.Clear();
        ReturnToMainPage();
    }

    private string GetBaseAdress()
    {
        return "https://<color=#FFFFFF>mail.com<color=#98A2A1>/"; // add company name
    }

    private string GetMailAdress(Mail mail)
    {
        return GetBaseAdress() + mail.adress;
    }
}