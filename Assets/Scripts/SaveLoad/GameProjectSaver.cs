namespace SaveLoad
{
    [System.Serializable]
    public abstract class GameProjectSaver : ProjectSaver
    {
        [System.Serializable]
        public class StageSaver
        {
            public float workload;
            public float workloadDone;
            public int[] sliders;
            public int[] features;

            public StageSaver(Stage stage)
            {
                workload = stage.workload;
                workloadDone = stage.workloadDone;

                sliders = stage.GetSliders().GetSliders();

                features = new int[stage.features.Count];
                for (int i = 0; i < features.Length; i++)
                {
                    features[i] = AttributesManager.instance.GetFeatureId(stage.features[i]);
                }
            }
        }

        public int size;
        public int engine;
        public int genre;
        public int theme;
        public int[] platforms;

        public float bugs;

        public StageSaver prototyping;
        public StageSaver developing;
        public StageSaver design;

        public int currentStageId;

        public int expenses;
        public Reviews reviews;
        public int spriteId;

        public GameProjectSaver(GameProject gameProject) : base(gameProject)
        {
            size = gameProject.size;
            engine = AttributesManager.instance.GetEngineId(gameProject.engine);
            genre = AttributesManager.instance.GetGenreId(gameProject.genre);
            theme = AttributesManager.instance.GetThemeId(gameProject.theme);
            platforms = new int[gameProject.platforms.Length];
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i] = PlatformsManager.instance.GetPlatformId(gameProject.platforms[i]);
            }
            bugs = gameProject.bugs;

            var stages = gameProject.GetStages();
            prototyping = new StageSaver(stages[0]);
            developing = new StageSaver(stages[1]);
            design = new StageSaver(stages[2]);

            currentStageId = gameProject.GetCurrentStageId();
            expenses = gameProject.expenses;
            reviews = gameProject.reviews;
            spriteId = gameProject.genre.GetSpriteId(gameProject.sprite);
        }
    }
}