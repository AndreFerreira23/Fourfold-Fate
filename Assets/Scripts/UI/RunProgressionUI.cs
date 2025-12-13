using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Roguelike;
using FourfoldFate.Party;

namespace FourfoldFate.UI
{
    /// <summary>
    /// UI showing run progression, current level, party status, and collected relics.
    /// </summary>
    public class RunProgressionUI : BaseUI
    {
        [Header("Progression Display")]
        [SerializeField] private Text levelText;
        [SerializeField] private Slider levelProgressBar;
        [SerializeField] private Text nextBossText;

        [Header("Party Status")]
        [SerializeField] private Text partySizeText;
        [SerializeField] private Text partyStatusText;

        [Header("Relic Count")]
        [SerializeField] private Text relicCountText;

        [Header("Lore Text")]
        [SerializeField] private Text loreStatusText;

        private RunManager runManager;
        private PartyManager partyManager;
        private Relics.RelicManager relicManager;

        protected override void Awake()
        {
            base.Awake();
            runManager = FindObjectOfType<RunManager>();
            partyManager = FindObjectOfType<PartyManager>();
            relicManager = FindObjectOfType<Relics.RelicManager>();
        }

        private void Update()
        {
            UpdateProgressionDisplay();
        }

        private void UpdateProgressionDisplay()
        {
            if (runManager != null)
            {
                // Update level text
                if (levelText != null)
                    levelText.text = $"Trial {runManager.CurrentLevel}/100";

                // Update progress bar
                if (levelProgressBar != null)
                    levelProgressBar.value = runManager.CurrentLevel / 100f;

                // Update next boss info
                if (nextBossText != null)
                {
                    int nextBoss = GetNextBossLevel(runManager.CurrentLevel);
                    if (nextBoss > 0)
                    {
                        string bossName = GetBossName(nextBoss);
                        nextBossText.text = $"Next: {bossName} at Trial {nextBoss}";
                    }
                    else
                    {
                        nextBossText.text = "Final Trial Ahead";
                    }
                }
            }

            if (partyManager != null)
            {
                if (partySizeText != null)
                    partySizeText.text = $"Circle: {partyManager.CurrentPartySize}/4";

                if (partyStatusText != null)
                {
                    int nextUnlock = partyManager.GetNextUnlockLevel();
                    if (nextUnlock > 0)
                        partyStatusText.text = $"Next seat at Trial {nextUnlock}";
                    else
                        partyStatusText.text = "Circle complete";
                }
            }

            if (relicManager != null)
            {
                if (relicCountText != null)
                    relicCountText.text = $"Relics: {relicManager.CollectedRelicCount}";
            }

            // Update lore status
            if (loreStatusText != null && runManager != null)
            {
                loreStatusText.text = GetLoreStatusText(runManager.CurrentLevel);
            }
        }

        private int GetNextBossLevel(int currentLevel)
        {
            int[] bossLevels = { 10, 20, 30, 40, 50, 60, 80, 90, 100 };
            foreach (int level in bossLevels)
            {
                if (level > currentLevel)
                    return level;
            }
            return -1;
        }

        private string GetBossName(int level)
        {
            return level switch
            {
                10 => "First Knot",
                20 => "Tollgate",
                30 => "Myth-Eater",
                40 => "Tollgate",
                50 => "Myth-Eater",
                60 => "Tollgate",
                80 => "Myth-Eater",
                90 => "Tollgate",
                100 => "The Sundered Arbiter",
                _ => "Trial Keeper"
            };
        }

        private string GetLoreStatusText(int level)
        {
            if (level < 5)
                return "The First Seat holds. The Trials watch.";
            else if (level < 10)
                return "The Second Seat fills. Need becomes strength.";
            else if (level < 15)
                return "The Third Seat answers. Debt becomes bond.";
            else if (level < 100)
                return "The Circle is complete. The Trials test your worth.";
            else
                return "The final Trial awaits. The Sundered Arbiter speaks.";
        }
    }
}

