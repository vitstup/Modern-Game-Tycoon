using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameUpdate : Project
{
    public Game updateGame;
    public int updateSize;

    [field: SerializeField] public float bugs { get; private set; }

    [field: SerializeField] public int workload { get; private set; }
    [field: SerializeField] public float workloadDone { get; private set; }

    public Platform[] platformsAdded = new Platform[4];

    public bool isPolishing;

    public Platform[] GetUsedPlatforms()
    {
        Platform[] used = new Platform[4];

        for (int i = 0; i < used.Length; i++)
        {
            if (platformsAdded[i] != null) used[i] = platformsAdded[i];
            if (updateGame == null) continue;
            if (updateGame.platforms[i] != null) used[i] = updateGame.platforms[i];
        }

        return used;
    }

    public override void DevelopmentStarted()
    {
        workload = Constans.updateSizesWorkloads[updateSize] * Constans.sizesScale[updateGame.size];
        bugs = updateGame.bugs;
        if (updateSize == 0) projectName = "small";
        else if (updateSize == 1) projectName = "medium";
        else if (updateSize == 2) projectName = "big";
    }

    public override void Develop()
    {
        float efficincy = 1f;
        if (isPolishing) efficincy = 0.2f;

        var workers = RosterManager.instance.GetAssignWorkers();

        float[] workValues = new float[6];

        for (int i = 0; i < workers.Length; i++)
        {
            var work = workers[i].skills.GenerateRandom(isPolishing);
            work.value *= workers[i].DevelopmentSpeed(BaseDevelopmentSpeed());
            workValues[work.skillId] += work.value;

            if (work.skillId != 5) PointsManager.instance.ShowPoint(workers[i].table.transform, work.skillId, work.value * efficincy);
            else PointsManager.instance.ShowPoint(workers[i].table.transform, work.value > 0 ? 5 : 6, work.value);
        }

        workloadDone += workValues.Sum();

        AddPoints(workValues, efficincy);
        bugs += workValues[5];
        if (bugs < 0) bugs = 0;

        if (!isPolishing && ReadyPercent() >= 0.99f) UpdateDoneUI.instance.OpenUpdateDone(this);
    }

    public override void Done()
    {
        float interestBuff = Constans.updateSizesInterestBuffs[updateSize];
        updateGame.UpdateGame(plot, gameDesign, gameplay, graphics, sound, bugs, interestBuff, platformsAdded);
    }

    public float ReadyPercent()
    {
        return workloadDone / workload;
    }

    public GameUpdate() { }

    public GameUpdate(SaveLoad.GameUpdateSaver saver) : base(saver)
    {
        updateGame = Statistics.instance.games[saver.updateGame];
        updateSize = saver.updateSize;
        bugs = saver.bugs;
        workload = saver.workload;
        workloadDone = saver.workloadDone;
        platformsAdded = new Platform[saver.platformsAdded.Length];
        for (int i = 0; i < platformsAdded.Length; i++)
        {
            if (saver.platformsAdded[i] >= 0) platformsAdded[i] = PlatformsManager.instance.platforms[saver.platformsAdded[i]];
        }
        isPolishing = saver.isPolishing;
    }
}