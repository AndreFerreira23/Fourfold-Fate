using UnityEngine;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Checks if Input System settings are correct and warns if not.
    /// </summary>
    public class InputSystemChecker : MonoBehaviour
    {
        private void Start()
        {
            CheckInputSystem();
        }

        [ContextMenu("Check Input System Settings")]
        public void CheckInputSystem()
        {
            #if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            Debug.LogError("❌ INPUT SYSTEM ERROR!");
            Debug.LogError("Unity is using the NEW Input System, but the game needs the OLD Input System.");
            Debug.LogError("");
            Debug.LogError("TO FIX:");
            Debug.LogError("1. Go to: Edit > Project Settings");
            Debug.LogError("2. Click 'Player' in the left sidebar");
            Debug.LogError("3. Scroll down to 'Other Settings'");
            Debug.LogError("4. Find 'Active Input Handling'");
            Debug.LogError("5. Change it to: 'Input Manager (Old)'");
            Debug.LogError("6. Restart Unity if prompted");
            Debug.LogError("");
            Debug.LogError("Buttons will NOT work until you fix this!");
            #elif ENABLE_LEGACY_INPUT_MANAGER
            Debug.Log("✅ Input System is correct (using Input Manager Old)");
            #else
            Debug.LogWarning("⚠️ Input System status unknown");
            #endif
        }
    }
}

