using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    public static ProjectManager instance;

    public Project project;

    public int maxSizeGameCreated; // use this to generate contracts 

    private void Awake()
    {
        instance = this;
    }
}