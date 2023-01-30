using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageSkillTable : TotalSkillTable
{
    public override void UpdateInfo(List<Persona> personas)
    {
        int[] skills = GetTotalSkills(personas);
        if(personas.Count == 0) { SetInfo(skills); return; }
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] /= personas.Count;
        }
        SetInfo(skills);
    }
}