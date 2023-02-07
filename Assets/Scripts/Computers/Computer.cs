using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Computer
{
    [field: SerializeField] public int price { get; private set; }
    [field: SerializeField] public float productionBonus { get; private set; }
    [field: SerializeField] public float unlockYear { get; private set; }
}