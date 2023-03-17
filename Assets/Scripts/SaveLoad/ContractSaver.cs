namespace SaveLoad
{
    [System.Serializable]
    public class ContractSaver : GameProjectSaver
    {
        public int payment;
        public int bonus;
        public float minScore;
        public int term;

        public int developmentDuration;

        public ContractSaver(Contract contract) : base(contract)
        {
            payment = contract.payment;
            bonus = contract.bonus;
            minScore = contract.minScore;
            term = contract.term;
            developmentDuration = contract.developmentDuration;
        }
    }
}