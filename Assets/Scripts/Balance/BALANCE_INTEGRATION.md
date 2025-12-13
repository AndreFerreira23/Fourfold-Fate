# Balance Integration Guide

## Overview

All game balance is now controlled by the Balance Agent's recommendations. The balance configuration has been integrated into the codebase.

## Balance Configuration

### Scaling Factor
- **1.15**: Exponential scaling per level
- Formula: `Mathf.Pow(1.15, level - 1)`

### Damage Formula
- **Flat Reduction**: `Damage - Armor`
- Minimum damage: 0

### Attack Speed Mode
- **Interval**: Seconds per attack
- Lower = faster attacks

## Archetype Baselines

### Tank
- **MaxHealth**: 250
- **MaxMana**: 50
- **AttackDamage**: 12
- **AttackSpeed**: 1.5 (slow)
- **Armor**: 8
- **MagicResist**: 5
- **MovementSpeed**: 2.5
- **AttackRange**: 1.2
- **Role**: Frontline
- **Description**: High survivability, slow attacks. Vulnerable to burst, strong against attrition.

### Fighter
- **MaxHealth**: 180
- **MaxMana**: 60
- **AttackDamage**: 18
- **AttackSpeed**: 1.1 (balanced)
- **Armor**: 4
- **MagicResist**: 3
- **MovementSpeed**: 3.5
- **AttackRange**: 1.5
- **Role**: Frontline
- **Description**: Balanced stats. Sustained damage output.

### Assassin
- **MaxHealth**: 120
- **MaxMana**: 80
- **AttackDamage**: 22
- **AttackSpeed**: 0.7 (very fast)
- **Armor**: 1
- **MagicResist**: 2
- **MovementSpeed**: 4.5
- **AttackRange**: 1.2
- **Role**: Midline
- **Description**: High burst damage, very fast attacks, fragile.

### Mage
- **MaxHealth**: 100
- **MaxMana**: 150
- **AttackDamage**: 10
- **AttackSpeed**: 1.3
- **Armor**: 0
- **MagicResist**: 10
- **MovementSpeed**: 3.0
- **AttackRange**: 4.5 (ranged)
- **Role**: Backline
- **Description**: Ranged, high mana pool, relies on abilities.

## Balanced Units

All units in `UnitDefinitions.cs` now use balanced stats based on these baselines:

### Example Units (from Balance Agent)
- **Ironclad Guardian** (Tank): 280 HP, 14 damage, 1.6 attack speed, 10 armor
- **Shadowblade** (Assassin): 110 HP, 25 damage, 0.6 attack speed, 2 armor
- **Arcane Weaver** (Mage): 90 HP, 8 damage, 1.4 attack speed, 200 mana

## Balanced Abilities

### Shield Bash (Tank)
- **Damage**: 25
- **ManaCost**: 30
- **Cooldown**: 6 seconds
- **Type**: Damage

### Fireball (Mage)
- **Damage**: 45
- **ManaCost**: 40
- **Cooldown**: 4 seconds
- **Type**: Damage
- **Description**: High burst damage, intended for Mages

### Battle Cry (Fighter)
- **ManaCost**: 50
- **Cooldown**: 12 seconds
- **Duration**: 5 seconds
- **Type**: Buff
- **Target**: All Allies
- **Description**: Buffs damage for all allies

## Using Balance System

### Get Baseline Stats
```csharp
BalanceManager balanceManager = BalanceManager.Instance;
var baseline = balanceManager.GetArchetypeBaseline(ArchetypeType.Tank);
// Returns: Tank baseline stats
```

### Calculate Difficulty
```csharp
float multiplier = balanceManager.GetDifficultyMultiplier(level);
// Level 10 = 1.15^9 = ~3.52x
```

### Calculate Damage
```csharp
float finalDamage = balanceManager.CalculateDamage(baseDamage, armor);
// Uses: Flat Reduction formula
```

### Validate Unit Stats
```csharp
bool isValid = balanceManager.ValidateUnitStats(unitData);
// Checks if stats are within reasonable range of baseline
```

## Adding New Balanced Units

When adding new units, use the archetype baselines as reference:

```csharp
"my_new_tank" => new UnitDataConfig
{
    maxHealth = 250f,  // Start with baseline
    attackDamage = 12f,  // Start with baseline
    attackSpeed = 1.5f,  // Start with baseline
    armor = 8f,  // Start with baseline
    // Then adjust for variant (e.g., +30 HP for tankier variant)
    maxHealth = 280f,  // Variant: +30 HP
    attackDamage = 14f,  // Variant: +2 damage
    // ... rest of config
}
```

## Balance Principles

1. **Tanks**: High HP, slow attacks, high armor
2. **Fighters**: Balanced stats, sustained damage
3. **Assassins**: Low HP, fast attacks, high damage
4. **Mages**: Low HP, high mana, ranged, relies on abilities

All units should stay within 80-150% of their archetype baseline for balance.

