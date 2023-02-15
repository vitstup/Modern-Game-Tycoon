using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Project 
{
    public string projectName;

    [field: SerializeField] public int plot { get; private set; }
    [field: SerializeField] public int gameDesign { get; private set; }
    [field: SerializeField] public int gameplay { get; private set; }
    [field: SerializeField] public int graphics { get; private set; }
    [field: SerializeField] public int sound { get; private set; }

    public void Develop() // dont know, is i really need this void 
    {

    }

    public void Cancel() // do it abstact
    {

    }
}