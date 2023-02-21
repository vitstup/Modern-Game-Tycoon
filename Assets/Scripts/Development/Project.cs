using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Project 
{
    public string projectName;

    [field: SerializeField] public float plot { get; private set; }
    [field: SerializeField] public float gameDesign { get; private set; }
    [field: SerializeField] public float gameplay { get; private set; }
    [field: SerializeField] public float graphics { get; private set; }
    [field: SerializeField] public float sound { get; private set; }

    public virtual float BaseDevelopmentSpeed()
    {
        return 1f;
    }

    public virtual void Develop(float[] points) 
    {
        plot += points[0];
        gameDesign += points[1];
        gameplay += points[2];
        graphics += points[3];
        sound += points[4];
    }

    public void Cancel() // do it abstact
    {

    }
}