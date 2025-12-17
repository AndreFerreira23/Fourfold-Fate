using System.Collections.Generic;
using FourfoldFate.Core;
using FourfoldFate.Data;
using UnityEngine;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages encounter selection and enemy spawning.
    /// </summary>
    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Select an encounter for the given level.
        /// </summary>
        public EncounterData SelectEncounter(int level, bool isMiniboss)
        {
            // Get encounter from GameDataManager
            var gameData = GameDataManager.Instance;
            if (gameData == null) return null;

            // Find appropriate encounter
            foreach (var encounter in gameData.GetAllEncounters())
            {
                if (encounter.minLevel <= level && level <= encounter.maxLevel)
                {
                    if (isMiniboss && encounter.isMiniboss) return encounter;
                    if (!isMiniboss && !encounter.isMiniboss && !encounter.isFinalBoss) return encounter;
                }
            }

            return null;
        }

        /// <summary>
        /// Spawn enemies for an encounter.
        /// </summary>
        public List<Unit> SpawnEnemies(EncounterData encounter)
        {
            List<Unit> enemies = new List<Unit>();

            if (encounter == null || encounter.enemyUnits == null) return enemies;

            foreach (var enemyData in encounter.enemyUnits)
            {
                GameObject enemyObj = new GameObject(enemyData.unitName);
                Unit enemy = enemyObj.AddComponent<Unit>();
                enemy.Initialize(enemyData);
                enemies.Add(enemy);
            }

            return enemies;
        }
    }
}

