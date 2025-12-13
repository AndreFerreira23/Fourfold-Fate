using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Assassin archetype - Deals bonus damage to low-health or debuffed enemies.
    /// Can chain kills to refresh cooldowns.
    /// </summary>
    public class AssassinArchetype : Archetype
    {
        [Header("Opportunity System")]
        [SerializeField] private float lowHealthThreshold = 0.3f; // 30% HP
        [SerializeField] private float opportunityDamageBonus = 0.5f; // 50% bonus damage
        [SerializeField] private float debuffedDamageBonus = 0.3f; // 30% bonus damage
        
        [Header("Kill Chain System")]
        [SerializeField] private bool killChainEnabled = true;
        [SerializeField] private float cooldownReductionPerKill = 0.5f; // 50% cooldown reduction
        [SerializeField] private float killChainWindow = 3f; // 3 seconds to chain kills
        [SerializeField] private int killChainCount = 0;
        
        private Unit unit;
        private float lastKillTime = 0f;

        public int KillChainCount => killChainCount;
        public float OpportunityDamageBonus => opportunityDamageBonus;

        public override void Initialize(Unit unit)
        {
            this.unit = unit;
            archetypeType = ArchetypeType.Assassin;
            archetypeName = "Assassin";
            description = "Deals bonus damage to low-health or debuffed enemies. Chains kills to refresh cooldowns.";
            killChainCount = 0;
        }

        public override void UpdateArchetype(Unit unit)
        {
            // Reset kill chain if window expired
            if (killChainEnabled && killChainCount > 0)
            {
                if (Time.time - lastKillTime > killChainWindow)
                {
                    ResetKillChain();
                }
            }
        }

        public override void OnCombatStart(Unit unit)
        {
            killChainCount = 0;
            lastKillTime = 0f;
        }

        public override void OnCombatEnd(Unit unit)
        {
            // Kill chain resets between encounters
            killChainCount = 0;
        }

        /// <summary>
        /// Calculate bonus damage based on target's state
        /// </summary>
        public float CalculateOpportunityDamage(Unit target, float baseDamage)
        {
            if (target == null) return baseDamage;

            float bonus = 0f;

            // Check if target is low health
            if (target.CurrentHealth / target.Data.MaxHealth <= lowHealthThreshold)
            {
                bonus += opportunityDamageBonus;
            }

            // Check if target has debuffs
            // This would need integration with a debuff system
            // if (target.HasDebuffs())
            // {
            //     bonus += debuffedDamageBonus;
            // }

            return baseDamage * (1f + bonus);
        }

        /// <summary>
        /// Called when the assassin kills an enemy
        /// </summary>
        public void OnKill()
        {
            if (!killChainEnabled) return;

            float timeSinceLastKill = Time.time - lastKillTime;
            
            if (timeSinceLastKill <= killChainWindow)
            {
                killChainCount++;
            }
            else
            {
                killChainCount = 1;
            }

            lastKillTime = Time.time;
            OnKillChainUpdated?.Invoke(killChainCount);

            // Apply cooldown reduction
            if (killChainCount > 0)
            {
                float reduction = cooldownReductionPerKill * killChainCount;
                OnCooldownReduction?.Invoke(reduction);
            }
        }

        private void ResetKillChain()
        {
            killChainCount = 0;
            OnKillChainUpdated?.Invoke(killChainCount);
        }

        /// <summary>
        /// Get current cooldown reduction from kill chain
        /// </summary>
        public float GetCooldownReduction()
        {
            if (killChainCount > 0)
            {
                return cooldownReductionPerKill * killChainCount;
            }
            return 0f;
        }

        // Events
        public System.Action<int> OnKillChainUpdated;
        public System.Action<float> OnCooldownReduction;
    }
}

