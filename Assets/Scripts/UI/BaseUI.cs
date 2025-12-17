using UnityEngine;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Base class for all UI screens.
    /// </summary>
    public abstract class BaseUI : MonoBehaviour
    {
        [Header("UI Panel")]
        public GameObject rootPanel;

        /// <summary>
        /// Show this UI screen.
        /// </summary>
        public virtual void Show()
        {
            if (rootPanel != null)
            {
                rootPanel.SetActive(true);
                Debug.Log($"Showing UI: {rootPanel.name}");
            }
            else
            {
                Debug.LogWarning($"Cannot show UI: rootPanel is null on {gameObject.name}");
            }
        }

        /// <summary>
        /// Hide this UI screen.
        /// </summary>
        public virtual void Hide()
        {
            if (rootPanel != null)
            {
                rootPanel.SetActive(false);
            }
        }
    }
}

