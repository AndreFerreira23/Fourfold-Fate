namespace FourfoldFate.Relics
{
    /// <summary>
    /// Individual effect that a relic can apply.
    /// </summary>
    [System.Serializable]
    public class RelicEffect
    {
        public RelicEffectType effectType;
        public float value;
        public string targetStat;
    }

    /// <summary>
    /// Types of effects relics can have.
    /// </summary>
    public enum RelicEffectType
    {
        StatBoost,
        DamageModifier,
        HealModifier,
        CooldownReduction,
        ManaCostReduction,
        SpecialAbility
    }
}

