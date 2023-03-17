namespace SaveLoad
{
    [System.Serializable]
    public class FreelanceSaver : ProjectSaver
    {
        public int plotNeeded;
        public int gameDesignNeeded;
        public int gameplayNeeded;
        public int graphicsNeeded;
        public int soundNeeded;

        public int payment;
        public int term;
        public int penalty;

        public int developmentDuration;

        public FreelanceSaver(Freelance freelance) : base(freelance)
        {
            plotNeeded = freelance.plotNeeded;
            gameDesignNeeded = freelance.gameDesignNeeded;
            gameplayNeeded = freelance.gameplayNeeded;
            graphicsNeeded = freelance.graphicsNeeded;
            soundNeeded = freelance.soundNeeded;

            payment = freelance.payment;
            term = freelance.term;
            penalty = freelance.penalty;
            developmentDuration = freelance.developmentDuration;
        }
    }
}