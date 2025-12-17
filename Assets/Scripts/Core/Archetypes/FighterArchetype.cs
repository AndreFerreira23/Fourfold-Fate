using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Fighter archetype: "Momentum" - Gains attack speed and damage as combat progresses.
    /// </summary>
    public class FighterArchetype : Archetype
    {
        public override ArchetypeType Type => ArchetypeType.Fighter;
        public override string DisplayName => "Momentum";

        private const float MOMENTUM_PER_SECOND = 0.05f; // 5% per second
        private const float MAX_MOMENTUM = 0.5f; // 50% max bonus

        private float combatTime = 0f;

        public override void Update(Unit unit, float deltaTime)
        {
            combatTime += deltaTime;
            float momentum = Mathf.Min(combatTime * MOMENTUM_PER_SECOND, MAX_MOMENTUM);
            // Momentum affects attack speed and damage - applied in Unit class
        }

        public override float OnDealDamage(float damage, Unit unit)
        {
            float momentum = Mathf.Min(combatTime * MOMENTUM_PER_SECOND, MAX_MOMENTUM);
            return damage * (1f + momentum);
        }
    }
}

