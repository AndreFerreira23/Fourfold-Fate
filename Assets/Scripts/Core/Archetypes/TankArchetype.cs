using FourfoldFate.Core;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Tank archetype: "Guard" - Gains damage reduction when health is low.
    /// </summary>
    public class TankArchetype : Archetype
    {
        public override ArchetypeType Type => ArchetypeType.Tank;
        public override string DisplayName => "Guard";

        private const float LOW_HEALTH_THRESHOLD = 0.3f; // 30% health
        private const float DAMAGE_REDUCTION = 0.3f; // 30% damage reduction

        public override float OnTakeDamage(float damage, Unit unit)
        {
            if (unit.CurrentHealth / unit.MaxHealth <= LOW_HEALTH_THRESHOLD)
            {
                return damage * (1f - DAMAGE_REDUCTION);
            }
            return damage;
        }
    }
}

