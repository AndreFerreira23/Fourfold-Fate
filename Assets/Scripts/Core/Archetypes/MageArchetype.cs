using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Mage archetype - Each spell cast increases spell power.
    /// At max stacks, Mage risks Overload (self-stun or backlash).
    /// </summary>
    public class MageArchetype : Archetype
    {
        [Header("Mana Surge System")]
        [SerializeField] private int currentManaSurge = 0;
        [SerializeField] private int maxManaSurge = 10;
        [SerializeField] private int surgePerSpell = 1;
        
        [Header("Spell Power")]
        [SerializeField] private float spellPowerBonusPerSurge = 0.1f; // 10% per stack
        
        [Header("Overload System")]
        [SerializeField] private bool overloadEnabled = true;
        [SerializeField] private float overloadChance = 0.3f; // 30% chance at max surge
        [SerializeField] private float overloadStunDuration = 2f;
        [SerializeField] private float overloadBacklashDamage = 0.2f; // 20% max HP

        private Unit unit;
        private bool isOverloaded = false;
        private float overloadTimer = 0f;

        public int CurrentManaSurge => currentManaSurge;
        public int MaxManaSurge => maxManaSurge;
        public bool IsOverloaded => isOverloaded;

        public override void Initialize(Unit unit)
        {
            this.unit = unit;
            archetypeType = ArchetypeType.Mage;
            archetypeName = "Mage";
            description = "Each spell cast increases spell power. At max stacks, risks Overload.";
            currentManaSurge = 0;
        }

        public override void UpdateArchetype(Unit unit)
        {
            if (isOverloaded)
            {
                overloadTimer -= Time.deltaTime;
                if (overloadTimer <= 0f)
                {
                    isOverloaded = false;
                    OnOverloadEnded?.Invoke();
                }
            }
        }

        public override void OnCombatStart(Unit unit)
        {
            currentManaSurge = 0;
            isOverloaded = false;
        }

        public override void OnCombatEnd(Unit unit)
        {
            // Mana Surge resets between encounters
            currentManaSurge = 0;
            isOverloaded = false;
        }

        /// <summary>
        /// Called when the mage casts a spell
        /// </summary>
        public void OnSpellCast()
        {
            if (isOverloaded) return;

            currentManaSurge = Mathf.Min(currentManaSurge + surgePerSpell, maxManaSurge);
            OnManaSurgeChanged?.Invoke(currentManaSurge, maxManaSurge);

            // Check for overload at max surge
            if (overloadEnabled && currentManaSurge >= maxManaSurge)
            {
                CheckOverload();
            }
        }

        private void CheckOverload()
        {
            float roll = Random.Range(0f, 1f);
            if (roll < overloadChance)
            {
                TriggerOverload();
            }
        }

        private void TriggerOverload()
        {
            isOverloaded = true;
            overloadTimer = overloadStunDuration;
            
            // Deal backlash damage
            if (unit != null && unit.Data != null)
            {
                float damage = unit.Data.MaxHealth * overloadBacklashDamage;
                unit.TakeDamage(damage);
            }
            
            // Reset surge
            currentManaSurge = 0;
            OnManaSurgeChanged?.Invoke(currentManaSurge, maxManaSurge);
            OnOverloadTriggered?.Invoke(overloadStunDuration);
        }

        /// <summary>
        /// Get current spell power multiplier from mana surge
        /// </summary>
        public float GetSpellPowerMultiplier()
        {
            if (isOverloaded) return 0f; // Can't cast while overloaded
            return 1f + (currentManaSurge * spellPowerBonusPerSurge);
        }

        /// <summary>
        /// Check if the mage can cast spells (not overloaded)
        /// </summary>
        public bool CanCastSpell()
        {
            return !isOverloaded;
        }

        // Events
        public System.Action<int, int> OnManaSurgeChanged;
        public System.Action<float> OnOverloadTriggered;
        public System.Action OnOverloadEnded;
    }
}

