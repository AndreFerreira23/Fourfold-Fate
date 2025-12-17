using System.Collections.Generic;
using FourfoldFate.Core.Archetypes;
using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Base unit class for all combatants (player characters and enemies).
    /// </summary>
    public class Unit : MonoBehaviour
    {
        [Header("Unit Data")]
        public string unitId;
        public string unitName;
        public string description;

        [Header("Stats")]
        public float maxHealth;
        public float currentHealth;
        public float maxMana;
        public float currentMana;
        public float attackDamage;
        public float attackSpeed;
        public float armor;
        public float magicResist;
        public float movementSpeed;
        public float attackRange;

        [Header("Archetype & Tags")]
        public ArchetypeType archetypeType;
        public SynergyTag synergyTag1;
        public SynergyTag synergyTag2;
        public UnitType unitType;
        public UnitRole unitRole;

        [Header("Combat")]
        public List<Ability> abilities = new List<Ability>();
        private float lastAttackTime = 0f;

        // Properties
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        public float MaxMana => maxMana;
        public float CurrentMana => currentMana;

        // Archetype instance
        private Archetype archetype;

        /// <summary>
        /// Initialize unit from UnitData.
        /// </summary>
        public void Initialize(UnitData data)
        {
            unitId = data.unitId;
            unitName = data.unitName;
            description = data.description;
            maxHealth = data.maxHealth;
            currentHealth = maxHealth;
            maxMana = data.maxMana;
            currentMana = maxMana;
            attackDamage = data.attackDamage;
            attackSpeed = data.attackSpeed;
            armor = data.armor;
            magicResist = data.magicResist;
            movementSpeed = data.movementSpeed;
            attackRange = data.attackRange;
            archetypeType = data.archetypeType;
            synergyTag1 = data.synergyTag1;
            synergyTag2 = data.synergyTag2;
            unitType = data.unitType;
            unitRole = data.unitRole;

            // Initialize archetype
            archetype = CreateArchetype(archetypeType);
        }

        /// <summary>
        /// Create archetype instance based on type.
        /// </summary>
        private Archetype CreateArchetype(ArchetypeType type)
        {
            return type switch
            {
                ArchetypeType.Tank => new TankArchetype(),
                ArchetypeType.Fighter => new FighterArchetype(),
                ArchetypeType.Mage => new MageArchetype(),
                ArchetypeType.Assassin => new AssassinArchetype(),
                _ => null
            };
        }

        /// <summary>
        /// Attack a target unit.
        /// </summary>
        public void Attack(Unit target)
        {
            if (target == null || target.currentHealth <= 0) return;

            float timeSinceLastAttack = Time.time - lastAttackTime;
            float attackCooldown = 1f / attackSpeed;

            if (timeSinceLastAttack < attackCooldown) return;

            float damage = attackDamage;
            if (archetype != null)
            {
                damage = archetype.OnDealDamage(damage, this);
            }

            target.TakeDamage(damage, this);
            lastAttackTime = Time.time;
        }

        /// <summary>
        /// Take damage from an attacker.
        /// </summary>
        public void TakeDamage(float damage, Unit attacker)
        {
            if (currentHealth <= 0) return;

            // Apply armor/magic resist (simplified - assume physical damage)
            float finalDamage = damage * (1f - armor / (armor + 100f));

            if (archetype != null)
            {
                finalDamage = archetype.OnTakeDamage(finalDamage, this);
            }

            currentHealth = Mathf.Max(0f, currentHealth - finalDamage);
        }

        /// <summary>
        /// Heal the unit.
        /// </summary>
        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        }

        /// <summary>
        /// Use an ability.
        /// </summary>
        public bool UseAbility(Ability ability, Unit target = null)
        {
            if (ability == null) return false;
            if (currentMana < ability.manaCost) return false;
            if (ability.IsOnCooldown()) return false;

            currentMana -= ability.manaCost;
            ability.Use(this, target);
            return true;
        }

        /// <summary>
        /// Update archetype mechanics.
        /// </summary>
        private void Update()
        {
            if (archetype != null)
            {
                archetype.Update(this, Time.deltaTime);
            }
        }
    }
}

