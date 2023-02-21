using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        return Random.Range(skill / 3, skill) / 100f;
    }

    private int GenerateBug(int skill)
    {
        if (Random.Range(skill, 105) < Random.Range(0, 105)) return 1;
        return 0;
    }

    public float[] RandomSkill()
    {
        float[] values = new float[5];
        values[0] = GeneratePoint(programming);
        values[1] = GeneratePoint(gameDesign);
        values[2] = GeneratePoint(artDesign);
        values[3] = GeneratePoint(soundDesign);
        values[4] = GeneratePoint(screenwriting);

        float summ = values.Sum();

        float random = Random.Range(0, summ);

        int skillType = 0;

        if (random < values[0]) skillType = 0;
        else if (random < values[0] + values[1]) skillType = 1;
        else if (random < values[0] + values[1] + values[2]) skillType = 2;
        else if (random < values[0] + values[1] + values[2] + values[3]) skillType = 3;
        else skillType = 4;

        return new float[] { values[skillType], skillType };
    }

    public float[] RandomSkillWithBugs()
    {
        var skill = RandomSkill();
        int type = (int)skill[1];
        int bug = 0;

        if (type == 0) bug = GenerateBug(programming);
        else if (type == 1) bug = GenerateBug(gameDesign);
        else if (type == 2) bug = GenerateBug(artDesign);
        else if (type == 3) bug = GenerateBug(soundDesign);
        else bug = GenerateBug(screenwriting);

        return new float[] { skill[0], type, bug };
    }
}