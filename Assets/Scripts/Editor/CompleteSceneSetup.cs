using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using FourfoldFate.UI;
using FourfoldFate.Data;
using FourfoldFate.Balance;
using FourfoldFate.Roguelike;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Relics;
using FourfoldFate.MetaProgression;
using FourfoldFate.Agents;
using FourfoldFate.Animation;
using FourfoldFate.Setup;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Complete automated scene setup - does everything in one click!
    /// Creates all managers, UI, prefabs, and sets up the entire scene.
    /// </summary>
    public class CompleteSceneSetup
    {
        [MenuItem("Fourfold Fate/Complete Scene Setup (One-Click)")]
        public static void SetupCompleteScene()
        {
            Debug.Log("=== Starting Complete Scene Setup ===");
            
            // Step 1: Create or get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Untitled" || !currentScene.isLoaded)
            {
                // Create new scene
                currentScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                EditorSceneManager.SaveScene(currentScene, "Assets/Scenes/GameScene.unity");
            }
            
            // Step 2: Clear existing objects (optional - comment out if you want to keep existing)
            ClearExistingSetup();
            
            // Step 3: Setup Canvas
            Canvas canvas = SetupCanvas();
            
            // Step 4: Create all managers
            CreateAllManagers();
            
            // Step 5: Create UI prefabs and assign them
            CreateAndAssignUI(canvas);
            
            // Step 6: Create unit prefabs
            CreateUnitPrefabs();
            
            // Step 7: Assign prefabs to managers
            AssignPrefabsToManagers();
            
            // Step 8: Save everything
            EditorSceneManager.MarkSceneDirty(currentScene);
            EditorSceneManager.SaveScene(currentScene);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            Debug.Log("=== Complete Scene Setup Finished! ===");
            EditorUtility.DisplayDialog("Success!", 
                "Complete scene setup finished!\n\n" +
                "✓ Canvas created and configured\n" +
                "✓ All managers created\n" +
                "✓ UI prefabs created and assigned\n" +
                "✓ Unit prefabs created\n" +
                "✓ Everything connected and ready\n\n" +
                "Press Play to test!", 
                "OK");
        }
        
        private static void ClearExistingSetup()
        {
            // Clear existing managers (optional)
            GameObject[] existingManagers = GameObject.FindGameObjectsWithTag("Untagged");
            foreach (GameObject obj in existingManagers)
            {
                if (obj.name.Contains("Manager") || obj.name == "Canvas" || obj.name == "MainMenuPanel")
                {
                    Object.DestroyImmediate(obj);
                }
            }
        }
        
        private static Canvas SetupCanvas()
        {
            // Find or create Canvas
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.pixelPerfect = false;
                
                // Add CanvasScaler with proper settings for high quality
                CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 0.5f; // Balance between width and height
                
                // Set canvas for better quality
                canvas.pixelPerfect = false; // Disable for smoother rendering
                canvas.sortingOrder = 0;
                
                // Add GraphicRaycaster
                canvasObj.AddComponent<GraphicRaycaster>();
                
                Debug.Log("✓ Canvas created and configured");
            }
            else
            {
                // Fix existing Canvas with high quality settings
                canvas.pixelPerfect = false; // Better quality
                canvas.sortingOrder = 0;
                
                CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
                if (scaler != null)
                {
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1920, 1080);
                    scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = 0.5f;
                    EditorUtility.SetDirty(scaler);
                }
                else
                {
                    // Add CanvasScaler if missing
                    scaler = canvas.gameObject.AddComponent<CanvasScaler>();
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1920, 1080);
                    scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = 0.5f;
                }
                
                EditorUtility.SetDirty(canvas);
                Debug.Log("✓ Canvas found and configured for high quality");
            }
            
            return canvas;
        }
        
        private static void CreateAllManagers()
        {
            // Create Managers parent
            GameObject managersParent = GameObject.Find("Managers");
            if (managersParent == null)
            {
                managersParent = new GameObject("Managers");
            }
            
            // GameDataManager (root - for DontDestroyOnLoad)
            if (Object.FindObjectOfType<GameDataManager>() == null)
            {
                GameObject obj = new GameObject("GameDataManager");
                obj.AddComponent<GameDataManager>();
                Debug.Log("✓ GameDataManager created");
            }
            
            // BalanceManager (root - for DontDestroyOnLoad)
            if (Object.FindObjectOfType<BalanceManager>() == null)
            {
                GameObject obj = new GameObject("BalanceManager");
                obj.AddComponent<BalanceManager>();
                Debug.Log("✓ BalanceManager created");
            }
            
            // GameManager
            if (Object.FindObjectOfType<GameManager>() == null)
            {
                GameObject obj = new GameObject("GameManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<GameManager>();
                Debug.Log("✓ GameManager created");
            }
            
            // RunManager
            if (Object.FindObjectOfType<RunManager>() == null)
            {
                GameObject obj = new GameObject("RunManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<RunManager>();
                Debug.Log("✓ RunManager created");
            }
            
            // BattleManager
            if (Object.FindObjectOfType<BattleManager>() == null)
            {
                GameObject obj = new GameObject("BattleManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<BattleManager>();
                Debug.Log("✓ BattleManager created");
            }
            
            // PartyManager
            if (Object.FindObjectOfType<PartyManager>() == null)
            {
                GameObject obj = new GameObject("PartyManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<PartyManager>();
                Debug.Log("✓ PartyManager created");
            }
            
            // RelicManager
            if (Object.FindObjectOfType<RelicManager>() == null)
            {
                GameObject obj = new GameObject("RelicManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<RelicManager>();
                Debug.Log("✓ RelicManager created");
            }
            
            // MetaProgressionManager
            if (Object.FindObjectOfType<MetaProgressionManager>() == null)
            {
                GameObject obj = new GameObject("MetaProgressionManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<MetaProgressionManager>();
                Debug.Log("✓ MetaProgressionManager created");
            }
            
            // AgentManager
            if (Object.FindObjectOfType<AgentManager>() == null)
            {
                GameObject obj = new GameObject("AgentManager");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<AgentManager>();
                Debug.Log("✓ AgentManager created");
            }
            
            // AttackAnimationSystem
            if (Object.FindObjectOfType<AttackAnimationSystem>() == null)
            {
                GameObject obj = new GameObject("AttackAnimationSystem");
                obj.transform.SetParent(managersParent.transform);
                obj.AddComponent<AttackAnimationSystem>();
                Debug.Log("✓ AttackAnimationSystem created");
            }
            
            // UIManager (under Canvas)
            if (Object.FindObjectOfType<UIManager>() == null)
            {
                Canvas canvas = Object.FindObjectOfType<Canvas>();
                if (canvas != null)
                {
                    GameObject obj = new GameObject("UIManager");
                    obj.transform.SetParent(canvas.transform);
                    obj.AddComponent<UIManager>();
                    Debug.Log("✓ UIManager created");
                }
            }
        }
        
        private static void CreateAndAssignUI(Canvas canvas)
        {
            // Create prefab folders
            string prefabPath = "Assets/Prefabs/UI";
            if (!AssetDatabase.IsValidFolder(prefabPath))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                {
                    AssetDatabase.CreateFolder("Assets", "Prefabs");
                }
                AssetDatabase.CreateFolder("Assets/Prefabs", "UI");
            }
            
            // Create MainMenuPanel prefab
            string mainMenuPath = prefabPath + "/MainMenuPanel.prefab";
            GameObject mainMenuPrefab = CreateMainMenuPrefab(canvas, mainMenuPath);
            
            // CRITICAL: Instantiate MainMenuPanel in scene (the prefab creation doesn't keep it in scene)
            UIManager uiManager = Object.FindObjectOfType<UIManager>();
            if (uiManager != null && mainMenuPrefab != null)
            {
                // Check if MainMenuPanel already exists in scene
                MainMenuUI existingMainMenu = canvas.GetComponentInChildren<MainMenuUI>(true);
                
                GameObject mainMenuInstance;
                if (existingMainMenu != null)
                {
                    mainMenuInstance = existingMainMenu.gameObject;
                    Debug.Log("✓ MainMenuPanel already exists in scene");
                }
                else
                {
                    // CRITICAL: Instantiate the prefab in the scene
                    mainMenuInstance = PrefabUtility.InstantiatePrefab(mainMenuPrefab, canvas.transform) as GameObject;
                    if (mainMenuInstance == null)
                    {
                        Debug.LogError("Failed to instantiate MainMenuPanel! Trying alternative method...");
                        // Alternative: Create directly from prefab
                        mainMenuInstance = Object.Instantiate(mainMenuPrefab, canvas.transform);
                    }
                    if (mainMenuInstance != null)
                    {
                        mainMenuInstance.name = "MainMenuPanel";
                        mainMenuInstance.SetActive(true);
                        Debug.Log("✓ MainMenuPanel instantiated in scene");
                    }
                    else
                    {
                        Debug.LogError("CRITICAL: Could not instantiate MainMenuPanel!");
                        return;
                    }
                }
                
                MainMenuUI mainMenuUI = mainMenuInstance.GetComponent<MainMenuUI>();
                if (mainMenuUI != null)
                {
                    // Assign to UIManager
                    SerializedObject serializedUI = new SerializedObject(uiManager);
                    serializedUI.FindProperty("mainMenuUI").objectReferenceValue = mainMenuUI;
                    serializedUI.ApplyModifiedProperties();
                    
                    // Ensure it's active and visible
                    mainMenuInstance.SetActive(true);
                    
                    // Make sure BaseUI rootPanel is set
                    SerializedObject serializedMainMenu = new SerializedObject(mainMenuUI);
                    var rootPanelProp = serializedMainMenu.FindProperty("rootPanel");
                    if (rootPanelProp.objectReferenceValue == null)
                    {
                        rootPanelProp.objectReferenceValue = mainMenuInstance;
                        serializedMainMenu.ApplyModifiedProperties();
                    }
                    
                    // Force show it immediately
                    mainMenuUI.Show();
                    
                    EditorUtility.SetDirty(mainMenuInstance);
                    EditorUtility.SetDirty(uiManager);
                    
                    Debug.Log("✓ MainMenuPanel assigned to UIManager and made visible");
                }
            }
        }
        
        private static GameObject CreateMainMenuPrefab(Canvas canvas, string prefabPath)
        {
            // Delete old prefab if exists
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                AssetDatabase.DeleteAsset(prefabPath);
            }
            
            // Create MainMenuPanel
            GameObject panel = new GameObject("MainMenuPanel");
            panel.transform.SetParent(canvas.transform, false);
            
            // Setup RectTransform
            RectTransform rectTransform = panel.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
            
            // Add Image component with background color
            Image panelImage = panel.AddComponent<Image>();
            Texture2D whiteTexture = Texture2D.whiteTexture;
            if (whiteTexture != null)
            {
                Sprite whiteSprite = Sprite.Create(whiteTexture, new Rect(0, 0, whiteTexture.width, whiteTexture.height), new Vector2(0.5f, 0.5f));
                panelImage.sprite = whiteSprite;
            }
            panelImage.color = new Color(0.1f, 0.1f, 0.15f, 1f); // Dark blue-gray
            panelImage.type = Image.Type.Simple;
            
            // Create TitleText
            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            RectTransform titleRect = titleObj.AddComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0.5f, 1f);
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0, -50);
            titleRect.sizeDelta = new Vector2(1200, 120);
            
            Text titleText = titleObj.AddComponent<Text>();
            titleText.text = "Fourfold Fate";
            titleText.fontSize = 48;
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.color = Color.white;
            titleText.horizontalOverflow = HorizontalWrapMode.Overflow;
            titleText.verticalOverflow = VerticalWrapMode.Overflow;
            try
            {
                titleText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }
            catch { }
            
            // Create StartRunButton
            GameObject buttonObj = new GameObject("StartRunButton");
            buttonObj.transform.SetParent(panel.transform, false);
            RectTransform buttonRect = buttonObj.AddComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
            buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
            buttonRect.pivot = new Vector2(0.5f, 0.5f);
            buttonRect.anchoredPosition = Vector2.zero;
            buttonRect.sizeDelta = new Vector2(300, 60);
            
            Button button = buttonObj.AddComponent<Button>();
            Image buttonImage = buttonObj.AddComponent<Image>();
            if (whiteTexture != null)
            {
                Sprite buttonSprite = Sprite.Create(whiteTexture, new Rect(0, 0, whiteTexture.width, whiteTexture.height), new Vector2(0.5f, 0.5f));
                buttonImage.sprite = buttonSprite;
            }
            buttonImage.color = new Color(0.2f, 0.4f, 0.8f, 1f); // Blue
            
            // Button text
            GameObject buttonTextObj = new GameObject("Text");
            buttonTextObj.transform.SetParent(buttonObj.transform, false);
            RectTransform buttonTextRect = buttonTextObj.AddComponent<RectTransform>();
            buttonTextRect.anchorMin = Vector2.zero;
            buttonTextRect.anchorMax = Vector2.one;
            buttonTextRect.sizeDelta = Vector2.zero;
            
            Text buttonText = buttonTextObj.AddComponent<Text>();
            buttonText.text = "Start New Run";
            buttonText.fontSize = 20;
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.color = Color.white;
            buttonText.horizontalOverflow = HorizontalWrapMode.Overflow;
            buttonText.verticalOverflow = VerticalWrapMode.Overflow;
            try
            {
                buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }
            catch { }
            
            // Add MainMenuUI component
            MainMenuUI mainMenuUI = panel.AddComponent<MainMenuUI>();
            SerializedObject serializedUI = new SerializedObject(mainMenuUI);
            serializedUI.FindProperty("titleText").objectReferenceValue = titleText;
            serializedUI.FindProperty("startRunButton").objectReferenceValue = button;
            serializedUI.ApplyModifiedProperties();
            
            // Create prefab (this will remove panel from scene temporarily)
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabPath);
            
            // IMPORTANT: Don't destroy panel - we need to keep it in scene
            // The prefab is saved, but we keep the instance
            // Connect it to the prefab so changes sync
            PrefabUtility.SaveAsPrefabAssetAndConnect(panel, prefabPath, InteractionMode.AutomatedAction);
            
            // Ensure it's active and visible
            panel.SetActive(true);
            
            Debug.Log("✓ MainMenuPanel prefab created and kept in scene");
            return prefab;
        }
        
        private static void CreateUnitPrefabs()
        {
            // Create prefab folders
            string prefabPath = "Assets/Prefabs/Units";
            if (!AssetDatabase.IsValidFolder(prefabPath))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                {
                    AssetDatabase.CreateFolder("Assets", "Prefabs");
                }
                AssetDatabase.CreateFolder("Assets/Prefabs", "Units");
            }
            
            // Create UnitPrefab
            string unitPrefabPath = prefabPath + "/UnitPrefab.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(unitPrefabPath) == null)
            {
                GameObject unitObj = new GameObject("UnitPrefab");
                unitObj.AddComponent<Unit>();
                // Add a simple visual (empty SpriteRenderer for now)
                SpriteRenderer sr = unitObj.AddComponent<SpriteRenderer>();
                sr.color = Color.white;
                
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(unitObj, unitPrefabPath);
                Object.DestroyImmediate(unitObj);
                Debug.Log("✓ UnitPrefab created");
            }
            
            // Create EnemyPrefab
            string enemyPrefabPath = prefabPath + "/EnemyPrefab.prefab";
            if (AssetDatabase.LoadAssetAtPath<GameObject>(enemyPrefabPath) == null)
            {
                GameObject enemyObj = new GameObject("EnemyPrefab");
                enemyObj.AddComponent<Unit>();
                SpriteRenderer sr = enemyObj.AddComponent<SpriteRenderer>();
                sr.color = Color.red; // Different color for enemies
                
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(enemyObj, enemyPrefabPath);
                Object.DestroyImmediate(enemyObj);
                Debug.Log("✓ EnemyPrefab created");
            }
        }
        
        private static void AssignPrefabsToManagers()
        {
            // Load prefabs
            GameObject unitPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Units/UnitPrefab.prefab");
            GameObject enemyPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Units/EnemyPrefab.prefab");
            
            // Assign to GameManager
            GameManager gameManager = Object.FindObjectOfType<GameManager>();
            if (gameManager != null && unitPrefab != null)
            {
                SerializedObject serialized = new SerializedObject(gameManager);
                serialized.FindProperty("unitPrefab").objectReferenceValue = unitPrefab;
                serialized.ApplyModifiedProperties();
                Debug.Log("✓ UnitPrefab assigned to GameManager");
            }
            
            // Assign to EncounterManager (inside RunManager)
            RunManager runManager = Object.FindObjectOfType<RunManager>();
            if (runManager != null)
            {
                EncounterManager encounterManager = runManager.GetComponentInChildren<EncounterManager>();
                if (encounterManager == null)
                {
                    GameObject encounterObj = new GameObject("EncounterManager");
                    encounterObj.transform.SetParent(runManager.transform);
                    encounterManager = encounterObj.AddComponent<EncounterManager>();
                }
                
                if (enemyPrefab != null)
                {
                    SerializedObject serialized = new SerializedObject(encounterManager);
                    serialized.FindProperty("enemyPrefab").objectReferenceValue = enemyPrefab;
                    serialized.ApplyModifiedProperties();
                    Debug.Log("✓ EnemyPrefab assigned to EncounterManager");
                }
            }
        }
    }
}

