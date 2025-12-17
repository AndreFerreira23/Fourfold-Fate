using UnityEngine;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Initializes the game on startup.
    /// NOTE: AutoSceneSetup handles all setup now. This script is kept for compatibility.
    /// If you have AutoSceneSetup, you can remove GameInitializer.
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        private void Awake()
        {
            // AutoSceneSetup now handles all manager creation
            // This script is kept for backward compatibility but does nothing
            // You can safely remove this component if AutoSceneSetup is present
        }
    }
}

