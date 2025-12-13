using FourfoldFate.Relics;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all relics here. Easy to edit and add new relics.
    /// All relic data is controlled in code.
    /// </summary>
    public static class RelicDefinitions
    {
        /// <summary>
        /// Get relic configuration by ID. Add new relics here.
        /// </summary>
        public static RelicConfig GetRelicConfig(string relicId)
        {
            return relicId switch
            {
                "arcane_battery" => new RelicConfig
                {
                    relicName = "Arcane Battery",
                    description = "A caged thunderheart, eager to be spent before it learns restraint.",
                    rarity = Rarity.Uncommon
                },

                "tainted_dagger" => new RelicConfig
                {
                    relicName = "Tainted Dagger",
                    description = "A blade that learned poison because it learned regret.",
                    rarity = Rarity.Rare
                },

                "earth_totem" => new RelicConfig
                {
                    relicName = "Earth Totem",
                    description = "Root and bone bound together—rewarding those who refuse to fall.",
                    rarity = Rarity.Uncommon
                },

                "blood_idol" => new RelicConfig
                {
                    relicName = "Blood Idol",
                    description = "It worships victory loudly enough to drown out mercy.",
                    rarity = Rarity.Epic
                },

                "storm_core" => new RelicConfig
                {
                    relicName = "Storm Core",
                    description = "A splinter of Tempest's crown; every strike it blesses tries to become two.",
                    rarity = Rarity.Rare
                },

                "shadow_veil" => new RelicConfig
                {
                    relicName = "Shadow Veil",
                    description = "A cloak that remembers silence. Assassins find their mark more easily.",
                    rarity = Rarity.Uncommon
                },

                "holy_sigil" => new RelicConfig
                {
                    relicName = "Holy Sigil",
                    description = "A mark of the Court of Dawn. Shields form more readily.",
                    rarity = Rarity.Common
                },

                _ => null
            };
        }
    }
}

