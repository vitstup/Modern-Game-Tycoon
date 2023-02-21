using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameProject : Project
{
    public int size;
    public Engine engine;
    public GenreInfo genre;
    public ThemeInfo theme;
    public Platform[] platforms = new Platform[4];

    private PrototypingStage prototyping = new PrototypingStage();
    private DevelopingStage developing = new DevelopingStage();
    private DesignStage design = new DesignStage();
    private PolishingStage polishing = new PolishingStage();

    [field: SerializeField] public Stage currentStage { get; private set; }

    [field: SerializeField] public float bugs { get; private set; }

    // reviews and user score

    public Sprite sprite;

    [field: SerializeField] public float currentEfficiency { get; private set; }

    public override float BaseDevelopmentSpeed()
    {
        return engine.info.developmentSpeed;
    }

    public override void Develop(float[] points)
    {
        CheckStage();
        base.Develop(points);
        bugs += points[5];

        currentStage.workloadDone += points[0];
        currentStage.workloadDone += points[1];
        currentStage.workloadDone += points[2];
        currentStage.workloadDone += points[3];
        currentStage.workloadDone += points[4];
    }

    public void CheckStage()
    {
        if (currentStage == null || currentStage.StageDone()) ChangeStage();
    }

    private void ChangeStage()
    {
        if (currentStage == null) currentStage = prototyping;
        else if (currentStage is PrototypingStage) currentStage = developing;
        else if (currentStage is DevelopingStage) currentStage = design;
        else if (currentStage is DesignStage) currentStage = polishing;

        if (currentStage is PolishingStage) GameReadyUI.instance.OpenGameReady(this);
        else ProjectUI.instance.OpenStageCanvas();
    }

    public int GetComputingUsage()
    {
        return prototyping.GetComputeUsage() + developing.GetComputeUsage() + design.GetComputeUsage();
    }

    public int GetGraphicsUsage()
    {
        return prototyping.GetGraphicsUsage() + developing.GetGraphicsUsage() + design.GetGraphicsUsage();
    }

    private float DevelopmentEfficiency() // can use this method instead "currentEfficiency", but it will be less optimized
    {
        if (currentStage is PolishingStage) return 1;
        else
        {
            float efficiency = 1;
            efficiency += genre.GetThemeBonus(theme);
            efficiency += currentStage.StageEfficiency(this);
            return efficiency;
        }
    }

    public void SetEfficiency()
    {
        currentEfficiency = DevelopmentEfficiency();
    }
}