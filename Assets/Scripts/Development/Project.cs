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

    public virtual int GetForefit()
    {
        return 0;
    }

    public virtual float BaseDevelopmentSpeed()
    {
        return 1f;
    }

    public abstract void Develop();

    public virtual void Cancel()
    {
        Debug.Log("Canceled");
    }

    public abstract void Done();

    public abstract void DevelopmentStarted();

    public Project() { }

    public Project(SaveLoad.ProjectSaver saver)
    {
        projectName = saver.name;
        plot = saver.plot;
        gameDesign = saver.gameDesign;
        gameplay = saver.gameplay;
        graphics = saver.graphics;
        sound = saver.sound;
    } 
}