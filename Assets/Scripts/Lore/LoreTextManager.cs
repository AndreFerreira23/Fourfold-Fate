using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Lore
{
    /// <summary>
    /// Central manager for all lore text. Ensures consistency with the narrative canon.
    /// </summary>
    public static class LoreTextManager
    {
        // High-level premise
        public const string GAME_PREMISE = "Fourfold Fate is a fantasy roguelike auto-battler where each run is an ascent through the Hundred Trials. You build a Circle of Four—a party whose archetypes, synergy tags, and relics weave together into a single, fate-tested story.";

        // Taglines
        public const string TAGLINE_A = "Four seats. One climb. A hundred chances to become worth the summit.";
        public const string TAGLINE_B = "The Trials do not ask if you will win—only what you will sacrifice to keep all four alive.";

        // Core setting nouns
        public const string HUNDRED_TRIALS = "The Hundred Trials";
        public const string HALL_OF_ECHOES = "The Hall of Echoes";
        public const string CIRCLE_OF_FOUR = "The Fatebound Circle";
        public const string MEMORY_FORGED_RELICS = "Memory-Forged Relics";

        // Run start text
        public const string RUN_START = "The Hall of Echoes is quiet until you arrive. A circle waits with four empty seats and one lit candle. You take the first seat. The Trials begin to write your name.";

        // Level 15 completion
        public const string CIRCLE_COMPLETE = "The fourth seat accepts you. The Circle closes. From here, fate stops asking who you are—and starts asking what you will pay to remain it.";

        // Party unlock milestones
        public static string GetPartyUnlockLore(int slotIndex)
        {
            return slotIndex switch
            {
                1 => "The First Seat (Will): you step in by choice.",
                2 => "The Second Seat (Need): the Trials admit someone you cannot replace easily.",
                3 => "The Third Seat (Debt): an ally arrives with a vow/price implied.",
                4 => "The Fourth Seat (Fate): the Circle closes; the Trials stop asking who you are and start asking what you deserve.",
                _ => "The Circle awaits."
            };
        }

        // Archetype lore names
        public static string GetArchetypeLoreName(ArchetypeType type)
        {
            return type switch
            {
                ArchetypeType.Tank => "The Method of Keeping",
                ArchetypeType.Fighter => "The Method of Motion",
                ArchetypeType.Mage => "The Method of Witness",
                ArchetypeType.Assassin => "The Method of Ending",
                _ => type.ToString()
            };
        }

        public static string GetArchetypeDescription(ArchetypeType type)
        {
            return type switch
            {
                ArchetypeType.Tank => "Oath-bearer who turns harm into stored promise. Builds Guard when hit; spends it to Taunt or reduce damage.",
                ArchetypeType.Fighter => "Victory through continuity; commitment becomes inevitability. Gains Momentum with consecutive attacks.",
                ArchetypeType.Mage => "Scribe of living law; power grows as proof until reality snaps back. Mana Surge increases spell power, risks Overload.",
                ArchetypeType.Assassin => "Practitioner of conclusions; cuts threads where they're already frayed. Deals bonus damage to weakened enemies.",
                _ => ""
            };
        }

        // Archetype barks
        public static string GetArchetypeBark(ArchetypeType type)
        {
            return type switch
            {
                ArchetypeType.Tank => "Stand behind me. If fate must bite, it will bite my name first.",
                ArchetypeType.Fighter => "Don't blink. The moment you rest, the story forgets you.",
                ArchetypeType.Mage => "Power is easy. Control is the miracle.",
                ArchetypeType.Assassin => "Mercy is finishing the fight before it becomes a tragedy.",
                _ => ""
            };
        }

        // Synergy Court names
        public static string GetSynergyCourtName(SynergyTag tag)
        {
            return tag switch
            {
                SynergyTag.Fire => "Court of Ember",
                SynergyTag.Nature => "Court of Verdance",
                SynergyTag.Shadow => "Court of Gloam",
                SynergyTag.Holy => "Court of Dawn",
                SynergyTag.Arcane => "Court of Aether",
                SynergyTag.Steel => "Court of Anvil",
                SynergyTag.Storm => "Court of Tempest",
                _ => tag.ToString()
            };
        }

        public static string GetSynergyCourtDescription(SynergyTag tag)
        {
            return tag switch
            {
                SynergyTag.Fire => "Wrath, courage, cleansing heat. Impatience and scorch risk.",
                SynergyTag.Nature => "Growth, healing, poison-as-truth. Hunger and inevitability.",
                SynergyTag.Shadow => "Secrets, debuffs, crit certainty. Erosion and paranoia.",
                SynergyTag.Holy => "Shields, cleansing, vows. Judgment and rigidity.",
                SynergyTag.Arcane => "Mana, time-bending, cooldown miracles. Overload temptation.",
                SynergyTag.Steel => "Armor, block, endurance. Heaviness and inflexibility.",
                SynergyTag.Storm => "Speed, chaining, tempo. Volatility and missteps.",
                _ => ""
            };
        }

        // Boss names
        public static string GetBossName(int level)
        {
            return level switch
            {
                10 => "First Knot",
                20 => "Tollgate",
                30 => "Myth-Eater",
                40 => "Tollgate",
                50 => "Myth-Eater",
                60 => "Tollgate",
                80 => "Myth-Eater",
                90 => "Tollgate",
                100 => "The Sundered Arbiter",
                _ => "Trial Keeper"
            };
        }

        public static string GetBossDescription(int level)
        {
            return level switch
            {
                10 => "A Trial-Keeper that certifies your Circle.",
                20 => "A floor where the Trials demand adaptation.",
                40 => "A floor where the Trials demand payment.",
                60 => "A floor where the Trials demand adaptation.",
                90 => "A floor where the Trials demand payment.",
                30 => "A boss that embodies a philosophy and punishes one-dimensional builds.",
                50 => "A boss that embodies a philosophy and punishes one-dimensional builds.",
                80 => "A boss that embodies a philosophy and punishes one-dimensional builds.",
                100 => "The final boss that speaks in shifting Court-voices across phases.",
                _ => "A guardian of the Trials."
            };
        }

        // Level-up path names
        public static string GetLevelUpPathName(Progression.LevelUpSystem.LevelUpPathType pathType)
        {
            return pathType switch
            {
                Progression.LevelUpSystem.LevelUpPathType.Offense => "Path of Wrath",
                Progression.LevelUpSystem.LevelUpPathType.Defense => "Path of Endurance",
                Progression.LevelUpSystem.LevelUpPathType.Utility => "Path of Versatility",
                Progression.LevelUpSystem.LevelUpPathType.Chaos => "Path of Risk",
                _ => pathType.ToString()
            };
        }

        // Relic flavor text examples (from lore doc)
        public static string GetRelicFlavorText(string relicName)
        {
            return relicName.ToLower() switch
            {
                "arcane battery" => "A caged thunderheart, eager to be spent before it learns restraint.",
                "tainted dagger" => "A blade that learned poison because it learned regret.",
                "earth totem" => "Root and bone bound together—rewarding those who refuse to fall.",
                "blood idol" => "It worships victory loudly enough to drown out mercy.",
                "storm core" => "A splinter of Tempest's crown; every strike it blesses tries to become two.",
                _ => "A Memory-Forged artifact that remembers old uses."
            };
        }

        // Biome description
        public const string BIOME_DESCRIPTION = "The Briar-Cairn March: a haunted borderland where old wars were buried without being forgiven. Thorn-thickets through helms, half-sunk chapels, ash like snow, candlelit fog.";

        // Status text
        public static string GetStatusText(int level)
        {
            if (level < 5)
                return "The First Seat holds. The Trials watch.";
            else if (level < 10)
                return "The Second Seat fills. Need becomes strength.";
            else if (level < 15)
                return "The Third Seat answers. Debt becomes bond.";
            else if (level < 100)
                return "The Circle is complete. The Trials test your worth.";
            else
                return "The final Trial awaits. The Sundered Arbiter speaks.";
        }
    }
}

