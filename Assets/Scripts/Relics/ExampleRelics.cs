using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Example relic implementations based on the game design document.
    /// These can be used as templates or directly in the game.
    /// </summary>
    
    // Arcane Battery - Start combat with +20% mana
    [CreateAssetMenu(fileName = "Arcane Battery", menuName = "Fourfold Fate/Relics/Arcane Battery")]
    public class ArcaneBatteryRelic : Relic
    {
        public override void OnCombatStart(Unit unit)
        {
            // Increase starting mana by 20%
            // This would need integration with Unit's mana system
            // unit.CurrentMana += unit.Data.MaxMana * 0.2f;
        }
    }

    // Tainted Dagger - Assassin crits apply poison
    [CreateAssetMenu(fileName = "Tainted Dagger", menuName = "Fourfold Fate/Relics/Tainted Dagger")]
    public class TaintedDaggerRelic : Relic
    {
        // Would need integration with crit system and poison debuff
    }

    // Earth Totem - +5% max HP per Nature ally
    [CreateAssetMenu(fileName = "Earth Totem", menuName = "Fourfold Fate/Relics/Earth Totem")]
    public class EarthTotemRelic : Relic
    {
        public override void ApplyPassiveEffects(Unit unit)
        {
            // Count Nature synergy tags in party
            // Apply HP bonus based on count
            // This would need integration with PartyManager and SynergyManager
        }
    }

    // Blood Idol - +25% damage, -50% healing
    [CreateAssetMenu(fileName = "Blood Idol", menuName = "Fourfold Fate/Relics/Blood Idol")]
    public class BloodIdolRelic : Relic
    {
        public override void ApplyPassiveEffects(Unit unit)
        {
            // Increase damage by 25%
            // Reduce healing by 50%
            // Would need stat modifiers on Unit/UnitData
        }
    }

    // Storm Core - Abilities have a chance to chain
    [CreateAssetMenu(fileName = "Storm Core", menuName = "Fourfold Fate/Relics/Storm Core")]
    public class StormCoreRelic : Relic
    {
        [SerializeField] private float chainChance = 0.3f; // 30% chance

        public override void OnCombatStart(Unit unit)
        {
            // Add chain attack chance to abilities
            // Would need integration with Ability system
        }
    }
}

