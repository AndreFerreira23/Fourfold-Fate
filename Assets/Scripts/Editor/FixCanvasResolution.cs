using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Fix Canvas resolution and quality issues.
    /// </summary>
    public class FixCanvasResolution
    {
        [MenuItem("Fourfold Fate/Fix Canvas Resolution & Quality")]
        public static void FixCanvasQuality()
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                EditorUtility.DisplayDialog("Error", "No Canvas found in scene!", "OK");
                return;
            }

            // Fix Canvas settings
            canvas.pixelPerfect = false; // Disable for better quality
            canvas.sortingOrder = 0;
            
            // Fix Canvas Scaler
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null)
            {
                scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            }
            
            // Set optimal scaling settings
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f; // Balance between width and height
            
            // Check Game view resolution
            var gameView = EditorWindow.GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            if (gameView != null)
            {
                // Try to set a reasonable game view size
                var gameViewType = gameView.GetType();
                var selectedSizeIndexProperty = gameViewType.GetProperty("selectedSizeIndex", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (selectedSizeIndexProperty != null)
                {
                    // Try to set to a reasonable size (index varies, but we'll try common ones)
                    Debug.Log("Game view resolution may need manual adjustment");
                }
            }
            
            EditorUtility.SetDirty(canvas);
            EditorUtility.SetDirty(scaler);
            
            EditorUtility.DisplayDialog("Success", 
                "Canvas resolution and quality settings have been optimized!\n\n" +
                "Settings applied:\n" +
                "• Pixel Perfect: OFF (better quality)\n" +
                "• Reference Resolution: 1920x1080\n" +
                "• Screen Match: Match Width Or Height (0.5)\n\n" +
                "If resolution still looks low:\n" +
                "1. Check Game view resolution (top toolbar)\n" +
                "2. Try Free Aspect or a specific resolution\n" +
                "3. Check your monitor's display scaling", 
                "OK");
            
            Debug.Log("✓ Canvas resolution and quality fixed");
        }
    }
}

