namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Reward data for encounters.
    /// </summary>
    [System.Serializable]
    public class RewardData
    {
        public int gold;
        public int experience;
        public bool grantsRelic;
        public bool grantsLevelUp;
    }
}

