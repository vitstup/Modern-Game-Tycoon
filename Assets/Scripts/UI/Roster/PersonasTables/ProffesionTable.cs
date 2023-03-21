using System.Collections.Generic;

public class ProffesionTable : PersonasTable
{
    public override void UpdateInfo(List<Persona> personas)
    {
        int[] proffesions = new int[5];
        for (int i = 0; i < personas.Count; i++)
        {
            proffesions[personas[i].skills.mainSkill]++;
        }
        SetInfo(proffesions);
    }
}