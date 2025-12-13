using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all units here. Easy to edit and add new characters.
    /// All unit data is controlled in code.
    /// </summary>
    public static class UnitDefinitions
    {
        /// <summary>
        /// Get unit configuration by ID. Add new units here.
        /// </summary>
        public static UnitDataConfig GetUnitConfig(string unitId)
        {
            return unitId switch
            {
                // TANKS - Using balanced baseline stats
                "guardian_shield" => new UnitDataConfig
                {
                    unitName = "Guardian Shield",
                    description = "A steadfast protector bound to the Court of Dawn and Court of Anvil. The Method of Keeping.",
                    maxHealth = 280f,  // Balanced: Tank baseline 250 + variant
                    maxMana = 50f,
                    attackDamage = 14f,  // Balanced: Tank baseline 12 + variant
                    attackSpeed = 1.6f,  // Balanced: Tank baseline 1.5 + variant
                    armor = 10f,  // Balanced: Tank baseline 8 + variant
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Holy,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "earthwarden" => new UnitDataConfig
                {
                    unitName = "Earthwarden",
                    description = "Root and bone bound together—rewarding those who refuse to fall. The Method of Keeping.",
                    maxHealth = 250f,  // Balanced: Tank baseline
                    maxMana = 50f,
                    attackDamage = 12f,  // Balanced: Tank baseline
                    attackSpeed = 1.5f,  // Balanced: Tank baseline
                    armor = 8f,  // Balanced: Tank baseline
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                // FIGHTERS - Using balanced baseline stats
                "blade_dancer" => new UnitDataConfig
                {
                    unitName = "Blade Dancer",
                    description = "A relentless warrior of the Court of Tempest. The Method of Motion.",
                    maxHealth = 180f,  // Balanced: Fighter baseline
                    maxMana = 60f,
                    attackDamage = 18f,  // Balanced: Fighter baseline
                    attackSpeed = 1.1f,  // Balanced: Fighter baseline
                    armor = 4f,  // Balanced: Fighter baseline
                    magicResist = 3f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Storm,
                    synergyTag2 = SynergyTag.Fire,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                "flame_striker" => new UnitDataConfig
                {
                    unitName = "Flame Striker",
                    description = "Victory through continuity; commitment becomes inevitability. The Method of Motion.",
                    maxHealth = 180f,  // Balanced: Fighter baseline
                    maxMana = 60f,
                    attackDamage = 18f,  // Balanced: Fighter baseline
                    attackSpeed = 1.1f,  // Balanced: Fighter baseline
                    armor = 4f,  // Balanced: Fighter baseline
                    magicResist = 3f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Fire,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                // MAGES - Using balanced baseline stats
                "rune_scribe" => new UnitDataConfig
                {
                    unitName = "Rune Scribe",
                    description = "A scholar of the Court of Aether. The Method of Witness.",
                    maxHealth = 90f,  // Balanced: Mage baseline 100 - variant
                    maxMana = 200f,  // Balanced: Mage baseline 150 + variant
                    attackDamage = 8f,  // Balanced: Mage baseline 10 - variant
                    attackSpeed = 1.4f,  // Balanced: Mage baseline 1.3 + variant
                    armor = 0f,  // Balanced: Mage baseline
                    magicResist = 10f,  // Balanced: Mage baseline
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Arcane,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "stormcaller" => new UnitDataConfig
                {
                    unitName = "Stormcaller",
                    description = "Scribe of living law; power grows as proof until reality snaps back. The Method of Witness.",
                    maxHealth = 100f,  // Balanced: Mage baseline
                    maxMana = 150f,  // Balanced: Mage baseline
                    attackDamage = 10f,  // Balanced: Mage baseline
                    attackSpeed = 1.3f,  // Balanced: Mage baseline
                    armor = 0f,  // Balanced: Mage baseline
                    magicResist = 10f,  // Balanced: Mage baseline
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Storm,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                // ASSASSINS - Using balanced baseline stats
                "shadow_blade" => new UnitDataConfig
                {
                    unitName = "Shadow Blade",
                    description = "Practitioner of conclusions; cuts threads where they're already frayed. The Method of Ending.",
                    maxHealth = 110f,  // Balanced: Assassin baseline 120 - variant
                    maxMana = 80f,  // Balanced: Assassin baseline
                    attackDamage = 25f,  // Balanced: Assassin baseline 22 + variant
                    attackSpeed = 0.6f,  // Balanced: Assassin baseline 0.7 - variant (faster)
                    armor = 2f,  // Balanced: Assassin baseline 1 + variant
                    magicResist = 2f,  // Balanced: Assassin baseline
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Storm,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                "venom_weaver" => new UnitDataConfig
                {
                    unitName = "Venom Weaver",
                    description = "Mercy is finishing the fight before it becomes a tragedy. The Method of Ending.",
                    maxHealth = 120f,  // Balanced: Assassin baseline
                    maxMana = 80f,  // Balanced: Assassin baseline
                    attackDamage = 22f,  // Balanced: Assassin baseline
                    attackSpeed = 0.7f,  // Balanced: Assassin baseline
                    armor = 1f,  // Balanced: Assassin baseline
                    magicResist = 2f,  // Balanced: Assassin baseline
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Nature,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                // Balance Agent Example Units
                "ironclad_guardian" => new UnitDataConfig
                {
                    unitName = "Ironclad Guardian",
                    description = "A heavily armored protector. The Method of Keeping.",
                    maxHealth = 280f,  // Balanced: From balance agent example
                    maxMana = 50f,
                    attackDamage = 14f,  // Balanced: From balance agent example
                    attackSpeed = 1.6f,  // Balanced: From balance agent example
                    armor = 10f,  // Balanced: From balance agent example
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "shadowblade" => new UnitDataConfig
                {
                    unitName = "Shadowblade",
                    description = "A deadly assassin of the shadows. The Method of Ending.",
                    maxHealth = 110f,  // Balanced: From balance agent example
                    maxMana = 80f,
                    attackDamage = 25f,  // Balanced: From balance agent example
                    attackSpeed = 0.6f,  // Balanced: From balance agent example
                    armor = 2f,  // Balanced: From balance agent example
                    magicResist = 2f,
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                "arcane_weaver" => new UnitDataConfig
                {
                    unitName = "Arcane Weaver",
                    description = "A master of arcane magic. The Method of Witness.",
                    maxHealth = 90f,  // Balanced: From balance agent example
                    maxMana = 200f,  // Balanced: From balance agent example
                    attackDamage = 8f,  // Balanced: From balance agent example
                    attackSpeed = 1.4f,  // Balanced: From balance agent example
                    armor = 0f,
                    magicResist = 10f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Arcane,
                    synergyTag2 = SynergyTag.Storm,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                _ => null
            };
        }
    }
}

