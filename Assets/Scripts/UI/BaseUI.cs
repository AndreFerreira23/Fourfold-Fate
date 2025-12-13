using UnityEngine;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Base class for all UI screens.
    /// Provides common show/hide functionality.
    /// </summary>
    public abstract class BaseUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] protected GameObject rootPanel;
        [SerializeField] protected CanvasGroup canvasGroup;

        protected virtual void Awake()
        {
            if (rootPanel == null)
                rootPanel = gameObject;

            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup == null)
                canvasGroup = rootPanel.AddComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            if (rootPanel != null)
                rootPanel.SetActive(true);

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void Hide()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            if (rootPanel != null)
                rootPanel.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            // Override in derived classes
        }

        protected virtual void OnDisable()
        {
            // Override in derived classes
        }
    }
}

