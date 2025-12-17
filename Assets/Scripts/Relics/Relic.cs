using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Base class for all relics. Relics provide run-defining bonuses.
    /// </summary>
    public class Relic : ScriptableObject
    {
        [Header("Relic Info")]
        public string relicName;
        public string description;
        public Sprite icon;
        public RelicRarity rarity;

        [Header("Effects")]
        public RelicEffect[] effects;

        /// <summary>
        /// Called when combat starts.
        /// </summary>
        public virtual void OnCombatStart(Unit unit)
        {
        }

        /// <summary>
        /// Called when combat ends.
        /// </summary>
        public virtual void OnCombatEnd(Unit unit)
        {
        }

        /// <summary>
        /// Apply passive effects to a unit.
        /// </summary>
        public virtual void ApplyPassiveEffects(Unit unit)
        {
        }
    }

    /// <summary>
    /// Relic rarity levels.
    /// </summary>
    public enum RelicRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}

