using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Core;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// Battle arena UI screen with full combat interface.
    /// </summary>
    public class BattleArenaUI : BaseUI
    {
        [Header("Party Display")]
        public Transform partyContainer;
        public GameObject partyUnitPrefab; // Will be created dynamically if null

        [Header("Enemy Display")]
        public Transform enemyContainer;
        public GameObject enemyUnitPrefab; // Will be created dynamically if null

        [Header("Action Buttons")]
        public Button attackButton;
        public Button endTurnButton;
        public Transform abilityButtonContainer;
        public GameObject abilityButtonPrefab;

        [Header("Combat Info")]
        public Text turnIndicatorText;
        public Text combatLogText;
        public ScrollRect combatLogScrollRect;

        [Header("Party Stats")]
        public Text synergyBadgesText;

        private BattleManager battleManager;
        private List<GameObject> partyUIElements = new List<GameObject>();
        private List<GameObject> enemyUIElements = new List<GameObject>();
        private Unit selectedUnit;
        private Unit selectedTarget;

        private void Start()
        {
            SetupButtons();
            battleManager = BattleManager.Instance;
            
            if (battleManager != null)
            {
                battleManager.OnDamageDealt += OnDamageDealt;
                battleManager.OnCombatEnded += OnCombatEnded;
            }
        }

        private void Update()
        {
            UpdateUI();
        }

        private void SetupButtons()
        {
            if (attackButton != null)
            {
                attackButton.onClick.AddListener(OnAttackClicked);
            }

            if (endTurnButton != null)
            {
                endTurnButton.onClick.AddListener(OnEndTurnClicked);
            }
        }

        private void UpdateUI()
        {
            if (battleManager == null) return;

            // Update turn indicator
            if (turnIndicatorText != null)
            {
                turnIndicatorText.text = battleManager.isPlayerTurn ? "Your Turn" : "Enemy Turn";
            }

            // Update party display
            UpdatePartyDisplay();
            
            // Update enemy display
            UpdateEnemyDisplay();

            // Update button states
            if (attackButton != null)
            {
                attackButton.interactable = battleManager.isPlayerTurn && battleManager.isCombatActive;
            }

            if (endTurnButton != null)
            {
                endTurnButton.interactable = battleManager.isPlayerTurn && battleManager.isCombatActive;
            }

            // Update synergy badges
            UpdateSynergyBadges();
        }

        private void UpdatePartyDisplay()
        {
            if (partyContainer == null || battleManager == null) return;

            // Clear existing UI
            foreach (var ui in partyUIElements)
            {
                if (ui != null) Destroy(ui);
            }
            partyUIElements.Clear();

            // Create UI for each party member
            for (int i = 0; i < battleManager.partyUnits.Count; i++)
            {
                Unit unit = battleManager.partyUnits[i];
                if (unit == null) continue;

                GameObject unitUI = CreateUnitUI(unit, partyContainer, true);
                partyUIElements.Add(unitUI);
            }
        }

        private void UpdateEnemyDisplay()
        {
            if (enemyContainer == null || battleManager == null) return;

            // Clear existing UI
            foreach (var ui in enemyUIElements)
            {
                if (ui != null) Destroy(ui);
            }
            enemyUIElements.Clear();

            // Create UI for each enemy
            for (int i = 0; i < battleManager.enemyUnits.Count; i++)
            {
                Unit unit = battleManager.enemyUnits[i];
                if (unit == null) continue;

                GameObject unitUI = CreateUnitUI(unit, enemyContainer, false);
                enemyUIElements.Add(unitUI);
            }
        }

        private GameObject CreateUnitUI(Unit unit, Transform parent, bool isParty)
        {
            // Create unit UI panel
            GameObject unitPanel = new GameObject($"{unit.unitName}_UI");
            unitPanel.transform.SetParent(parent, false);

            RectTransform rect = unitPanel.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(200, 150);
            rect.anchoredPosition = Vector2.zero;

            // Add background
            Image bg = unitPanel.AddComponent<Image>();
            bg.color = isParty ? new Color(0.2f, 0.4f, 0.8f, 0.5f) : new Color(0.8f, 0.2f, 0.2f, 0.5f);

            // Add unit name
            GameObject nameObj = new GameObject("Name");
            nameObj.transform.SetParent(unitPanel.transform, false);
            Text nameText = nameObj.AddComponent<Text>();
            nameText.text = unit.unitName;
            nameText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            nameText.fontSize = 14;
            nameText.color = Color.white;
            RectTransform nameRect = nameObj.GetComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0, 0.7f);
            nameRect.anchorMax = new Vector2(1, 1);
            nameRect.sizeDelta = Vector2.zero;
            nameRect.anchoredPosition = Vector2.zero;

            // Add health bar
            GameObject healthBarObj = new GameObject("HealthBar");
            healthBarObj.transform.SetParent(unitPanel.transform, false);
            RectTransform healthBarRect = healthBarObj.AddComponent<RectTransform>();
            healthBarRect.anchorMin = new Vector2(0.05f, 0.4f);
            healthBarRect.anchorMax = new Vector2(0.95f, 0.6f);
            healthBarRect.sizeDelta = Vector2.zero;
            healthBarRect.anchoredPosition = Vector2.zero;

            // Health bar background
            Image healthBg = healthBarObj.AddComponent<Image>();
            healthBg.color = Color.black;

            // Health bar fill
            GameObject healthFillObj = new GameObject("HealthFill");
            healthFillObj.transform.SetParent(healthBarObj.transform, false);
            Image healthFill = healthFillObj.AddComponent<Image>();
            healthFill.color = Color.green;
            RectTransform healthFillRect = healthFillObj.GetComponent<RectTransform>();
            healthFillRect.anchorMin = Vector2.zero;
            healthFillRect.anchorMax = new Vector2(1, 1);
            healthFillRect.sizeDelta = Vector2.zero;
            healthFillRect.anchoredPosition = Vector2.zero;

            // Health text
            GameObject healthTextObj = new GameObject("HealthText");
            healthTextObj.transform.SetParent(unitPanel.transform, false);
            Text healthText = healthTextObj.AddComponent<Text>();
            healthText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            healthText.fontSize = 12;
            healthText.color = Color.white;
            healthText.alignment = TextAnchor.MiddleCenter;
            RectTransform healthTextRect = healthTextObj.GetComponent<RectTransform>();
            healthTextRect.anchorMin = new Vector2(0, 0.2f);
            healthTextRect.anchorMax = new Vector2(1, 0.4f);
            healthTextRect.sizeDelta = Vector2.zero;
            healthTextRect.anchoredPosition = Vector2.zero;

            // Store references for updating
            UnitUIComponent uiComponent = unitPanel.AddComponent<UnitUIComponent>();
            uiComponent.unit = unit;
            uiComponent.healthFill = healthFill;
            uiComponent.healthText = healthText;

            // Add click handler for targeting
            if (isParty)
            {
                Button selectButton = unitPanel.AddComponent<Button>();
                selectButton.onClick.AddListener(() => SelectPartyUnit(unit));
            }
            else
            {
                Button targetButton = unitPanel.AddComponent<Button>();
                targetButton.onClick.AddListener(() => SelectTarget(unit));
            }

            return unitPanel;
        }

        private void UpdateSynergyBadges()
        {
            if (synergyBadgesText == null || Party.PartyManager.Instance == null) return;

            var synergies = Party.PartyManager.Instance.GetActiveSynergies();
            if (synergies.Count > 0)
            {
                string synergyText = "Active Synergies: ";
                foreach (var tag in synergies)
                {
                    synergyText += tag.ToString() + " ";
                }
                synergyBadgesText.text = synergyText;
            }
            else
            {
                synergyBadgesText.text = "No Active Synergies";
            }
        }

        private void OnAttackClicked()
        {
            if (battleManager == null || !battleManager.isPlayerTurn) return;
            if (battleManager.partyUnits.Count == 0 || battleManager.enemyUnits.Count == 0) return;

            // Auto-attack first enemy for now
            Unit attacker = battleManager.partyUnits[0];
            Unit target = battleManager.enemyUnits[0];
            
            battleManager.PlayerAttack(attacker, target);
            AddCombatLog($"{attacker.unitName} attacks {target.unitName}!");
        }

        private void OnEndTurnClicked()
        {
            if (battleManager != null && battleManager.isPlayerTurn)
            {
                battleManager.EndPlayerTurn();
                AddCombatLog("Turn ended.");
            }
        }

        private void SelectPartyUnit(Unit unit)
        {
            selectedUnit = unit;
            AddCombatLog($"Selected {unit.unitName}");
        }

        private void SelectTarget(Unit unit)
        {
            selectedTarget = unit;
            AddCombatLog($"Targeting {unit.unitName}");
        }

        private void OnDamageDealt(Unit attacker, Unit target, float damage)
        {
            AddCombatLog($"{attacker.unitName} deals {damage:F0} damage to {target.unitName}!");
        }

        private void OnCombatEnded(bool victory)
        {
            if (victory)
            {
                AddCombatLog("Victory! You won the battle!");
                // TODO: Show reward screen
            }
            else
            {
                AddCombatLog("Defeat! Your party has fallen.");
                // TODO: Show game over screen
            }
        }

        private void AddCombatLog(string message)
        {
            if (combatLogText != null)
            {
                combatLogText.text += message + "\n";
                
                // Auto-scroll to bottom
                if (combatLogScrollRect != null)
                {
                    Canvas.ForceUpdateCanvases();
                    combatLogScrollRect.verticalNormalizedPosition = 0f;
                }
            }
        }

        private void OnDestroy()
        {
            if (battleManager != null)
            {
                battleManager.OnDamageDealt -= OnDamageDealt;
                battleManager.OnCombatEnded -= OnCombatEnded;
            }
        }
    }

    /// <summary>
    /// Component to hold references for unit UI updates.
    /// </summary>
    public class UnitUIComponent : MonoBehaviour
    {
        public Unit unit;
        public Image healthFill;
        public Text healthText;

        private void Update()
        {
            if (unit != null && healthFill != null && healthText != null)
            {
                float healthPercent = unit.CurrentHealth / unit.MaxHealth;
                healthFill.fillAmount = healthPercent;
                healthText.text = $"{unit.CurrentHealth:F0} / {unit.MaxHealth:F0}";
            }
        }
    }
}

