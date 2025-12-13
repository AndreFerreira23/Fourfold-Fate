using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Party
{
    /// <summary>
    /// Manages the player's party. Handles party size unlocks at levels 1, 5, 10, 15.
    /// </summary>
    public class PartyManager : MonoBehaviour
    {
        [Header("Party Configuration")]
        [SerializeField] private int maxPartySize = 4;
        [SerializeField] private List<Unit> partyMembers = new List<Unit>();
        
        [Header("Unlock Levels")]
        [SerializeField] private int[] unlockLevels = new int[] { 1, 5, 10, 15 };
        
        [Header("Current State")]
        [SerializeField] private int currentUnlockedSlots = 1;
        [SerializeField] private int currentLevel = 1;

        public int MaxPartySize => maxPartySize;
        public int CurrentPartySize => partyMembers.Count;
        public int CurrentUnlockedSlots => currentUnlockedSlots;
        public List<Unit> PartyMembers => new List<Unit>(partyMembers);

        /// <summary>
        /// Initialize party with starting character
        /// </summary>
        public void InitializeParty(Unit startingUnit)
        {
            partyMembers.Clear();
            currentUnlockedSlots = 1;
            currentLevel = 1;
            
            if (startingUnit != null)
            {
                AddPartyMember(startingUnit);
            }
        }

        /// <summary>
        /// Check and unlock party slots based on level
        /// </summary>
        public void UpdateUnlocks(int level)
        {
            currentLevel = level;
            
            for (int i = 0; i < unlockLevels.Length; i++)
            {
                if (level >= unlockLevels[i])
                {
                    int newUnlockedSlots = i + 1;
                    if (newUnlockedSlots > currentUnlockedSlots)
                    {
                        currentUnlockedSlots = newUnlockedSlots;
                        OnPartySlotUnlocked?.Invoke(newUnlockedSlots);
                    }
                }
            }
        }

        /// <summary>
        /// Add a unit to the party if there's space
        /// </summary>
        public bool AddPartyMember(Unit unit)
        {
            if (partyMembers.Count >= currentUnlockedSlots)
            {
                Debug.LogWarning($"Party is full. Unlock more slots by reaching level {GetNextUnlockLevel()}.");
                return false;
            }

            if (partyMembers.Contains(unit))
            {
                Debug.LogWarning("Unit is already in the party.");
                return false;
            }

            partyMembers.Add(unit);
            OnPartyMemberAdded?.Invoke(unit);
            UpdateSynergies();
            
            return true;
        }

        /// <summary>
        /// Remove a unit from the party
        /// </summary>
        public bool RemovePartyMember(Unit unit)
        {
            if (partyMembers.Remove(unit))
            {
                OnPartyMemberRemoved?.Invoke(unit);
                UpdateSynergies();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the level at which the next party slot unlocks
        /// </summary>
        public int GetNextUnlockLevel()
        {
            for (int i = 0; i < unlockLevels.Length; i++)
            {
                if (unlockLevels[i] > currentLevel)
                {
                    return unlockLevels[i];
                }
            }
            return -1; // All slots unlocked
        }

        /// <summary>
        /// Check if a party slot is unlocked at the current level
        /// </summary>
        public bool IsSlotUnlocked(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= unlockLevels.Length)
                return false;
            
            return currentLevel >= unlockLevels[slotIndex];
        }

        /// <summary>
        /// Update synergy bonuses for the party
        /// </summary>
        private void UpdateSynergies()
        {
            var synergies = SynergyManager.CalculateSynergies(partyMembers);
            OnSynergiesUpdated?.Invoke(synergies);
        }

        /// <summary>
        /// Get active synergy bonuses
        /// </summary>
        public Dictionary<SynergyTag, SynergyBonus> GetActiveSynergies()
        {
            return SynergyManager.CalculateSynergies(partyMembers);
        }

        // Events
        public System.Action<int> OnPartySlotUnlocked;
        public System.Action<Unit> OnPartyMemberAdded;
        public System.Action<Unit> OnPartyMemberRemoved;
        public System.Action<Dictionary<SynergyTag, SynergyBonus>> OnSynergiesUpdated;
    }
}

