using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    [SerializeField] private PointScript prefab;
    [SerializeField] private Transform pointsParent;

    [SerializeField] private Transform pointsTarget;

    private PointScript[] points;

    private void Awake()
    {
        instance = this;
        InitializePoints();
    }

    private void InitializePoints()
    {
        points = new PointScript[100];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = Instantiate(prefab, pointsParent);
            points[i].gameObject.SetActive(false);
        }
    }

    public void ShowPoint(Transform table)
    {
        var point = Instantiate(prefab, pointsParent);
        point.Show(table.position, pointsTarget.position);
    }
}