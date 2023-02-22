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

    protected void AddPoints(float[] points, float efficiency)
    {
        gameplay += points[0] * efficiency;
        gameDesign += points[1] * efficiency;
        graphics += points[2] * efficiency;
        sound += points[3] * efficiency;
        plot += points[4] * efficiency;
    }

    public virtual float BaseDevelopmentSpeed()
    {
        return 1f;
    }

    public virtual void Develop() 
    {
        
    }

    public virtual void Cancel() // do it abstact
    {

    }
}