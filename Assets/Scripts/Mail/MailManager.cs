using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;

    [SerializeField, TextArea] private string[] mail0;

    private void Awake()
    {
        instance = this;
    }
}