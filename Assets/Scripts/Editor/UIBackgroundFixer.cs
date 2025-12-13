using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using FourfoldFate.UI;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Utility to fix background color issues in UI prefabs.
    /// </summary>
    public class UIBackgroundFixer
    {
        [MenuItem("Fourfold Fate/Fix Main Menu Background Color")]
        public static void FixMainMenuBackground()
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
            
            // Find MainMenuPanel Image component
            Image panelImage = prefabInstance.GetComponent<Image>();
            if (panelImage == null)
            {
                panelImage = prefabInstance.AddComponent<Image>();
            }

            // Method 1: Try using Unity's default white sprite and set color
            // This is simpler and more reliable
            if (panelImage.sprite == null)
            {
                // Try to get Unity's default white sprite
                Texture2D whiteTexture = Texture2D.whiteTexture;
                if (whiteTexture != null)
                {
                    Sprite whiteSprite = Sprite.Create(whiteTexture, new Rect(0, 0, whiteTexture.width, whiteTexture.height), new Vector2(0.5f, 0.5f));
                    panelImage.sprite = whiteSprite;
                }
                else
                {
                    // Create solid color sprite as fallback
                    Texture2D texture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                    texture.SetPixel(0, 0, Color.white);
                    texture.Apply();
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 100f);
                    panelImage.sprite = sprite;
                }
            }
            
            // Set the color directly - this is what you're doing manually
            Color bgColor = new Color(0.1f, 0.1f, 0.15f, 1f); // Dark blue-gray
            panelImage.color = bgColor; // Set color directly
            panelImage.type = Image.Type.Simple;
            panelImage.preserveAspect = false;
            
            EditorUtility.SetDirty(panelImage);
            EditorUtility.SetDirty(prefabInstance);
            
            // Save prefab
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, prefabPath);
            PrefabUtility.UnloadPrefabContents(prefabInstance);
            
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Success", 
                "Main Menu background color has been fixed!\n\n" +
                "The background should now show a dark blue-gray color.\n\n" +
                "Press Play to test.", "OK");
        }
    }
}

