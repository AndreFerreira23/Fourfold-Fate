#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Editor script to automate UI prefab creation.
    /// </summary>
    public class UIAutoSetup : EditorWindow
    {
        [MenuItem("Fourfold Fate/Setup UI")]
        public static void SetupUI()
        {
            // Call the Battle UI setup
            BattleUISetup.SetupBattleUI();
        }
    }
}
#endif

