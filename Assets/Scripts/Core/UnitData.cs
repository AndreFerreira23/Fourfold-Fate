using UnityEngine;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Core
{
    /// <summary>
    /// ScriptableObject containing unit stats and properties.
    /// Used for easy configuration and balance tweaking.
    /// </summary>
    [CreateAssetMenu(fileName = "New Unit", menuName = "Fourfold Fate/Unit Data")]
    public class UnitData : ScriptableObject
    {
        [Header("Basic Info")]
        public string unitName;
        public string description;
        public Sprite icon;
        
        [Header("Stats")]
        public float MaxHealth = 100f;
        public float MaxMana = 50f;
        public float AttackDamage = 10f;
        public float AttackSpeed = 1f; // Attacks per second
        public float Armor = 0f;
        public float MagicResist = 0f;
        public float MovementSpeed = 1f;
        
        [Header("Combat")]
        public float AttackRange = 1.5f;
        public UnitType unitType;
        public UnitRole unitRole;
        
        [Header("Archetype")]
        public ArchetypeType archetypeType;
        
        [Header("Synergy Tags")]
        public SynergyTag SynergyTag1 = SynergyTag.None;
        public SynergyTag SynergyTag2 = SynergyTag.None;
        
        [Header("Abilities")]
        public AbilityData[] abilities;
    }

    public enum UnitType
    {
        Warrior,
        Mage,
        Rogue,
        Tank,
        Support
    }

    public enum UnitRole
    {
        Frontline,
        Midline,
        Backline
    }
}

