using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private MeshRenderer[] renderers;

    private bool isBuilded = false;

    private List<Collider> obstacles = new List<Collider>();
    private List<Collider> floor = new List<Collider>();

    [SerializeField] private int price;

    [field: SerializeField] public float happiness { get; private set; }

    protected virtual void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void Colorize(Color color)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            for (int m = 0; m < renderers[i].materials.Length; m++)
            {
                renderers[i].materials[m].color = color;
            }
        }

    }

    public virtual void Put()
    {
        BuildingManager.instance.interactedThisUpdate = true;
        Colorize(Color.white);
        if (!isBuilded) OpenShopPanel();
        else TimeManager.instance.ChangeRunStatus(RunStatus.standart);
        isBuilded = true;
    }

    public virtual void Rotate(bool Right)
    {
        if (Right) transform.Rotate(0, 45, 0);
        else transform.Rotate(0, -45, 0);
    }

    public virtual void Delete()
    {
        // if builded, return some money
        Destroy(gameObject);
    }

    public virtual int GetPrice()
    {
        return price;
    }

    public bool isAvailable()
    {
        //obstacles.RemoveAll(Collider => Collider == null);   // uncoment this if have problem, that builded obj during moving can't be builded
        return (obstacles.Count == 0 && floor.Count > 0)? true : false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Building") { obstacles.Add(collider); }
        if (collider.tag == "Floor") { floor.Add(collider); }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Building") { obstacles.Remove(collider); }
        if (collider.tag == "Floor") { floor.Remove(collider); }
    }

    private void OnMouseDown()
    {
        if (isBuilded)
        {
            if (TimeManager.instance.runStatus != RunStatus.standart) return;
            if (BuildingManager.instance.currentBuilding == null && !BuildingManager.instance.interactedThisUpdate) Move();
        }
    }

    protected virtual void Move()
    {
        obstacles.RemoveAll(Collider => Collider == null);
        BuildingManager.instance.Move(this);
    }

    private void OpenShopPanel()
    {
        ShopUI.instance.OpenShopPanel();
    }

}