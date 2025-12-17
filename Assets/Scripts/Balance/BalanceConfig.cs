namespace FourfoldFate.Balance
{
    /// <summary>
    /// Baseline balance configuration for the game.
    /// </summary>
    [System.Serializable]
    public class BalanceConfig
    {
        // Baseline stats for each archetype
        public float tankBaseHealth = 250f;
        public float tankBaseArmor = 8f;
        public float tankBaseDamage = 12f;
        public float tankBaseAttackSpeed = 1.5f;

        public float fighterBaseHealth = 180f;
        public float fighterBaseArmor = 4f;
        public float fighterBaseDamage = 18f;
        public float fighterBaseAttackSpeed = 1.1f;

        public float mageBaseHealth = 100f;
        public float mageBaseArmor = 0f;
        public float mageBaseDamage = 10f;
        public float mageBaseAttackSpeed = 1.3f;
        public float mageBaseMagicResist = 10f;

        public float assassinBaseHealth = 120f;
        public float assassinBaseArmor = 1f;
        public float assassinBaseDamage = 22f;
        public float assassinBaseAttackSpeed = 0.7f;

        // Difficulty scaling
        public float levelScalingMultiplier = 1.05f; // 5% per level
    }
}

