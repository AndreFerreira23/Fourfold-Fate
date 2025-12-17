using UnityEngine;
using FourfoldFate.Data;
using FourfoldFate.Balance;
using FourfoldFate.Roguelike;
using FourfoldFate.Party;
using FourfoldFate.Core;
using FourfoldFate.UI;
using FourfoldFate.Relics;
using UnityEngine.UI;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Add this script to ANY GameObject in your scene and it will set up everything automatically.
    /// Just add it, press Play, and it will create all managers and UI.
    /// </summary>
    public class AutoSceneSetup : MonoBehaviour
    {
        [Header("Auto Setup")]
        public bool setupOnStart = true;
        public bool showDebugMessages = true;

        private void Start()
        {
            // Use a coroutine to ensure everything is set up after all Awake() calls
            StartCoroutine(SetupEverythingDelayed());
        }

        private System.Collections.IEnumerator SetupEverythingDelayed()
        {
            // Wait one frame to ensure all Awake() methods have run
            yield return null;
            
            if (setupOnStart)
            {
                yield return StartCoroutine(SetupEverythingCoroutine());
            }
        }

        [ContextMenu("Setup Everything Now")]
        public void SetupEverything()
        {
            StartCoroutine(SetupEverythingCoroutine());
        }

        private System.Collections.IEnumerator SetupEverythingCoroutine()
        {
            if (showDebugMessages) Debug.Log("=== AUTO SETUP STARTING ===");

            // Create managers if they don't exist
            yield return StartCoroutine(CreateManagersCoroutine());

            // Create Canvas if it doesn't exist
            CreateCanvas();

            // Create UI if it doesn't exist
            CreateUI();

            // Wait a bit more to ensure all managers are fully initialized
            yield return null;
            yield return null;

            // Connect everything (this is now a coroutine too)
            yield return StartCoroutine(ConnectManagersCoroutine());

            if (showDebugMessages) Debug.Log("=== AUTO SETUP COMPLETE ===");
            if (showDebugMessages) Debug.Log("Press Play to test the game!");
        }

        private void CreateManagers()
        {
            StartCoroutine(CreateManagersCoroutine());
        }

        private System.Collections.IEnumerator CreateManagersCoroutine()
        {
            // Find Managers GameObject (handle duplicates)
            GameObject managersObj = GameObject.Find("Managers");
            
            // If multiple exist, find the one with most components
            if (managersObj != null)
            {
                GameObject[] allManagers = GameObject.FindGameObjectsWithTag("Untagged");
                GameObject bestManagersObj = managersObj;
                int maxComponents = managersObj.GetComponents<Component>().Length;
                
                foreach (var obj in allManagers)
                {
                    if (obj != null && obj.name == "Managers" && obj != managersObj)
                    {
                        try
                        {
                            int componentCount = obj.GetComponents<Component>().Length;
                            if (componentCount > maxComponents)
                            {
                                maxComponents = componentCount;
                                bestManagersObj = obj;
                            }
                        }
                        catch
                        {
                            // Object was destroyed, skip it
                            continue;
                        }
                    }
                }
                
                managersObj = bestManagersObj;
                
                if (showDebugMessages && allManagers.Length > 1)
                {
                    Debug.Log($"Found multiple Managers GameObjects, using the one with most components ({maxComponents})");
                }
            }
            
            // If not found, create it
            if (managersObj == null)
            {
                managersObj = new GameObject("Managers");
                if (showDebugMessages) Debug.Log("Created Managers GameObject");
            }
            else
            {
                if (showDebugMessages) Debug.Log($"Using existing Managers GameObject: {managersObj.name}");
            }

            // Verify managersObj is still valid
            if (managersObj == null)
            {
                Debug.LogError("❌ Managers GameObject was destroyed! Creating new one.");
                managersObj = new GameObject("Managers");
            }
            
            // Add all managers
            if (managersObj != null && managersObj.GetComponent<GameDataManager>() == null)
            {
                managersObj.AddComponent<GameDataManager>();
                if (showDebugMessages) Debug.Log("Created GameDataManager");
            }
            if (managersObj != null && managersObj.GetComponent<BalanceManager>() == null)
            {
                managersObj.AddComponent<BalanceManager>();
                if (showDebugMessages) Debug.Log("Created BalanceManager");
            }
            // Verify managersObj is still valid before checking RunManager
            if (managersObj == null)
            {
                Debug.LogError("❌ Managers GameObject was destroyed before adding RunManager! Recreating...");
                managersObj = new GameObject("Managers");
                // Re-add the other managers
                if (managersObj.GetComponent<GameDataManager>() == null)
                    managersObj.AddComponent<GameDataManager>();
                if (managersObj.GetComponent<BalanceManager>() == null)
                    managersObj.AddComponent<BalanceManager>();
            }
            
            // Check if RunManager.Instance already exists (from previous play or elsewhere)
            if (RunManager.Instance != null)
            {
                if (showDebugMessages) Debug.Log($"RunManager.Instance already exists, using existing one");
                // Don't add a new one - it would destroy itself
            }
            else
            {
                // Check if RunManager component exists on this GameObject
                RunManager existingRunManager = null;
                try
                {
                    if (managersObj != null)
                    {
                        existingRunManager = managersObj.GetComponent<RunManager>();
                    }
                }
                catch
                {
                    Debug.LogError("❌ Error accessing Managers GameObject - it may have been destroyed. Recreating...");
                    managersObj = new GameObject("Managers");
                    // Re-add the other managers
                    if (managersObj.GetComponent<GameDataManager>() == null)
                        managersObj.AddComponent<GameDataManager>();
                    if (managersObj.GetComponent<BalanceManager>() == null)
                        managersObj.AddComponent<BalanceManager>();
                    existingRunManager = null; // Force creation
                }
                
                if (existingRunManager == null)
                {
                    // Add RunManager only if Instance doesn't exist
                    if (managersObj != null)
                    {
                        RunManager rm = managersObj.AddComponent<RunManager>();
                        if (showDebugMessages) Debug.Log($"Created RunManager component on {managersObj.name}");
                        
                        // Give it a moment for Awake to run
                        yield return null;
                        
                        // Verify managersObj still exists (it might have been destroyed)
                        if (managersObj == null)
                        {
                            Debug.LogWarning("⚠️ Managers GameObject was destroyed after adding RunManager!");
                            Debug.LogWarning("   RunManager.Instance was probably already set elsewhere. Finding existing Instance...");
                            
                            // Find the existing RunManager.Instance's GameObject
                            if (RunManager.Instance != null)
                            {
                                managersObj = RunManager.Instance.gameObject;
                                Debug.Log($"✅ Found existing RunManager.Instance on {managersObj.name}");
                            }
                            else
                            {
                                Debug.LogError("❌ RunManager.Instance is null and Managers was destroyed! Creating new one...");
                                // Create a new Managers GameObject as fallback
                                managersObj = new GameObject("Managers");
                                if (managersObj.GetComponent<GameDataManager>() == null)
                                    managersObj.AddComponent<GameDataManager>();
                                if (managersObj.GetComponent<BalanceManager>() == null)
                                    managersObj.AddComponent<BalanceManager>();
                            }
                        }
                
                // Check Instance
                if (RunManager.Instance == null)
                {
                    Debug.LogError("❌ RunManager.Instance is still null after creation!");
                }
                else
                {
                    if (showDebugMessages) Debug.Log($"✅ RunManager.Instance is set on {RunManager.Instance.gameObject.name}");
                }
                    }
                }
                else
                {
                    if (showDebugMessages) Debug.Log($"RunManager already exists on {managersObj.name}");
                    
                    // Verify Instance is set
                    yield return null; // Wait a frame
                    if (RunManager.Instance == null)
                    {
                        Debug.LogWarning("⚠️ RunManager exists but Instance is null");
                    }
                    else
                    {
                        if (showDebugMessages) Debug.Log("✅ RunManager.Instance is already set");
                    }
                }
            }
            // Final verification: ensure Managers GameObject exists
            if (managersObj == null)
            {
                Debug.LogError("❌ Managers GameObject is null! Creating new one...");
                managersObj = new GameObject("Managers");
                if (managersObj.GetComponent<GameDataManager>() == null)
                    managersObj.AddComponent<GameDataManager>();
                if (managersObj.GetComponent<BalanceManager>() == null)
                    managersObj.AddComponent<BalanceManager>();
            }
            
            // Final wait to ensure everything is initialized
            yield return null;
            
            // Re-check managersObj after yield - it might have been destroyed
            if (managersObj == null)
            {
                Debug.LogWarning("⚠️ Managers GameObject was destroyed after yield! Recreating...");
                managersObj = new GameObject("Managers");
                if (managersObj.GetComponent<GameDataManager>() == null)
                    managersObj.AddComponent<GameDataManager>();
                if (managersObj.GetComponent<BalanceManager>() == null)
                    managersObj.AddComponent<BalanceManager>();
                
                // If RunManager.Instance exists, use that GameObject instead
                if (RunManager.Instance != null)
                {
                    GameObject existingManagers = RunManager.Instance.gameObject;
                    if (existingManagers != null && existingManagers.name == "Managers")
                    {
                        managersObj = existingManagers;
                        Debug.Log($"✅ Using existing Managers GameObject from RunManager.Instance");
                    }
                }
            }
            
            if (showDebugMessages) Debug.Log($"✅ CreateManagersCoroutine complete. Managers GameObject: {(managersObj != null ? managersObj.name : "NULL")}");
            
            if (managersObj != null && managersObj.GetComponent<EncounterManager>() == null)
            {
                managersObj.AddComponent<EncounterManager>();
                if (showDebugMessages) Debug.Log("Created EncounterManager");
            }
            if (managersObj != null && managersObj.GetComponent<ProgressionManager>() == null)
            {
                managersObj.AddComponent<ProgressionManager>();
                if (showDebugMessages) Debug.Log("Created ProgressionManager");
            }
            if (managersObj != null && managersObj.GetComponent<PartyManager>() == null)
            {
                managersObj.AddComponent<PartyManager>();
                if (showDebugMessages) Debug.Log("Created PartyManager");
            }
            if (managersObj != null && managersObj.GetComponent<RelicManager>() == null)
            {
                managersObj.AddComponent<RelicManager>();
                if (showDebugMessages) Debug.Log("Created RelicManager");
            }
            if (managersObj != null && managersObj.GetComponent<BattleManager>() == null)
            {
                managersObj.AddComponent<BattleManager>();
                if (showDebugMessages) Debug.Log("Created BattleManager");
            }
            if (managersObj != null && managersObj.GetComponent<UIManager>() == null)
            {
                managersObj.AddComponent<UIManager>();
                if (showDebugMessages) Debug.Log("Created UIManager");
            }

            if (showDebugMessages) Debug.Log("✅ All managers created");
            
            // Final verification
            if (RunManager.Instance == null)
            {
                Debug.LogError("❌ RunManager.Instance is STILL null after all creation! Something is wrong.");
            }
            else
            {
                if (showDebugMessages) Debug.Log("✅ RunManager.Instance verified and ready");
            }
        }

        private void CreateCanvas()
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
                if (showDebugMessages) Debug.Log("✅ Canvas created");
            }

            // CRITICAL: EventSystem is needed for UI clicks!
            UnityEngine.EventSystems.EventSystem eventSystem = FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
            if (eventSystem == null)
            {
                GameObject eventSystemObj = new GameObject("EventSystem");
                eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
                
                // Use StandaloneInputModule (old Input System)
                // NOTE: Make sure Project Settings > Player > Active Input Handling is set to "Input Manager (Old)"
                eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                
                if (showDebugMessages) Debug.Log("✅ EventSystem created (needed for button clicks!)");
                if (showDebugMessages) Debug.Log("⚠️ Make sure Project Settings > Player > Active Input Handling is set to 'Input Manager (Old)'");
            }
        }

        private void CreateUI()
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null) return;

            // Create Main Menu if it doesn't exist
            MainMenuUI mainMenu = FindFirstObjectByType<MainMenuUI>();
            if (mainMenu == null)
            {
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
                Text titleText = titleObj.AddComponent<Text>();
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
                GameObject startBtn = CreateButton("Start Run", menuObj.transform, new Vector2(0.5f, 0.5f));
                Button startButton = startBtn.GetComponent<Button>();

                // Add MainMenuUI component first
                mainMenu = menuObj.AddComponent<MainMenuUI>();
                
                // CRITICAL: Assign rootPanel BEFORE anything else
                mainMenu.rootPanel = menuObj;
                mainMenu.titleText = titleText;
                mainMenu.startRunButton = startButton;
                
                // Make sure the panel is visible
                menuObj.SetActive(true);
                
                // Verify rootPanel is set
                if (mainMenu.rootPanel == null)
                {
                    Debug.LogError("❌ rootPanel is still null after assignment!");
                }
                else
                {
                    if (showDebugMessages) Debug.Log($"✅ rootPanel assigned: {mainMenu.rootPanel.name}");
                }
                
                // Now setup buttons (this will add the listener properly)
                // We need to call SetupButtons manually since Start() might have already run
                if (mainMenu != null && startButton != null)
                {
                    startButton.onClick.RemoveAllListeners(); // Clear any existing listeners
                    startButton.onClick.AddListener(() => {
                        Debug.Log("Button clicked directly!");
                        mainMenu.OnStartRun();
                    });
                    if (showDebugMessages) Debug.Log("✅ Start Run button connected");
                }
                else
                {
                    if (showDebugMessages) Debug.LogWarning("⚠️ Could not connect Start Run button!");
                }

                if (showDebugMessages) Debug.Log("✅ Main Menu created");
            }

            // Create Battle UI if it doesn't exist
            BattleArenaUI battleUI = FindFirstObjectByType<BattleArenaUI>();
            if (battleUI == null)
            {
                // Create battle panel
                GameObject battlePanel = new GameObject("BattleArenaPanel");
                battlePanel.transform.SetParent(canvas.transform, false);
                battlePanel.SetActive(false); // Hidden by default
                RectTransform battleRect = battlePanel.AddComponent<RectTransform>();
                battleRect.anchorMin = Vector2.zero;
                battleRect.anchorMax = Vector2.one;
                battleRect.sizeDelta = Vector2.zero;

                Image bg = battlePanel.AddComponent<Image>();
                bg.color = new Color(0.1f, 0.1f, 0.15f, 1f);

                // Party container
                GameObject partyContainer = new GameObject("PartyContainer");
                partyContainer.transform.SetParent(battlePanel.transform, false);
                RectTransform partyRect = partyContainer.AddComponent<RectTransform>();
                partyRect.anchorMin = new Vector2(0, 0);
                partyRect.anchorMax = new Vector2(0.5f, 1);
                partyRect.sizeDelta = Vector2.zero;

                // Enemy container
                GameObject enemyContainer = new GameObject("EnemyContainer");
                enemyContainer.transform.SetParent(battlePanel.transform, false);
                RectTransform enemyRect = enemyContainer.AddComponent<RectTransform>();
                enemyRect.anchorMin = new Vector2(0.5f, 0);
                enemyRect.anchorMax = new Vector2(1, 1);
                enemyRect.sizeDelta = Vector2.zero;

                // Action buttons
                GameObject actionPanel = new GameObject("ActionPanel");
                actionPanel.transform.SetParent(battlePanel.transform, false);
                RectTransform actionRect = actionPanel.AddComponent<RectTransform>();
                actionRect.anchorMin = new Vector2(0, 0);
                actionRect.anchorMax = new Vector2(1, 0.2f);
                actionRect.sizeDelta = Vector2.zero;

                HorizontalLayoutGroup layout = actionPanel.AddComponent<HorizontalLayoutGroup>();
                layout.spacing = 10;
                layout.padding = new RectOffset(10, 10, 10, 10);
                layout.childAlignment = TextAnchor.MiddleCenter;

                GameObject attackBtn = CreateButton("Attack", actionPanel.transform, Vector2.zero);
                GameObject endTurnBtn = CreateButton("End Turn", actionPanel.transform, Vector2.zero);

                // Turn indicator
                GameObject turnIndicator = new GameObject("TurnIndicator");
                turnIndicator.transform.SetParent(battlePanel.transform, false);
                Text turnText = turnIndicator.AddComponent<Text>();
                turnText.text = "Your Turn";
                turnText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                turnText.fontSize = 24;
                turnText.color = Color.white;
                turnText.alignment = TextAnchor.UpperCenter;
                RectTransform turnRect = turnIndicator.GetComponent<RectTransform>();
                turnRect.anchorMin = new Vector2(0.5f, 0.8f);
                turnRect.anchorMax = new Vector2(0.5f, 0.8f);
                turnRect.sizeDelta = new Vector2(200, 50);
                turnRect.anchoredPosition = Vector2.zero;

                // Combat log
                GameObject logPanel = new GameObject("CombatLogPanel");
                logPanel.transform.SetParent(battlePanel.transform, false);
                RectTransform logRect = logPanel.AddComponent<RectTransform>();
                logRect.anchorMin = new Vector2(0, 0.2f);
                logRect.anchorMax = new Vector2(1, 0.4f);
                logRect.sizeDelta = Vector2.zero;

                Image logBg = logPanel.AddComponent<Image>();
                logBg.color = new Color(0, 0, 0, 0.7f);

                GameObject logScroll = new GameObject("ScrollView");
                logScroll.transform.SetParent(logPanel.transform, false);
                RectTransform scrollRect = logScroll.AddComponent<RectTransform>();
                scrollRect.anchorMin = Vector2.zero;
                scrollRect.anchorMax = Vector2.one;
                scrollRect.sizeDelta = Vector2.zero;

                ScrollRect scrollView = logScroll.AddComponent<ScrollRect>();
                scrollView.horizontal = false;
                scrollView.vertical = true;

                GameObject logContent = new GameObject("Content");
                logContent.transform.SetParent(logScroll.transform, false);
                RectTransform contentRect = logContent.AddComponent<RectTransform>();
                contentRect.anchorMin = new Vector2(0, 1);
                contentRect.anchorMax = new Vector2(1, 1);
                contentRect.pivot = new Vector2(0.5f, 1);
                contentRect.sizeDelta = new Vector2(0, 0);

                Text logText = logContent.AddComponent<Text>();
                logText.text = "Combat Log:\n";
                logText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                logText.fontSize = 14;
                logText.color = Color.white;
                logText.alignment = TextAnchor.UpperLeft;

                scrollView.content = contentRect;
                scrollView.viewport = scrollRect;

                // Synergy badges
                GameObject synergyText = new GameObject("SynergyBadges");
                synergyText.transform.SetParent(battlePanel.transform, false);
                Text synergyTextComp = synergyText.AddComponent<Text>();
                synergyTextComp.text = "No Active Synergies";
                synergyTextComp.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                synergyTextComp.fontSize = 16;
                synergyTextComp.color = Color.yellow;
                synergyTextComp.alignment = TextAnchor.UpperLeft;
                RectTransform synergyRect = synergyText.GetComponent<RectTransform>();
                synergyRect.anchorMin = new Vector2(0, 0.95f);
                synergyRect.anchorMax = new Vector2(0.5f, 1);
                synergyRect.sizeDelta = Vector2.zero;

                // Add BattleArenaUI component
                battleUI = battlePanel.AddComponent<BattleArenaUI>();
                battleUI.rootPanel = battlePanel;
                battleUI.partyContainer = partyContainer.transform;
                battleUI.enemyContainer = enemyContainer.transform;
                battleUI.attackButton = attackBtn.GetComponent<Button>();
                battleUI.endTurnButton = endTurnBtn.GetComponent<Button>();
                battleUI.turnIndicatorText = turnText;
                battleUI.combatLogText = logText;
                battleUI.combatLogScrollRect = scrollView;
                battleUI.synergyBadgesText = synergyTextComp;

                if (showDebugMessages) Debug.Log("✅ Battle UI created");
            }
        }

        private void ConnectManagers()
        {
            StartCoroutine(ConnectManagersCoroutine());
        }

        private System.Collections.IEnumerator ConnectManagersCoroutine()
        {
            // Wait a bit to ensure CreateManagersCoroutine has finished
            yield return null;
            yield return null;
            
            // Connect UI to UIManager
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.mainMenuUI = FindFirstObjectByType<MainMenuUI>();
                uiManager.battleArenaUI = FindFirstObjectByType<BattleArenaUI>();
                if (showDebugMessages) Debug.Log("✅ UIManager connected");
            }
            else
            {
                Debug.LogWarning("⚠️ UIManager not found!");
            }

            // Wait a frame to ensure all Awake() methods have completed
            yield return null;
            
            // First try to use Instance (most reliable - it was verified earlier)
            RunManager runManager = RunManager.Instance;
            
            if (runManager == null)
            {
                // Try FindFirstObjectByType as fallback
                runManager = FindFirstObjectByType<RunManager>();
                if (runManager != null)
                {
                    if (showDebugMessages) Debug.Log("Found RunManager via FindFirstObjectByType (Instance was null)");
                }
            }
            
            if (runManager == null)
            {
                // Last resort: Try to find the Managers GameObject
                GameObject managersObj = GameObject.Find("Managers");
                if (managersObj == null)
                {
                    // Try waiting a bit more - maybe it's still being created
                    yield return null;
                    yield return null;
                    managersObj = GameObject.Find("Managers");
                }
                
                if (managersObj != null)
                {
                    runManager = managersObj.GetComponent<RunManager>();
                    if (runManager == null)
                    {
                        Debug.LogError("❌ RunManager component not found on Managers GameObject!");
                        Debug.LogError($"   Managers GameObject exists: {managersObj != null}");
                        Debug.LogError($"   Managers GameObject name: {managersObj.name}");
                        Component[] allComponents = managersObj.GetComponents<Component>();
                        Debug.LogError($"   Components on Managers: {allComponents.Length}");
                        foreach (var comp in allComponents)
                        {
                            Debug.LogError($"     - {comp.GetType().Name}");
                        }
                    }
                    else
                    {
                        Debug.Log($"✅ Found RunManager on {managersObj.name}");
                    }
                }
                else
                {
                    Debug.LogError("❌ Managers GameObject not found in scene!");
                    Debug.LogError("   CreateManagersCoroutine may not have finished yet, or Managers was destroyed.");
                }
            }
            else
            {
                if (showDebugMessages) Debug.Log($"✅ Found RunManager (using Instance)");
            }
            
            if (runManager != null)
            {
                runManager.encounterManager = FindFirstObjectByType<EncounterManager>();
                runManager.progressionManager = FindFirstObjectByType<ProgressionManager>();
                runManager.partyManager = FindFirstObjectByType<PartyManager>();
                
                // Verify Instance is set
                if (RunManager.Instance == null)
                {
                    Debug.LogError("❌ RunManager.Instance is null even though RunManager exists! This is a bug.");
                }
                else
                {
                    if (showDebugMessages) Debug.Log("✅ RunManager.Instance is set and managers connected");
                }
            }
            else
            {
                Debug.LogError("❌ RunManager not found in scene after all attempts!");
            }

            if (showDebugMessages) Debug.Log("✅ All managers connected");

            // Add Input System checker
            GameObject checkerObj = GameObject.Find("InputSystemChecker");
            if (checkerObj == null)
            {
                checkerObj = new GameObject("InputSystemChecker");
                checkerObj.AddComponent<InputSystemChecker>();
            }
        }

        private GameObject CreateButton(string text, Transform parent, Vector2 anchor)
        {
            GameObject btn = new GameObject(text + "Button");
            btn.transform.SetParent(parent, false);

            Image img = btn.AddComponent<Image>();
            img.color = new Color(0.2f, 0.4f, 0.8f, 1f);

            Button button = btn.AddComponent<Button>();
            button.targetGraphic = img;

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btn.transform, false);
            Text txt = textObj.AddComponent<Text>();
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
            if (anchor != Vector2.zero)
            {
                btnRect.anchorMin = anchor;
                btnRect.anchorMax = anchor;
                btnRect.sizeDelta = new Vector2(200, 50);
                btnRect.anchoredPosition = Vector2.zero;
            }
            else
            {
                btnRect.sizeDelta = new Vector2(150, 50);
            }

            return btn;
        }
    }
}

