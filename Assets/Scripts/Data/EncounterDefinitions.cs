using System.Collections.Generic;
using FourfoldFate.Roguelike;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all encounters here. Easy to edit and add new encounters.
    /// All encounter data is controlled in code.
    /// </summary>
    public static class EncounterDefinitions
    {
        /// <summary>
        /// Get encounter configuration by ID. Add new encounters here.
        /// </summary>
        public static EncounterDataConfig GetEncounterConfig(string encounterId)
        {
            return encounterId switch
            {
                // ENCOUNTER PACKS (from lore doc)
                "hedge_ambush" => new EncounterDataConfig
                {
                    encounterName = "Hedge Ambush",
                    description = "Bandits and shadows strike from the thickets.",
                    minLevel = 1,
                    maxLevel = 15,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "briar_cairn_footpad", "pollen_skulk", "hex_crow" },
                    goldReward = 50
                },

                "soot_tithe" => new EncounterDataConfig
                {
                    encounterName = "Soot Tithe",
                    description = "Marauders collect their due in ash and steel.",
                    minLevel = 5,
                    maxLevel = 25,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "ash_tithe_marauder", "anvil_woken", "lantern_gnaw_wisp" },
                    goldReward = 75
                },

                "sunken_chapel" => new EncounterDataConfig
                {
                    encounterName = "Sunken Chapel",
                    description = "When a chapel sinks, prayers don't drown. They ferment.",
                    minLevel = 10,
                    maxLevel = 30,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "chapel_husk", "gloam_confessor", "tempest_chorister" },
                    goldReward = 100
                },

                "bog_hunger" => new EncounterDataConfig
                {
                    encounterName = "Bog Hunger",
                    description = "The March breeds patience into hunger.",
                    minLevel = 15,
                    maxLevel = 35,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "bog_needle_leech", "root_chain_warden", "thornworn_bulwark" },
                    goldReward = 120
                },

                "reliquary_raid" => new EncounterDataConfig
                {
                    encounterName = "Reliquary Raid",
                    description = "Some thieves steal gold. This one steals confidence.",
                    minLevel = 20,
                    maxLevel = 50,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "reliquary_breaker", "storm_split_slinker", "lantern_gnaw_wisp" },
                    goldReward = 150
                },

                // STANDARD ENCOUNTERS (Level 1-30)
                "encounter_1_5" => new EncounterDataConfig
                {
                    encounterName = "Trial Keepers",
                    description = "Standard guardians of the early Trials.",
                    minLevel = 1,
                    maxLevel = 5,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "briar_cairn_footpad", "briar_cairn_footpad" },
                    goldReward = 40
                },

                "encounter_6_10" => new EncounterDataConfig
                {
                    encounterName = "March Patrol",
                    description = "The March's guardians grow more dangerous.",
                    minLevel = 6,
                    maxLevel = 10,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "ash_tithe_marauder", "pollen_skulk", "hex_crow" },
                    goldReward = 60
                },

                "encounter_11_20" => new EncounterDataConfig
                {
                    encounterName = "Thorn Guard",
                    description = "The March's defenses thicken.",
                    minLevel = 11,
                    maxLevel = 20,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "thornworn_bulwark", "bog_needle_leech", "lantern_gnaw_wisp" },
                    goldReward = 80
                },

                "encounter_21_30" => new EncounterDataConfig
                {
                    encounterName = "Gloam Gathering",
                    description = "Shadows and secrets converge.",
                    minLevel = 21,
                    maxLevel = 30,
                    isMiniboss = false,
                    enemyUnitIds = new List<string> { "gloam_confessor", "storm_split_slinker", "rustbound_warder" },
                    goldReward = 100
                },

                // MINIBOSSES
                "first_knot" => new EncounterDataConfig
                {
                    encounterName = "First Knot",
                    description = "A Trial-Keeper that certifies your Circle.",
                    minLevel = 10,
                    maxLevel = 10,
                    isMiniboss = true,
                    enemyUnitIds = new List<string> { "briar_matron", "cinder_scribe", "oath_less_duellist" },
                    goldReward = 200
                },

                "tollgate_20" => new EncounterDataConfig
                {
                    encounterName = "Tollgate",
                    description = "A floor where the Trials demand adaptation.",
                    minLevel = 20,
                    maxLevel = 20,
                    isMiniboss = true,
                    enemyUnitIds = new List<string> { "anvil_woken", "tempest_chorister", "reliquary_breaker" },
                    goldReward = 300
                },

                "myth_eater_30" => new EncounterDataConfig
                {
                    encounterName = "Myth-Eater",
                    description = "A boss that embodies a philosophy and punishes one-dimensional builds.",
                    minLevel = 30,
                    maxLevel = 30,
                    isMiniboss = true,
                    isMajorMiniboss = true,
                    enemyUnitIds = new List<string> { "sanctum_pyrebrand", "root_chain_warden", "mirror_hex_adept" },
                    goldReward = 500
                },

                "tollgate_40" => new EncounterDataConfig
                {
                    encounterName = "Tollgate",
                    description = "A floor where the Trials demand payment.",
                    minLevel = 40,
                    maxLevel = 40,
                    isMiniboss = true,
                    enemyUnitIds = new List<string> { "reliquary_breaker", "sanctum_pyrebrand", "root_chain_warden" },
                    goldReward = 400
                },

                "myth_eater_50" => new EncounterDataConfig
                {
                    encounterName = "Myth-Eater",
                    description = "A boss that embodies a philosophy and punishes one-dimensional builds.",
                    minLevel = 50,
                    maxLevel = 50,
                    isMiniboss = true,
                    isMajorMiniboss = true,
                    enemyUnitIds = new List<string> { "mirror_hex_adept", "briar_matron", "cinder_scribe" },
                    goldReward = 600
                },

                "tollgate_60" => new EncounterDataConfig
                {
                    encounterName = "Tollgate",
                    description = "A floor where the Trials demand adaptation.",
                    minLevel = 60,
                    maxLevel = 60,
                    isMiniboss = true,
                    enemyUnitIds = new List<string> { "root_chain_warden", "sanctum_pyrebrand", "reliquary_breaker" },
                    goldReward = 500
                },

                "myth_eater_80" => new EncounterDataConfig
                {
                    encounterName = "Myth-Eater",
                    description = "A boss that embodies a philosophy and punishes one-dimensional builds.",
                    minLevel = 80,
                    maxLevel = 80,
                    isMiniboss = true,
                    isMajorMiniboss = true,
                    enemyUnitIds = new List<string> { "mirror_hex_adept", "root_chain_warden", "sanctum_pyrebrand" },
                    goldReward = 800
                },

                "tollgate_90" => new EncounterDataConfig
                {
                    encounterName = "Tollgate",
                    description = "A floor where the Trials demand payment.",
                    minLevel = 90,
                    maxLevel = 90,
                    isMiniboss = true,
                    enemyUnitIds = new List<string> { "reliquary_breaker", "mirror_hex_adept", "sanctum_pyrebrand" },
                    goldReward = 700
                },

                "sundered_arbiter" => new EncounterDataConfig
                {
                    encounterName = "The Sundered Arbiter",
                    description = "The final boss that speaks in shifting Court-voices across phases.",
                    minLevel = 100,
                    maxLevel = 100,
                    isMiniboss = true,
                    isFinalBoss = true,
                    enemyUnitIds = new List<string> { "mirror_hex_adept", "root_chain_warden", "sanctum_pyrebrand", "reliquary_breaker" },
                    goldReward = 1000
                },

                _ => null
            };
        }
    }
}

