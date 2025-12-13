using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Tank archetype - Builds Guard stacks when hit.
    /// Can spend Guard to taunt enemies or heavily reduce damage.
    /// </summary>
    public class TankArchetype : Archetype
    {
        [Header("Guard System")]
        [SerializeField] private int currentGuard = 0;
        [SerializeField] private int maxGuard = 100;
        [SerializeField] private int guardPerHit = 5;
        [SerializeField] private float guardDamageReduction = 0.1f; // 10% per guard stack
        
        [Header("Guard Abilities")]
        [SerializeField] private int tauntGuardCost = 30;
        [SerializeField] private float tauntDuration = 3f;
        [SerializeField] private int damageReductionGuardCost = 20;
        [SerializeField] private float damageReductionDuration = 2f;
        [SerializeField] private float maxDamageReduction = 0.8f; // 80% max reduction

        private Unit unit;
        private bool isDamageReductionActive = false;
        private float damageReductionTimer = 0f;

        public int CurrentGuard => currentGuard;
        public int MaxGuard => maxGuard;

        public override void Initialize(Unit unit)
        {
            this.unit = unit;
            archetypeType = ArchetypeType.Tank;
            archetypeName = "Tank";
            description = "Builds Guard when hit. Spend Guard to taunt or reduce damage.";
            currentGuard = 0;
        }

        public override void UpdateArchetype(Unit unit)
        {
            if (isDamageReductionActive)
            {
                damageReductionTimer -= Time.deltaTime;
                if (damageReductionTimer <= 0f)
                {
                    isDamageReductionActive = false;
                }
            }
        }

        public override void OnCombatStart(Unit unit)
        {
            currentGuard = 0;
            isDamageReductionActive = false;
        }

        public override void OnCombatEnd(Unit unit)
        {
            // Guard persists between encounters in a run
        }

        /// <summary>
        /// Called when the tank takes damage. Builds guard.
        /// </summary>
        public void OnTakeDamage(float damage)
        {
            // Build guard when hit
            currentGuard = Mathf.Min(currentGuard + guardPerHit, maxGuard);
            OnGuardChanged?.Invoke(currentGuard, maxGuard);
        }

        /// <summary>
        /// Use guard to taunt all enemies
        /// </summary>
        public bool UseTaunt()
        {
            if (currentGuard >= tauntGuardCost)
            {
                currentGuard -= tauntGuardCost;
                OnGuardChanged?.Invoke(currentGuard, maxGuard);
                OnTauntUsed?.Invoke(tauntDuration);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Use guard to activate heavy damage reduction
        /// </summary>
        public bool ActivateDamageReduction()
        {
            if (currentGuard >= damageReductionGuardCost)
            {
                currentGuard -= damageReductionGuardCost;
                isDamageReductionActive = true;
                damageReductionTimer = damageReductionDuration;
                OnGuardChanged?.Invoke(currentGuard, maxGuard);
                OnDamageReductionActivated?.Invoke(damageReductionDuration);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get current damage reduction based on guard and active abilities
        /// </summary>
        public float GetDamageReduction()
        {
            float reduction = 0f;
            
            // Base reduction from guard stacks
            reduction += (currentGuard / (float)maxGuard) * guardDamageReduction;
            
            // Heavy reduction if active
            if (isDamageReductionActive)
            {
                reduction += maxDamageReduction;
            }
            
            return Mathf.Min(reduction, 0.95f); // Cap at 95%
        }

        // Events
        public System.Action<int, int> OnGuardChanged;
        public System.Action<float> OnTauntUsed;
        public System.Action<float> OnDamageReductionActivated;
    }
}

