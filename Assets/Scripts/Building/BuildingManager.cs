using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Camera mainCamera;

    private Building currentBuilding;

    private void Awake()
    {
        //grid = new Building[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
    }

    public void Build(Building building)
    {
        // maybe delete previos
        currentBuilding = Instantiate(building);
    }

    private void Update()
    {
        if (currentBuilding != null)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = currentBuilding.isAvailable();

                currentBuilding.Colorize(available ? Color.green : Color.red);

                currentBuilding.transform.position = new Vector3(x, 0, y);

                if (available && Input.GetMouseButtonDown(0))
                {
                    currentBuilding.Put();
                    currentBuilding = null;
                }
            }
        }
    }
}
