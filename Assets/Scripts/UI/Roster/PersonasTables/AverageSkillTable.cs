using System.Collections.Generic;

public class AverageSkillTable : PersonasTable
{
    public override void UpdateInfo(List<Persona> personas)
    {
        var skills = RosterManager.instance.GetAverageSkills(personas.ToArray());
        SetInfo(skills);
    }
}