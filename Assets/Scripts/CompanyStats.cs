using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyStats : MonoBehaviour
{
    public static CompanyStats instance;

    [SaveIsEasy.QuickAccess] private string gameVersion;
    public string companyName { get; private set; }

    private void Awake()
    {
        instance = this;
        gameVersion = Application.version;
    }

    public void SetCompanyName(string name)
    {
        companyName = name;
    }
}