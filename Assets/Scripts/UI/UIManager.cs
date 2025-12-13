using UnityEngine;
using UnityEngine.UI;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Central manager for all UI systems.
    /// Handles UI state transitions and screen management.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Screens")]
        [SerializeField] private MainMenuUI mainMenuUI;
        [SerializeField] private BattleArenaUI battleArenaUI;
        [SerializeField] private PartyManagementUI partyManagementUI;
        [SerializeField] private LevelUpUI levelUpUI;
        [SerializeField] private RelicSelectionUI relicSelectionUI;
        [SerializeField] private RunProgressionUI runProgressionUI;

        [Header("UI State")]
        [SerializeField] private UIState currentState = UIState.MainMenu;

        public UIState CurrentState => currentState;

        private void Awake()
        {
            InitializeUI();
        }

        private void Start()
        {
            // Show main menu on startup
            ShowMainMenu();
        }

        private void InitializeUI()
        {
            // Hide all UI screens initially (except main menu which will be shown in Start)
            if (battleArenaUI != null) battleArenaUI.Hide();
            if (partyManagementUI != null) partyManagementUI.Hide();
            if (levelUpUI != null) levelUpUI.Hide();
            if (relicSelectionUI != null) relicSelectionUI.Hide();
            if (runProgressionUI != null) runProgressionUI.Hide();
            // Don't hide mainMenuUI here - it will be shown in Start()
        }

        public void ShowMainMenu()
        {
            HideAllScreens();
            if (mainMenuUI != null) mainMenuUI.Show();
            currentState = UIState.MainMenu;
        }

        public void ShowBattleArena()
        {
            HideAllScreens();
            if (battleArenaUI != null) battleArenaUI.Show();
            currentState = UIState.Battle;
        }

        public void ShowPartyManagement()
        {
            HideAllScreens();
            if (partyManagementUI != null) partyManagementUI.Show();
            currentState = UIState.PartyManagement;
        }

        public void ShowLevelUp()
        {
            if (levelUpUI != null) levelUpUI.Show();
            currentState = UIState.LevelUp;
        }

        public void ShowRelicSelection()
        {
            if (relicSelectionUI != null) relicSelectionUI.Show();
            currentState = UIState.RelicSelection;
        }

        public void ShowRunProgression()
        {
            if (runProgressionUI != null) runProgressionUI.Show();
            currentState = UIState.RunProgression;
        }

        private void HideAllScreens()
        {
            if (mainMenuUI != null) mainMenuUI.Hide();
            if (battleArenaUI != null) battleArenaUI.Hide();
            if (partyManagementUI != null) partyManagementUI.Hide();
            if (levelUpUI != null) levelUpUI.Hide();
            if (relicSelectionUI != null) relicSelectionUI.Hide();
            if (runProgressionUI != null) runProgressionUI.Hide();
        }

        // Events
        public System.Action<UIState> OnStateChanged;
    }

    public enum UIState
    {
        MainMenu,
        Battle,
        PartyManagement,
        LevelUp,
        RelicSelection,
        RunProgression,
        Paused
    }
}

