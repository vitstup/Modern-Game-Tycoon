namespace SaveLoad
{
    [System.Serializable]
    public class GameUpdateSaver : ProjectSaver
    {
        public int updateGame = -1;

        public int updateSize;

        public float bugs;

        public int workload;
        public float workloadDone;

        public int[] platformsAdded;

        public bool isPolishing;

        public GameUpdateSaver(GameUpdate gameUpdate) : base(gameUpdate)
        {
            updateGame = Statistics.instance.GetGameId(gameUpdate.updateGame);
            updateSize = gameUpdate.updateSize;

            bugs = gameUpdate.bugs;
            workload = gameUpdate.workload;
            workloadDone = gameUpdate.workloadDone;

            platformsAdded = new int[gameUpdate.platformsAdded.Length];
            for (int i = 0; i < platformsAdded.Length; i++)
            {
                platformsAdded[i] = PlatformsManager.instance.GetPlatformId(gameUpdate.platformsAdded[i]);
            }

            isPolishing = gameUpdate.isPolishing;
        }
    }
}
