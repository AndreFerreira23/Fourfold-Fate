using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using FourfoldFate.UI;
#if UNITY_TEXTMESHPRO
using TMPro;
#endif

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Editor script to automatically create UI prefabs.
    /// Right-click in Project window → Fourfold Fate → Create Main Menu UI
    /// </summary>
    public class UIAutoSetup : EditorWindow
    {
        [MenuItem("Fourfold Fate/Create Main Menu UI")]
        public static void CreateMainMenuUI()
        {
            // Find or create Canvas
            Canvas canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                
                // Add and configure CanvasScaler for proper scaling and quality
                UnityEngine.UI.CanvasScaler scaler = canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080); // Standard HD resolution
                scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 0.5f; // Balance between width and height
                
                // Set canvas to use best quality
                canvas.pixelPerfect = false; // Disable pixel perfect for smoother rendering
                
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
            else
            {
                // Fix existing Canvas Scaler if it exists
                UnityEngine.UI.CanvasScaler existingScaler = canvas.GetComponent<UnityEngine.UI.CanvasScaler>();
                if (existingScaler != null)
                {
                    existingScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    existingScaler.referenceResolution = new Vector2(1920, 1080);
                    existingScaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    existingScaler.matchWidthOrHeight = 0.5f;
                    EditorUtility.SetDirty(existingScaler);
                }
                else
                {
                    // Add CanvasScaler if missing
                    UnityEngine.UI.CanvasScaler scaler = canvas.gameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
                    scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1920, 1080);
                    scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = 0.5f;
                }
                
                // Set canvas quality
                canvas.pixelPerfect = false;
            }

            // Create MainMenuPanel
            GameObject panel = new GameObject("MainMenuPanel");
            panel.transform.SetParent(canvas.transform, false);
            
            // Add RectTransform and set to fill screen
            RectTransform rectTransform = panel.AddComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.sizeDelta = Vector2.zero;
            rectTransform.anchoredPosition = Vector2.zero;
            
            // Add Image component for background
            Image panelImage = panel.AddComponent<Image>();
            
            // Use Unity's white texture and set color directly (matches what works in Inspector)
            // Unity multiplies the sprite color with the Image.color property
            Texture2D whiteTexture = Texture2D.whiteTexture;
            if (whiteTexture != null)
            {
                // Create sprite from white texture
                Sprite whiteSprite = Sprite.Create(whiteTexture, new Rect(0, 0, whiteTexture.width, whiteTexture.height), new Vector2(0.5f, 0.5f));
                panelImage.sprite = whiteSprite;
            }
            else
            {
                // Fallback: create a simple white sprite
                Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                texture.SetPixel(0, 0, Color.white);
                texture.Apply();
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 100f);
                panelImage.sprite = sprite;
            }
            
            // Set the color directly - this is what you set manually in Inspector
            Color bgColor = new Color(0.1f, 0.1f, 0.15f, 1f); // Dark blue-gray
            panelImage.color = bgColor; // Set color property directly
            panelImage.type = Image.Type.Simple;
            panelImage.preserveAspect = false;
            
            // Force update
            EditorUtility.SetDirty(panelImage);

            // Create TitleText - Use explicit size instead of anchors
            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            
            // Set RectTransform with explicit size (not anchor-based)
            RectTransform titleRect = titleObj.AddComponent<RectTransform>();
            // Use center anchor but explicit size
            titleRect.anchorMin = new Vector2(0.5f, 1f); // Center horizontally, top vertically
            titleRect.anchorMax = new Vector2(0.5f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0, -50);
            // Use screen-relative sizing instead of fixed pixels
            // This will scale properly with different resolutions
            // Use a much wider size to ensure text fits
            // With Canvas Scaler, this will scale properly
            titleRect.sizeDelta = new Vector2(1200, 120); // Wide enough for "Fourfold Fate"
            
            // Now add Text component
            Text titleText = titleObj.AddComponent<Text>();
            
            // Set text FIRST
            titleText.text = "Fourfold Fate";
            
            // Get font
            Font defaultFont = null;
            try
            {
                defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }
            catch 
            {
                string[] guids = AssetDatabase.FindAssets("t:Font");
                if (guids.Length > 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    defaultFont = AssetDatabase.LoadAssetAtPath<Font>(path);
                }
            }
            
            // Set all text properties with explicit overflow
            if (defaultFont != null)
            {
                titleText.font = defaultFont;
            }
            
            titleText.fontSize = 48; // Slightly smaller to ensure it fits
            titleText.alignment = TextAnchor.MiddleCenter;
            titleText.color = Color.white;
            titleText.horizontalOverflow = HorizontalWrapMode.Overflow; // Critical - allow overflow
            titleText.verticalOverflow = VerticalWrapMode.Overflow;
            titleText.resizeTextForBestFit = false;
            titleText.supportRichText = false;
            titleText.raycastTarget = false; // Don't block raycasts
            
            // CRITICAL: Ensure the font has all characters by using a simple approach
            // If LegacyRuntime doesn't work, Unity will fall back to default
            if (defaultFont == null)
            {
                // Create a simple default font reference
                Debug.LogWarning("Could not load font, Unity will use default");
            }
            
            // Force the text to update
            titleText.SetAllDirty();
            
            // Force update
            EditorUtility.SetDirty(titleText);
            EditorUtility.SetDirty(titleObj);

            // Create StartRunButton
            GameObject buttonObj = new GameObject("StartRunButton");
            buttonObj.transform.SetParent(panel.transform, false);
            Button button = buttonObj.AddComponent<Button>();
            Image buttonImage = buttonObj.AddComponent<Image>();
            
            // Create solid color sprite for button (better quality)
            Texture2D buttonTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            buttonTexture.SetPixel(0, 0, new Color(0.2f, 0.4f, 0.8f, 1f)); // Blue
            buttonTexture.Apply();
            Sprite buttonSprite = Sprite.Create(buttonTexture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 100f);
            buttonImage.sprite = buttonSprite;
            buttonImage.color = Color.white; // Use white so sprite color shows
            buttonImage.type = Image.Type.Simple;
            
            RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
            buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
            buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
            buttonRect.pivot = new Vector2(0.5f, 0.5f);
            buttonRect.anchoredPosition = Vector2.zero;
            buttonRect.sizeDelta = new Vector2(250, 50);

            // Create button text
            GameObject buttonTextObj = new GameObject("Text");
            buttonTextObj.transform.SetParent(buttonObj.transform, false);
            Text buttonText = buttonTextObj.AddComponent<Text>();
            buttonText.text = "Start New Run";
            buttonText.fontSize = 20; // Slightly smaller to ensure it fits
            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.color = Color.white;
            buttonText.horizontalOverflow = HorizontalWrapMode.Overflow;
            buttonText.verticalOverflow = VerticalWrapMode.Overflow;
            buttonText.resizeTextForBestFit = false;
            // Try to get default font, but don't fail if it doesn't work
            try
            {
                buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            }
            catch
            {
                // Font will use Unity's default
            }
            
            RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
            buttonTextRect.anchorMin = Vector2.zero;
            buttonTextRect.anchorMax = Vector2.one;
            buttonTextRect.sizeDelta = Vector2.zero;
            buttonTextRect.anchoredPosition = Vector2.zero;

            // Add MainMenuUI component
            MainMenuUI mainMenuUI = panel.AddComponent<MainMenuUI>();
            
            // Use SerializedObject to set SerializeField values
            SerializedObject serializedUI = new SerializedObject(mainMenuUI);
            serializedUI.FindProperty("titleText").objectReferenceValue = titleText;
            serializedUI.FindProperty("startRunButton").objectReferenceValue = button;
            serializedUI.ApplyModifiedProperties();

            // Create prefab folder if it doesn't exist
            string prefabPath = "Assets/Prefabs/UI";
            if (!AssetDatabase.IsValidFolder(prefabPath))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                {
                    AssetDatabase.CreateFolder("Assets", "Prefabs");
                }
                AssetDatabase.CreateFolder("Assets/Prefabs", "UI");
            }

            // Verify text is set correctly before creating prefab
            Text verifyText = titleObj.GetComponent<Text>();
            if (verifyText != null)
            {
                Debug.Log($"Text component text: '{verifyText.text}', Font: {verifyText.font}, Size: {verifyText.fontSize}");
                Debug.Log($"RectTransform size: {titleRect.sizeDelta}, Anchors: {titleRect.anchorMin} to {titleRect.anchorMax}");
            }
            
            // Create prefab
            string prefabFilePath = prefabPath + "/MainMenuPanel.prefab";
            
            // Delete old prefab if it exists
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabFilePath) != null)
            {
                AssetDatabase.DeleteAsset(prefabFilePath);
            }
            
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(panel, prefabFilePath);
            
            if (prefab != null)
            {
                // Verify prefab text and fix if needed
                Text prefabText = prefab.GetComponentInChildren<Text>();
                if (prefabText != null)
                {
                    Debug.Log($"Prefab text verified: '{prefabText.text}'");
                    Debug.Log($"Prefab text font: {prefabText.font}, Size: {prefabText.fontSize}");
                    Debug.Log($"Prefab text overflow: H={prefabText.horizontalOverflow}, V={prefabText.verticalOverflow}");
                    
                    // Force fix the text settings on the prefab
                    prefabText.text = "Fourfold Fate"; // Re-set text
                    prefabText.horizontalOverflow = HorizontalWrapMode.Overflow;
                    prefabText.verticalOverflow = VerticalWrapMode.Overflow;
                    
                    RectTransform prefabRect = prefabText.GetComponent<RectTransform>();
                    if (prefabRect != null)
                    {
                        prefabRect.sizeDelta = new Vector2(1200, 120); // Wide enough
                        Debug.Log($"Prefab RectTransform size set to: {prefabRect.sizeDelta}");
                    }
                    
                    // Also ensure font is set correctly
                    if (prefabText.font == null)
                    {
                        try
                        {
                            prefabText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                            Debug.Log("Font set on prefab");
                        }
                        catch
                        {
                            Debug.LogWarning("Could not set font on prefab");
                        }
                    }
                    
                    EditorUtility.SetDirty(prefab);
                    AssetDatabase.SaveAssets();
                }
                
                Debug.Log($"✅ Main Menu UI prefab created at: {prefabFilePath}");
                
                // Refresh asset database
                AssetDatabase.Refresh();
                
                // Assign to UIManager if it exists
                UIManager uiManager = FindObjectOfType<UIManager>();
                if (uiManager != null)
                {
                    SerializedObject serializedUIManager = new SerializedObject(uiManager);
                    serializedUIManager.FindProperty("mainMenuUI").objectReferenceValue = mainMenuUI;
                    serializedUIManager.ApplyModifiedProperties();
                    Debug.Log("✅ Assigned to UIManager");
                }
                
                // Delete the scene instance (we'll use the prefab)
                DestroyImmediate(panel);
                
                EditorUtility.DisplayDialog("Success", 
                    "Main Menu UI created successfully!\n\n" +
                    "Prefab saved to: Assets/Prefabs/UI/MainMenuPanel.prefab\n\n" +
                    "Check Console for debug info.\n" +
                    "If text still shows 'Fo', select TitleText in prefab and check font settings.", 
                    "OK");
            }
            else
            {
                Debug.LogError("Failed to create prefab!");
            }
        }

        [MenuItem("Fourfold Fate/Create Battle Arena UI")]
        public static void CreateBattleArenaUI()
        {
            // Similar setup for Battle Arena UI
            Debug.Log("Battle Arena UI creation - Coming soon!");
        }
    }
}

