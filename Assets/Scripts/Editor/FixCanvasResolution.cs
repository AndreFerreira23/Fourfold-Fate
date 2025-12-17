#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Editor utility to fix canvas resolution.
    /// </summary>
    public class FixCanvasResolution : EditorWindow
    {
        [MenuItem("Fourfold Fate/Fix Canvas Resolution")]
        public static void FixResolution()
        {
            // TODO: Implement canvas resolution fix
            Debug.Log("Fix Canvas Resolution - TODO: Implement");
        }
    }
}
#endif

