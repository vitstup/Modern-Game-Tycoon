using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    private Camera mainCamera;

    private Building currentBuilding;

    private void Awake()
    {
        instance = this;
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

                //int x = Mathf.RoundToInt(worldPosition.x);
                //int y = Mathf.RoundToInt(worldPosition.z);
                float x = (float)System.Math.Round(worldPosition.x / 0.5) * 0.5f;
                float y = (float)System.Math.Round(worldPosition.z / 0.5) * 0.5f;

                bool available = currentBuilding.isAvailable();

                currentBuilding.Colorize(available ? Color.green : Color.red);

                currentBuilding.transform.position = new Vector3(x, 0, y);

                if (Input.GetKeyDown(KeyCode.Q)) currentBuilding.Rotate(false);
                if (Input.GetKeyDown(KeyCode.R)) currentBuilding.Rotate(true);

                if (Input.GetKeyDown(KeyCode.Delete)) currentBuilding.Delete();

                if (available && Input.GetMouseButtonDown(0))
                {
                    currentBuilding.Put();
                    currentBuilding = null;
                }
            }
        }
    }

    public void Move(Building building)
    {
        if (currentBuilding == null) currentBuilding = building;
    }

    private void ChangeBuildingStatus(bool building)
    {
        if (building)
        {
            TimeManager.instance.ChangeRunStatus(RunStatus.building);
        }
        else
        {
            TimeManager.instance.ChangeRunStatus(RunStatus.standart);
        }
    }
}
