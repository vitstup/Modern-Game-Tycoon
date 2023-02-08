using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFurnitureHelper : MonoBehaviour
{
    [SerializeField] private Transform[] group1;
    [SerializeField] private Transform[] group2;

    [ContextMenu("Change transform")]
    private void ApplyFirstGroupTransformToSecond()
    {
        if (group1.Length != group2.Length) Debug.LogWarning("Not same count");
        for (int i = 0; i < group1.Length; i++)
        {
            group2[i].localPosition = group1[i].localPosition;
            group2[i].localRotation = group1[i].localRotation;
        }
    }
}