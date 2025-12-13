using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Fighter archetype - Gains Momentum with consecutive attacks.
    /// Momentum increases attack speed and damage.
    /// Momentum resets if the fighter misses or changes targets.
    /// </summary>
    public class FighterArchetype : Archetype
    {
        [Header("Momentum System")]
        [SerializeField] private int currentMomentum = 0;
        [SerializeField] private int maxMomentum = 10;
        [SerializeField] private int momentumPerHit = 1;
        
        [Header("Momentum Bonuses")]
        [SerializeField] private float attackSpeedBonusPerMomentum = 0.05f; // 5% per stack
        [SerializeField] private float damageBonusPerMomentum = 0.03f; // 3% per stack
        
        [Header("Momentum Loss")]
        [SerializeField] private bool resetOnMiss = true;
        [SerializeField] private bool resetOnTargetChange = true;

        private Unit unit;
        private Unit lastTarget;
        private float baseAttackSpeed;
        private float baseAttackDamage;

        public int CurrentMomentum => currentMomentum;
        public int MaxMomentum => maxMomentum;

        public override void Initialize(Unit unit)
        {
            this.unit = unit;
            archetypeType = ArchetypeType.Fighter;
            archetypeName = "Fighter";
            description = "Gains Momentum with consecutive attacks. Momentum increases attack speed and damage.";
            currentMomentum = 0;
            lastTarget = null;
        }

        public override void UpdateArchetype(Unit unit)
        {
            // Check if target changed
            if (resetOnTargetChange && unit.Target != lastTarget && lastTarget != null)
            {
                ResetMomentum();
            }
            lastTarget = unit.Target;
        }

        public override void OnCombatStart(Unit unit)
        {
            currentMomentum = 0;
            lastTarget = null;
            if (unit.Data != null)
            {
                baseAttackSpeed = unit.Data.AttackSpeed;
                baseAttackDamage = unit.Data.AttackDamage;
            }
        }

        public override void OnCombatEnd(Unit unit)
        {
            // Momentum resets between encounters
            currentMomentum = 0;
        }

        /// <summary>
        /// Called when the fighter successfully hits an enemy
        /// </summary>
        public void OnSuccessfulHit()
        {
            currentMomentum = Mathf.Min(currentMomentum + momentumPerHit, maxMomentum);
            OnMomentumChanged?.Invoke(currentMomentum, maxMomentum);
            UpdateStats();
        }

        /// <summary>
        /// Called when the fighter misses an attack
        /// </summary>
        public void OnMiss()
        {
            if (resetOnMiss)
            {
                ResetMomentum();
            }
        }

        /// <summary>
        /// Reset momentum to zero
        /// </summary>
        private void ResetMomentum()
        {
            currentMomentum = 0;
            OnMomentumChanged?.Invoke(currentMomentum, maxMomentum);
            UpdateStats();
        }

        private void UpdateStats()
        {
            // Apply momentum bonuses to unit stats
            // This would need to be integrated with the Unit class
            float attackSpeedMultiplier = 1f + (currentMomentum * attackSpeedBonusPerMomentum);
            float damageMultiplier = 1f + (currentMomentum * damageBonusPerMomentum);
            
            OnStatsUpdated?.Invoke(attackSpeedMultiplier, damageMultiplier);
        }

        /// <summary>
        /// Get current attack speed multiplier from momentum
        /// </summary>
        public float GetAttackSpeedMultiplier()
        {
            return 1f + (currentMomentum * attackSpeedBonusPerMomentum);
        }

        /// <summary>
        /// Get current damage multiplier from momentum
        /// </summary>
        public float GetDamageMultiplier()
        {
            return 1f + (currentMomentum * damageBonusPerMomentum);
        }

        // Events
        public System.Action<int, int> OnMomentumChanged;
        public System.Action<float, float> OnStatsUpdated;
    }
}

