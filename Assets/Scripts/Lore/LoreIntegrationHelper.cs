using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Relics;

namespace FourfoldFate.Lore
{
    /// <summary>
    /// Helper class to integrate lore text into game systems.
    /// Use this to ensure all descriptions and UI text follow the canon.
    /// </summary>
    public static class LoreIntegrationHelper
    {
        /// <summary>
        /// Get a formatted description for a unit that includes lore
        /// </summary>
        public static string GetUnitDescription(UnitData unitData)
        {
            if (unitData == null) return "";

            string archetypeLore = LoreTextManager.GetArchetypeLoreName(unitData.archetypeType);
            string archetypeDesc = LoreTextManager.GetArchetypeDescription(unitData.archetypeType);
            
            string tag1 = unitData.SynergyTag1 != SynergyTag.None 
                ? LoreTextManager.GetSynergyCourtName(unitData.SynergyTag1) 
                : "";
            string tag2 = unitData.SynergyTag2 != SynergyTag.None 
                ? LoreTextManager.GetSynergyCourtName(unitData.SynergyTag2) 
                : "";

            string tags = "";
            if (!string.IsNullOrEmpty(tag1) && !string.IsNullOrEmpty(tag2))
                tags = $"Bound to {tag1} and {tag2}.";
            else if (!string.IsNullOrEmpty(tag1))
                tags = $"Bound to {tag1}.";

            return $"{archetypeLore}. {archetypeDesc} {tags}";
        }

        /// <summary>
        /// Get a formatted description for a relic that includes lore
        /// </summary>
        public static string GetRelicDescription(Relic relic)
        {
            if (relic == null) return "";

            string flavor = LoreTextManager.GetRelicFlavorText(relic.relicName);
            if (string.IsNullOrEmpty(flavor))
            {
                flavor = relic.description;
            }

            return $"{flavor} {relic.description}";
        }

        /// <summary>
        /// Get encounter description with lore
        /// </summary>
        public static string GetEncounterDescription(int level, bool isMiniboss, bool isMajorMiniboss, bool isFinalBoss)
        {
            if (isFinalBoss)
                return "The Sundered Arbiter awaits. The final Trial speaks in shifting Court-voices across phases.";
            
            if (isMajorMiniboss)
                return "A Myth-Eater rises. This boss embodies a philosophy and punishes one-dimensional builds.";
            
            if (isMiniboss)
                return "A Tollgate blocks your path. The Trials demand adaptation and payment.";
            
            return "The Trials test your Circle. Standard combat awaits.";
        }

        /// <summary>
        /// Get level-up choice description with lore
        /// </summary>
        public static string GetLevelUpDescription(Progression.LevelUpSystem.LevelUpPathType pathType)
        {
            string pathName = LoreTextManager.GetLevelUpPathName(pathType);
            return $"The Trials acknowledge your progress. Choose {pathName} to shape your Circle's story.";
        }
    }
}

