using UnityEngine;
using UnityEditor;
using FourfoldFate.UI;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Utility to clean up duplicate UI elements and fix common issues.
    /// </summary>
    public class UICleanup
    {
        [MenuItem("Fourfold Fate/Clean Up Duplicate UI")]
        public static void CleanupDuplicateUI()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                EditorUtility.DisplayDialog("Error", "No Canvas found in scene!", "OK");
                return;
            }

            // Find all MainMenuPanel instances
            MainMenuUI[] mainMenus = canvas.GetComponentsInChildren<MainMenuUI>(true);
            
            if (mainMenus.Length <= 1)
            {
                EditorUtility.DisplayDialog("Info", $"Found {mainMenus.Length} MainMenuPanel(s). No duplicates to clean up.", "OK");
                return;
            }

            // Keep the first one, delete the rest
            int deleted = 0;
            for (int i = 1; i < mainMenus.Length; i++)
            {
                Debug.Log($"Deleting duplicate MainMenuPanel: {mainMenus[i].gameObject.name}");
                Object.DestroyImmediate(mainMenus[i].gameObject);
                deleted++;
            }

            EditorUtility.DisplayDialog("Success", 
                $"Cleaned up {deleted} duplicate MainMenuPanel(s).\n\n" +
                "Only one MainMenuPanel remains in the scene.", 
                "OK");
        }

        [MenuItem("Fourfold Fate/Fix Canvas Quality")]
        public static void FixCanvasQuality()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                EditorUtility.DisplayDialog("Error", "No Canvas found in scene!", "OK");
                return;
            }

            // Fix Canvas settings
            canvas.pixelPerfect = false; // Better quality
            
            // Fix Canvas Scaler
            UnityEngine.UI.CanvasScaler scaler = canvas.GetComponent<UnityEngine.UI.CanvasScaler>();
            if (scaler != null)
            {
                scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 0.5f;
                EditorUtility.SetDirty(scaler);
            }

            // Fix Canvas Renderer
            CanvasRenderer renderer = canvas.GetComponent<CanvasRenderer>();
            if (renderer != null)
            {
                renderer.cullTransparentMesh = true;
            }

            EditorUtility.SetDirty(canvas);
            EditorUtility.DisplayDialog("Success", "Canvas quality settings have been optimized!", "OK");
        }
    }
}

