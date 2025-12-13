using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Party;
using FourfoldFate.Progression;
using FourfoldFate.Relics;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages the current roguelike run.
    /// Handles level progression (1-100), encounters, and run state.
    /// </summary>
    public class RunManager : MonoBehaviour
    {
        [Header("Run State")]
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private bool isRunActive = false;
        
        [Header("Run Data")]
        [SerializeField] private RunData currentRunData;
        
        [Header("Managers")]
        private EncounterManager encounterManager;
        private ProgressionManager progressionManager;
        private PartyManager partyManager;
        private LevelUpSystem levelUpSystem;
        private RelicManager relicManager;

        public int CurrentLevel => currentLevel;
        public bool IsRunActive => isRunActive;

        private void Awake()
        {
            encounterManager = GetComponent<EncounterManager>();
            progressionManager = GetComponent<ProgressionManager>();
            partyManager = FindObjectOfType<PartyManager>();
            levelUpSystem = GetComponent<LevelUpSystem>();
            relicManager = FindObjectOfType<RelicManager>();
        }

        public void StartNewRun(Unit startingUnit)
        {
            currentLevel = 1;
            isRunActive = true;
            
            // Initialize party with starting unit
            if (partyManager != null && startingUnit != null)
            {
                partyManager.InitializeParty(startingUnit);
                partyManager.UpdateUnlocks(currentLevel);
            }
            
            OnRunStarted?.Invoke();
        }

        public void StartNextEncounter()
        {
            if (encounterManager != null)
            {
                bool isMiniboss = IsMinibossLevel(currentLevel);
                encounterManager.StartEncounter(currentLevel, isMiniboss);
            }
            
            OnEncounterStarted?.Invoke(currentLevel);
        }

        public void CompleteEncounter(bool victory)
        {
            if (victory)
            {
                // Level up
                currentLevel++;
                
                // Update party unlocks
                if (partyManager != null)
                {
                    partyManager.UpdateUnlocks(currentLevel);
                }
                
                // Handle victory rewards
                if (progressionManager != null)
                {
                    progressionManager.GrantRewards(currentLevel - 1, 0);
                }
                
                // Check if level-up choice should be shown
                if (levelUpSystem != null && partyManager != null)
                {
                    OnLevelUpAvailable?.Invoke(currentLevel);
                }
                
                // Check if run is complete (reached level 100)
                if (currentLevel > 100)
                {
                    EndRun(true);
                }
                else
                {
                    // Show reward selection screen
                    OnRewardsAvailable?.Invoke();
                }
            }
            else
            {
                // Run ended in defeat
                EndRun(false);
            }
        }

        /// <summary>
        /// Check if current level is a miniboss level
        /// </summary>
        public bool IsMinibossLevel(int level)
        {
            // Minibosses at: 10, 20, 30, 40, 50, 60, 80, 90
            // Major minibosses at: 30, 50, 80
            // Final boss at: 100
            return level == 10 || level == 20 || level == 30 || level == 40 || 
                   level == 50 || level == 60 || level == 80 || level == 90 || level == 100;
        }

        /// <summary>
        /// Check if current level is a major miniboss
        /// </summary>
        public bool IsMajorMinibossLevel(int level)
        {
            return level == 30 || level == 50 || level == 80;
        }

        /// <summary>
        /// Check if current level is the final boss
        /// </summary>
        public bool IsFinalBossLevel(int level)
        {
            return level == 100;
        }

        public void EndRun(bool victory)
        {
            isRunActive = false;
            
            if (victory)
            {
                // Handle victory rewards and meta-progression
            }
            
            OnRunEnded?.Invoke(victory);
        }

        // Events
        public System.Action OnRunStarted;
        public System.Action<int> OnEncounterStarted;
        public System.Action OnRewardsAvailable;
        public System.Action<int> OnLevelUpAvailable;
        public System.Action<bool> OnRunEnded;
    }
}

