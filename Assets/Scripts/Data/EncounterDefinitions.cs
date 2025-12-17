using System.Collections.Generic;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all encounters here. Easy to edit and add new encounters.
    /// All encounter data is controlled in code.
    /// </summary>
    public static class EncounterDefinitions
    {
        /// <summary>
        /// Get all encounter configurations.
        /// </summary>
        public static List<EncounterDataConfig> GetAllEncounters()
        {
            List<EncounterDataConfig> encounters = new List<EncounterDataConfig>();

            // LEVEL 1-5 ENCOUNTERS
            encounters.Add(new EncounterDataConfig
            {
                encounterId = "encounter_1",
                encounterName = "Bandit Ambush",
                description = "A group of desperate bandits blocks your path.",
                minLevel = 1,
                maxLevel = 5,
                isMiniboss = false,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "briar_cairn_footpad", "ash_tithe_marauder" },
                goldReward = 25
            });

            encounters.Add(new EncounterDataConfig
            {
                encounterId = "encounter_2",
                encounterName = "Wisp Gathering",
                description = "Mysterious lights flicker in the darkness.",
                minLevel = 1,
                maxLevel = 5,
                isMiniboss = false,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "lantern_gnaw_wisp", "hex_crow" },
                goldReward = 30
            });

            encounters.Add(new EncounterDataConfig
            {
                encounterId = "encounter_3",
                encounterName = "Thornworn Patrol",
                description = "Animated armor guards the path ahead.",
                minLevel = 2,
                maxLevel = 6,
                isMiniboss = false,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "thornworn_bulwark", "pollen_skulk" },
                goldReward = 35
            });

            // LEVEL 5-10 ENCOUNTERS
            encounters.Add(new EncounterDataConfig
            {
                encounterId = "encounter_4",
                encounterName = "Shadow Pack",
                description = "A coordinated group of rogues strikes from the shadows.",
                minLevel = 5,
                maxLevel = 10,
                isMiniboss = false,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "pollen_skulk", "bog_needle_leech", "storm_split_slinker" },
                goldReward = 50
            });

            encounters.Add(new EncounterDataConfig
            {
                encounterId = "encounter_5",
                encounterName = "Chapel Ruins",
                description = "The remains of a holy place, now corrupted.",
                minLevel = 5,
                maxLevel = 10,
                isMiniboss = false,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "chapel_husk", "rustbound_warder", "lantern_gnaw_wisp" },
                goldReward = 55
            });

            // LEVEL 10 MINIBOSS
            encounters.Add(new EncounterDataConfig
            {
                encounterId = "miniboss_10",
                encounterName = "The Cinder-Scribe",
                description = "A powerful mage who writes in ash and flame.",
                minLevel = 10,
                maxLevel = 10,
                isMiniboss = true,
                isMajorMiniboss = false,
                isFinalBoss = false,
                enemyUnitIds = new List<string> { "cinder_scribe", "ash_tithe_marauder", "briar_cairn_footpad" },
                goldReward = 100
            });

            return encounters;
        }
    }
}

