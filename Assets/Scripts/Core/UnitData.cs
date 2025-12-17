using FourfoldFate.Core.Archetypes;
using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Configuration data for creating a unit. Used by GameDataManager.
    /// </summary>
    [System.Serializable]
    public class UnitDataConfig
    {
        public string unitName;
        public string description;
        public float maxHealth;
        public float maxMana;
        public float attackDamage;
        public float attackSpeed;
        public float armor;
        public float magicResist;
        public float movementSpeed;
        public float attackRange;
        public ArchetypeType archetypeType;
        public SynergyTag synergyTag1;
        public SynergyTag synergyTag2;
        public UnitType unitType;
        public UnitRole unitRole;
    }

    /// <summary>
    /// Runtime unit data structure.
    /// </summary>
    [System.Serializable]
    public class UnitData
    {
        public string unitId;
        public string unitName;
        public string description;
        public float maxHealth;
        public float maxMana;
        public float attackDamage;
        public float attackSpeed;
        public float armor;
        public float magicResist;
        public float movementSpeed;
        public float attackRange;
        public ArchetypeType archetypeType;
        public SynergyTag synergyTag1;
        public SynergyTag synergyTag2;
        public UnitType unitType;
        public UnitRole unitRole;
    }
}

