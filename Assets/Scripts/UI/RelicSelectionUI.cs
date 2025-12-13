using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Relics;

namespace FourfoldFate.UI
{
    /// <summary>
    /// UI for selecting relics after encounters.
    /// </summary>
    public class RelicSelectionUI : BaseUI
    {
        [Header("UI Elements")]
        [SerializeField] private Text titleText;
        [SerializeField] private Text loreText;
        [SerializeField] private Transform relicsContainer;
        [SerializeField] private GameObject relicCardPrefab;
        [SerializeField] private List<RelicCard> relicCards = new List<RelicCard>();
        [SerializeField] private Button skipButton;

        [Header("Lore Text")]
        [SerializeField] private string relicSelectionLore = "Memory-Forged artifacts offer their lessons. Choose the one that shapes your Circle's story.";

        private RelicManager relicManager;
        private List<Relic> offeredRelics = new List<Relic>();

        protected override void Awake()
        {
            base.Awake();
            relicManager = FindObjectOfType<RelicManager>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (skipButton != null)
                skipButton.onClick.AddListener(OnSkipClicked);
        }

        public void ShowRelicSelection(List<Relic> relics)
        {
            offeredRelics = relics;
            Show();
            SetupRelicCards();
        }

        private void SetupRelicCards()
        {
            if (relicsContainer == null || relicCardPrefab == null) return;

            // Clear existing cards
            foreach (var card in relicCards)
            {
                if (card != null) Destroy(card.gameObject);
            }
            relicCards.Clear();

            // Create cards for each offered relic
            foreach (var relic in offeredRelics)
            {
                GameObject cardObj = Instantiate(relicCardPrefab, relicsContainer);
                RelicCard card = cardObj.GetComponent<RelicCard>();
                if (card == null) card = cardObj.AddComponent<RelicCard>();

                card.Initialize(relic, OnRelicSelected);
                relicCards.Add(card);
            }

            if (loreText != null)
                loreText.text = relicSelectionLore;

            if (titleText != null)
                titleText.text = "Memory-Forged Relics";
        }

        private void OnRelicSelected(Relic relic)
        {
            if (relicManager != null)
            {
                relicManager.AddRelic(relic);
                OnRelicChosen?.Invoke(relic);
                Hide();
            }
        }

        private void OnSkipClicked()
        {
            OnRelicSkipped?.Invoke();
            Hide();
        }

        // Events
        public System.Action<Relic> OnRelicChosen;
        public System.Action OnRelicSkipped;
    }

    /// <summary>
    /// UI card for displaying a relic
    /// </summary>
    public class RelicCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image relicIcon;
        [SerializeField] private Text relicNameText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Text rarityText;
        [SerializeField] private Button selectButton;
        [SerializeField] private Image rarityBackground;

        [Header("Rarity Colors")]
        [SerializeField] private Color commonColor = Color.gray;
        [SerializeField] private Color uncommonColor = Color.green;
        [SerializeField] private Color rareColor = Color.blue;
        [SerializeField] private Color epicColor = Color.magenta;
        [SerializeField] private Color legendaryColor = Color.yellow;

        private Relic relic;
        private System.Action<Relic> onSelectCallback;

        public void Initialize(Relic relicData, System.Action<Relic> onSelect)
        {
            relic = relicData;
            onSelectCallback = onSelect;

            if (relicNameText != null)
                relicNameText.text = relicData.relicName;

            if (descriptionText != null)
                descriptionText.text = relicData.description;

            if (relicIcon != null && relicData.icon != null)
                relicIcon.sprite = relicData.icon;

            if (rarityText != null)
                rarityText.text = relicData.rarity.ToString();

            // Set rarity color
            Color rarityColor = GetRarityColor(relicData.rarity);
            if (rarityBackground != null)
                rarityBackground.color = rarityColor;

            if (selectButton != null)
                selectButton.onClick.AddListener(() => onSelectCallback?.Invoke(relic));
        }

        private Color GetRarityColor(Rarity rarity)
        {
            return rarity switch
            {
                Rarity.Common => commonColor,
                Rarity.Uncommon => uncommonColor,
                Rarity.Rare => rareColor,
                Rarity.Epic => epicColor,
                Rarity.Legendary => legendaryColor,
                _ => Color.white
            };
        }
    }
}

