namespace FourfoldFate.Core
{
    /// <summary>
    /// Configuration data for creating an ability. Used by GameDataManager.
    /// </summary>
    [System.Serializable]
    public class AbilityDataConfig
    {
        public string abilityId;
        public string abilityName;
        public string description;
        public AbilityType abilityType;
        public float manaCost;
        public float cooldown;
        public float damage;
        public float healAmount;
        public float range;
    }

    /// <summary>
    /// Runtime ability data structure.
    /// </summary>
    [System.Serializable]
    public class AbilityData
    {
        public string abilityId;
        public string abilityName;
        public string description;
        public AbilityType abilityType;
        public float manaCost;
        public float cooldown;
        public float damage;
        public float healAmount;
        public float range;
    }

    /// <summary>
    /// Ability type classification.
    /// </summary>
    public enum AbilityType
    {
        Damage,
        Heal,
        Buff,
        Debuff,
        Utility
    }
}

