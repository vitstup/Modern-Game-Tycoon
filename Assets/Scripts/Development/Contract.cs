using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : GameProject
{
    public int payment;
    public int bonus;
    public float minScore;
    public int term;

    public Contract(int size)
    {
        projectName = Constans.gameNames[Random.Range(0, Constans.gameNames.Length)];
        this.size = size;
        genre = AttributesManager.instance.genres[Random.Range(0, AttributesManager.instance.genres.Length)];
        // platforms

        var platfs = AttributesManager.instance.availablePlatforms();
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

        minScore = Random.Range(0.25f, 1f);
        bonus = (int)(Constans.contractPaymentPerScore * minScore * Constans.sizesScale[size] * Random.Range(0.75f, 1.25f));
        payment = (int)(Constans.contractPaymentPerScore * minScore * Constans.sizesScale[size] * Random.Range(0.2f, 0.3f));
        term = (int)(minScore * 365 * Random.Range(0.75f, 1.25f));
    }
}