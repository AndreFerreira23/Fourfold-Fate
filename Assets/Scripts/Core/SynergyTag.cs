using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Synergy tags that characters can have. Party-wide bonuses when tags are stacked.
    /// </summary>
    public enum SynergyTag
    {
        None,
        Fire,
        Nature,
        Shadow,
        Holy,
        Arcane,
        Steel,
        Storm
    }

    /// <summary>
    /// Manages synergy bonuses for the party.
    /// </summary>
    public static class SynergyManager
    {
        private static Dictionary<SynergyTag, int> tagCounts = new Dictionary<SynergyTag, int>();
        private static Dictionary<SynergyTag, SynergyBonus> activeBonuses = new Dictionary<SynergyTag, SynergyBonus>();

        /// <summary>
        /// Calculate synergy bonuses based on party tags
        /// </summary>
        public static Dictionary<SynergyTag, SynergyBonus> CalculateSynergies(List<Unit> party)
        {
            tagCounts.Clear();
            activeBonuses.Clear();

            // Count tags in party
            foreach (var unit in party)
            {
                if (unit == null || !unit.IsAlive) continue;
                
                var unitData = unit.Data;
                if (unitData != null)
                {
                    if (unitData.SynergyTag1 != SynergyTag.None)
                        IncrementTag(unitData.SynergyTag1);
                    
                    if (unitData.SynergyTag2 != SynergyTag.None)
                        IncrementTag(unitData.SynergyTag2);
                }
            }

            // Apply bonuses based on thresholds
            ApplySynergyBonuses();

            return new Dictionary<SynergyTag, SynergyBonus>(activeBonuses);
        }

        private static void IncrementTag(SynergyTag tag)
        {
            if (tag == SynergyTag.None) return;
            
            if (tagCounts.ContainsKey(tag))
                tagCounts[tag]++;
            else
                tagCounts[tag] = 1;
        }

        private static void ApplySynergyBonuses()
        {
            foreach (var kvp in tagCounts)
            {
                var tag = kvp.Key;
                var count = kvp.Value;

                var bonus = GetSynergyBonus(tag, count);
                if (bonus != null)
                {
                    activeBonuses[tag] = bonus;
                }
            }
        }

        private static SynergyBonus GetSynergyBonus(SynergyTag tag, int count)
        {
            // Define synergy thresholds and bonuses
            switch (tag)
            {
                case SynergyTag.Fire:
                    if (count >= 2)
                        return new SynergyBonus { burnDurationBonus = 0.2f }; // +20% burn duration
                    break;

                case SynergyTag.Nature:
                    if (count >= 3)
                        return new SynergyBonus { partyHealPerTurn = 0.01f }; // 1% HP per turn
                    break;

                case SynergyTag.Shadow:
                    if (count >= 2)
                        return new SynergyBonus { critDamageBonus = 0.1f }; // +10% crit damage
                    break;

                case SynergyTag.Holy:
                    if (count >= 2)
                        return new SynergyBonus { damageToShieldConversion = 0.05f }; // 5% damage to shields
                    break;

                case SynergyTag.Arcane:
                    if (count >= 2)
                        return new SynergyBonus { cooldownReduction = 0.1f }; // 10% cooldown reduction
                    break;

                case SynergyTag.Steel:
                    if (count >= 2)
                        return new SynergyBonus { armorBonus = 10f }; // +10 armor
                    break;

                case SynergyTag.Storm:
                    if (count >= 2)
                        return new SynergyBonus { chainAttackChance = 0.15f }; // 15% chain attack chance
                    break;
            }

            return null;
        }
    }

    /// <summary>
    /// Represents active synergy bonuses
    /// </summary>
    [System.Serializable]
    public class SynergyBonus
    {
        public float burnDurationBonus = 0f;
        public float partyHealPerTurn = 0f;
        public float critDamageBonus = 0f;
        public float damageToShieldConversion = 0f;
        public float cooldownReduction = 0f;
        public float armorBonus = 0f;
        public float chainAttackChance = 0f;
    }
}

