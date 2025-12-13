using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Data;
using FourfoldFate.Party;
using FourfoldFate.Roguelike;
using FourfoldFate.UI;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Initializes the game on startup. Handles first-time setup and connects all systems.
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        [Header("Initialization")]
        [SerializeField] private bool initializeOnStart = true;
        [SerializeField] private string startingCharacterId = "guardian_shield";

        [Header("Prefabs")]
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private GameObject enemyPrefab;

        private void Start()
        {
            if (initializeOnStart)
            {
                InitializeGame();
            }
        }

        public void InitializeGame()
        {
            Debug.Log("Initializing Fourfold Fate...");

            // Ensure GameDataManager is initialized
            GameDataManager dataManager = FindObjectOfType<GameDataManager>();
            if (dataManager == null)
            {
                Debug.LogError("GameDataManager not found! Run SceneSetupHelper first.");
                return;
            }

            // Ensure all managers exist
            EnsureManagersExist();

            // Setup UI
            SetupUI();

            // Create starting character if in a run
            if (Application.isPlaying)
            {
                CreateStartingCharacter();
            }

            Debug.Log("Game initialization complete!");
        }

        private void EnsureManagersExist()
        {
            // Check for critical managers
            if (FindObjectOfType<PartyManager>() == null)
            {
                Debug.LogWarning("PartyManager not found!");
            }

            if (FindObjectOfType<RunManager>() == null)
            {
                Debug.LogWarning("RunManager not found!");
            }

            if (FindObjectOfType<BattleManager>() == null)
            {
                Debug.LogWarning("BattleManager not found!");
            }
        }

        private void SetupUI()
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.ShowMainMenu();
            }
        }

        private void CreateStartingCharacter()
        {
            if (unitPrefab == null)
            {
                Debug.LogWarning("Unit prefab not assigned! Cannot create starting character.");
                return;
            }

            GameDataManager dataManager = GameDataManager.Instance;
            if (dataManager == null) return;

            UnitData startingData = dataManager.GetUnit(startingCharacterId);
            if (startingData == null)
            {
                Debug.LogWarning($"Starting character '{startingCharacterId}' not found!");
                return;
            }

            // Create starting unit
            GameObject unitObj = Instantiate(unitPrefab);
            unitObj.name = startingData.unitName;
            unitObj.transform.position = new Vector3(-3f, 0f, 0f);  // Left side of screen

            Unit unit = unitObj.GetComponent<Unit>();
            if (unit != null)
            {
                unit.Initialize(startingData);
            }
        }

        /// <summary>
        /// Create a unit from UnitData
        /// </summary>
        public Unit CreateUnit(UnitData unitData, Vector3 position)
        {
            if (unitPrefab == null || unitData == null) return null;

            GameObject unitObj = Instantiate(unitPrefab, position, Quaternion.identity);
            unitObj.name = unitData.unitName;

            Unit unit = unitObj.GetComponent<Unit>();
            if (unit != null)
            {
                unit.Initialize(unitData);
            }

            return unit;
        }

        /// <summary>
        /// Create an enemy from UnitData
        /// </summary>
        public Unit CreateEnemy(UnitData enemyData, Vector3 position)
        {
            if (enemyPrefab == null || enemyData == null) return null;

            GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemyObj.name = enemyData.unitName;

            Unit enemy = enemyObj.GetComponent<Unit>();
            if (enemy != null)
            {
                enemy.Initialize(enemyData);
            }

            return enemy;
        }
    }
}

