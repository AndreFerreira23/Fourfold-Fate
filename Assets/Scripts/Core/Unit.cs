using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Base unit class for autobattler combatants.
    /// Represents a single entity in battle.
    /// </summary>
    public class Unit : MonoBehaviour
    {
        [Header("Unit Data")]
        [SerializeField] private UnitData unitData;
        
        [Header("Current Stats")]
        [SerializeField] private float currentHealth;
        [SerializeField] private float currentMana;
        
        [Header("Combat")]
        [SerializeField] private List<Ability> abilities = new List<Ability>();
        [SerializeField] private Unit target;
        
        [Header("Archetype")]
        [SerializeField] private Archetype archetype;
        
        private float attackTimer = 0f;
        private bool isAlive = true;

        public UnitData Data => unitData;
        public float CurrentHealth => currentHealth;
        public float CurrentMana => currentMana;
        public bool IsAlive => isAlive;
        public Unit Target => target;
        public Archetype Archetype => archetype;

        private void Awake()
        {
            if (unitData != null)
            {
                InitializeFromData();
            }
        }

        public void Initialize(UnitData data)
        {
            unitData = data;
            InitializeFromData();
            InitializeArchetype();
        }

        private void InitializeFromData()
        {
            currentHealth = unitData.MaxHealth;
            currentMana = unitData.MaxMana;
            isAlive = true;
            attackTimer = 0f;
        }

        private void InitializeArchetype()
        {
            if (unitData == null) return;

            // Remove existing archetype
            if (archetype != null)
            {
                Destroy(archetype);
            }

            // Create appropriate archetype based on unit data
            switch (unitData.archetypeType)
            {
                case ArchetypeType.Tank:
                    archetype = gameObject.AddComponent<TankArchetype>();
                    break;
                case ArchetypeType.Fighter:
                    archetype = gameObject.AddComponent<FighterArchetype>();
                    break;
                case ArchetypeType.Mage:
                    archetype = gameObject.AddComponent<MageArchetype>();
                    break;
                case ArchetypeType.Assassin:
                    archetype = gameObject.AddComponent<AssassinArchetype>();
                    break;
            }

            if (archetype != null)
            {
                archetype.Initialize(this);
            }
        }

        private void Update()
        {
            if (!isAlive) return;

            // Update archetype
            if (archetype != null)
            {
                archetype.UpdateArchetype(this);
            }

            attackTimer += Time.deltaTime;
            
            // Apply attack speed modifiers from archetype
            float attackSpeed = unitData.AttackSpeed;
            if (archetype is FighterArchetype fighter)
            {
                attackSpeed /= fighter.GetAttackSpeedMultiplier();
            }
            
            if (target != null && attackTimer >= attackSpeed)
            {
                PerformAttack();
                attackTimer = 0f;
            }
        }

        private void PerformAttack()
        {
            if (target == null || !target.IsAlive) return;

            float damage = unitData.AttackDamage;
            
            // Apply damage modifiers from archetype
            if (archetype is FighterArchetype fighter)
            {
                damage *= fighter.GetDamageMultiplier();
            }
            else if (archetype is AssassinArchetype assassin)
            {
                damage = assassin.CalculateOpportunityDamage(target, damage);
            }
            
            target.TakeDamage(damage);
            
            // Notify archetype of successful hit
            if (archetype is FighterArchetype fighterArchetype)
            {
                fighterArchetype.OnSuccessfulHit();
            }
        }

        public void TakeDamage(float damage)
        {
            // Apply damage reduction from archetype
            float damageReduction = 0f;
            if (archetype is TankArchetype tank)
            {
                damageReduction = tank.GetDamageReduction();
            }
            
            float reducedDamage = damage * (1f - damageReduction);
            float actualDamage = Mathf.Max(0, reducedDamage - unitData.Armor);
            currentHealth -= actualDamage;
            
            // Notify tank archetype of taking damage
            if (archetype is TankArchetype tankArchetype)
            {
                tankArchetype.OnTakeDamage(actualDamage);
            }
            
            if (currentHealth <= 0)
            {
                Die();
            }
            
            OnHealthChanged?.Invoke(currentHealth, unitData.MaxHealth);
        }

        public void Heal(float amount)
        {
            currentHealth = Mathf.Min(currentHealth + amount, unitData.MaxHealth);
            OnHealthChanged?.Invoke(currentHealth, unitData.MaxHealth);
        }

        public void SetTarget(Unit newTarget)
        {
            target = newTarget;
        }

        public void UseAbility(int abilityIndex)
        {
            if (abilityIndex < 0 || abilityIndex >= abilities.Count) return;
            if (abilities[abilityIndex] == null) return;
            
            abilities[abilityIndex].Use(this, target);
        }

        private void Die()
        {
            isAlive = false;
            
            // Notify assassin archetype of kill
            if (target != null && target.Archetype is AssassinArchetype assassin)
            {
                assassin.OnKill();
            }
            
            OnUnitDied?.Invoke(this);
        }

        // Events
        public System.Action<float, float> OnHealthChanged;
        public System.Action<Unit> OnUnitDied;
    }
}

