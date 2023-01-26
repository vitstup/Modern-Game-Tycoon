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
}