using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Battle arena UI showing combat state, party health, enemy health, and combat feedback.
    /// </summary>
    public class BattleArenaUI : BaseUI
    {
        [Header("Party Display")]
        [SerializeField] private Transform partyContainer;
        [SerializeField] private GameObject unitPanelPrefab;
        [SerializeField] private List<UnitPanel> unitPanels = new List<UnitPanel>();

        [Header("Enemy Display")]
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private GameObject enemyPanelPrefab;
        [SerializeField] private List<EnemyPanel> enemyPanels = new List<EnemyPanel>();

        [Header("Combat Info")]
        [SerializeField] private Text levelText;
        [SerializeField] private Text encounterTypeText;
        [SerializeField] private Text combatLogText;
        [SerializeField] private ScrollRect combatLogScroll;

        [Header("Synergy Display")]
        [SerializeField] private Transform synergyContainer;
        [SerializeField] private GameObject synergyBadgePrefab;

        [Header("Archetype Indicators")]
        [SerializeField] private Color tankColor = new Color(0.2f, 0.6f, 0.8f);
        [SerializeField] private Color fighterColor = new Color(0.8f, 0.3f, 0.2f);
        [SerializeField] private Color mageColor = new Color(0.6f, 0.2f, 0.8f);
        [SerializeField] private Color assassinColor = new Color(0.3f, 0.3f, 0.3f);

        private BattleManager battleManager;
        private PartyManager partyManager;
        private RunManager runManager;

        protected override void Awake()
        {
            base.Awake();
            battleManager = FindObjectOfType<BattleManager>();
            partyManager = FindObjectOfType<PartyManager>();
            runManager = FindObjectOfType<RunManager>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (battleManager != null)
            {
                battleManager.OnBattleStarted += OnBattleStarted;
                battleManager.OnBattleEnded += OnBattleEnded;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (battleManager != null)
            {
                battleManager.OnBattleStarted -= OnBattleStarted;
                battleManager.OnBattleEnded -= OnBattleEnded;
            }
        }

        private void Update()
        {
            UpdatePartyDisplay();
            UpdateEnemyDisplay();
            UpdateLevelDisplay();
        }

        private void OnBattleStarted()
        {
            SetupPartyDisplay();
            SetupEnemyDisplay();
            SetupSynergyDisplay();
            AddCombatLog("The Trials begin to test your Circle.");
        }

        private void OnBattleEnded(BattleResult result)
        {
            string resultText = result == BattleResult.Victory 
                ? "Your Circle stands. The Trials acknowledge your victory." 
                : "The Circle breaks. The Trials claim their due.";
            AddCombatLog(resultText);
        }

        private void SetupPartyDisplay()
        {
            if (partyManager == null || partyContainer == null) return;

            // Clear existing panels
            foreach (var panel in unitPanels)
            {
                if (panel != null) Destroy(panel.gameObject);
            }
            unitPanels.Clear();

            // Create panels for each party member
            foreach (var unit in partyManager.PartyMembers)
            {
                if (unit == null) continue;

                GameObject panelObj = Instantiate(unitPanelPrefab, partyContainer);
                UnitPanel panel = panelObj.GetComponent<UnitPanel>();
                if (panel == null) panel = panelObj.AddComponent<UnitPanel>();

                panel.Initialize(unit);
                unitPanels.Add(panel);

                // Subscribe to unit events
                unit.OnHealthChanged += panel.OnHealthChanged;
                unit.OnUnitDied += panel.OnUnitDied;
            }
        }

        private void SetupEnemyDisplay()
        {
            if (battleManager == null || enemyContainer == null) return;

            // Clear existing panels
            foreach (var panel in enemyPanels)
            {
                if (panel != null) Destroy(panel.gameObject);
            }
            enemyPanels.Clear();

            // Get enemies from battle manager
            foreach (var enemy in battleManager.EnemyTeam)
            {
                if (enemy == null) continue;

                GameObject panelObj = Instantiate(enemyPanelPrefab, enemyContainer);
                EnemyPanel panel = panelObj.GetComponent<EnemyPanel>();
                if (panel == null) panel = panelObj.AddComponent<EnemyPanel>();

                panel.Initialize(enemy);
                enemyPanels.Add(panel);

                // Subscribe to enemy events
                enemy.OnHealthChanged += panel.OnHealthChanged;
                enemy.OnUnitDied += panel.OnUnitDied;
            }
        }

        private void UpdatePartyDisplay()
        {
            foreach (var panel in unitPanels)
            {
                if (panel != null && panel.Unit != null)
                {
                    panel.UpdateDisplay();
                }
            }
        }

        private void UpdateEnemyDisplay()
        {
            foreach (var panel in enemyPanels)
            {
                if (panel != null && panel.Enemy != null)
                {
                    panel.UpdateDisplay();
                }
            }
        }

        private void SetupSynergyDisplay()
        {
            if (partyManager == null || synergyContainer == null) return;

            // Clear existing badges
            foreach (Transform child in synergyContainer)
            {
                Destroy(child.gameObject);
            }

            // Get active synergies
            var synergies = partyManager.GetActiveSynergies();
            foreach (var kvp in synergies)
            {
                GameObject badgeObj = Instantiate(synergyBadgePrefab, synergyContainer);
                Text text = badgeObj.GetComponentInChildren<Text>();
                if (text != null)
                {
                    text.text = GetSynergyCourtName(kvp.Key);
                }
            }
        }

        private void UpdateLevelDisplay()
        {
            if (runManager != null && levelText != null)
            {
                levelText.text = $"Trial {runManager.CurrentLevel}/100";
            }

            if (encounterTypeText != null && runManager != null)
            {
                if (runManager.IsFinalBossLevel(runManager.CurrentLevel))
                    encounterTypeText.text = "The Sundered Arbiter";
                else if (runManager.IsMajorMinibossLevel(runManager.CurrentLevel))
                    encounterTypeText.text = "Myth-Eater";
                else if (runManager.IsMinibossLevel(runManager.CurrentLevel))
                    encounterTypeText.text = "Tollgate";
                else
                    encounterTypeText.text = "Standard Trial";
            }
        }

        public void AddCombatLog(string message)
        {
            if (combatLogText != null)
            {
                combatLogText.text += $"\n{message}";
                
                // Auto-scroll to bottom
                if (combatLogScroll != null)
                {
                    Canvas.ForceUpdateCanvases();
                    combatLogScroll.verticalNormalizedPosition = 0f;
                }
            }
        }

        private string GetSynergyCourtName(SynergyTag tag)
        {
            return tag switch
            {
                SynergyTag.Fire => "Court of Ember",
                SynergyTag.Nature => "Court of Verdance",
                SynergyTag.Shadow => "Court of Gloam",
                SynergyTag.Holy => "Court of Dawn",
                SynergyTag.Arcane => "Court of Aether",
                SynergyTag.Steel => "Court of Anvil",
                SynergyTag.Storm => "Court of Tempest",
                _ => tag.ToString()
            };
        }
    }

    /// <summary>
    /// UI panel for displaying a unit's combat state
    /// </summary>
    public class UnitPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text healthText;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image archetypeIndicator;
        [SerializeField] private Text archetypeText;
        [SerializeField] private Text guardText;
        [SerializeField] private Text momentumText;
        [SerializeField] private Text surgeText;
        [SerializeField] private Text opportunityText;

        public Unit Unit { get; private set; }

        public void Initialize(Unit unit)
        {
            Unit = unit;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (Unit == null || Unit.Data == null) return;

            if (nameText != null)
                nameText.text = Unit.Data.unitName;

            if (healthText != null)
                healthText.text = $"{Unit.CurrentHealth:F0}/{Unit.Data.MaxHealth:F0}";

            if (healthBar != null)
            {
                float healthPercent = Unit.CurrentHealth / Unit.Data.MaxHealth;
                healthBar.fillAmount = healthPercent;
            }

            // Update archetype-specific displays
            if (Unit.Archetype != null)
            {
                UpdateArchetypeDisplay();
            }
        }

        private void UpdateArchetypeDisplay()
        {
            if (archetypeText != null)
            {
                archetypeText.text = GetArchetypeLoreName(Unit.Archetype.Type);
            }

            // Update archetype-specific resource displays
            switch (Unit.Archetype)
            {
                case Core.Archetypes.TankArchetype tank:
                    if (guardText != null)
                    {
                        guardText.text = $"Guard: {tank.CurrentGuard}/{tank.MaxGuard}";
                        guardText.gameObject.SetActive(true);
                    }
                    break;

                case Core.Archetypes.FighterArchetype fighter:
                    if (momentumText != null)
                    {
                        momentumText.text = $"Momentum: {fighter.CurrentMomentum}/{fighter.MaxMomentum}";
                        momentumText.gameObject.SetActive(true);
                    }
                    break;

                case Core.Archetypes.MageArchetype mage:
                    if (surgeText != null)
                    {
                        surgeText.text = $"Surge: {mage.CurrentManaSurge}/{mage.MaxManaSurge}";
                        if (mage.IsOverloaded)
                            surgeText.text += " [OVERLOAD]";
                        surgeText.gameObject.SetActive(true);
                    }
                    break;

                case Core.Archetypes.AssassinArchetype assassin:
                    if (opportunityText != null)
                    {
                        opportunityText.text = $"Chain: {assassin.KillChainCount}";
                        opportunityText.gameObject.SetActive(true);
                    }
                    break;
            }
        }

        public void OnHealthChanged(float current, float max)
        {
            UpdateDisplay();
        }

        public void OnUnitDied(Unit unit)
        {
            // Fade out or show death indicator
        }

        private string GetArchetypeLoreName(Core.Archetypes.ArchetypeType type)
        {
            return type switch
            {
                Core.Archetypes.ArchetypeType.Tank => "The Method of Keeping",
                Core.Archetypes.ArchetypeType.Fighter => "The Method of Motion",
                Core.Archetypes.ArchetypeType.Mage => "The Method of Witness",
                Core.Archetypes.ArchetypeType.Assassin => "The Method of Ending",
                _ => type.ToString()
            };
        }
    }

    /// <summary>
    /// UI panel for displaying enemy combat state
    /// </summary>
    public class EnemyPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text healthText;
        [SerializeField] private Image healthBar;

        public Unit Enemy { get; private set; }

        public void Initialize(Unit enemy)
        {
            Enemy = enemy;
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            if (Enemy == null || Enemy.Data == null) return;

            if (nameText != null)
                nameText.text = Enemy.Data.unitName;

            if (healthText != null)
                healthText.text = $"{Enemy.CurrentHealth:F0}/{Enemy.Data.MaxHealth:F0}";

            if (healthBar != null)
            {
                float healthPercent = Enemy.CurrentHealth / Enemy.Data.MaxHealth;
                healthBar.fillAmount = healthPercent;
            }
        }

        public void OnHealthChanged(float current, float max)
        {
            UpdateDisplay();
        }

        public void OnUnitDied(Unit unit)
        {
            // Fade out or show death indicator
        }
    }
}

