using System.Collections;
using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Animation
{
    /// <summary>
    /// System for handling character attack animations.
    /// Manages visual feedback for attacks, abilities, and archetype-specific effects.
    /// </summary>
    public class AttackAnimationSystem : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float attackDuration = 0.5f;
        [SerializeField] private float damageNumberDuration = 1f;
        [SerializeField] private GameObject damageNumberPrefab;
        [SerializeField] private GameObject hitEffectPrefab;

        [Header("Archetype Effects")]
        [SerializeField] private GameObject guardEffectPrefab;
        [SerializeField] private GameObject momentumEffectPrefab;
        [SerializeField] private GameObject surgeEffectPrefab;
        [SerializeField] private GameObject opportunityEffectPrefab;

        [Header("Synergy Effects")]
        [SerializeField] private GameObject fireEffectPrefab;
        [SerializeField] private GameObject natureEffectPrefab;
        [SerializeField] private GameObject shadowEffectPrefab;
        [SerializeField] private GameObject holyEffectPrefab;
        [SerializeField] private GameObject arcaneEffectPrefab;
        [SerializeField] private GameObject steelEffectPrefab;
        [SerializeField] private GameObject stormEffectPrefab;

        /// <summary>
        /// Play attack animation for a unit
        /// </summary>
        public void PlayAttackAnimation(Unit attacker, Unit target, float damage)
        {
            if (attacker == null || target == null) return;

            StartCoroutine(AttackAnimationCoroutine(attacker, target, damage));
        }

        private IEnumerator AttackAnimationCoroutine(Unit attacker, Unit target, float damage)
        {
            // Move attacker toward target (or play attack animation)
            Vector3 startPos = attacker.transform.position;
            Vector3 targetPos = target.transform.position;
            Vector3 attackPos = Vector3.Lerp(startPos, targetPos, 0.3f);

            float elapsed = 0f;
            while (elapsed < attackDuration * 0.5f)
            {
                elapsed += Time.deltaTime;
                attacker.transform.position = Vector3.Lerp(startPos, attackPos, elapsed / (attackDuration * 0.5f));
                yield return null;
            }

            // Play hit effect
            PlayHitEffect(target.transform.position, attacker.Archetype);

            // Show damage number
            ShowDamageNumber(target.transform.position, damage);

            // Return to start position
            elapsed = 0f;
            while (elapsed < attackDuration * 0.5f)
            {
                elapsed += Time.deltaTime;
                attacker.transform.position = Vector3.Lerp(attackPos, startPos, elapsed / (attackDuration * 0.5f));
                yield return null;
            }

            attacker.transform.position = startPos;
        }

        /// <summary>
        /// Play ability animation
        /// </summary>
        public void PlayAbilityAnimation(Unit caster, Unit target, AbilityData ability)
        {
            if (caster == null || ability == null) return;

            StartCoroutine(AbilityAnimationCoroutine(caster, target, ability));
        }

        private IEnumerator AbilityAnimationCoroutine(Unit caster, Unit target, AbilityData ability)
        {
            // Play ability-specific animation based on type
            switch (ability.AbilityType)
            {
                case AbilityType.Damage:
                    PlayDamageAbilityEffect(caster, target);
                    break;
                case AbilityType.Heal:
                    PlayHealAbilityEffect(caster, target);
                    break;
                case AbilityType.Buff:
                    PlayBuffAbilityEffect(caster, target);
                    break;
                case AbilityType.Debuff:
                    PlayDebuffAbilityEffect(caster, target);
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }

        private void PlayHitEffect(Vector3 position, Archetype archetype)
        {
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, position, Quaternion.identity);
                Destroy(effect, 1f);
            }

            // Play archetype-specific effect
            if (archetype != null)
            {
                GameObject archetypeEffect = GetArchetypeEffect(archetype.Type);
                if (archetypeEffect != null)
                {
                    GameObject effect = Instantiate(archetypeEffect, position, Quaternion.identity);
                    Destroy(effect, 1f);
                }
            }
        }

        private void ShowDamageNumber(Vector3 position, float damage)
        {
            if (damageNumberPrefab != null)
            {
                GameObject numberObj = Instantiate(damageNumberPrefab, position, Quaternion.identity);
                TextMesh textMesh = numberObj.GetComponent<TextMesh>();
                if (textMesh != null)
                {
                    textMesh.text = $"-{damage:F0}";
                }
                Destroy(numberObj, damageNumberDuration);
            }
        }

        private GameObject GetArchetypeEffect(ArchetypeType type)
        {
            return type switch
            {
                ArchetypeType.Tank => guardEffectPrefab,
                ArchetypeType.Fighter => momentumEffectPrefab,
                ArchetypeType.Mage => surgeEffectPrefab,
                ArchetypeType.Assassin => opportunityEffectPrefab,
                _ => null
            };
        }

        private void PlayDamageAbilityEffect(Unit caster, Unit target)
        {
            // Play damage ability visual
            if (target != null)
            {
                PlayHitEffect(target.transform.position, caster.Archetype);
            }
        }

        private void PlayHealAbilityEffect(Unit caster, Unit target)
        {
            // Play heal visual (green particles, etc.)
            if (target != null)
            {
                // Instantiate heal effect at target position
            }
        }

        private void PlayBuffAbilityEffect(Unit caster, Unit target)
        {
            // Play buff visual (golden aura, etc.)
            if (target != null)
            {
                // Instantiate buff effect at target position
            }
        }

        private void PlayDebuffAbilityEffect(Unit caster, Unit target)
        {
            // Play debuff visual (dark particles, etc.)
            if (target != null)
            {
                // Instantiate debuff effect at target position
            }
        }

        /// <summary>
        /// Play synergy effect based on tag
        /// </summary>
        public void PlaySynergyEffect(SynergyTag tag, Vector3 position)
        {
            GameObject effectPrefab = GetSynergyEffectPrefab(tag);
            if (effectPrefab != null)
            {
                GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
                Destroy(effect, 2f);
            }
        }

        private GameObject GetSynergyEffectPrefab(SynergyTag tag)
        {
            return tag switch
            {
                SynergyTag.Fire => fireEffectPrefab,
                SynergyTag.Nature => natureEffectPrefab,
                SynergyTag.Shadow => shadowEffectPrefab,
                SynergyTag.Holy => holyEffectPrefab,
                SynergyTag.Arcane => arcaneEffectPrefab,
                SynergyTag.Steel => steelEffectPrefab,
                SynergyTag.Storm => stormEffectPrefab,
                _ => null
            };
        }
    }
}

