using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    [SerializeField] private PointScript prefab;
    [SerializeField] private Transform pointsParent;

    [SerializeField] private Transform pointsTarget;

    private List<PointScript> points = new List<PointScript>();
    private List<PointScript> activePoints = new List<PointScript>();

    private void Awake()
    {
        instance = this;
        InitializePoints();
    }

    private void InitializePoints()
    {
        for (int i = 0; i < Constans.PointsPool; i++)
        {
            points.Add(Instantiate(prefab, pointsParent));
            points[i].gameObject.SetActive(false);
        }
    }

    public void ShowPoint(Transform table, int sprite, float value)
    {
        var point = points.Count > 0? points[0] : activePoints[0];
        points.Remove(point);
        activePoints.Add(point);
        point.Show(table.position, pointsTarget.position, sprite, value);
    }

    public void AddToPool(PointScript point)
    {
        points.Add(point);
        activePoints.Remove(point);
    }
}