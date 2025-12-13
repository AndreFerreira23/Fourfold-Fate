using UnityEngine;
using FourfoldFate.Agents;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Progression;
using FourfoldFate.Relics;
using FourfoldFate.Roguelike;
using FourfoldFate.UI;
using FourfoldFate.Data;
using FourfoldFate.Balance;
using FourfoldFate.MetaProgression;
using FourfoldFate.Animation;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Helper script to automatically set up the game scene with all required managers.
    /// Attach this to an empty GameObject and run SetupScene() to create everything.
    /// </summary>
    public class SceneSetupHelper : MonoBehaviour
    {
        [Header("Setup Options")]
        [SerializeField] private bool setupOnStart = false;
        [SerializeField] private bool createPrefabs = true;

        [ContextMenu("Setup Scene")]
        public void SetupScene()
        {
            Debug.Log("Setting up Fourfold Fate scene...");

            // Create manager hierarchy
            GameObject managersRoot = CreateManagersRoot();
            
            // Core managers
            CreateGameDataManager(managersRoot);
            CreateBalanceManager(managersRoot);
            CreateGameManager(managersRoot);
            
            // Gameplay managers
            CreateRunManager(managersRoot);
            CreateBattleManager(managersRoot);
            CreatePartyManager(managersRoot);
            CreateEncounterManager(managersRoot);
            CreateProgressionManager(managersRoot);
            CreateLevelUpSystem(managersRoot);
            CreateRelicManager(managersRoot);
            
            // Meta-progression
            CreateMetaProgressionManager(managersRoot);
            
            // Agents
            CreateAgentManager(managersRoot);
            
            // UI
            CreateUISystem();
            
            // Animation
            CreateAnimationSystem(managersRoot);
            
            // Camera setup
            SetupCamera();
            
            Debug.Log("Scene setup complete!");
        }

        private GameObject CreateManagersRoot()
        {
            GameObject root = GameObject.Find("Managers");
            if (root == null)
            {
                root = new GameObject("Managers");
            }
            return root;
        }

        private void CreateGameDataManager(GameObject parent)
        {
            if (FindObjectOfType<GameDataManager>() != null) return;
            
            // GameDataManager needs to be root for DontDestroyOnLoad
            GameObject obj = new GameObject("GameDataManager");
            // Don't parent it - keep as root
            obj.AddComponent<GameDataManager>();
        }

        private void CreateBalanceManager(GameObject parent)
        {
            if (FindObjectOfType<BalanceManager>() != null) return;
            
            // BalanceManager needs to be root for DontDestroyOnLoad
            GameObject obj = new GameObject("BalanceManager");
            // Don't parent it - keep as root
            obj.AddComponent<BalanceManager>();
        }

        private void CreateGameManager(GameObject parent)
        {
            if (FindObjectOfType<GameManager>() != null) return;
            
            GameObject obj = new GameObject("GameManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<GameManager>();
        }

        private void CreateRunManager(GameObject parent)
        {
            if (FindObjectOfType<RunManager>() != null) return;
            
            GameObject obj = new GameObject("RunManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<RunManager>();
        }

        private void CreateBattleManager(GameObject parent)
        {
            if (FindObjectOfType<BattleManager>() != null) return;
            
            GameObject obj = new GameObject("BattleManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<BattleManager>();
        }

        private void CreatePartyManager(GameObject parent)
        {
            if (FindObjectOfType<PartyManager>() != null) return;
            
            GameObject obj = new GameObject("PartyManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<PartyManager>();
        }

        private void CreateEncounterManager(GameObject parent)
        {
            RunManager runManager = FindObjectOfType<RunManager>();
            if (runManager == null) return;
            
            if (runManager.GetComponent<EncounterManager>() != null) return;
            
            runManager.gameObject.AddComponent<EncounterManager>();
        }

        private void CreateProgressionManager(GameObject parent)
        {
            RunManager runManager = FindObjectOfType<RunManager>();
            if (runManager == null) return;
            
            if (runManager.GetComponent<ProgressionManager>() != null) return;
            
            runManager.gameObject.AddComponent<ProgressionManager>();
        }

        private void CreateLevelUpSystem(GameObject parent)
        {
            RunManager runManager = FindObjectOfType<RunManager>();
            if (runManager == null) return;
            
            if (runManager.GetComponent<LevelUpSystem>() != null) return;
            
            runManager.gameObject.AddComponent<LevelUpSystem>();
        }

        private void CreateRelicManager(GameObject parent)
        {
            if (FindObjectOfType<RelicManager>() != null) return;
            
            GameObject obj = new GameObject("RelicManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<RelicManager>();
        }

        private void CreateMetaProgressionManager(GameObject parent)
        {
            if (FindObjectOfType<MetaProgressionManager>() != null) return;
            
            GameObject obj = new GameObject("MetaProgressionManager");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<MetaProgressionManager>();
        }

        private void CreateAgentManager(GameObject parent)
        {
            if (FindObjectOfType<AgentManager>() != null) return;
            
            GameObject obj = new GameObject("AgentManager");
            obj.transform.SetParent(parent.transform);
            AgentManager agentManager = obj.AddComponent<AgentManager>();
            
            // Add agent components
            obj.AddComponent<StoryAgent>();
            obj.AddComponent<BalanceAgent>();
            obj.AddComponent<CodeAgent>();
        }

        private void CreateUISystem()
        {
            // Create Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }

            // Create UIManager
            if (FindObjectOfType<UIManager>() == null)
            {
                GameObject uiManagerObj = new GameObject("UIManager");
                uiManagerObj.transform.SetParent(canvas.transform);
                uiManagerObj.AddComponent<UIManager>();
            }

            // Note: UI prefabs need to be created manually in Unity
            Debug.Log("UI system created. Remember to create UI prefabs and assign them to UIManager.");
        }

        private void CreateAnimationSystem(GameObject parent)
        {
            if (FindObjectOfType<AttackAnimationSystem>() != null) return;
            
            GameObject obj = new GameObject("AttackAnimationSystem");
            obj.transform.SetParent(parent.transform);
            obj.AddComponent<AttackAnimationSystem>();
        }

        private void SetupCamera()
        {
            Camera mainCamera = Camera.main;
            if (mainCamera == null)
            {
                GameObject cameraObj = new GameObject("Main Camera");
                mainCamera = cameraObj.AddComponent<Camera>();
                cameraObj.tag = "MainCamera";
            }

            // Set up 2D camera
            mainCamera.orthographic = true;
            mainCamera.orthographicSize = 5f;
            mainCamera.transform.position = new Vector3(0, 0, -10);
        }

        private void Start()
        {
            if (setupOnStart)
            {
                SetupScene();
            }
        }
    }
}

