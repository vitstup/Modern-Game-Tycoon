using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Office 
{
    [field: SerializeField] public GameObject officeObj { get; private set; }

    [field: SerializeField] public Sprite sprite { get; private set; }

    [field: SerializeField] public int square { get; private set; }

    [field: SerializeField] public int price { get; private set; }

    [field: SerializeField] public int rentPrice { get; private set; }

    public OfficeState state;
}