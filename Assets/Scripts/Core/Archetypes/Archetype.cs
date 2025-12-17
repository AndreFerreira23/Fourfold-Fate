namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Base archetype class. Each archetype has unique mechanics.
    /// </summary>
    public abstract class Archetype
    {
        public abstract ArchetypeType Type { get; }
        public abstract string DisplayName { get; }

        /// <summary>
        /// Called when the unit takes damage. Archetypes can modify damage here.
        /// </summary>
        public virtual float OnTakeDamage(float damage, Unit unit)
        {
            return damage;
        }

        /// <summary>
        /// Called when the unit deals damage. Archetypes can modify damage here.
        /// </summary>
        public virtual float OnDealDamage(float damage, Unit unit)
        {
            return damage;
        }

        /// <summary>
        /// Called each frame during combat. Archetypes can update their mechanics here.
        /// </summary>
        public virtual void Update(Unit unit, float deltaTime)
        {
        }
    }
}

