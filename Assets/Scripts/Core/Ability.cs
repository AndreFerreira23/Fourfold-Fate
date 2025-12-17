using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Base ability class for unit abilities.
    /// </summary>
    public class Ability : MonoBehaviour
    {
        [Header("Ability Data")]
        public string abilityId;
        public string abilityName;
        public string description;
        public AbilityType abilityType;
        public float manaCost;
        public float cooldown;
        public float damage;
        public float healAmount;
        public float range;

        private float lastUsedTime = 0f;

        /// <summary>
        /// Initialize ability from AbilityData.
        /// </summary>
        public void Initialize(AbilityData data)
        {
            abilityId = data.abilityId;
            abilityName = data.abilityName;
            description = data.description;
            abilityType = data.abilityType;
            manaCost = data.manaCost;
            cooldown = data.cooldown;
            damage = data.damage;
            healAmount = data.healAmount;
            range = data.range;
        }

        /// <summary>
        /// Check if ability is on cooldown.
        /// </summary>
        public bool IsOnCooldown()
        {
            return Time.time - lastUsedTime < cooldown;
        }

        /// <summary>
        /// Use the ability on a target.
        /// </summary>
        public virtual void Use(Unit caster, Unit target = null)
        {
            lastUsedTime = Time.time;

            switch (abilityType)
            {
                case AbilityType.Damage:
                    if (target != null)
                    {
                        target.TakeDamage(damage, caster);
                    }
                    break;

                case AbilityType.Heal:
                    if (target != null)
                    {
                        target.Heal(healAmount);
                    }
                    else
                    {
                        caster.Heal(healAmount);
                    }
                    break;

                case AbilityType.Buff:
                case AbilityType.Debuff:
                case AbilityType.Utility:
                    // Implement buff/debuff/utility effects
                    break;
            }
        }
    }
}

