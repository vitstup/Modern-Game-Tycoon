using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Freelance : Project
{
    [field: SerializeField] public int plotNeeded { get; private set; }
    [field: SerializeField] public int gameDesignNeeded { get; private set; }
    [field: SerializeField] public int gameplayNeeded { get; private set; }
    [field: SerializeField] public int graphicsNeeded { get; private set; }
    [field: SerializeField] public int soundNeeded { get; private set; }

    [field: SerializeField] public int payment { get; private set; }
    [field: SerializeField] public int term { get; private set; }
    [field: SerializeField] public int penalty { get; private set; }


    [field: SerializeField] public int developmentDuration { get; private set; }

    public Freelance(int mainTask)
    {
        int maxScale = Constans.sizesScale[ProjectManager.instance.maxSizeGameCreated];
        int mainPointsNeeded = Random.Range(Constans.minContractPoints, Constans.maxContractPoints * maxScale);

        switch (mainTask) // setting main task points needed, setting freelance name
        {
            case 0:
                plotNeeded = mainPointsNeeded;
                projectName = Constans.freelancePlotNames[Random.Range(0, Constans.freelancePlotNames.Length)];
                break;
            case 1:
                gameDesignNeeded = mainPointsNeeded;
                projectName = Constans.freelanceGamedesignNames[Random.Range(0, Constans.freelanceGamedesignNames.Length)];
                break;
            case 2:
                gameplayNeeded = mainPointsNeeded;
                projectName = Constans.freelanceProgrammingNames[Random.Range(0, Constans.freelanceProgrammingNames.Length)];
                break;
            case 3:
                graphicsNeeded = mainPointsNeeded;
                projectName = Constans.freelanceGraphicsNames[Random.Range(0, Constans.freelanceGraphicsNames.Length)];
                break;
            case 4:
                soundNeeded = mainPointsNeeded;
                projectName = Constans.freelanceSoundNames[Random.Range(0, Constans.freelanceSoundNames.Length)];
                break;
        }

        // setting other points needed
        if (plotNeeded == 0) plotNeeded = GetOtherPoint(mainPointsNeeded, maxScale);
        if (gameDesignNeeded == 0) gameDesignNeeded = GetOtherPoint(mainPointsNeeded, maxScale);
        if (gameplayNeeded == 0) gameplayNeeded = GetOtherPoint(mainPointsNeeded, maxScale);
        if (graphicsNeeded == 0) graphicsNeeded = GetOtherPoint(mainPointsNeeded, maxScale);
        if (soundNeeded == 0) soundNeeded = GetOtherPoint(mainPointsNeeded, maxScale);

        // setting other parametrs
        payment = (int)(GetPointsNeeded() * Constans.contractPaymentPerPoint * Random.Range(0.75f, 1.25f));
        term = (int)(GetPointsNeeded() / maxScale / Random.Range(1.5f, 3f));
        penalty = (int)((term / 500f) * payment);
    }

    public override void DevelopmentStarted()
    {
        Debug.Log("Freelance started");
    }

    public override void Done()
    {
        FreelanceDoneUI.instance.OpenFreelanceDone(this);

        AchievementsManager.instance.SetAchievment(3);
    }

    private int GetOtherPoint(int mainValue, int maxScale)
    {
        int points = Random.Range(0, Constans.maxContractOtherPoints * maxScale);
        if (points > mainValue) points = (int)(mainValue * Random.Range(0.5f, 0.95f));
        return points;
    }

    private int GetPointsNeeded()
    {
        return plotNeeded + gameDesignNeeded + gameplayNeeded + graphicsNeeded + soundNeeded;
    }

    public override void Develop()
    {
        var workers = RosterManager.instance.GetAssignWorkers();

        float[] workValues = new float[5];

        for (int i = 0; i < workers.Length; i++)
        {
            var work = workers[i].skills.GenerateDefinite(GetNeededForDevSkills());
            work.value *= workers[i].DevelopmentSpeed(BaseDevelopmentSpeed());
            workValues[work.skillId] += work.value;

            PointsManager.instance.ShowPoint(workers[i].table.transform, work.skillId, work.value);
        }

        AddPoints(workValues, 1f);

        developmentDuration++;

        if (GetDevDonePercent() >= 0.99f) ProjectManager.instance.DoneDevelopment();
    }

    private int[] GetNeededForDevSkills()
    {
        List<int> needed = new List<int>();
        if (gameplayNeeded - gameplay > 0) needed.Add(0);
        if (gameDesignNeeded - gameDesign > 0) needed.Add(1);
        if (graphicsNeeded - graphics > 0) needed.Add(2);
        if (soundNeeded - sound > 0) needed.Add(3);
        if (plotNeeded - plot > 0) needed.Add(4);
        return needed.ToArray();
    }

    public float GetDevDonePercent()
    {
        float needed = GetPointsNeeded();
        float done = 0;

        done += plot > plotNeeded ? plotNeeded : plot;
        done += gameDesign > gameDesignNeeded ? gameDesignNeeded : gameDesign;
        done += gameplay > gameplayNeeded ? gameplayNeeded : gameplay;
        done += graphics > graphicsNeeded ? graphicsNeeded : graphics;
        done += sound > soundNeeded ? soundNeeded : sound;

        Debug.Log("Needed " + needed + ", Done " + done);

        return done / needed;
    }

    public int GetPenalty()
    {
        return developmentDuration > term ? penalty : 0;
    } 

    public int GetRecievedPayment()
    {
        return payment - GetPenalty();
    }

    public int GetDifficulty()
    {
        var skills = RosterManager.instance.GetTotalSkills(RosterManager.instance.GetAssignWorkers());

        float[] pointsCoeff = new float[skills.Length];

        pointsCoeff[0] = (skills[0] / 30f * term) / (gameplayNeeded + 1f);
        pointsCoeff[1] = (skills[1] / 30f * term) / (gameDesignNeeded + 1f);
        pointsCoeff[2] = (skills[2] / 30f * term) / (graphicsNeeded + 1f);
        pointsCoeff[3] = (skills[3] / 30f * term) / (soundNeeded + 1f);
        pointsCoeff[4] = (skills[4] / 30f * term) / (plotNeeded + 1f);

        float minCoeff = pointsCoeff[0];
        for (int i = 1; i < pointsCoeff.Length; i++)
        {
            if (pointsCoeff[i] < minCoeff) minCoeff = pointsCoeff[i];
        }

        if (minCoeff > 2f) return 0;
        else if (minCoeff > 1.05f) return 1;
        else return 2;
    }

    public Freelance(SaveLoad.FreelanceSaver saver) : base(saver)
    {
        plotNeeded = saver.plotNeeded;
        gameDesignNeeded = saver.gameDesignNeeded;
        gameplayNeeded = saver.gameplayNeeded;
        graphicsNeeded = saver.graphicsNeeded;
        soundNeeded = saver.soundNeeded;

        payment = saver.payment;
        term = saver.term;
        penalty = saver.penalty;
        developmentDuration = saver.developmentDuration;
    }
}