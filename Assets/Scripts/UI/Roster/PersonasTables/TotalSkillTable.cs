using System.Collections.Generic;

public class TotalSkillTable : PersonasTable
{
    public override void UpdateInfo(List<Persona> personas)
    {
        var skills = RosterManager.instance.GetTotalSkills(personas.ToArray());
        SetInfo(skills);
    }

}