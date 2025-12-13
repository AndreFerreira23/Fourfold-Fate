using FourfoldFate.Core;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all abilities here. Easy to edit and add new abilities.
    /// All ability data is controlled in code.
    /// </summary>
    public static class AbilityDefinitions
    {
        /// <summary>
        /// Get ability configuration by ID. Add new abilities here.
        /// </summary>
        public static AbilityDataConfig GetAbilityConfig(string abilityId)
        {
            return abilityId switch
            {
                // TANK ABILITIES - Balanced from balance agent
                "shield_bash" => new AbilityDataConfig
                {
                    abilityName = "Shield Bash",
                    description = "Moderate physical damage. The Method of Keeping demands sacrifice.",
                    abilityType = AbilityType.Damage,
                    damage = 25f,  // Balanced: 25 damage
                    manaCost = 30f,  // Balanced: 30 mana
                    cooldown = 6f,  // Balanced: 6 seconds
                    range = 1.2f,
                    targetType = TargetType.Enemy
                },

                "taunt" => new AbilityDataConfig
                {
                    abilityName = "Taunt",
                    description = "Draws enemy attention. The Method of Keeping demands sacrifice.",
                    abilityType = AbilityType.Utility,
                    manaCost = 30f,
                    cooldown = 8f,
                    range = 5f,
                    targetType = TargetType.AllEnemies
                },

                "guard_wall" => new AbilityDataConfig
                {
                    abilityName = "Guard Wall",
                    description = "Spends Guard to heavily reduce incoming damage.",
                    abilityType = AbilityType.Buff,
                    manaCost = 0f,
                    cooldown = 12f,
                    range = 0f,
                    targetType = TargetType.Self
                },

                // FIGHTER ABILITIES
                "whirlwind" => new AbilityDataConfig
                {
                    abilityName = "Whirlwind Strike",
                    description = "A spinning attack that maintains Momentum.",
                    abilityType = AbilityType.Damage,
                    damage = 30f,
                    manaCost = 25f,
                    cooldown = 5f,
                    range = 2f,
                    targetType = TargetType.AllEnemies
                },

                "battle_cry" => new AbilityDataConfig
                {
                    abilityName = "Battle Cry",
                    description = "Buffs damage for all allies. The Method of Motion inspires the Circle.",
                    abilityType = AbilityType.Buff,
                    manaCost = 50f,  // Balanced: 50 mana
                    cooldown = 12f,  // Balanced: 12 seconds
                    duration = 5f,  // Balanced: 5 second duration
                    range = 0f,
                    targetType = TargetType.AllAllies
                },

                "momentum_rush" => new AbilityDataConfig
                {
                    abilityName = "Momentum Rush",
                    description = "Gains maximum Momentum instantly.",
                    abilityType = AbilityType.Buff,
                    manaCost = 30f,
                    cooldown = 15f,
                    range = 0f,
                    targetType = TargetType.Self
                },

                // MAGE ABILITIES - Balanced from balance agent
                "fireball" => new AbilityDataConfig
                {
                    abilityName = "Fireball",
                    description = "High burst damage, intended for Mages. Each cast increases Mana Surge.",
                    abilityType = AbilityType.Damage,
                    damage = 45f,  // Balanced: 45 damage
                    manaCost = 40f,  // Balanced: 40 mana
                    cooldown = 4f,  // Balanced: 4 seconds
                    range = 4.5f,
                    targetType = TargetType.Enemy
                },

                "arcane_bolt" => new AbilityDataConfig
                {
                    abilityName = "Arcane Bolt",
                    description = "A bolt of pure arcane energy. Risk Overload for greater power.",
                    abilityType = AbilityType.Damage,
                    damage = 50f,
                    manaCost = 40f,
                    cooldown = 4f,
                    range = 4.5f,
                    targetType = TargetType.Enemy
                },

                "heal" => new AbilityDataConfig
                {
                    abilityName = "Heal",
                    description = "Restores health to an ally.",
                    abilityType = AbilityType.Heal,
                    healAmount = 40f,
                    manaCost = 20f,
                    cooldown = 6f,
                    range = 4f,
                    targetType = TargetType.Ally
                },

                // ASSASSIN ABILITIES
                "backstab" => new AbilityDataConfig
                {
                    abilityName = "Backstab",
                    description = "Deals massive damage to low-health enemies. Opportunity strikes.",
                    abilityType = AbilityType.Damage,
                    damage = 40f,
                    manaCost = 20f,
                    cooldown = 6f,
                    range = 1.5f,
                    targetType = TargetType.Enemy
                },

                "poison_blade" => new AbilityDataConfig
                {
                    abilityName = "Poison Blade",
                    description = "Applies poison to target. Chains kills refresh cooldowns.",
                    abilityType = AbilityType.Debuff,
                    damage = 15f,
                    manaCost = 15f,
                    cooldown = 5f,
                    range = 1.2f,
                    targetType = TargetType.Enemy
                },

                _ => null
            };
        }
    }
}

