#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using FourfoldFate.Data;
using FourfoldFate.Balance;
using FourfoldFate.Roguelike;
using FourfoldFate.Party;
using FourfoldFate.Core;
using FourfoldFate.UI;
using FourfoldFate.Relics;
using FourfoldFate.Setup;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Quick one-click scene setup with all managers.
    /// </summary>
    public class QuickSceneSetup : EditorWindow
    {
        [MenuItem("Fourfold Fate/Quick Scene Setup (All Managers)", false, 1)]
        [MenuItem("GameObject/Fourfold Fate/Quick Scene Setup", false, 10)]
        public static void SetupScene()
        {
            Debug.Log("Starting Quick Scene Setup...");

            // Create managers GameObject
            GameObject managersObj = new GameObject("Managers");
            
            // Create all managers
            managersObj.AddComponent<GameDataManager>();
            managersObj.AddComponent<BalanceManager>();
            managersObj.AddComponent<RunManager>();
            managersObj.AddComponent<EncounterManager>();
            managersObj.AddComponent<ProgressionManager>();
            managersObj.AddComponent<PartyManager>();
            managersObj.AddComponent<RelicManager>();
            managersObj.AddComponent<BattleManager>();
            managersObj.AddComponent<UIManager>();
            managersObj.AddComponent<SceneDiagnostics>();

            Debug.Log("✅ Created all managers");

            // Create Canvas if it doesn't exist
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                Debug.Log("✅ Created Canvas");
            }

            // Setup Battle UI
            BattleUISetup.SetupBattleUI();

            // Find Main Menu UI or create it
            MainMenuUI mainMenu = FindFirstObjectByType<MainMenuUI>();
            if (mainMenu == null)
            {
                // Create simple main menu
                GameObject menuObj = new GameObject("MainMenuPanel");
                menuObj.transform.SetParent(canvas.transform, false);
                RectTransform menuRect = menuObj.AddComponent<RectTransform>();
                menuRect.anchorMin = Vector2.zero;
                menuRect.anchorMax = Vector2.one;
                menuRect.sizeDelta = Vector2.zero;

                Image bg = menuObj.AddComponent<Image>();
                bg.color = new Color(0.1f, 0.1f, 0.15f, 1f);

                // Title
                GameObject titleObj = new GameObject("Title");
                titleObj.transform.SetParent(menuObj.transform, false);
                UnityEngine.UI.Text titleText = titleObj.AddComponent<UnityEngine.UI.Text>();
                titleText.text = "Fourfold Fate";
                titleText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                titleText.fontSize = 48;
                titleText.color = Color.white;
                titleText.alignment = TextAnchor.MiddleCenter;
                RectTransform titleRect = titleObj.GetComponent<RectTransform>();
                titleRect.anchorMin = new Vector2(0.5f, 0.8f);
                titleRect.anchorMax = new Vector2(0.5f, 0.8f);
                titleRect.sizeDelta = new Vector2(400, 60);
                titleRect.anchoredPosition = Vector2.zero;

                // Start button
                GameObject startBtn = CreateMenuButton("Start Run", menuObj.transform, new Vector2(0.5f, 0.5f));
                startBtn.GetComponent<Button>().onClick.AddListener(() => {
                    if (RunManager.Instance != null)
                    {
                        RunManager.Instance.StartNewRun();
                        if (UIManager.Instance != null)
                            UIManager.Instance.ShowBattleArena();
                    }
                });

                mainMenu = menuObj.AddComponent<MainMenuUI>();
                mainMenu.rootPanel = menuObj;
                mainMenu.titleText = titleText;
                mainMenu.startRunButton = startBtn.GetComponent<Button>();

                Debug.Log("✅ Created Main Menu UI");
            }

            // Assign UI to UIManager
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.mainMenuUI = mainMenu;
                uiManager.battleArenaUI = FindFirstObjectByType<BattleArenaUI>();
                Debug.Log("✅ Assigned UI to UIManager");
            }

            // Assign managers to RunManager
            RunManager runManager = FindFirstObjectByType<RunManager>();
            if (runManager != null)
            {
                runManager.encounterManager = FindFirstObjectByType<EncounterManager>();
                runManager.progressionManager = FindFirstObjectByType<ProgressionManager>();
                runManager.partyManager = FindFirstObjectByType<PartyManager>();
                Debug.Log("✅ Assigned managers to RunManager");
            }

            Debug.Log("✅ Quick Scene Setup Complete!");
            Debug.Log("Press Play and check Console for diagnostics.");
        }

        private static GameObject CreateMenuButton(string text, Transform parent, Vector2 anchor)
        {
            GameObject btn = new GameObject(text + "Button");
            btn.transform.SetParent(parent, false);

            Image img = btn.AddComponent<Image>();
            img.color = new Color(0.2f, 0.4f, 0.8f, 1f);

            Button button = btn.AddComponent<Button>();
            button.targetGraphic = img;

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btn.transform, false);
            UnityEngine.UI.Text txt = textObj.AddComponent<UnityEngine.UI.Text>();
            txt.text = text;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.fontSize = 24;
            txt.color = Color.white;
            txt.alignment = TextAnchor.MiddleCenter;

            RectTransform txtRect = textObj.GetComponent<RectTransform>();
            txtRect.anchorMin = Vector2.zero;
            txtRect.anchorMax = Vector2.one;
            txtRect.sizeDelta = Vector2.zero;
            txtRect.anchoredPosition = Vector2.zero;

            RectTransform btnRect = btn.GetComponent<RectTransform>();
            btnRect.anchorMin = anchor;
            btnRect.anchorMax = anchor;
            btnRect.sizeDelta = new Vector2(200, 50);
            btnRect.anchoredPosition = Vector2.zero;

            return btn;
        }
    }
}
#endif

