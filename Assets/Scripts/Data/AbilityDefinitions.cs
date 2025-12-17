using FourfoldFate.Core;
using System.Collections.Generic;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all abilities here. Easy to edit and add new abilities.
    /// All ability data is controlled in code.
    /// </summary>
    public static class AbilityDefinitions
    {
        /// <summary>
        /// Get all ability configurations.
        /// </summary>
        public static List<AbilityDataConfig> GetAllAbilities()
        {
            return new List<AbilityDataConfig>
            {
                // TANK ABILITIES
                new AbilityDataConfig
                {
                    abilityId = "shield_bash",
                    abilityName = "Shield Bash",
                    description = "Bash the enemy with your shield, dealing damage and reducing their attack speed.",
                    abilityType = AbilityType.Damage,
                    manaCost = 20f,
                    cooldown = 3f,
                    damage = 25f,
                    healAmount = 0f,
                    range = 1.5f
                },
                new AbilityDataConfig
                {
                    abilityId = "fortify",
                    abilityName = "Fortify",
                    description = "Increase your armor and magic resist for the duration of combat.",
                    abilityType = AbilityType.Buff,
                    manaCost = 30f,
                    cooldown = 10f,
                    damage = 0f,
                    healAmount = 0f,
                    range = 0f
                },
                new AbilityDataConfig
                {
                    abilityId = "taunt",
                    abilityName = "Taunt",
                    description = "Force enemies to target you, protecting your allies.",
                    abilityType = AbilityType.Utility,
                    manaCost = 15f,
                    cooldown = 5f,
                    damage = 0f,
                    healAmount = 0f,
                    range = 5f
                },

                // FIGHTER ABILITIES
                new AbilityDataConfig
                {
                    abilityId = "cleave",
                    abilityName = "Cleave",
                    description = "Strike multiple enemies in front of you.",
                    abilityType = AbilityType.Damage,
                    manaCost = 25f,
                    cooldown = 4f,
                    damage = 30f,
                    healAmount = 0f,
                    range = 2f
                },
                new AbilityDataConfig
                {
                    abilityId = "battle_cry",
                    abilityName = "Battle Cry",
                    description = "Rally your allies, increasing their attack damage.",
                    abilityType = AbilityType.Buff,
                    manaCost = 30f,
                    cooldown = 8f,
                    damage = 0f,
                    healAmount = 0f,
                    range = 10f
                },
                new AbilityDataConfig
                {
                    abilityId = "whirlwind",
                    abilityName = "Whirlwind",
                    description = "Spin and attack all nearby enemies.",
                    abilityType = AbilityType.Damage,
                    manaCost = 40f,
                    cooldown = 6f,
                    damage = 20f,
                    healAmount = 0f,
                    range = 2.5f
                },

                // MAGE ABILITIES
                new AbilityDataConfig
                {
                    abilityId = "fireball",
                    abilityName = "Fireball",
                    description = "Hurl a fireball at an enemy, dealing magic damage.",
                    abilityType = AbilityType.Damage,
                    manaCost = 30f,
                    cooldown = 2f,
                    damage = 40f,
                    healAmount = 0f,
                    range = 5f
                },
                new AbilityDataConfig
                {
                    abilityId = "arcane_bolt",
                    abilityName = "Arcane Bolt",
                    description = "Launch a quick bolt of arcane energy.",
                    abilityType = AbilityType.Damage,
                    manaCost = 15f,
                    cooldown = 1f,
                    damage = 20f,
                    healAmount = 0f,
                    range = 5f
                },
                new AbilityDataConfig
                {
                    abilityId = "mana_surge",
                    abilityName = "Mana Surge",
                    description = "Spend extra mana to deal massive damage, but risk overload.",
                    abilityType = AbilityType.Damage,
                    manaCost = 60f,
                    cooldown = 5f,
                    damage = 80f,
                    healAmount = 0f,
                    range = 5f
                },

                // ASSASSIN ABILITIES
                new AbilityDataConfig
                {
                    abilityId = "backstab",
                    abilityName = "Backstab",
                    description = "Strike from behind for critical damage.",
                    abilityType = AbilityType.Damage,
                    manaCost = 25f,
                    cooldown = 3f,
                    damage = 50f,  // High damage for opportunity
                    healAmount = 0f,
                    range = 1.5f
                },
                new AbilityDataConfig
                {
                    abilityId = "poison_strike",
                    abilityName = "Poison Strike",
                    description = "Apply a poison that deals damage over time.",
                    abilityType = AbilityType.Debuff,
                    manaCost = 20f,
                    cooldown = 4f,
                    damage = 10f,  // Initial damage + DoT
                    healAmount = 0f,
                    range = 1.5f
                },
                new AbilityDataConfig
                {
                    abilityId = "shadow_step",
                    abilityName = "Shadow Step",
                    description = "Teleport behind an enemy and strike.",
                    abilityType = AbilityType.Damage,
                    manaCost = 30f,
                    cooldown = 5f,
                    damage = 35f,
                    healAmount = 0f,
                    range = 8f
                }
            };
        }
    }
}

