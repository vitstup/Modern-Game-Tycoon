namespace SaveLoad
{
    [System.Serializable]
    public abstract class ProjectSaver
    {
        public string name;
        public float plot;
        public float gameDesign;
        public float gameplay;
        public float graphics;
        public float sound;

        public ProjectSaver(Project project)
        {
            name = project.projectName;
            plot = project.plot;
            gameDesign = project.gameDesign;
            gameplay = project.gameplay;
            graphics = project.graphics;
            sound = project.sound;
        }
    }
}