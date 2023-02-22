using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public override void Develop()
    {
        CheckStage();

        bool isPolishing = currentStage is PolishingStage;

        var workers = RosterManager.instance.GetAssignWorkers();

        float[] workValues = new float[6];

        for (int i = 0; i < workers.Length; i++)
        {
            var work = workers[i].skills.GenerateRandom(isPolishing);
            work.value *= workers[i].DevelopmentSpeed(BaseDevelopmentSpeed());
            workValues[work.skillId] += work.value;

            if(work.skillId != 5) PointsManager.instance.ShowPoint(workers[i].table.transform, work.skillId);
            else PointsManager.instance.ShowPoint(workers[i].table.transform, work.value > 0? 5: 6);
        }

        currentStage.workloadDone += workValues.Sum();

        AddPoints(workValues, currentEfficiency);
        bugs += workValues[5];
        if (bugs < 0) bugs = 0;


        Debug.Log(currentEfficiency + " eff, " + bugs + " bugs, " + workValues[5] + " today bugs");
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
        if (currentStage is PolishingStage) return 0.2f;
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