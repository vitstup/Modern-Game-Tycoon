using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyStats : MonoBehaviour
{
    public static CompanyStats instance;

    public string companyName { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void SetCompanyName(string name)
    {
        companyName = name;
    }
}