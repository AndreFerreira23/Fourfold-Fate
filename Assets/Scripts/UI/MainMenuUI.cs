using UnityEngine;
using UnityEngine.UI;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Main menu UI screen.
    /// </summary>
    public class MainMenuUI : BaseUI
    {
        [Header("UI Elements")]
        public Text titleText;
        public Text taglineText;
        public Text introLoreText;
        public Button startRunButton;
        public Button continueRunButton;
        public Button metaProgressionButton;
        public Button settingsButton;
        public Button quitButton;

        private void Start()
        {
            SetupUI();
            SetupButtons();
        }

        private void SetupUI()
        {
            if (titleText != null)
            {
                titleText.text = "Fourfold Fate";
            }

            if (taglineText != null)
            {
                taglineText.text = "A Roguelike Adventure";
            }

            if (introLoreText != null)
            {
                introLoreText.text = "Welcome to the March...";
            }
        }

        private void SetupButtons()
        {
            if (startRunButton != null)
            {
                startRunButton.onClick.AddListener(OnStartRun);
            }

            if (continueRunButton != null)
            {
                continueRunButton.onClick.AddListener(OnContinueRun);
            }

            if (quitButton != null)
            {
                quitButton.onClick.AddListener(OnQuit);
            }
        }

        public void OnStartRun()  // Changed to public so it can be called from button
        {
            Debug.Log("=== Start Run button clicked ===");
            
            // Try to find RunManager if Instance is null
            if (Roguelike.RunManager.Instance == null)
            {
                Debug.LogWarning("RunManager.Instance is null, trying to find it...");
                Roguelike.RunManager runManager = FindFirstObjectByType<Roguelike.RunManager>();
                if (runManager == null)
                {
                    Debug.LogError("❌ RunManager not found in scene! Make sure AutoSceneSetup ran or create RunManager manually.");
                    return;
                }
                Debug.Log("✅ Found RunManager in scene (but Instance wasn't set - this is a timing issue)");
            }
            Debug.Log("✅ RunManager found");

            if (UIManager.Instance == null)
            {
                Debug.LogError("❌ UIManager.Instance is null! Make sure UIManager exists in scene.");
                return;
            }
            Debug.Log("✅ UIManager found");

            if (UIManager.Instance.battleArenaUI == null)
            {
                Debug.LogError("❌ BattleArenaUI is not assigned to UIManager!");
                return;
            }
            Debug.Log("✅ BattleArenaUI found");

            Debug.Log("Calling RunManager.StartNewRun()...");
            Roguelike.RunManager.Instance.StartNewRun();
            
            Debug.Log("Calling UIManager.ShowBattleArena()...");
            UIManager.Instance.ShowBattleArena();
            
            Debug.Log("✅ Run started, switched to Battle Arena");
        }

        private void OnContinueRun()
        {
            // Continue existing run
            UIManager.Instance.ShowBattleArena();
        }

        private void OnQuit()
        {
            Application.Quit();
        }
    }
}

