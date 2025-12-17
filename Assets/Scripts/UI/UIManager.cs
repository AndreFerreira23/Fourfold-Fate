using UnityEngine;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Central manager for all UI systems.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Screens")]
        public MainMenuUI mainMenuUI;
        public BattleArenaUI battleArenaUI;
        public PartyManagementUI partyManagementUI;
        public LevelUpUI levelUpUI;
        public RelicSelectionUI relicSelectionUI;
        public RunProgressionUI runProgressionUI;

        public static UIManager Instance { get; private set; }

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

        private void Start()
        {
            // Show main menu on startup
            if (mainMenuUI != null)
            {
                Debug.Log("UIManager: Showing main menu on startup");
                ShowMainMenu();
            }
            else
            {
                Debug.LogWarning("UIManager: mainMenuUI is null! Cannot show main menu.");
            }
        }

        public void ShowMainMenu()
        {
            HideAll();
            if (mainMenuUI != null) mainMenuUI.Show();
        }

        public void ShowBattleArena()
        {
            HideAll();
            if (battleArenaUI != null) battleArenaUI.Show();
        }

        public void ShowPartyManagement()
        {
            HideAll();
            if (partyManagementUI != null) partyManagementUI.Show();
        }

        public void ShowLevelUp()
        {
            HideAll();
            if (levelUpUI != null) levelUpUI.Show();
        }

        public void ShowRelicSelection()
        {
            HideAll();
            if (relicSelectionUI != null) relicSelectionUI.Show();
        }

        public void ShowRunProgression()
        {
            HideAll();
            if (runProgressionUI != null) runProgressionUI.Show();
        }

        private void HideAll()
        {
            if (mainMenuUI != null) mainMenuUI.Hide();
            if (battleArenaUI != null) battleArenaUI.Hide();
            if (partyManagementUI != null) partyManagementUI.Hide();
            if (levelUpUI != null) levelUpUI.Hide();
            if (relicSelectionUI != null) relicSelectionUI.Hide();
            if (runProgressionUI != null) runProgressionUI.Hide();
        }
    }
}

