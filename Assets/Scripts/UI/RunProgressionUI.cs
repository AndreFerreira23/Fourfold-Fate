using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Run progression UI screen.
    /// </summary>
    public class RunProgressionUI : BaseUI
    {
        [Header("UI Elements")]
        public Text currentLevelText;
        public Text nextBossText;
        public Text partyStatusText;
        public Text relicCountText;

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (RunManager.Instance != null)
            {
                if (currentLevelText != null)
                {
                    currentLevelText.text = $"Level: {RunManager.Instance.currentLevel}/100";
                }
            }
        }
    }
}

