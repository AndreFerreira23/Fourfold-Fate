using UnityEngine;

namespace FourfoldFate
{
    /// <summary>
    /// Main game manager that coordinates all systems.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Managers")]
        public Data.GameDataManager gameDataManager;
        public Balance.BalanceManager balanceManager;
        public Roguelike.RunManager runManager;
        public Party.PartyManager partyManager;
        public Relics.RelicManager relicManager;
        public UI.UIManager uiManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                // Only call DontDestroyOnLoad if this is a root GameObject
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Ensure all managers are initialized
            if (gameDataManager == null)
            {
                gameDataManager = FindFirstObjectByType<Data.GameDataManager>();
            }

            if (balanceManager == null)
            {
                balanceManager = FindFirstObjectByType<Balance.BalanceManager>();
            }

            if (runManager == null)
            {
                runManager = FindFirstObjectByType<Roguelike.RunManager>();
            }

            if (partyManager == null)
            {
                partyManager = FindFirstObjectByType<Party.PartyManager>();
            }

            if (relicManager == null)
            {
                relicManager = FindFirstObjectByType<Relics.RelicManager>();
            }

            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UI.UIManager>();
            }
        }
    }
}

