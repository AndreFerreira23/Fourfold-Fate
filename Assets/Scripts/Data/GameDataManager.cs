using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Relics;
using FourfoldFate.Roguelike;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Central manager for all game data defined in code.
    /// All units, abilities, relics, and encounters are defined here.
    /// </summary>
    public class GameDataManager : MonoBehaviour
    {
        private static GameDataManager instance;
        public static GameDataManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameDataManager>();
                return instance;
            }
        }

        [Header("Data Collections")]
        private Dictionary<string, UnitData> unitDatabase = new Dictionary<string, UnitData>();
        private Dictionary<string, AbilityData> abilityDatabase = new Dictionary<string, AbilityData>();
        private Dictionary<string, Relic> relicDatabase = new Dictionary<string, Relic>();
        private Dictionary<string, EncounterData> encounterDatabase = new Dictionary<string, EncounterData>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                // Only use DontDestroyOnLoad if this is a root GameObject
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
                InitializeAllData();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeAllData()
        {
            InitializeUnits();
            InitializeAbilities();
            InitializeRelics();
            InitializeEncounters();
        }

        #region Unit Data Initialization

        private void InitializeUnits()
        {
            // Load all player units from UnitDefinitions
            // Add new units by adding them to UnitDefinitions.cs
            string[] unitIds = { "guardian_shield", "earthwarden", "blade_dancer", "flame_striker", 
                                "rune_scribe", "stormcaller", "shadow_blade", "venom_weaver",
                                "ironclad_guardian", "shadowblade", "arcane_weaver" };
            
            foreach (string unitId in unitIds)
            {
                var config = UnitDefinitions.GetUnitConfig(unitId);
                if (config != null)
                {
                    CreateUnit(unitId, config);
                }
            }

            // Load all enemies from EnemyDefinitions
            InitializeEnemies();
        }

        private void InitializeEnemies()
        {
            // Common enemies (Level 1-30)
            string[] commonEnemyIds = {
                "briar_cairn_footpad", "ash_tithe_marauder", "thornworn_bulwark", "chapel_husk",
                "pollen_skulk", "lantern_gnaw_wisp", "hex_crow", "bog_needle_leech",
                "storm_split_slinker", "rustbound_warder"
            };

            // Uncommon enemies (Level 10-60)
            string[] uncommonEnemyIds = {
                "cinder_scribe", "briar_matron", "gloam_confessor", "anvil_woken",
                "tempest_chorister", "oath_less_duellist"
            };

            // Elite enemies (Level 20-90)
            string[] eliteEnemyIds = {
                "reliquary_breaker", "sanctum_pyrebrand", "root_chain_warden", "mirror_hex_adept"
            };

            // Create all enemies
            foreach (string enemyId in commonEnemyIds)
            {
                var config = EnemyDefinitions.GetEnemyConfig(enemyId);
                if (config != null)
                {
                    CreateUnit(enemyId, config);
                }
            }

            foreach (string enemyId in uncommonEnemyIds)
            {
                var config = EnemyDefinitions.GetEnemyConfig(enemyId);
                if (config != null)
                {
                    CreateUnit(enemyId, config);
                }
            }

            foreach (string enemyId in eliteEnemyIds)
            {
                var config = EnemyDefinitions.GetEnemyConfig(enemyId);
                if (config != null)
                {
                    CreateUnit(enemyId, config);
                }
            }
        }

        private void CreateUnit(string id, UnitDataConfig config)
        {
            UnitData unitData = ScriptableObject.CreateInstance<UnitData>();
            unitData.unitName = config.unitName;
            unitData.description = config.description;
            unitData.MaxHealth = config.maxHealth;
            unitData.MaxMana = config.maxMana;
            unitData.AttackDamage = config.attackDamage;
            unitData.AttackSpeed = config.attackSpeed;
            unitData.Armor = config.armor;
            unitData.MagicResist = config.magicResist;
            unitData.MovementSpeed = config.movementSpeed;
            unitData.AttackRange = config.attackRange;
            unitData.archetypeType = config.archetypeType;
            unitData.SynergyTag1 = config.synergyTag1;
            unitData.SynergyTag2 = config.synergyTag2;
            unitData.unitType = config.unitType;
            unitData.unitRole = config.unitRole;
            unitData.abilities = config.abilities;

            unitDatabase[id] = unitData;
        }

        #endregion

        #region Ability Data Initialization

        private void InitializeAbilities()
        {
            // Load all abilities from AbilityDefinitions
            // Add new abilities by adding them to AbilityDefinitions.cs
            string[] abilityIds = { "shield_bash", "taunt", "guard_wall", "whirlwind", "battle_cry", "momentum_rush", 
                                    "fireball", "arcane_bolt", "heal", "backstab", "poison_blade" };
            
            foreach (string abilityId in abilityIds)
            {
                var config = AbilityDefinitions.GetAbilityConfig(abilityId);
                if (config != null)
                {
                    CreateAbility(abilityId, config);
                }
            }
        }

        private void CreateAbility(string id, AbilityDataConfig config)
        {
            AbilityData abilityData = ScriptableObject.CreateInstance<AbilityData>();
            abilityData.abilityName = config.abilityName;
            abilityData.description = config.description;
            abilityData.AbilityType = config.abilityType;
            abilityData.ManaCost = config.manaCost;
            abilityData.Cooldown = config.cooldown;
            abilityData.Damage = config.damage;
            abilityData.HealAmount = config.healAmount;
            abilityData.Duration = config.duration;
            abilityData.TargetType = config.targetType;
            abilityData.Range = config.range;

            abilityDatabase[id] = abilityData;
        }

        #endregion

        #region Relic Data Initialization

        private void InitializeRelics()
        {
            // Load all relics from RelicDefinitions
            // Add new relics by adding them to RelicDefinitions.cs
            string[] relicIds = { "arcane_battery", "tainted_dagger", "earth_totem", 
                                 "blood_idol", "storm_core", "shadow_veil", "holy_sigil" };
            
            foreach (string relicId in relicIds)
            {
                var config = RelicDefinitions.GetRelicConfig(relicId);
                if (config != null)
                {
                    CreateRelic(relicId, config);
                }
            }
        }

        private void CreateRelic(string id, RelicConfig config)
        {
            Relic relic = ScriptableObject.CreateInstance<Relic>();
            relic.relicName = config.relicName;
            relic.description = config.description;
            relic.rarity = config.rarity;
            relic.effects = config.effects;

            relicDatabase[id] = relic;
        }

        #endregion

        #region Encounter Data Initialization

        private void InitializeEncounters()
        {
            // Load all encounters from EncounterDefinitions
            string[] encounterIds = {
                // Encounter packs
                "hedge_ambush", "soot_tithe", "sunken_chapel", "bog_hunger", "reliquary_raid",
                // Standard encounters
                "encounter_1_5", "encounter_6_10", "encounter_11_20", "encounter_21_30",
                // Minibosses
                "first_knot", "tollgate_20", "myth_eater_30", "tollgate_40", "myth_eater_50",
                "tollgate_60", "myth_eater_80", "tollgate_90", "sundered_arbiter"
            };

            foreach (string encounterId in encounterIds)
            {
                var config = EncounterDefinitions.GetEncounterConfig(encounterId);
                if (config != null)
                {
                    // Convert enemy IDs to UnitData references
                    if (config.enemyUnitIds != null && config.enemyUnitIds.Count > 0)
                    {
                        List<UnitData> enemyUnitsList = new List<UnitData>();
                        foreach (string enemyId in config.enemyUnitIds)
                        {
                            UnitData enemyData = GetUnit(enemyId);
                            if (enemyData != null)
                            {
                                enemyUnitsList.Add(enemyData);
                            }
                        }
                        config.enemyUnits = enemyUnitsList.ToArray();
                    }

                    CreateEncounter(encounterId, config);
                }
            }
        }

        private void CreateEncounter(string id, EncounterDataConfig config)
        {
            EncounterData encounter = ScriptableObject.CreateInstance<EncounterData>();
            encounter.encounterName = config.encounterName;
            encounter.description = config.description;
            encounter.minLevel = config.minLevel;
            encounter.maxLevel = config.maxLevel;
            encounter.isMiniboss = config.isMiniboss;
            encounter.isMajorMiniboss = config.isMajorMiniboss;
            encounter.isFinalBoss = config.isFinalBoss;
            encounter.enemyUnits = new List<UnitData>(config.enemyUnits ?? new UnitData[0]);
            encounter.goldReward = config.goldReward;

            encounterDatabase[id] = encounter;
        }

        #endregion

        #region Public Access Methods

        public UnitData GetUnit(string id)
        {
            return unitDatabase.TryGetValue(id, out UnitData unit) ? unit : null;
        }

        public AbilityData GetAbility(string id)
        {
            return abilityDatabase.TryGetValue(id, out AbilityData ability) ? ability : null;
        }

        public Relic GetRelic(string id)
        {
            return relicDatabase.TryGetValue(id, out Relic relic) ? relic : null;
        }

        public EncounterData GetEncounter(string id)
        {
            return encounterDatabase.TryGetValue(id, out EncounterData encounter) ? encounter : null;
        }

        public List<UnitData> GetAllUnits()
        {
            return new List<UnitData>(unitDatabase.Values);
        }

        public List<Relic> GetAllRelics()
        {
            return new List<Relic>(relicDatabase.Values);
        }

        public List<EncounterData> GetEncountersForLevel(int level)
        {
            List<EncounterData> validEncounters = new List<EncounterData>();
            foreach (var encounter in encounterDatabase.Values)
            {
                if (encounter != null && level >= encounter.minLevel && level <= encounter.maxLevel)
                {
                    validEncounters.Add(encounter);
                }
            }
            return validEncounters;
        }

        public List<EncounterData> GetAllEncounters()
        {
            return new List<EncounterData>(encounterDatabase.Values);
        }

        #endregion
    }

    #region Configuration Classes

    [System.Serializable]
    public class UnitDataConfig
    {
        public string unitName;
        public string description;
        public float maxHealth = 100f;
        public float maxMana = 50f;
        public float attackDamage = 10f;
        public float attackSpeed = 1f;
        public float armor = 0f;
        public float magicResist = 0f;
        public float movementSpeed = 1f;
        public float attackRange = 1.5f;
        public Core.Archetypes.ArchetypeType archetypeType;
        public SynergyTag synergyTag1 = SynergyTag.None;
        public SynergyTag synergyTag2 = SynergyTag.None;
        public UnitType unitType;
        public UnitRole unitRole;
        public AbilityData[] abilities;
    }

    [System.Serializable]
    public class AbilityDataConfig
    {
        public string abilityName;
        public string description;
        public AbilityType abilityType;
        public float manaCost = 20f;
        public float cooldown = 5f;
        public float damage = 0f;
        public float healAmount = 0f;
        public float duration = 0f;
        public TargetType targetType;
        public float range = 5f;
    }

    [System.Serializable]
    public class RelicConfig
    {
        public string relicName;
        public string description;
        public Rarity rarity = Rarity.Common;
        public RelicEffect[] effects;
    }

    [System.Serializable]
    public class EncounterDataConfig
    {
        public string encounterName;
        public string description;
        public int minLevel = 1;
        public int maxLevel = 100;
        public bool isMiniboss = false;
        public bool isMajorMiniboss = false;
        public bool isFinalBoss = false;
        public List<string> enemyUnitIds;  // Enemy IDs (converted to UnitData in GameDataManager)
        public UnitData[] enemyUnits;  // Populated from enemyUnitIds
        public int goldReward = 50;
    }

    #endregion
}

