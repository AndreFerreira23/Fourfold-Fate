using UnityEngine;

namespace FourfoldFate.Core.Archetypes
{
    /// <summary>
    /// Base archetype class. Each archetype has unique mechanics.
    /// </summary>
    public abstract class Archetype : MonoBehaviour
    {
        [Header("Archetype Info")]
        [SerializeField] protected ArchetypeType archetypeType;
        [SerializeField] protected string archetypeName;
        [SerializeField] protected string description;

        public ArchetypeType Type => archetypeType;
        public string Name => archetypeName;
        public string Description => description;

        public abstract void Initialize(Unit unit);
        public abstract void UpdateArchetype(Unit unit);
        public abstract void OnCombatStart(Unit unit);
        public abstract void OnCombatEnd(Unit unit);
    }

    public enum ArchetypeType
    {
        Tank,
        Fighter,
        Mage,
        Assassin
    }
}

