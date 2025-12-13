using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages individual combat encounters during a run.
    /// Generates enemies and sets up battles.
    /// </summary>
    public class EncounterManager : MonoBehaviour
    {
        [Header("Encounter Settings")]
        [SerializeField] private List<EncounterData> encounterPool = new List<EncounterData>();
        
        private BattleManager battleManager;
        private List<Unit> currentEnemies = new List<Unit>();
        private Data.GameDataManager dataManager;

        private void Awake()
        {
            battleManager = FindObjectOfType<BattleManager>();
            dataManager = FindObjectOfType<Data.GameDataManager>();
        }

        public void StartEncounter(int level, bool isMiniboss)
        {
            // Select encounter based on level and boss status
            EncounterData encounter = SelectEncounter(level, isMiniboss);
            
            if (encounter != null)
            {
                SpawnEnemies(encounter, level);
                
                // Start battle
                if (battleManager != null)
                {
                    var partyManager = FindObjectOfType<Party.PartyManager>();
                    if (partyManager != null)
                    {
                        battleManager.StartBattle(partyManager.PartyMembers, currentEnemies);
                    }
                }
            }
        }

        private EncounterData SelectEncounter(int level, bool isMiniboss)
        {
            List<EncounterData> validEncounters = new List<EncounterData>();
            
            // First try to get encounters from GameDataManager
            if (dataManager != null)
            {
                var allEncounters = dataManager.GetEncountersForLevel(level);
                validEncounters = allEncounters.FindAll(e => 
                    level >= e.minLevel && level <= e.maxLevel && 
                    e.isMiniboss == isMiniboss);
                
                if (validEncounters.Count == 0)
                {
                    // Fallback: just match level range
                    validEncounters = allEncounters.FindAll(e => 
                        level >= e.minLevel && level <= e.maxLevel);
                }
            }
            
            // Fallback to inspector-assigned encounters
            if (validEncounters.Count == 0 && encounterPool != null && encounterPool.Count > 0)
            {
                validEncounters = encounterPool.FindAll(e => 
                    level >= e.minLevel && level <= e.maxLevel && 
                    e.isMiniboss == isMiniboss);
                
                if (validEncounters.Count == 0)
                {
                    validEncounters = encounterPool.FindAll(e => 
                        level >= e.minLevel && level <= e.maxLevel);
                }
                
                if (validEncounters.Count == 0)
                    validEncounters = encounterPool;
            }
            
            if (validEncounters.Count == 0)
            {
                Debug.LogWarning($"No encounters found for level {level}, isMiniboss: {isMiniboss}");
                return null;
            }
            
            // Random selection from valid encounters
            return validEncounters[Random.Range(0, validEncounters.Count)];
        }

        [Header("Enemy Spawning")]
        [SerializeField] private GameObject enemyPrefab;  // Assign in Inspector
        [SerializeField] private Transform[] enemySpawnPoints;  // Assign spawn positions
        
        private void SpawnEnemies(EncounterData encounter, int level)
        {
            // Clear previous enemies
            foreach (var enemy in currentEnemies)
            {
                if (enemy != null)
                    Destroy(enemy.gameObject);
            }
            currentEnemies.Clear();
            
            if (encounter == null || encounter.enemyUnits == null || encounter.enemyUnits.Count == 0)
            {
                Debug.LogWarning("Encounter has no enemy units!");
                return;
            }
            
            // Spawn new enemies with level scaling
            float difficultyMultiplier = CalculateDifficultyMultiplier(level);
            
            for (int i = 0; i < encounter.enemyUnits.Count; i++)
            {
                var enemyData = encounter.enemyUnits[i];
                if (enemyData == null) continue;

                // Get spawn position
                Vector3 spawnPosition = GetEnemySpawnPosition(i);
                
                // Instantiate enemy unit
                GameObject enemyObj = null;
                if (enemyPrefab != null)
                {
                    enemyObj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    // Fallback: create basic GameObject
                    enemyObj = new GameObject($"Enemy_{enemyData.unitName}");
                    enemyObj.transform.position = spawnPosition;
                }
                
                Unit enemy = enemyObj.GetComponent<Unit>();
                if (enemy == null)
                {
                    enemy = enemyObj.AddComponent<Unit>();
                }
                
                // Initialize with enemy data
                enemy.Initialize(enemyData);
                
                // Apply difficulty scaling
                ScaleUnitStats(enemy, difficultyMultiplier);
                
                currentEnemies.Add(enemy);
            }
        }

        private Vector3 GetEnemySpawnPosition(int index)
        {
            // Use spawn points if available
            if (enemySpawnPoints != null && index < enemySpawnPoints.Length && enemySpawnPoints[index] != null)
            {
                return enemySpawnPoints[index].position;
            }
            
            // Fallback: spawn in a line
            float spacing = 2f;
            float startX = 5f;  // Right side of screen
            return new Vector3(startX + (index * spacing), 0f, 0f);
        }

        private void ScaleUnitStats(Unit unit, float multiplier)
        {
            if (unit == null || unit.Data == null) return;
            
            // Scale health and damage
            var unitData = unit.Data;
            unitData.MaxHealth *= multiplier;
            unitData.AttackDamage *= multiplier;
            
            // Reinitialize unit with scaled stats
            unit.Initialize(unitData);
        }

        private float CalculateDifficultyMultiplier(int level)
        {
            // Exponential scaling: 1.15^(level-1)
            return Mathf.Pow(1.15f, level - 1);
        }
    }
}

