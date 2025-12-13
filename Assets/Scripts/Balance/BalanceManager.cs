using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Balance
{
    /// <summary>
    /// Manages balance configuration and applies balanced stats.
    /// Integrates with the Balance Agent's recommendations.
    /// </summary>
    public class BalanceManager : MonoBehaviour
    {
        [Header("Balance Configuration")]
        [SerializeField] private BalanceConfig balanceConfig;

        private static BalanceManager instance;
        public static BalanceManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<BalanceManager>();
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                // Only use DontDestroyOnLoad if this is a root GameObject
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
                
                if (balanceConfig == null)
                    balanceConfig = new BalanceConfig();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Get the balance configuration
        /// </summary>
        public BalanceConfig GetBalanceConfig()
        {
            return balanceConfig;
        }

        /// <summary>
        /// Get baseline stats for an archetype
        /// </summary>
        public Balance.ArchetypeBaseline GetArchetypeBaseline(Core.Archetypes.ArchetypeType archetypeType)
        {
            return balanceConfig.GetBaseline(archetypeType);
        }

        /// <summary>
        /// Calculate difficulty multiplier for a level
        /// </summary>
        public float GetDifficultyMultiplier(int level)
        {
            return balanceConfig.GetDifficultyMultiplier(level);
        }

        /// <summary>
        /// Apply damage formula: Flat Reduction (Damage - Armor)
        /// </summary>
        public float CalculateDamage(float baseDamage, float armor)
        {
            return Mathf.Max(0f, baseDamage - armor);
        }

        /// <summary>
        /// Validate unit stats against balance baselines
        /// </summary>
        public bool ValidateUnitStats(UnitData unitData)
        {
            if (unitData == null) return false;

            var baseline = balanceConfig.GetBaseline(unitData.archetypeType);
            
            // Check if stats are within reasonable range of baseline
            // This is a simple validation - can be expanded
            bool healthValid = unitData.MaxHealth >= baseline.maxHealth * 0.8f && 
                              unitData.MaxHealth <= baseline.maxHealth * 1.5f;
            bool damageValid = unitData.AttackDamage >= baseline.attackDamage * 0.7f && 
                              unitData.AttackDamage <= baseline.attackDamage * 1.5f;
            
            return healthValid && damageValid;
        }
    }
}

