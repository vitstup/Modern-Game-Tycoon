using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingManager : MonoBehaviour
{
    public class boolEvent : UnityEvent<bool> { }
    public static boolEvent buildingSomething = new boolEvent();

    public static BuildingManager instance;

    private Camera mainCamera;

    [HideInInspector] public Building currentBuilding { get; private set; }

    [HideInInspector] public bool interactedThisUpdate = false;

    [field: SerializeField] public Transform itemsParent { get; private set; }

    [HideInInspector] public float happiness = 1f;
    [HideInInspector] public List<Building> buildings { get; private set; } = new List<Building>();

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        TimeManager.DayUpdateEvent.AddListener(CalculateHappiness);
    }

    public void Build(Building building)
    {
        currentBuilding = Instantiate(building, itemsParent);
        buildingSomething?.Invoke(true);
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

                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Destroy(currentBuilding.gameObject);
                    TimeManager.instance.ChangeRunStatus(RunStatus.standart);
                    buildingSomething?.Invoke(false);
                }

                if (available && Input.GetMouseButtonDown(0) && !interactedThisUpdate)
                {
                    currentBuilding.Put();
                    currentBuilding = null;
                    buildingSomething?.Invoke(false);
                }
            }
        }
        interactedThisUpdate = false;
    }

    public void Move(Building building)
    {
        interactedThisUpdate = true;
        currentBuilding = building;
        TimeManager.instance.ChangeRunStatus(RunStatus.building);
        TimeManager.instance.ChangeSpeed(1);
        buildingSomething?.Invoke(true);
    }

    public void CalculateHappiness()
    {
        happiness = 1;
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i] != null) happiness += buildings[i].happiness;
        }
    }

    public void AutoFurniture(bool takeMoney)
    {
        Helpers.DeleteChilds(itemsParent);
        var furniture = OfficeManager.instance.GetCurrentOffice().autoFurniture;
        var buildings = furniture.GetComponentsInChildren<Building>();
        foreach (Building prefab in buildings)
        {
            var building = Instantiate(prefab, itemsParent);
            building.SetBuilding(takeMoney);
        }
    }

}