using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Base class for relic effects
    /// </summary>
    [System.Serializable]
    public class RelicEffect
    {
        public RelicEffectType effectType;
        public float value;
        public string statName;

        public virtual void ApplyOnCombatStart(Unit unit) { }
        public virtual void ApplyOnCombatEnd(Unit unit) { }
        public virtual void ApplyPassive(Unit unit) { }
    }

    public enum RelicEffectType
    {
        StatModifier,
        AbilityModifier,
        SynergyBonus,
        ConditionalBonus
    }
}

