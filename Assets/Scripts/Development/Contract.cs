using UnityEngine;

[System.Serializable]
public class Contract : GameProject
{
    public int payment;
    public int bonus;
    public float minScore;
    public int term; // in days

    public int developmentDuration; // in days

    public Contract(int size)
    {
        projectName = Constans.gameNames[Random.Range(0, Constans.gameNames.Length)];
        this.size = size;
        genre = AttributesManager.instance.genres[Random.Range(0, AttributesManager.instance.genres.Length)];
        // platforms

        var platfs = PlatformsManager.instance.availablePlatforms();
        int usingPlatforms = Random.Range(1, platfs.Length >= 4 ? 5 : platfs.Length + 1);
        int[] usedPlatforms = new int[usingPlatforms];
        for (int i = 0; i < usedPlatforms.Length; i++)
        {
            usedPlatforms[i] = -1;
        }
        for (int i = 0; i < usingPlatforms; i++)
        {
            while (true)
            {
                int plat = Random.Range(0, platfs.Length);
                bool used = false;
                for (int p = 0; p < usedPlatforms.Length; p++)
                {
                    if (plat == usedPlatforms[p]) { used = true; break; }
                }
                if (!used) { usedPlatforms[i] = plat; break; }
            }
            platforms[i] = platfs[usedPlatforms[i]];
        }

        minScore = Random.Range(0.2f, 1f);
        bonus = (int)(Constans.contractPaymentPerScore * minScore * minScore * Constans.sizesScale[size] * Random.Range(0.75f, 1.25f));
        payment = (int)(Constans.contractPaymentPerScore * (minScore / 3f) * Constans.sizesScale[size] * Random.Range(0.2f, 0.3f));
        term = (int)(minScore * 365 * Random.Range(0.75f, 1.25f));
    }

    public override int GetForefit()
    {
        return payment;
    }

    public override void Develop()
    {
        base.Develop();
        developmentDuration++;
    }

    public override void DevelopmentStarted()
    {
        base.DevelopmentStarted();
        Main.instance.AddMoney(payment);
    }

    public override void Done()
    {
        base.Done();
        ContractDoneUI.instance.OpenContractDoneUI(this);

        AchievementsManager.instance.SetAchievment(2);
    }

    public override void Cancel()
    {
        base.Cancel();
        Main.instance.MinusMoney(GetForefit());
    }

    public int GetReceivedBonus()
    {
        if (reviews.UserScore() < minScore) return 0;
        else
        {
            float timeDebaf = (float)term / developmentDuration;
            if (timeDebaf > 1) timeDebaf = 1f;
            return (int)(bonus * timeDebaf);
        }
    }

    public Contract() { }

    public Contract(SaveLoad.ContractSaver saver) : base(saver)
    {
        payment = saver.payment;
        bonus = saver.bonus;
        minScore = saver.minScore;
        term = saver.term;
        developmentDuration = saver.developmentDuration;
    }
}