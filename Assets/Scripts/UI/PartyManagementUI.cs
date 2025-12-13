using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Roguelike;

namespace FourfoldFate.UI
{
    /// <summary>
    /// UI for managing party composition and viewing party members.
    /// </summary>
    public class PartyManagementUI : BaseUI
    {
        [Header("Party Slots")]
        [SerializeField] private List<PartySlotUI> partySlots = new List<PartySlotUI>();
        [SerializeField] private GameObject partySlotPrefab;
        [SerializeField] private Transform partySlotsContainer;

        [Header("Character Selection")]
        [SerializeField] private GameObject characterSelectionPanel;
        [SerializeField] private Transform characterListContainer;
        [SerializeField] private GameObject characterCardPrefab;
        [SerializeField] private Button closeButton;

        [Header("Party Info")]
        [SerializeField] private Text partySizeText;
        [SerializeField] private Text unlockInfoText;
        [SerializeField] private Transform synergyDisplayContainer;
        [SerializeField] private GameObject synergyBadgePrefab;

        [Header("Lore Text")]
        [SerializeField] private Text loreHeaderText;

        private PartyManager partyManager;
        private RunManager runManager;

        protected override void Awake()
        {
            base.Awake();
            partyManager = FindObjectOfType<PartyManager>();
            runManager = FindObjectOfType<RunManager>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetupUI();
            UpdatePartyDisplay();
        }

        private void SetupUI()
        {
            if (loreHeaderText != null)
            {
                int partySize = partyManager != null ? partyManager.CurrentPartySize : 0;
                loreHeaderText.text = GetPartyLoreText(partySize);
            }

            if (closeButton != null)
                closeButton.onClick.AddListener(() => Hide());

            SetupPartySlots();
        }

        private void SetupPartySlots()
        {
            if (partySlotsContainer == null || partySlotPrefab == null) return;

            // Clear existing slots
            foreach (var slot in partySlots)
            {
                if (slot != null) Destroy(slot.gameObject);
            }
            partySlots.Clear();

            // Create 4 slots
            for (int i = 0; i < 4; i++)
            {
                GameObject slotObj = Instantiate(partySlotPrefab, partySlotsContainer);
                PartySlotUI slot = slotObj.GetComponent<PartySlotUI>();
                if (slot == null) slot = slotObj.AddComponent<PartySlotUI>();

                bool isUnlocked = partyManager != null && partyManager.IsSlotUnlocked(i + 1);
                string unlockLevel = GetUnlockLevelLore(i + 1);
                slot.Initialize(i, isUnlocked, unlockLevel, OnSlotClicked);
                partySlots.Add(slot);
            }
        }

        private void UpdatePartyDisplay()
        {
            if (partyManager == null) return;

            // Update party size text
            if (partySizeText != null)
            {
                partySizeText.text = $"Circle: {partyManager.CurrentPartySize}/4";
            }

            // Update unlock info
            if (unlockInfoText != null)
            {
                int nextUnlock = partyManager.GetNextUnlockLevel();
                if (nextUnlock > 0)
                {
                    unlockInfoText.text = $"Next seat unlocks at Trial {nextUnlock}";
                }
                else
                {
                    unlockInfoText.text = "All seats unlocked";
                }
            }

            // Update synergy display
            UpdateSynergyDisplay();

            // Update slot displays
            for (int i = 0; i < partySlots.Count; i++)
            {
                if (i < partyManager.PartyMembers.Count)
                {
                    partySlots[i].SetUnit(partyManager.PartyMembers[i]);
                }
                else
                {
                    partySlots[i].ClearUnit();
                }
            }
        }

        private void UpdateSynergyDisplay()
        {
            if (synergyDisplayContainer == null || partyManager == null) return;

            // Clear existing badges
            foreach (Transform child in synergyDisplayContainer)
            {
                Destroy(child.gameObject);
            }

            // Get active synergies
            var synergies = partyManager.GetActiveSynergies();
            foreach (var kvp in synergies)
            {
                GameObject badgeObj = Instantiate(synergyBadgePrefab, synergyDisplayContainer);
                Text text = badgeObj.GetComponentInChildren<Text>();
                if (text != null)
                {
                    text.text = GetSynergyCourtName(kvp.Key);
                }
            }
        }

        private void OnSlotClicked(int slotIndex)
        {
            // Show character selection
            ShowCharacterSelection(slotIndex);
        }

        private void ShowCharacterSelection(int slotIndex)
        {
            if (characterSelectionPanel != null)
                characterSelectionPanel.SetActive(true);

            // Populate available characters
            // This would load from unlocked characters
        }

        private string GetPartyLoreText(int partySize)
        {
            return partySize switch
            {
                1 => "The First Seat (Will): you step in by choice.",
                2 => "The Second Seat (Need): the Trials admit someone you cannot replace easily.",
                3 => "The Third Seat (Debt): an ally arrives with a vow/price implied.",
                4 => "The Fourth Seat (Fate): the Circle closes; the Trials stop asking who you are and start asking what you deserve.",
                _ => "The Circle awaits."
            };
        }

        private string GetUnlockLevelLore(int slotIndex)
        {
            return slotIndex switch
            {
                1 => "Trial 1 - The First Seat (Will)",
                2 => "Trial 5 - The Second Seat (Need)",
                3 => "Trial 10 - The Third Seat (Debt)",
                4 => "Trial 15 - The Fourth Seat (Fate)",
                _ => ""
            };
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
    /// UI component for a single party slot
    /// </summary>
    public class PartySlotUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image slotBackground;
        [SerializeField] private Image unitIcon;
        [SerializeField] private Text unitNameText;
        [SerializeField] private Text unlockText;
        [SerializeField] private Button slotButton;
        [SerializeField] private GameObject lockedOverlay;

        private int slotIndex;
        private System.Action<int> onClickCallback;

        public void Initialize(int index, bool isUnlocked, string unlockLore, System.Action<int> onClick)
        {
            slotIndex = index;
            onClickCallback = onClick;

            if (unlockText != null)
                unlockText.text = unlockLore;

            if (lockedOverlay != null)
                lockedOverlay.SetActive(!isUnlocked);

            if (slotButton != null)
            {
                slotButton.interactable = isUnlocked;
                slotButton.onClick.AddListener(() => onClickCallback?.Invoke(slotIndex));
            }
        }

        public void SetUnit(Unit unit)
        {
            if (unit == null || unit.Data == null) return;

            if (unitIcon != null && unit.Data.icon != null)
            {
                unitIcon.sprite = unit.Data.icon;
                unitIcon.gameObject.SetActive(true);
            }

            if (unitNameText != null)
            {
                unitNameText.text = unit.Data.unitName;
            }
        }

        public void ClearUnit()
        {
            if (unitIcon != null)
                unitIcon.gameObject.SetActive(false);

            if (unitNameText != null)
                unitNameText.text = "Empty";
        }
    }
}

