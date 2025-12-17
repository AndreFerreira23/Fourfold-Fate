using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;
using System.Collections.Generic;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all player units here. Easy to edit and add new characters.
    /// All unit data is controlled in code.
    /// </summary>
    public static class UnitDefinitions
    {
        /// <summary>
        /// Get all player unit configurations.
        /// </summary>
        public static List<UnitDataConfig> GetAllPlayerUnits()
        {
            return new List<UnitDataConfig>
            {
                // TANK - "The Warden"
                new UnitDataConfig
                {
                    unitName = "The Warden",
                    description = "A stalwart guardian who stands firm against the March's darkness.",
                    maxHealth = 300f,  // Higher than enemy tanks (player advantage)
                    maxMana = 50f,
                    attackDamage = 15f,
                    attackSpeed = 1.3f,  // Slightly faster than enemy tanks
                    armor = 10f,
                    magicResist = 6f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                // FIGHTER - "The Blade"
                new UnitDataConfig
                {
                    unitName = "The Blade",
                    description = "A relentless warrior who gains strength as battle rages on.",
                    maxHealth = 200f,  // Higher than enemy fighters
                    maxMana = 60f,
                    attackDamage = 22f,  // Higher than enemy fighters
                    attackSpeed = 1.0f,  // Faster than enemy fighters
                    armor = 5f,
                    magicResist = 3f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Fire,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                // MAGE - "The Seer"
                new UnitDataConfig
                {
                    unitName = "The Seer",
                    description = "A mystic who channels arcane power through careful mana management.",
                    maxHealth = 120f,  // Higher than enemy mages
                    maxMana = 200f,  // More mana than enemies
                    attackDamage = 12f,
                    attackSpeed = 1.2f,
                    armor = 0f,
                    magicResist = 12f,  // Higher magic resist
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Arcane,
                    synergyTag2 = SynergyTag.Storm,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                // ASSASSIN - "The Shadow"
                new UnitDataConfig
                {
                    unitName = "The Shadow",
                    description = "A deadly rogue who strikes when enemies are weakest.",
                    maxHealth = 140f,  // Higher than enemy assassins
                    maxMana = 80f,
                    attackDamage = 25f,  // Higher damage
                    attackSpeed = 0.6f,  // Faster attacks
                    armor = 2f,
                    magicResist = 3f,
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Nature,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                }
            };
        }
    }
}

