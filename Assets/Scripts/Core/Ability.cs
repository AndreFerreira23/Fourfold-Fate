using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Base ability class for unit abilities.
    /// Handles ability execution and cooldowns.
    /// </summary>
    public class Ability : MonoBehaviour
    {
        [Header("Ability Data")]
        [SerializeField] private AbilityData abilityData;
        
        private float cooldownTimer = 0f;
        private bool isOnCooldown = false;

        public AbilityData Data => abilityData;
        public bool IsOnCooldown => isOnCooldown;
        public float CooldownRemaining => cooldownTimer;

        public void Initialize(AbilityData data)
        {
            abilityData = data;
            cooldownTimer = 0f;
            isOnCooldown = false;
        }

        private void Update()
        {
            if (isOnCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    isOnCooldown = false;
                }
            }
        }

        public bool Use(Unit caster, Unit target)
        {
            if (isOnCooldown) return false;
            if (caster.CurrentMana < abilityData.ManaCost) return false;

            // Consume mana
            // Note: This would need to be implemented in Unit class
            // caster.ConsumeMana(abilityData.ManaCost);

            // Execute ability effect
            ExecuteAbility(caster, target);

            // Start cooldown
            cooldownTimer = abilityData.Cooldown;
            isOnCooldown = true;

            return true;
        }

        protected virtual void ExecuteAbility(Unit caster, Unit target)
        {
            switch (abilityData.AbilityType)
            {
                case AbilityType.Damage:
                    if (target != null)
                        target.TakeDamage(abilityData.Damage);
                    break;
                case AbilityType.Heal:
                    if (caster != null)
                        caster.Heal(abilityData.HealAmount);
                    break;
                case AbilityType.Buff:
                    // Apply buff logic here
                    break;
                case AbilityType.Debuff:
                    // Apply debuff logic here
                    break;
            }

            OnAbilityUsed?.Invoke(caster, target);
        }

        public System.Action<Unit, Unit> OnAbilityUsed;
    }
}

