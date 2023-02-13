using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequelDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private Game[] possibleForSequels;

    private void Awake() => dropdown = GetComponent<TMP_Dropdown>();

    private void Start() => UpdateVariants();


    public void UpdateVariants()
    {
        // do something
    }
}