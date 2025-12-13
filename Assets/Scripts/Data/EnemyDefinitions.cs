using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Data
{
    /// <summary>
    /// Define all enemies here. Easy to edit and add new enemies.
    /// All enemy data is controlled in code.
    /// </summary>
    public static class EnemyDefinitions
    {
        /// <summary>
        /// Get enemy configuration by ID. Add new enemies here.
        /// </summary>
        public static UnitDataConfig GetEnemyConfig(string enemyId)
        {
            return enemyId switch
            {
                // COMMON ENEMIES (Level 1-30)
                "briar_cairn_footpad" => new UnitDataConfig
                {
                    unitName = "Briar-Cairn Footpad",
                    description = "Bandits who learned the March's first law: steel is honest, and shadows are cheaper.",
                    maxHealth = 180f,  // Fighter baseline
                    maxMana = 60f,
                    attackDamage = 18f,  // Fighter baseline
                    attackSpeed = 1.1f,  // Fighter baseline
                    armor = 4f,  // Fighter baseline
                    magicResist = 3f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                "ash_tithe_marauder" => new UnitDataConfig
                {
                    unitName = "Ash-Tithe Marauder",
                    description = "They collect not coin, but soot—proof they stood close to someone else's ruin.",
                    maxHealth = 180f,  // Fighter baseline
                    maxMana = 60f,
                    attackDamage = 18f,  // Fighter baseline
                    attackSpeed = 1.1f,  // Fighter baseline
                    armor = 4f,  // Fighter baseline
                    magicResist = 3f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Fire,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                "thornworn_bulwark" => new UnitDataConfig
                {
                    unitName = "Thornworn Bulwark",
                    description = "A suit of armor left beneath a hedge long enough for the hedge to decide it deserved a heart.",
                    maxHealth = 250f,  // Tank baseline
                    maxMana = 50f,
                    attackDamage = 12f,  // Tank baseline
                    attackSpeed = 1.5f,  // Tank baseline
                    armor = 8f,  // Tank baseline
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "chapel_husk" => new UnitDataConfig
                {
                    unitName = "Chapel Husk",
                    description = "When a chapel sinks, prayers don't drown. They ferment.",
                    maxHealth = 250f,  // Tank baseline
                    maxMana = 50f,
                    attackDamage = 12f,  // Tank baseline
                    attackSpeed = 1.5f,  // Tank baseline
                    armor = 8f,  // Tank baseline
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Holy,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "pollen_skulk" => new UnitDataConfig
                {
                    unitName = "Pollen-Skulk",
                    description = "It moves like a sneeze you can't stop—soft, sudden, and humiliating.",
                    maxHealth = 120f,  // Assassin baseline
                    maxMana = 80f,
                    attackDamage = 22f,  // Assassin baseline
                    attackSpeed = 0.7f,  // Assassin baseline
                    armor = 1f,  // Assassin baseline
                    magicResist = 2f,
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                "lantern_gnaw_wisp" => new UnitDataConfig
                {
                    unitName = "Lantern-Gnaw Wisp",
                    description = "A light that learned appetite.",
                    maxHealth = 100f,  // Mage baseline
                    maxMana = 150f,  // Mage baseline
                    attackDamage = 10f,  // Mage baseline
                    attackSpeed = 1.3f,  // Mage baseline
                    armor = 0f,  // Mage baseline
                    magicResist = 10f,  // Mage baseline
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Holy,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "hex_crow" => new UnitDataConfig
                {
                    unitName = "Hex-Crow",
                    description = "Some birds mimic voices. This one mimics misfortune.",
                    maxHealth = 100f,  // Mage baseline
                    maxMana = 150f,  // Mage baseline
                    attackDamage = 10f,  // Mage baseline
                    attackSpeed = 1.3f,  // Mage baseline
                    armor = 0f,  // Mage baseline
                    magicResist = 10f,  // Mage baseline
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "bog_needle_leech" => new UnitDataConfig
                {
                    unitName = "Bog-Needle Leech",
                    description = "The March breeds patience into hunger.",
                    maxHealth = 120f,  // Assassin baseline
                    maxMana = 80f,
                    attackDamage = 22f,  // Assassin baseline
                    attackSpeed = 0.7f,  // Assassin baseline
                    armor = 1f,  // Assassin baseline
                    magicResist = 2f,
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                "storm_split_slinker" => new UnitDataConfig
                {
                    unitName = "Storm-split Slinker",
                    description = "A knife that learned to travel on thunder.",
                    maxHealth = 120f,  // Assassin baseline
                    maxMana = 80f,
                    attackDamage = 22f,  // Assassin baseline
                    attackSpeed = 0.7f,  // Assassin baseline
                    armor = 1f,  // Assassin baseline
                    magicResist = 2f,
                    movementSpeed = 4.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Assassin,
                    synergyTag1 = SynergyTag.Storm,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Rogue,
                    unitRole = UnitRole.Midline
                },

                "rustbound_warder" => new UnitDataConfig
                {
                    unitName = "Rustbound Warder",
                    description = "Old vows cling to old metal. Even rust remembers.",
                    maxHealth = 250f,  // Tank baseline
                    maxMana = 50f,
                    attackDamage = 12f,  // Tank baseline
                    attackSpeed = 1.5f,  // Tank baseline
                    armor = 8f,  // Tank baseline
                    magicResist = 5f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                // UNCOMMON ENEMIES (Level 10-60)
                "cinder_scribe" => new UnitDataConfig
                {
                    unitName = "Cinder-Scribe",
                    description = "It writes in ash because ink was never dramatic enough.",
                    maxHealth = 130f,  // Uncommon Mage (1.3x)
                    maxMana = 180f,
                    attackDamage = 13f,  // Uncommon Mage
                    attackSpeed = 1.25f, // Slightly faster
                    armor = 1f,
                    magicResist = 12f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Fire,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "briar_matron" => new UnitDataConfig
                {
                    unitName = "Briar Matron",
                    description = "A midwife to thickets and funerals alike.",
                    maxHealth = 325f,  // Uncommon Tank (1.3x)
                    maxMana = 60f,
                    attackDamage = 16f,  // Uncommon Tank
                    attackSpeed = 1.45f, // Slightly faster
                    armor = 11f,  // Uncommon Tank
                    magicResist = 8f,
                    movementSpeed = 2.5f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "gloam_confessor" => new UnitDataConfig
                {
                    unitName = "Gloam Confessor",
                    description = "It forgives you out loud while quietly sharpening the consequence.",
                    maxHealth = 130f,  // Uncommon Mage (1.3x)
                    maxMana = 180f,
                    attackDamage = 13f,  // Uncommon Mage
                    attackSpeed = 1.25f, // Slightly faster
                    armor = 1f,
                    magicResist = 12f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "anvil_woken" => new UnitDataConfig
                {
                    unitName = "Anvil-Woken",
                    description = "A forge-spirit wearing a corpse like an apron.",
                    maxHealth = 240f,  // Uncommon Fighter (1.33x)
                    maxMana = 70f,
                    attackDamage = 24f,  // Uncommon Fighter
                    attackSpeed = 1.05f, // Slightly faster
                    armor = 6f,  // Uncommon Fighter
                    magicResist = 5f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Fire,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                "tempest_chorister" => new UnitDataConfig
                {
                    unitName = "Tempest Chorister",
                    description = "It sings the weather into agreement.",
                    maxHealth = 130f,  // Uncommon Mage (1.3x)
                    maxMana = 180f,
                    attackDamage = 13f,  // Uncommon Mage
                    attackSpeed = 1.25f, // Slightly faster
                    armor = 1f,
                    magicResist = 12f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Storm,
                    synergyTag2 = SynergyTag.Holy,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "oath_less_duellist" => new UnitDataConfig
                {
                    unitName = "Oath-Less Duellist",
                    description = "A blade with no banner—only a schedule of victories.",
                    maxHealth = 230f,  // Uncommon Fighter (1.3x)
                    maxMana = 70f,
                    attackDamage = 23f,  // Uncommon Fighter
                    attackSpeed = 1.0f,  // Uncommon Fighter (Faster)
                    armor = 5f,
                    magicResist = 4f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Steel,
                    synergyTag2 = SynergyTag.Storm,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                // ELITE ENEMIES (Level 20-90)
                "reliquary_breaker" => new UnitDataConfig
                {
                    unitName = "Reliquary Breaker",
                    description = "Some thieves steal gold. This one steals confidence.",
                    maxHealth = 300f,  // Elite Fighter (1.66x)
                    maxMana = 80f,
                    attackDamage = 30f,  // Elite Fighter
                    attackSpeed = 0.9f,  // Elite Fighter (Fast)
                    armor = 8f,
                    magicResist = 6f,
                    movementSpeed = 3.5f,
                    attackRange = 1.5f,
                    archetypeType = ArchetypeType.Fighter,
                    synergyTag1 = SynergyTag.Shadow,
                    synergyTag2 = SynergyTag.Steel,
                    unitType = UnitType.Warrior,
                    unitRole = UnitRole.Frontline
                },

                "sanctum_pyrebrand" => new UnitDataConfig
                {
                    unitName = "Sanctum Pyrebrand",
                    description = "A sermon delivered with smoke.",
                    maxHealth = 170f,  // Elite Mage (1.7x)
                    maxMana = 250f,
                    attackDamage = 18f,  // Elite Mage
                    attackSpeed = 1.1f,  // Elite Mage
                    armor = 2f,
                    magicResist = 15f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Holy,
                    synergyTag2 = SynergyTag.Fire,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                "root_chain_warden" => new UnitDataConfig
                {
                    unitName = "Root-Chain Warden",
                    description = "The March does not chase. It keeps.",
                    maxHealth = 425f,  // Elite Tank (1.7x)
                    maxMana = 80f,
                    attackDamage = 20f,  // Elite Tank
                    attackSpeed = 1.4f,  // Elite Tank
                    armor = 14f,
                    magicResist = 10f,
                    movementSpeed = 2.0f,
                    attackRange = 1.2f,
                    archetypeType = ArchetypeType.Tank,
                    synergyTag1 = SynergyTag.Nature,
                    synergyTag2 = SynergyTag.Arcane,
                    unitType = UnitType.Tank,
                    unitRole = UnitRole.Frontline
                },

                "mirror_hex_adept" => new UnitDataConfig
                {
                    unitName = "Mirror-Hex Adept",
                    description = "A reflection that hates being seen.",
                    maxHealth = 180f,  // Elite Mage (1.8x)
                    maxMana = 300f,
                    attackDamage = 18f,
                    attackSpeed = 1.1f,
                    armor = 2f,
                    magicResist = 18f,
                    movementSpeed = 3.0f,
                    attackRange = 4.5f,  // Ranged
                    archetypeType = ArchetypeType.Mage,
                    synergyTag1 = SynergyTag.Arcane,
                    synergyTag2 = SynergyTag.Shadow,
                    unitType = UnitType.Mage,
                    unitRole = UnitRole.Backline
                },

                _ => null
            };
        }
    }
}

