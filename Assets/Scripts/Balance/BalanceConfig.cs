using UnityEngine;

namespace FourfoldFate.Balance
{
    /// <summary>
    /// Balance configuration from the Balance Agent.
    /// Contains baseline stats for each archetype and scaling factors.
    /// </summary>
    [System.Serializable]
    public class BalanceConfig
    {
        [Header("Scaling Configuration")]
        public float scalingFactor = 1.15f;
        public string damageFormula = "Flat Reduction (Damage - Armor)";
        public string attackSpeedMode = "Interval (Seconds per Attack)";

        [Header("Archetype Baselines")]
        public ArchetypeBaseline tankBaseline;
        public ArchetypeBaseline fighterBaseline;
        public ArchetypeBaseline assassinBaseline;
        public ArchetypeBaseline mageBaseline;

        public BalanceConfig()
        {
            InitializeBaselines();
        }

        private void InitializeBaselines()
        {
            // Tank baseline from balance agent
            tankBaseline = new ArchetypeBaseline
            {
                description = "High survivability, slow attacks. Vulnerable to burst, strong against attrition.",
                maxHealth = 250f,
                maxMana = 50f,
                attackDamage = 12f,
                attackSpeed = 1.5f,
                armor = 8f,
                magicResist = 5f,
                movementSpeed = 2.5f,
                attackRange = 1.2f,
                role = Core.UnitRole.Frontline
            };

            // Fighter baseline from balance agent
            fighterBaseline = new ArchetypeBaseline
            {
                description = "Balanced stats. Sustained damage output.",
                maxHealth = 180f,
                maxMana = 60f,
                attackDamage = 18f,
                attackSpeed = 1.1f,
                armor = 4f,
                magicResist = 3f,
                movementSpeed = 3.5f,
                attackRange = 1.5f,
                role = Core.UnitRole.Frontline
            };

            // Assassin baseline from balance agent
            assassinBaseline = new ArchetypeBaseline
            {
                description = "High burst damage, very fast attacks, fragile.",
                maxHealth = 120f,
                maxMana = 80f,
                attackDamage = 22f,
                attackSpeed = 0.7f,
                armor = 1f,
                magicResist = 2f,
                movementSpeed = 4.5f,
                attackRange = 1.2f,
                role = Core.UnitRole.Midline
            };

            // Mage baseline from balance agent
            mageBaseline = new ArchetypeBaseline
            {
                description = "Ranged, high mana pool, relies on abilities.",
                maxHealth = 100f,
                maxMana = 150f,
                attackDamage = 10f,
                attackSpeed = 1.3f,
                armor = 0f,
                magicResist = 10f,
                movementSpeed = 3.0f,
                attackRange = 4.5f,
                role = Core.UnitRole.Backline
            };
        }

        /// <summary>
        /// Get baseline stats for an archetype
        /// </summary>
        public ArchetypeBaseline GetBaseline(Core.Archetypes.ArchetypeType archetypeType)
        {
            return archetypeType switch
            {
                Core.Archetypes.ArchetypeType.Tank => tankBaseline,
                Core.Archetypes.ArchetypeType.Fighter => fighterBaseline,
                Core.Archetypes.ArchetypeType.Assassin => assassinBaseline,
                Core.Archetypes.ArchetypeType.Mage => mageBaseline,
                _ => fighterBaseline
            };
        }

        /// <summary>
        /// Calculate difficulty scaling for a level
        /// </summary>
        public float GetDifficultyMultiplier(int level)
        {
            return Mathf.Pow(scalingFactor, level - 1);
        }
    }

    [System.Serializable]
    public class ArchetypeBaseline
    {
        public string description;
        public float maxHealth;
        public float maxMana;
        public float attackDamage;
        public float attackSpeed;
        public float armor;
        public float magicResist;
        public float movementSpeed;
        public float attackRange;
        public Core.UnitRole role;
    }
}

