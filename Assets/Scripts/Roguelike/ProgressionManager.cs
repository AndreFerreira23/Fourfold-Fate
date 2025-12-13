using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages progression, rewards, and meta-progression.
    /// Handles currency, unlocks, and permanent upgrades.
    /// </summary>
    public class ProgressionManager : MonoBehaviour
    {
        [Header("Currency")]
        [SerializeField] private int currentGold = 0;
        [SerializeField] private int metaCurrency = 0; // Permanent currency across runs
        
        [Header("Unlocks")]
        [SerializeField] private List<string> unlockedUnits = new List<string>();
        [SerializeField] private List<string> unlockedAbilities = new List<string>();
        
        private void Awake()
        {
            LoadProgression();
        }

        public void GrantRewards(int floor, int encounter)
        {
            // Calculate rewards based on floor and encounter
            int goldReward = CalculateGoldReward(floor, encounter);
            AddGold(goldReward);
            
            // Grant other rewards (units, abilities, etc.)
            OnRewardsGranted?.Invoke(goldReward);
        }

        private int CalculateGoldReward(int floor, int encounter)
        {
            // Base reward scales with floor
            int baseReward = 50;
            return baseReward + (floor * 10) + (encounter * 5);
        }

        public void AddGold(int amount)
        {
            currentGold += amount;
            OnGoldChanged?.Invoke(currentGold);
        }

        public bool SpendGold(int amount)
        {
            if (currentGold >= amount)
            {
                currentGold -= amount;
                OnGoldChanged?.Invoke(currentGold);
                return true;
            }
            return false;
        }

        public void AddMetaCurrency(int amount)
        {
            metaCurrency += amount;
            SaveProgression();
        }

        public void UnlockUnit(string unitId)
        {
            if (!unlockedUnits.Contains(unitId))
            {
                unlockedUnits.Add(unitId);
                SaveProgression();
            }
        }

        public void UnlockAbility(string abilityId)
        {
            if (!unlockedAbilities.Contains(abilityId))
            {
                unlockedAbilities.Add(abilityId);
                SaveProgression();
            }
        }

        private void LoadProgression()
        {
            // Load from PlayerPrefs or save file
            metaCurrency = PlayerPrefs.GetInt("MetaCurrency", 0);
            // Load other progression data
        }

        private void SaveProgression()
        {
            // Save to PlayerPrefs or save file
            PlayerPrefs.SetInt("MetaCurrency", metaCurrency);
            PlayerPrefs.Save();
        }

        // Events
        public System.Action<int> OnGoldChanged;
        public System.Action<int> OnRewardsGranted;
    }
}

