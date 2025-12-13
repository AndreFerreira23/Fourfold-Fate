using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Core;
using FourfoldFate.Progression;
using LevelUpChoice = FourfoldFate.Progression.LevelUpChoice;

namespace FourfoldFate.UI
{
    /// <summary>
    /// UI for level-up choice selection.
    /// </summary>
    public class LevelUpUI : BaseUI
    {
        [Header("UI Elements")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text loreText;
        [SerializeField] private Transform choicesContainer;
        [SerializeField] private GameObject choiceCardPrefab;
        [SerializeField] private List<LevelUpChoiceCard> choiceCards = new List<LevelUpChoiceCard>();

        [Header("Lore Text")]
        [SerializeField] private string levelUpLore = "The Trials acknowledge your progress. Choose the path that shapes your Circle.";

        private LevelUpSystem levelUpSystem;
        private Unit currentUnit;
        private int currentLevel;

        protected override void Awake()
        {
            base.Awake();
            levelUpSystem = FindObjectOfType<LevelUpSystem>();
        }

        public void ShowLevelUp(Unit unit, int level)
        {
            currentUnit = unit;
            currentLevel = level;
            Show();
            SetupLevelUpChoices();
        }

        private void SetupLevelUpChoices()
        {
            if (levelUpSystem == null || currentUnit == null) return;

            // Clear existing cards
            foreach (var card in choiceCards)
            {
                if (card != null) Destroy(card.gameObject);
            }
            choiceCards.Clear();

            // Generate choices
            var choices = levelUpSystem.GenerateChoices(currentUnit, currentLevel);

            // Create UI cards
            foreach (var choice in choices)
            {
                GameObject cardObj = Instantiate(choiceCardPrefab, choicesContainer);
                LevelUpChoiceCard card = cardObj.GetComponent<LevelUpChoiceCard>();
                if (card == null) card = cardObj.AddComponent<LevelUpChoiceCard>();

                card.Initialize(choice, OnChoiceSelected);
                choiceCards.Add(card);
            }

            // Update lore text
            if (loreText != null)
                loreText.text = levelUpLore;

            if (titleText != null)
                titleText.text = $"Trial {currentLevel} - Choose Your Path";
        }

        private void OnChoiceSelected(LevelUpChoice choice)
        {
            if (levelUpSystem != null && currentUnit != null)
            {
                levelUpSystem.ApplyChoice(currentUnit, choice);
                OnLevelUpComplete?.Invoke(currentUnit, choice);
                Hide();
            }
        }

        // Events
        public System.Action<Unit, LevelUpChoice> OnLevelUpComplete;
    }

    /// <summary>
    /// UI card for a level-up choice
    /// </summary>
    public class LevelUpChoiceCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text pathTitleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Transform upgradesContainer;
        [SerializeField] private GameObject upgradeTextPrefab;
        [SerializeField] private Button selectButton;
        [SerializeField] private Image pathIcon;

        [Header("Path Colors")]
        [SerializeField] private Color offenseColor = Color.red;
        [SerializeField] private Color defenseColor = Color.blue;
        [SerializeField] private Color utilityColor = Color.green;
        [SerializeField] private Color chaosColor = Color.magenta;

        private LevelUpChoice choice;
        private System.Action<LevelUpChoice> onSelectCallback;

        public void Initialize(LevelUpChoice choiceData, System.Action<LevelUpChoice> onSelect)
        {
            choice = choiceData;
            onSelectCallback = onSelect;

            if (pathTitleText != null)
            {
                pathTitleText.text = GetPathLoreName(choiceData.pathType);
            }

            if (descriptionText != null)
                descriptionText.text = choiceData.description;

            // Set color based on path type
            Color pathColor = GetPathColor(choiceData.pathType);
            if (pathIcon != null)
                pathIcon.color = pathColor;

            // Display upgrades
            if (upgradesContainer != null && upgradeTextPrefab != null)
            {
                foreach (var upgrade in choiceData.upgrades)
                {
                    GameObject upgradeObj = Instantiate(upgradeTextPrefab, upgradesContainer);
                    Text text = upgradeObj.GetComponent<Text>();
                    if (text != null)
                    {
                        string sign = upgrade.value >= 0 ? "+" : "";
                        text.text = $"{sign}{upgrade.value:F1} {upgrade.statName}";
                        text.color = upgrade.value >= 0 ? Color.white : Color.red;
                    }
                }
            }

            if (selectButton != null)
                selectButton.onClick.AddListener(() => onSelectCallback?.Invoke(choice));
        }

        private string GetPathLoreName(LevelUpSystem.LevelUpPathType pathType)
        {
            return pathType switch
            {
                LevelUpSystem.LevelUpPathType.Offense => "Path of Wrath",
                LevelUpSystem.LevelUpPathType.Defense => "Path of Endurance",
                LevelUpSystem.LevelUpPathType.Utility => "Path of Versatility",
                LevelUpSystem.LevelUpPathType.Chaos => "Path of Risk",
                _ => pathType.ToString()
            };
        }

        private Color GetPathColor(LevelUpSystem.LevelUpPathType pathType)
        {
            return pathType switch
            {
                LevelUpSystem.LevelUpPathType.Offense => offenseColor,
                LevelUpSystem.LevelUpPathType.Defense => defenseColor,
                LevelUpSystem.LevelUpPathType.Utility => utilityColor,
                LevelUpSystem.LevelUpPathType.Chaos => chaosColor,
                _ => Color.white
            };
        }
    }
}

