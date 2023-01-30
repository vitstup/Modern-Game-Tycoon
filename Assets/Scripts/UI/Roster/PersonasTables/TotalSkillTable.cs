using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalSkillTable : PersonasTable
{
    public override void UpdateInfo(List<Persona> personas)
    {
        SetInfo(GetTotalSkills(personas));
    }

    protected int[] GetTotalSkills(List<Persona> personas)
    {
        int[] skills = new int[5];
        for (int i = 0; i < personas.Count; i++)
        {
            skills[0] += personas[i].skills.programming;
            skills[1] += personas[i].skills.gameDesign;
            skills[2] += personas[i].skills.artDesign;
            skills[3] += personas[i].skills.soundDesign;
            skills[4] += personas[i].skills.screenwriting;
        }
        return skills;
    }
}