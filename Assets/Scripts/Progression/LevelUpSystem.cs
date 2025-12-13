using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Progression
{
    /// <summary>
    /// Manages level-up choices. Each level grants a choice between upgrade paths.
    /// </summary>
    public class LevelUpSystem : MonoBehaviour
    {
        [Header("Level-Up Paths")]
        // Note: Paths are generated dynamically, no need for serialized list
        
        [Header("Path Weights")]
        [SerializeField] private float offenseWeight = 1f;
        [SerializeField] private float defenseWeight = 1f;
        [SerializeField] private float utilityWeight = 1f;
        [SerializeField] private float chaosWeight = 0.1f; // Rare

        public enum LevelUpPathType
        {
            Offense,
            Defense,
            Utility,
            Chaos
        }

        /// <summary>
        /// Generate level-up choices for a unit
        /// </summary>
        public List<LevelUpChoice> GenerateChoices(Unit unit, int level)
        {
            var choices = new List<LevelUpChoice>();

            // Always offer 3 standard paths + rare chance for Chaos
            choices.Add(GenerateOffenseChoice(unit, level));
            choices.Add(GenerateDefenseChoice(unit, level));
            choices.Add(GenerateUtilityChoice(unit, level));

            // Rare chance for Chaos path
            if (Random.Range(0f, 1f) < chaosWeight)
            {
                choices.Add(GenerateChaosChoice(unit, level));
            }

            return choices;
        }

        private LevelUpChoice GenerateOffenseChoice(Unit unit, int level)
        {
            var choice = new LevelUpChoice
            {
                pathType = LevelUpPathType.Offense,
                title = "Offense",
                description = "Increase combat effectiveness",
                upgrades = new List<StatUpgrade>()
            };

            // Random offense upgrade
            int roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    choice.upgrades.Add(new StatUpgrade { statName = "AttackDamage", value = 2f + (level * 0.1f) });
                    choice.description = "+Damage";
                    break;
                case 1:
                    choice.upgrades.Add(new StatUpgrade { statName = "CritChance", value = 0.05f });
                    choice.description = "+Crit Chance";
                    break;
                case 2:
                    choice.upgrades.Add(new StatUpgrade { statName = "ArmorPenetration", value = 5f });
                    choice.description = "+Armor Penetration";
                    break;
            }

            return choice;
        }

        private LevelUpChoice GenerateDefenseChoice(Unit unit, int level)
        {
            var choice = new LevelUpChoice
            {
                pathType = LevelUpPathType.Defense,
                title = "Defense",
                description = "Increase survivability",
                upgrades = new List<StatUpgrade>()
            };

            // Random defense upgrade
            int roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    choice.upgrades.Add(new StatUpgrade { statName = "MaxHealth", value = 20f + (level * 2f) });
                    choice.description = "+Max HP";
                    break;
                case 1:
                    choice.upgrades.Add(new StatUpgrade { statName = "Block", value = 0.1f });
                    choice.description = "+Block Chance";
                    break;
                case 2:
                    choice.upgrades.Add(new StatUpgrade { statName = "DamageReduction", value = 0.05f });
                    choice.description = "+Damage Reduction";
                    break;
            }

            return choice;
        }

        private LevelUpChoice GenerateUtilityChoice(Unit unit, int level)
        {
            var choice = new LevelUpChoice
            {
                pathType = LevelUpPathType.Utility,
                title = "Utility",
                description = "Increase versatility",
                upgrades = new List<StatUpgrade>()
            };

            // Random utility upgrade
            int roll = Random.Range(0, 3);
            switch (roll)
            {
                case 0:
                    choice.upgrades.Add(new StatUpgrade { statName = "CooldownReduction", value = 0.1f });
                    choice.description = "+Cooldown Reduction";
                    break;
                case 1:
                    choice.upgrades.Add(new StatUpgrade { statName = "StatusDuration", value = 0.2f });
                    choice.description = "+Status Duration";
                    break;
                case 2:
                    choice.upgrades.Add(new StatUpgrade { statName = "ResourceGeneration", value = 0.15f });
                    choice.description = "+Resource Generation";
                    break;
            }

            return choice;
        }

        private LevelUpChoice GenerateChaosChoice(Unit unit, int level)
        {
            var choice = new LevelUpChoice
            {
                pathType = LevelUpPathType.Chaos,
                title = "Chaos",
                description = "Risk-reward upgrade",
                upgrades = new List<StatUpgrade>()
            };

            // Powerful but risky upgrade
            int roll = Random.Range(0, 2);
            switch (roll)
            {
                case 0:
                    choice.upgrades.Add(new StatUpgrade { statName = "AttackDamage", value = 10f + (level * 0.5f) });
                    choice.upgrades.Add(new StatUpgrade { statName = "MaxHealth", value = -(10f + level) }); // Negative!
                    choice.description = "+Massive Damage, -HP";
                    break;
                case 1:
                    choice.upgrades.Add(new StatUpgrade { statName = "CritChance", value = 0.2f });
                    choice.upgrades.Add(new StatUpgrade { statName = "CritDamage", value = 0.5f });
                    choice.upgrades.Add(new StatUpgrade { statName = "AttackSpeed", value = -0.2f }); // Negative!
                    choice.description = "+Crit Power, -Attack Speed";
                    break;
            }

            return choice;
        }

        /// <summary>
        /// Apply a level-up choice to a unit
        /// </summary>
        public void ApplyChoice(Unit unit, LevelUpChoice choice)
        {
            foreach (var upgrade in choice.upgrades)
            {
                ApplyUpgrade(unit, upgrade);
            }

            OnLevelUpApplied?.Invoke(unit, choice);
        }

        private void ApplyUpgrade(Unit unit, StatUpgrade upgrade)
        {
            var unitData = unit.Data;
            if (unitData == null) return;

            // Apply stat upgrades
            // Note: This would need to be integrated with Unit/UnitData to actually modify stats
            switch (upgrade.statName)
            {
                case "AttackDamage":
                    unitData.AttackDamage += upgrade.value;
                    break;
                case "MaxHealth":
                    unitData.MaxHealth += upgrade.value;
                    unit.Heal(upgrade.value); // Heal if positive
                    break;
                case "CritChance":
                    // Would need to add crit chance to UnitData
                    break;
                case "ArmorPenetration":
                    // Would need to add armor penetration to UnitData
                    break;
                // Add more stat types as needed
            }
        }

        // Events
        public System.Action<Unit, LevelUpChoice> OnLevelUpApplied;
    }

    [System.Serializable]
    public class LevelUpChoice
    {
        public LevelUpSystem.LevelUpPathType pathType;
        public string title;
        public string description;
        public List<StatUpgrade> upgrades = new List<StatUpgrade>();
    }

    [System.Serializable]
    public class StatUpgrade
    {
        public string statName;
        public float value;
    }
}

