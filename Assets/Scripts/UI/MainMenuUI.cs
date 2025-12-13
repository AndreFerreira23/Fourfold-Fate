using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Main menu UI with lore-integrated text.
    /// </summary>
    public class MainMenuUI : BaseUI
    {
        [Header("UI Elements")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text taglineText;
        [SerializeField] private Text loreText;
        [SerializeField] private Button startRunButton;
        [SerializeField] private Button continueRunButton;
        [SerializeField] private Button metaProgressionButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        [Header("Lore Text")]
        [SerializeField] private string titleLore = "Fourfold Fate";
        [SerializeField] private string taglineLore = "Four seats. One climb. A hundred chances to become worth the summit.";
        [SerializeField] private string introLore = "The Hall of Echoes is quiet until you arrive. A circle waits with four empty seats and one lit candle. You take the first seat. The Trials begin to write your name.";

        private RunManager runManager;

        protected override void Awake()
        {
            base.Awake();
            runManager = FindObjectOfType<RunManager>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetupUI();
            SetupButtons();
        }

        private void SetupUI()
        {
            if (titleText != null)
            {
                titleText.text = titleLore;
                // Ensure overflow settings are correct
                titleText.horizontalOverflow = HorizontalWrapMode.Overflow;
                titleText.verticalOverflow = VerticalWrapMode.Overflow;
                // Force text to update
                titleText.SetAllDirty();
            }

            if (taglineText != null)
            {
                taglineText.text = taglineLore;
                taglineText.horizontalOverflow = HorizontalWrapMode.Overflow;
                taglineText.verticalOverflow = VerticalWrapMode.Overflow;
            }

            if (loreText != null)
            {
                loreText.text = introLore;
                loreText.horizontalOverflow = HorizontalWrapMode.Overflow;
                loreText.verticalOverflow = VerticalWrapMode.Overflow;
            }
        }

        private void SetupButtons()
        {
            if (startRunButton != null)
                startRunButton.onClick.AddListener(OnStartRunClicked);

            if (continueRunButton != null)
            {
                continueRunButton.onClick.AddListener(OnContinueRunClicked);
                // Only show if there's an active run
                continueRunButton.gameObject.SetActive(runManager != null && runManager.IsRunActive);
            }

            if (metaProgressionButton != null)
                metaProgressionButton.onClick.AddListener(OnMetaProgressionClicked);

            if (settingsButton != null)
                settingsButton.onClick.AddListener(OnSettingsClicked);

            if (quitButton != null)
                quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnStartRunClicked()
        {
            // This will be handled by GameManager
            OnStartRun?.Invoke();
        }

        private void OnContinueRunClicked()
        {
            if (runManager != null && runManager.IsRunActive)
            {
                OnContinueRun?.Invoke();
            }
        }

        private void OnMetaProgressionClicked()
        {
            OnMetaProgression?.Invoke();
        }

        private void OnSettingsClicked()
        {
            OnSettings?.Invoke();
        }

        private void OnQuitClicked()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        // Events
        public System.Action OnStartRun;
        public System.Action OnContinueRun;
        public System.Action OnMetaProgression;
        public System.Action OnSettings;
    }
}

