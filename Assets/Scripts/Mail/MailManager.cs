using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;

    [field: SerializeField] public List<Mail> mails { get; private set; } = new List<Mail>();

    private void Awake()
    {
        instance = this;
    }

    public void NewMail(Mail mail)
    {
        mails.Add(mail);
    }

}