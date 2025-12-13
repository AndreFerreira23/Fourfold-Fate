using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Base relic class. Relics are run-defining items that significantly alter gameplay.
    /// </summary>
    [CreateAssetMenu(fileName = "New Relic", menuName = "Fourfold Fate/Relic")]
    public class Relic : ScriptableObject
    {
        [Header("Relic Info")]
        public string relicName;
        public string description;
        public Sprite icon;
        public Rarity rarity = Rarity.Common;

        [Header("Relic Effects")]
        public RelicEffect[] effects;

        /// <summary>
        /// Apply relic effects when combat starts
        /// </summary>
        public virtual void OnCombatStart(Unit unit)
        {
            foreach (var effect in effects)
            {
                effect.ApplyOnCombatStart(unit);
            }
        }

        /// <summary>
        /// Apply relic effects when combat ends
        /// </summary>
        public virtual void OnCombatEnd(Unit unit)
        {
            foreach (var effect in effects)
            {
                effect.ApplyOnCombatEnd(unit);
            }
        }

        /// <summary>
        /// Apply passive relic effects
        /// </summary>
        public virtual void ApplyPassiveEffects(Unit unit)
        {
            foreach (var effect in effects)
            {
                effect.ApplyPassive(unit);
            }
        }
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}

