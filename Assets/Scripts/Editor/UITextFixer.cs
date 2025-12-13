using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using FourfoldFate.UI;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Utility to fix text display issues in UI prefabs.
    /// </summary>
    public class UITextFixer
    {
        [MenuItem("Fourfold Fate/Fix Main Menu Text Display")]
        public static void FixMainMenuText()
        {
            string prefabPath = "Assets/Prefabs/UI/MainMenuPanel.prefab";
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            
            if (prefab == null)
            {
                EditorUtility.DisplayDialog("Error", "MainMenuPanel prefab not found at:\n" + prefabPath, "OK");
                return;
            }

            // Open prefab in edit mode
            GameObject prefabInstance = PrefabUtility.LoadPrefabContents(prefabPath);
            
            // Find TitleText
            Text titleText = prefabInstance.GetComponentInChildren<Text>();
            if (titleText != null && titleText.name == "TitleText")
            {
                // Fix text settings
                titleText.text = "Fourfold Fate";
                titleText.horizontalOverflow = HorizontalWrapMode.Overflow;
                titleText.verticalOverflow = VerticalWrapMode.Overflow;
                titleText.resizeTextForBestFit = false;
                
                // Fix RectTransform
                RectTransform rect = titleText.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.sizeDelta = new Vector2(1200, 120);
                    rect.anchorMin = new Vector2(0.5f, 1f);
                    rect.anchorMax = new Vector2(0.5f, 1f);
                    rect.pivot = new Vector2(0.5f, 1f);
                    rect.anchoredPosition = new Vector2(0, -50);
                }
                
                // Ensure font is set
                if (titleText.font == null)
                {
                    try
                    {
                        titleText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                    }
                    catch
                    {
                        Debug.LogWarning("Could not set font");
                    }
                }
                
                EditorUtility.SetDirty(titleText);
                Debug.Log("Fixed TitleText settings");
            }
            
            // Save prefab
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
            PrefabUtility.UnloadPrefabContents(prefabInstance);
            
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Success", "Main Menu text has been fixed!\n\n" +
                "The text box is now 1200 pixels wide.\n" +
                "Overflow settings have been enabled.\n\n" +
                "Press Play to test.", "OK");
        }
    }
}

