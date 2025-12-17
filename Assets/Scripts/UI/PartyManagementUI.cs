using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Party management UI screen.
    /// </summary>
    public class PartyManagementUI : BaseUI
    {
        [Header("UI Elements")]
        public Text partySizeText;
        public Text unlockInfoText;
        public Text synergyBadgesText;

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (RunManager.Instance != null)
            {
                // Update party management info
            }
        }
    }
}

