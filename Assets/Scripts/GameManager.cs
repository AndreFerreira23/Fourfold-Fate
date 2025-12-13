using UnityEngine;
using FourfoldFate.Agents;
using FourfoldFate.Roguelike;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Data;
using FourfoldFate.UI;
using FourfoldFate.Setup;

namespace FourfoldFate
{
    /// <summary>
    /// Main game manager that coordinates all systems.
    /// Entry point for the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private AgentManager agentManager;
        [SerializeField] private RunManager runManager;
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private PartyManager partyManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GameDataManager dataManager;
        
        [Header("Game State")]
        [SerializeField] private GameState currentState = GameState.MainMenu;

        [Header("Prefabs")]
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private string startingCharacterId = "guardian_shield";

        public GameState CurrentState => currentState;

        private void Awake()
        {
            InitializeManagers();
            ConnectSystemEvents();
        }

        private void InitializeManagers()
        {
            if (agentManager == null)
                agentManager = FindObjectOfType<AgentManager>();
            
            if (runManager == null)
                runManager = FindObjectOfType<RunManager>();
            
            if (battleManager == null)
                battleManager = FindObjectOfType<BattleManager>();

            if (partyManager == null)
                partyManager = FindObjectOfType<PartyManager>();

            if (uiManager == null)
                uiManager = FindObjectOfType<UIManager>();

            if (dataManager == null)
                dataManager = FindObjectOfType<GameDataManager>();
        }

        private void ConnectSystemEvents()
        {
            // Connect RunManager events
            if (runManager != null)
            {
                runManager.OnRunStarted += OnRunStarted;
                runManager.OnEncounterStarted += OnEncounterStarted;
                runManager.OnLevelUpAvailable += OnLevelUpAvailable;
                runManager.OnRunEnded += OnRunEnded;
            }

            // Connect BattleManager events
            if (battleManager != null)
            {
                battleManager.OnBattleStarted += OnBattleStarted;
                battleManager.OnBattleEnded += OnBattleEnded;
            }

            // Connect UI events
            if (uiManager != null && FindObjectOfType<MainMenuUI>() != null)
            {
                var mainMenu = FindObjectOfType<MainMenuUI>();
                mainMenu.OnStartRun += StartNewRun;
            }
        }

        public void StartNewRun()
        {
            if (runManager == null || dataManager == null || unitPrefab == null)
            {
                Debug.LogError("Cannot start run: Missing managers or prefabs!");
                return;
            }

            // Create starting unit
            UnitData startingData = dataManager.GetUnit(startingCharacterId);
            if (startingData == null)
            {
                Debug.LogError($"Starting character '{startingCharacterId}' not found!");
                return;
            }

            GameObject unitObj = Instantiate(unitPrefab);
            unitObj.name = startingData.unitName;
            unitObj.transform.position = new Vector3(-3f, 0f, 0f);

            Unit startingUnit = unitObj.GetComponent<Unit>();
            if (startingUnit != null)
            {
                startingUnit.Initialize(startingData);
            }

            // Start run
            if (runManager != null)
            {
                runManager.StartNewRun(startingUnit);
                currentState = GameState.InRun;
            }

            // Show battle UI
            if (uiManager != null)
            {
                uiManager.ShowBattleArena();
            }
        }

        private void OnRunStarted()
        {
            Debug.Log("Run started!");
            currentState = GameState.InRun;
        }

        private void OnEncounterStarted(int level)
        {
            Debug.Log($"Encounter started at level {level}");
            currentState = GameState.InBattle;
            
            if (uiManager != null)
            {
                uiManager.ShowBattleArena();
            }
        }

        private void OnBattleStarted()
        {
            Debug.Log("Battle started!");
            currentState = GameState.InBattle;
        }

        private void OnBattleEnded(BattleResult result)
        {
            Debug.Log($"Battle ended: {result}");
            
            if (result == BattleResult.Victory && runManager != null)
            {
                runManager.CompleteEncounter(true);
            }
            else if (result == BattleResult.Defeat)
            {
                if (runManager != null)
                {
                    runManager.CompleteEncounter(false);
                }
            }
        }

        private void OnLevelUpAvailable(int level)
        {
            Debug.Log($"Level up available at level {level}");
            
            if (uiManager != null)
            {
                uiManager.ShowLevelUp();
            }
        }

        private void OnRunEnded(bool victory)
        {
            Debug.Log($"Run ended: {(victory ? "Victory" : "Defeat")}");
            currentState = GameState.GameOver;
            
            if (uiManager != null)
            {
                uiManager.ShowMainMenu();
            }
        }

        public void ReturnToMainMenu()
        {
            currentState = GameState.MainMenu;
            
            if (uiManager != null)
            {
                uiManager.ShowMainMenu();
            }
        }

        // Example: Using agents to get suggestions
        public void RequestStorySuggestion()
        {
            if (agentManager != null)
            {
                var request = new AgentRequest
                {
                    RequestType = "suggest_theme",
                    Context = "Roguelike autobattler game"
                };
                
                var response = agentManager.RequestAgent("story", request);
                Debug.Log($"Story Agent: {response.Message}");
            }
        }

        public void RequestBalanceAnalysis()
        {
            if (agentManager != null)
            {
                var request = new AgentRequest
                {
                    RequestType = "analyze_balance",
                    Context = "Current game state"
                };
                
                var response = agentManager.RequestAgent("balance", request);
                Debug.Log($"Balance Agent: {response.Message}");
            }
        }
    }

    public enum GameState
    {
        MainMenu,
        InRun,
        InBattle,
        Paused,
        GameOver
    }
}

