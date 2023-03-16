namespace SaveLoad
{
    [System.Serializable]
    public class PersonaSaver
    {
        public string name;
        public Skills skills;
        public int salary;
        public int modelId;
        public bool startWorker;
        public int tableId = -1;

        public PersonaSaver(Persona persona)
        {
            name = persona.personaName;
            skills = persona.skills;
            salary = persona.salary;
            modelId = persona.modelId;
            startWorker = persona.startWorker;
            if (persona.table != null) tableId = persona.table.buildingId;
        }

    }
}