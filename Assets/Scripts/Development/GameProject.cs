using UnityEngine;
using System.Linq;

[System.Serializable]
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

    public int expenses;

    public Reviews reviews;

    public Sprite sprite;

    [field: SerializeField] public float currentEfficiency { get; private set; }

    public override void Done()
    {
        ReviewChance.SetStartReviews(this);
    }

    public override void DevelopmentStarted()
    {
        CheckStage();
        SetSprite();
    }

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

            if(work.skillId != 5) PointsManager.instance.ShowPoint(workers[i].table.transform, work.skillId, work.value * currentEfficiency);
            else PointsManager.instance.ShowPoint(workers[i].table.transform, work.value > 0? 5: 6, work.value);
        }

        currentStage.workloadDone += workValues.Sum();

        AddPoints(workValues, currentEfficiency);
        bugs += workValues[5];
        if (bugs < 0) bugs = 0;


        //Debug.Log(currentEfficiency + " eff, " + bugs + " bugs, " + workValues[5] + " today bugs");
        //ReviewChance.PositiveReviewChance(this);
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

    public string[,] GetPrototypingFeaturesInfo() { return prototyping.GetGroupsLocalization(); }
    public string[,] GetDevelopingFeaturesInfo() { return developing.GetGroupsLocalization(); }
    public string[,] GetDesignFeaturesInfo() { return design.GetGroupsLocalization(); }

    public Stage[] GetStages()
    {
        return new Stage[] { prototyping , developing, design, polishing};
    }

    public int GetCurrentStageId()
    {
        if (currentStage is PrototypingStage) return 0;
        else if (currentStage is DevelopingStage) return 1;
        else if (currentStage is DesignStage) return 2;
        else if (currentStage is PolishingStage) return 3;
        return -1;
    }

    private float DevelopmentEfficiency() // can use this method instead "currentEfficiency", but it will be less optimized
    {
        if (currentStage is PolishingStage) return 0.2f;
        else
        {
            float efficiency = 1;
            efficiency += genre.GetThemeBonus(theme);
            efficiency += currentStage.StageEfficiency(this);
            if (engine.info.genre == genre) efficiency += 0.25f;
            return efficiency;
        }
    }

    public void SetEfficiency()
    {
        currentEfficiency = DevelopmentEfficiency();
    }

    private void SetSprite()
    {
        sprite = genre.sprites[Random.Range(0, genre.sprites.Length)];
    }

    protected void UpdateBugs(float bugs)
    {
        this.bugs = bugs;
    }

    public GameProject() { }

    public GameProject(SaveLoad.GameProjectSaver saver) : base(saver)
    {
        size = saver.size;
        engine = AttributesManager.instance.engines[saver.engine];
        genre = AttributesManager.instance.genres[saver.genre];
        theme = AttributesManager.instance.themes[saver.theme];
        platforms = new Platform[saver.platforms.Length];
        for (int i = 0; i < platforms.Length; i++)
        {
            if (saver.platforms[i] >= 0) platforms[i] = PlatformsManager.instance.platforms[saver.platforms[i]];
        }
        bugs = saver.bugs;
        prototyping = new PrototypingStage(saver.prototyping);
        developing = new DevelopingStage(saver.developing);
        design = new DesignStage(saver.design);

        if (saver.currentStageId == 0) currentStage = prototyping;
        else if (saver.currentStageId == 1) currentStage = developing;
        else if (saver.currentStageId == 2) currentStage = design;
        else if (saver.currentStageId == 3) currentStage = polishing;

        expenses = saver.expenses;
        reviews = saver.reviews;
        sprite = genre.sprites[saver.spriteId];

        SetEfficiency();
    }
}