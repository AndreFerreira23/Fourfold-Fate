using FourfoldFate.Core;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Mage archetype: "Mana Surge/Overload" - Can spend extra mana for bonus damage, but risks overload.
    /// </summary>
    public class MageArchetype : Archetype
    {
        public override ArchetypeType Type => ArchetypeType.Mage;
        public override string DisplayName => "Mana Surge";

        private const float OVERLOAD_THRESHOLD = 0.9f; // 90% mana spent
        private const float SURGE_DAMAGE_MULTIPLIER = 1.5f; // 50% bonus damage when surging

        public override float OnDealDamage(float damage, Unit unit)
        {
            float manaPercent = unit.CurrentMana / unit.MaxMana;
            if (manaPercent < OVERLOAD_THRESHOLD)
            {
                // Mana Surge: Bonus damage when mana is low
                return damage * SURGE_DAMAGE_MULTIPLIER;
            }
            return damage;
        }
    }
}

