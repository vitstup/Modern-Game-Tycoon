using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static Statistics instance;
    [field: SerializeField] public List<Game> games { get; private set; } = new List<Game>();

    private void Awake()
    {
        instance = this;
    }
}