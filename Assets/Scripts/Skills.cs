using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skills 
{
    [field: SerializeField] public int mainSkill { get; private set; }


    [field: SerializeField] public int programming { get; private set; }
    [field: SerializeField] public int gameDesign { get; private set; }
    [field: SerializeField] public int artDesign { get; private set; }
    [field: SerializeField] public int soundDesign { get; private set; }
    [field: SerializeField] public int screenwriting { get; private set; }
    
    public int GetSumm()
    {
        return programming + gameDesign + artDesign + soundDesign + screenwriting;
    }

    public int GetSkill(int num)
    {
        if (num == 0) return programming;
        else if (num == 1) return gameDesign;
        else if (num == 2) return artDesign;
        else if (num == 3) return soundDesign;
        else if (num == 4) return screenwriting;
        else
        {
            Debug.LogError("Theare no such skill");
            return 0;
        }
    }


    public Skills(int minSkill)
    {
        RandomSkills(minSkill);
        setMainSkill();
    }

    private void RandomSkills(int minSkill)
    {
        float tier = Random.Range(0, 100);
        int maxSkill = 40;
        if (tier > 95) maxSkill = 101;
        else if (tier > 80) maxSkill = 81;
        else if (tier > 60) maxSkill = 61;
        programming = Random.Range(minSkill, maxSkill);
        gameDesign = Random.Range(minSkill, maxSkill);
        artDesign = Random.Range(minSkill, maxSkill);
        soundDesign = Random.Range(minSkill, maxSkill);
        screenwriting = Random.Range(minSkill, maxSkill);
    }

    private void setMainSkill()
    {
        int skill = 0;
        int highestSkill = programming;
        if(gameDesign > highestSkill) { highestSkill = gameDesign; skill = 1; }
        if(artDesign > highestSkill) { highestSkill = artDesign; skill = 2; }
        if(soundDesign > highestSkill) { highestSkill = soundDesign; skill = 3; }
        if(screenwriting > highestSkill) { highestSkill = screenwriting; skill = 4; }
        mainSkill = skill;
    }

    // point generation 

    private float GeneratePoint(int skill)
    {
        float value = Random.Range(skill / 3, skill) / 20f;
        return value > 1f? value : 1f;
    }

    private float GenerateBug(float skillValue)
    {
        float bug = Random.Range(0, 5);
        float fix = Random.Range(0, skillValue);

        return bug - fix;
    }

    public DevValue GenerateDefinite(int[] definits)
    {
        int skill = 0;
        while (true)
        {
            bool rightSkill = false;
            skill = RandomSkill();
            for (int i = 0; i < definits.Length; i++)
            {
                if (skill == definits[i]) { rightSkill = true; break; }
            }
            if (rightSkill) break;
        }
        float value = GetSkillPointValue(skill);

        return new DevValue(value, skill);
    }

    public DevValue GenerateRandom(bool onlyAntiBug)
    {
        int skill = RandomSkill();
        float value = GetSkillPointValue(skill);

        if (Random.Range(0, 100) > 85)
        {
            float bug = GenerateBug(value);
            if (!onlyAntiBug || bug < 0) return new DevValue(bug, 5);
        }

        return new DevValue(value, skill);
    }

    private int RandomSkill() // use to choose random skill based on its value
    {
        int summ = GetSumm();
        int rnd = Random.Range(0, summ);

        if (rnd < programming) return 0;
        else if (rnd < programming + gameDesign) return 1;
        else if (rnd < programming + gameDesign + artDesign) return 2;
        else if (rnd < programming + gameDesign + artDesign + soundDesign) return 3;
        else return 4;
    }

    private float GetSkillPointValue(int skill) // use to generate value by skill id 
    {
        switch (skill)
        {
            case 0: return GeneratePoint(programming);
            case 1: return GeneratePoint(gameDesign);
            case 2: return GeneratePoint(artDesign);
            case 3: return GeneratePoint(soundDesign);
            case 4: return GeneratePoint(screenwriting);

        }
        Debug.LogWarning("wrong skill");
        return 0;
    }
}