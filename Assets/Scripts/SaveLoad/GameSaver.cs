namespace SaveLoad
{
    [System.Serializable]
    public class GameSaver : GameProjectSaver 
    {
        public int publisher = -1;
        public float price;
        public int sequelOf = -1;

        public int todaySales;
        public int todayProfit;
        public int[] recentProfits;
        public Game.PlatformSales[] sales;

        public int firstDayProfit;

        public float hype;

        public int daysTillMarketingCampaign;

        public float interest;

        public bool bugMailSended;

        public GameSaver(Game game) : base(game)
        {
            if (game.publisher != null) publisher = PublishersManager.instance.GetPublisherId(game.publisher);
            if (game.sequelOf != null) sequelOf = Statistics.instance.GetGameId(game.sequelOf);

            price = game.price;

            todaySales = game.todaySales;
            todayProfit = game.todayProfit;
            recentProfits = game.recentProfits;
            sales = game.sales;

            firstDayProfit = game.firstDayProfit;
            hype = game.hype;
            daysTillMarketingCampaign = game.daysTillMarketingCampaign;
            interest = game.interest;
            bugMailSended = game.bugMailSended;
        }
    }
}