using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages the overall roguelike run progression (levels 1-100).
    /// </summary>
    public class RunManager : MonoBehaviour
    {
        [Header("Run State")]
        public int currentLevel = 1;
        public bool isRunActive = false;

        [Header("Managers")]
        public EncounterManager encounterManager;
        public ProgressionManager progressionManager;
        public Party.PartyManager partyManager;

        public static RunManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                // Only destroy if there's a different Instance
                Debug.LogWarning($"RunManager.Instance already exists ({Instance.gameObject.name}), destroying duplicate on {gameObject.name}");
                Destroy(gameObject);
            }
            // If Instance == this, we're the singleton, do nothing
        }

        private void Start()
        {
            // Auto-find managers if not assigned
            if (encounterManager == null)
                encounterManager = FindFirstObjectByType<EncounterManager>();
            
            if (progressionManager == null)
                progressionManager = FindFirstObjectByType<ProgressionManager>();
            
            if (partyManager == null)
                partyManager = FindFirstObjectByType<Party.PartyManager>();
        }

        /// <summary>
        /// Start a new run.
        /// </summary>
        public void StartNewRun()
        {
            Debug.Log("StartNewRun called");
            currentLevel = 1;
            isRunActive = true;

            // Auto-find managers if not assigned
            if (encounterManager == null)
                encounterManager = FindFirstObjectByType<EncounterManager>();
            
            if (progressionManager == null)
                progressionManager = FindFirstObjectByType<ProgressionManager>();
            
            if (partyManager == null)
                partyManager = FindFirstObjectByType<Party.PartyManager>();

            // Initialize party with first character (The Warden - Tank)
            if (partyManager == null)
            {
                Debug.LogError("PartyManager is null! Cannot start run.");
                return;
            }

            if (Data.GameDataManager.Instance == null)
            {
                Debug.LogError("GameDataManager.Instance is null! Cannot start run.");
                return;
            }

            var gameData = Data.GameDataManager.Instance;
            
            // Get first player unit
            var allPlayerUnits = gameData.GetAllPlayerUnits();
            UnitData wardenData = null;
            
            if (allPlayerUnits != null && allPlayerUnits.Count > 0)
            {
                wardenData = allPlayerUnits[0];
            }
            else
            {
                // Fallback: try direct ID lookup
                wardenData = gameData.GetUnit("the_warden");
            }
            
            if (wardenData != null)
            {
                GameObject wardenObj = new GameObject("The Warden");
                Core.Unit warden = wardenObj.AddComponent<Core.Unit>();
                warden.Initialize(wardenData);
                partyManager.AddPartyMember(warden);
                Debug.Log($"Added {warden.unitName} to party");
            }
            else
            {
                Debug.LogError("Could not find player unit data! Check UnitDefinitions.");
            }

            // Start first encounter
            StartNextEncounter();
        }

        /// <summary>
        /// Start the next encounter for the current level.
        /// </summary>
        public void StartNextEncounter()
        {
            Debug.Log($"StartNextEncounter called for level {currentLevel}");
            
            if (encounterManager == null)
            {
                Debug.LogError("EncounterManager is null!");
                return;
            }

            bool isMiniboss = IsMinibossLevel(currentLevel);
            var encounter = encounterManager.SelectEncounter(currentLevel, isMiniboss);

            if (encounter == null)
            {
                Debug.LogWarning($"No encounter found for level {currentLevel}");
                return;
            }

            Debug.Log($"Selected encounter: {encounter.encounterName}");
            var enemies = encounterManager.SpawnEnemies(encounter);
            Debug.Log($"Spawned {enemies.Count} enemies");

            if (partyManager == null)
            {
                Debug.LogError("PartyManager is null!");
                return;
            }

            if (Core.BattleManager.Instance == null)
            {
                Debug.LogError("BattleManager.Instance is null!");
                return;
            }

            if (partyManager.partyMembers == null || partyManager.partyMembers.Count == 0)
            {
                Debug.LogError("Party has no members!");
                return;
            }

            Debug.Log($"Starting combat with {partyManager.partyMembers.Count} party members");
            Core.BattleManager.Instance.StartCombat(partyManager.partyMembers, enemies);
        }

        /// <summary>
        /// Check if a level is a miniboss level.
        /// </summary>
        public bool IsMinibossLevel(int level)
        {
            return level == 10 || level == 20 || level == 30 || level == 40 || 
                   level == 50 || level == 60 || level == 80 || level == 90;
        }

        /// <summary>
        /// Check if a level is a major miniboss level.
        /// </summary>
        public bool IsMajorMinibossLevel(int level)
        {
            return level == 30 || level == 50 || level == 80;
        }

        /// <summary>
        /// Check if a level is the final boss level.
        /// </summary>
        public bool IsFinalBossLevel(int level)
        {
            return level == 100;
        }

        /// <summary>
        /// Complete the current level and progress.
        /// </summary>
        public void CompleteLevel()
        {
            if (progressionManager != null)
            {
                progressionManager.GrantRewards(currentLevel - 1, 0);
            }

            currentLevel++;
            if (currentLevel > 100)
            {
                // Run complete
                isRunActive = false;
            }
        }
    }
}

