using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private MeshRenderer[] renderers;

    private bool isBuilded = false;

    private List<Collider> obstacles = new List<Collider>();
    private List<Collider> floor = new List<Collider>();

    [field: SerializeField] public int price { get; private set; }

    [field: SerializeField] public float happiness { get; private set; }

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void Colorize(Color color)
    {
        if (renderers == null) return;
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
        Debug.Log("Clicked");
    }
}