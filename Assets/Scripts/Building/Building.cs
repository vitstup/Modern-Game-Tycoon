using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private MeshRenderer[] renderers;

    private bool isBuilded = false;
    private bool isMoving = false;

    private List<Collider> obstacles = new List<Collider>();
    private List<Collider> floor = new List<Collider>();

    [SerializeField] private int price;

    [field: SerializeField] public float happiness { get; private set; }

    private void Awake()
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

    public void Put()
    {
        Colorize(Color.white);
        isBuilded = true;
        isMoving = false;
    }

    public void Rotate(bool Right)
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
        if (isBuilded && !isMoving)
        {
            StartCoroutine(TryToMove());
            Debug.Log("Cliked");
        }
    }

    private IEnumerator TryToMove()
    {
        yield return new WaitForFixedUpdate();
        isMoving = true;
        BuildingManager.instance.Move(this);
    }
}