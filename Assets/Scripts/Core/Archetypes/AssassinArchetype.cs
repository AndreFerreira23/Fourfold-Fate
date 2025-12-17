using FourfoldFate.Core;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Assassin archetype: "Opportunity" - Deals bonus damage to low-health enemies.
    /// </summary>
    public class AssassinArchetype : Archetype
    {
        public override ArchetypeType Type => ArchetypeType.Assassin;
        public override string DisplayName => "Opportunity";

        private const float OPPORTUNITY_THRESHOLD = 0.4f; // 40% health
        private const float OPPORTUNITY_DAMAGE_MULTIPLIER = 2.0f; // 100% bonus damage

        // Note: Opportunity damage is calculated in BattleManager when checking target health
        // This archetype's bonus is applied contextually during combat
    }
}

