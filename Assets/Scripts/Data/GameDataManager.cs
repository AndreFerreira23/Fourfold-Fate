using System.Collections.Generic;
using System.Linq;
using FourfoldFate.Core;
using FourfoldFate.Roguelike;
using FourfoldFate.Relics;
using UnityEngine;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Central manager for all game data (units, abilities, relics, encounters).
    /// All data is defined in code, not ScriptableObjects.
    /// </summary>
    public class GameDataManager : MonoBehaviour
    {
        [Header("Data Dictionaries")]
        private Dictionary<string, UnitData> units = new Dictionary<string, UnitData>();
        private Dictionary<string, AbilityData> abilities = new Dictionary<string, AbilityData>();
        private Dictionary<string, Relic> relics = new Dictionary<string, Relic>();
        private Dictionary<string, EncounterData> encounters = new Dictionary<string, EncounterData>();

        public static GameDataManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                LoadAllData();
                
                // Only call DontDestroyOnLoad if this is a root GameObject
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Load all game data from definition classes.
        /// </summary>
        private void LoadAllData()
        {
            LoadUnits();
            LoadAbilities();
            LoadRelics();
            LoadEncounters();
        }

        /// <summary>
        /// Load all unit definitions.
        /// </summary>
        private void LoadUnits()
        {
            // Load player units from UnitDefinitions
            if (UnitDefinitions.GetAllPlayerUnits() != null)
            {
                foreach (var config in UnitDefinitions.GetAllPlayerUnits())
                {
                    UnitData data = ConvertConfigToData(config);
                    units[data.unitId] = data;
                }
            }

            // Load enemy units from EnemyDefinitions
            // We'll need to get all enemy IDs - for now, hardcode common ones
            string[] enemyIds = {
                "briar_cairn_footpad", "ash_tithe_marauder", "thornworn_bulwark", "chapel_husk",
                "pollen_skulk", "lantern_gnaw_wisp", "hex_crow", "bog_needle_leech",
                "storm_split_slinker", "rustbound_warder", "cinder_scribe", "briar_matron",
                "gloam_confessor", "anvil_woken", "tempest_chorister", "oath_less_duellist",
                "reliquary_breaker", "sanctum_pyrebrand", "root_chain_warden", "mirror_hex_adept"
            };

            foreach (var enemyId in enemyIds)
            {
                var config = EnemyDefinitions.GetEnemyConfig(enemyId);
                if (config != null)
                {
                    UnitData data = ConvertConfigToData(config, enemyId);
                    units[data.unitId] = data;
                }
            }
        }

        /// <summary>
        /// Load all ability definitions.
        /// </summary>
        private void LoadAbilities()
        {
            if (AbilityDefinitions.GetAllAbilities() != null)
            {
                foreach (var config in AbilityDefinitions.GetAllAbilities())
                {
                    AbilityData data = ConvertConfigToData(config);
                    abilities[data.abilityId] = data;
                }
            }
        }

        /// <summary>
        /// Load all relic definitions.
        /// </summary>
        private void LoadRelics()
        {
            // Relics will be loaded from RelicDefinitions if needed
        }

        /// <summary>
        /// Load all encounter definitions.
        /// </summary>
        private void LoadEncounters()
        {
            if (EncounterDefinitions.GetAllEncounters() != null)
            {
                foreach (var config in EncounterDefinitions.GetAllEncounters())
                {
                    EncounterData data = ConvertConfigToEncounterData(config);
                    encounters[data.encounterId] = data;
                }
            }
        }

        /// <summary>
        /// Convert UnitDataConfig to UnitData.
        /// </summary>
        private UnitData ConvertConfigToData(UnitDataConfig config, string unitId = null)
        {
            // Generate ID from unit name if not provided
            string id = unitId;
            if (string.IsNullOrEmpty(id))
            {
                id = config.unitName.ToLower()
                    .Replace(" ", "_")
                    .Replace("-", "_")
                    .Replace("'", "");
            }

            return new UnitData
            {
                unitId = id,
                unitName = config.unitName,
                description = config.description,
                maxHealth = config.maxHealth,
                maxMana = config.maxMana,
                attackDamage = config.attackDamage,
                attackSpeed = config.attackSpeed,
                armor = config.armor,
                magicResist = config.magicResist,
                movementSpeed = config.movementSpeed,
                attackRange = config.attackRange,
                archetypeType = config.archetypeType,
                synergyTag1 = config.synergyTag1,
                synergyTag2 = config.synergyTag2,
                unitType = config.unitType,
                unitRole = config.unitRole
            };
        }

        /// <summary>
        /// Convert AbilityDataConfig to AbilityData.
        /// </summary>
        private AbilityData ConvertConfigToData(AbilityDataConfig config)
        {
            return new AbilityData
            {
                abilityId = config.abilityId,
                abilityName = config.abilityName,
                description = config.description,
                abilityType = config.abilityType,
                manaCost = config.manaCost,
                cooldown = config.cooldown,
                damage = config.damage,
                healAmount = config.healAmount,
                range = config.range
            };
        }

        /// <summary>
        /// Convert EncounterDataConfig to EncounterData.
        /// </summary>
        private EncounterData ConvertConfigToEncounterData(EncounterDataConfig config)
        {
            List<UnitData> enemyUnits = new List<UnitData>();
            if (config.enemyUnitIds != null)
            {
                foreach (var enemyId in config.enemyUnitIds)
                {
                    if (units.ContainsKey(enemyId))
                    {
                        enemyUnits.Add(units[enemyId]);
                    }
                }
            }

            return new EncounterData
            {
                encounterId = config.encounterId,
                encounterName = config.encounterName,
                description = config.description,
                minLevel = config.minLevel,
                maxLevel = config.maxLevel,
                isMiniboss = config.isMiniboss,
                isMajorMiniboss = config.isMajorMiniboss,
                isFinalBoss = config.isFinalBoss,
                enemyUnits = enemyUnits,
                goldReward = config.goldReward
            };
        }

        // Public getters
        public UnitData GetUnit(string unitId) => units.ContainsKey(unitId) ? units[unitId] : null;
        public AbilityData GetAbility(string abilityId) => abilities.ContainsKey(abilityId) ? abilities[abilityId] : null;
        public Relic GetRelic(string relicId) => relics.ContainsKey(relicId) ? relics[relicId] : null;
        public EncounterData GetEncounter(string encounterId) => encounters.ContainsKey(encounterId) ? encounters[encounterId] : null;
        public List<EncounterData> GetAllEncounters() => encounters.Values.ToList();
        
        /// <summary>
        /// Get all player units (non-enemy units).
        /// </summary>
        public List<UnitData> GetAllPlayerUnits()
        {
            List<UnitData> playerUnits = new List<UnitData>();
            // Player units should have IDs like "the_warden", "the_blade", etc.
            string[] playerIds = { "the_warden", "the_blade", "the_seer", "the_shadow" };
            foreach (var id in playerIds)
            {
                if (units.ContainsKey(id))
                {
                    playerUnits.Add(units[id]);
                }
            }
            return playerUnits;
        }
    }

    /// <summary>
    /// Configuration structure for encounter data (used in EncounterDefinitions).
    /// </summary>
    [System.Serializable]
    public class EncounterDataConfig
    {
        public string encounterId;
        public string encounterName;
        public string description;
        public int minLevel;
        public int maxLevel;
        public bool isMiniboss;
        public bool isMajorMiniboss;
        public bool isFinalBoss;
        public List<string> enemyUnitIds = new List<string>();
        public int goldReward;
    }
}

