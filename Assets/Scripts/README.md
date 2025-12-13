# **Fourfold Fate** - 2D Roguelike Autobattler

## Project Overview

**Fourfold Fate** is a fantasy roguelike auto-battler where players build a party of up to four characters and progress through levels 1-100, unlocking characters, relics, and powerful synergies along the way.

## Core Systems

### 1. Archetype System

Each character belongs to one of four archetypes with unique mechanics:

#### **Tank** - Guard Meter
- Builds Guard stacks when hit
- Can spend Guard to taunt enemies or heavily reduce damage
- Located in: `Assets/Scripts/Core/Archetypes/TankArchetype.cs`

#### **Fighter** - Momentum
- Gains Momentum with consecutive attacks
- Momentum increases attack speed and damage
- Momentum resets if fighter misses or changes targets
- Located in: `Assets/Scripts/Core/Archetypes/FighterArchetype.cs`

#### **Mage** - Mana Surge / Overload
- Each spell cast increases spell power
- At max stacks, risks Overload (self-stun or backlash)
- Located in: `Assets/Scripts/Core/Archetypes/MageArchetype.cs`

#### **Assassin** - Opportunity
- Deals bonus damage to low-health or debuffed enemies
- Can chain kills to refresh cooldowns
- Located in: `Assets/Scripts/Core/Archetypes/AssassinArchetype.cs`

### 2. Synergy Tag System

Characters have two synergy tags. Party-wide bonuses are granted when tags are stacked:

- **Fire**: +20% burn duration (2+)
- **Nature**: Party heals 1% HP per turn (3+)
- **Shadow**: +10% crit damage (2+)
- **Holy**: 5% of damage converted to shields (2+)
- **Arcane**: 10% cooldown reduction (2+)
- **Steel**: +10 armor (2+)
- **Storm**: 15% chain attack chance (2+)

Located in: `Assets/Scripts/Core/SynergyTag.cs`

### 3. Party Management

Party size unlocks at specific levels:
- **Level 1**: Start with 1 character
- **Level 5**: Unlock 2nd character
- **Level 10**: Unlock 3rd character + first miniboss
- **Level 15**: Unlock 4th character (full party)

Located in: `Assets/Scripts/Party/PartyManager.cs`

### 4. Level-Up System

Each level grants a choice between upgrade paths:

- **Offense**: Increased damage, crit chance, armor penetration
- **Defense**: Max HP, block, damage reduction
- **Utility**: Cooldown reduction, status duration, resource generation
- **Chaos** (Rare): Risk-reward upgrades with powerful but unstable effects

Located in: `Assets/Scripts/Progression/LevelUpSystem.cs`

### 5. Relic System

Relics are run-defining items that significantly alter gameplay. Examples:
- *Arcane Battery* — Start combat with +20% mana
- *Tainted Dagger* — Assassin crits apply poison
- *Earth Totem* — +5% max HP per Nature ally
- *Blood Idol* — +25% damage, −50% healing
- *Storm Core* — Abilities have a chance to chain

Located in: `Assets/Scripts/Relics/`

### 6. Run Progression (Levels 1-100)

- **Levels 1-9**: Early encounters, party formation
- **Level 10**: First miniboss
- **Levels 20, 40, 60, 90**: Standard minibosses
- **Levels 30, 50, 80**: Major minibosses with unique mechanics
- **Level 100**: Final boss with multiple phases

Located in: `Assets/Scripts/Roguelike/RunManager.cs`

### 7. Meta-Progression

Between runs, players unlock:
- New characters
- New relics
- New synergy tags
- Difficulty modifiers
- Cosmetic upgrades

Meta-progression expands variety, not raw power.

Located in: `Assets/Scripts/MetaProgression/MetaProgressionManager.cs`

## Agent System

Three specialized agents for different aspects of development:

### **StoryAgent**
Handles story, lore, and thematic elements:
- Suggests themes
- Debates narrative choices
- Creates lore elements
- Validates story coherence

### **BalanceAgent**
Manages game balance and mechanics tuning:
- Analyzes balance
- Suggests stat adjustments
- Validates numerical design
- Calculates difficulty scaling

### **CodeAgent**
Handles code architecture and implementation:
- Suggests architecture patterns
- Generates code templates
- Reviews code quality
- Provides optimization suggestions

Located in: `Assets/Scripts/Agents/`

## Project Structure

```
Assets/Scripts/
├── Agents/              # Story, Balance, and Code agents
│   ├── IAgent.cs
│   ├── StoryAgent.cs
│   ├── BalanceAgent.cs
│   ├── CodeAgent.cs
│   └── AgentManager.cs
├── Core/                # Core game systems
│   ├── Unit.cs          # Base unit class
│   ├── UnitData.cs      # Unit configuration (ScriptableObject)
│   ├── Ability.cs       # Ability system
│   ├── AbilityData.cs   # Ability configuration
│   ├── BattleManager.cs # Autobattler combat
│   ├── Archetypes/      # Archetype implementations
│   │   ├── Archetype.cs
│   │   ├── TankArchetype.cs
│   │   ├── FighterArchetype.cs
│   │   ├── MageArchetype.cs
│   │   └── AssassinArchetype.cs
│   └── SynergyTag.cs    # Synergy system
├── Party/               # Party management
│   └── PartyManager.cs
├── Progression/         # Level-up and progression
│   └── LevelUpSystem.cs
├── Relics/              # Relic system
│   ├── Relic.cs
│   ├── RelicManager.cs
│   └── ExampleRelics.cs
├── Roguelike/           # Run management
│   ├── RunManager.cs
│   ├── RunData.cs
│   ├── EncounterManager.cs
│   ├── EncounterData.cs
│   ├── ProgressionManager.cs
│   └── RewardData.cs
├── MetaProgression/     # Meta-progression
│   └── MetaProgressionManager.cs
└── GameManager.cs       # Main coordinator
```

## Usage Examples

### Creating a Unit with Archetype

1. Create a `UnitData` ScriptableObject
2. Set the `archetypeType` field (Tank, Fighter, Mage, or Assassin)
3. Set `SynergyTag1` and `SynergyTag2`
4. Configure stats and abilities
5. The `Unit` class will automatically create the appropriate archetype component

### Starting a Run

```csharp
GameManager gameManager = FindObjectOfType<GameManager>();
RunManager runManager = FindObjectOfType<RunManager>();
PartyManager partyManager = FindObjectOfType<PartyManager>();

// Create starting unit
Unit startingUnit = // ... instantiate from UnitData
runManager.StartNewRun(startingUnit);
```

### Using Agents

```csharp
AgentManager agentManager = FindObjectOfType<AgentManager>();

// Request story suggestions
var request = new AgentRequest
{
    RequestType = "suggest_theme",
    Context = "Roguelike autobattler"
};
var response = agentManager.RequestAgent("story", request);
```

### Getting Synergy Bonuses

```csharp
PartyManager partyManager = FindObjectOfType<PartyManager>();
var synergies = partyManager.GetActiveSynergies();

foreach (var kvp in synergies)
{
    Debug.Log($"Active synergy: {kvp.Key} with bonuses");
}
```

## Next Steps

1. **Create Unit Prefabs**: Set up prefabs for each character archetype
2. **Create UnitData Assets**: Configure starting characters and enemies
3. **Create AbilityData Assets**: Set up abilities for each archetype
4. **Create Relic Assets**: Implement the relic effects
5. **Create EncounterData Assets**: Set up enemy encounters for each level range
6. **Implement UI**: Create UI for party management, level-up choices, relic selection
7. **Add Visuals**: Implement 2D pixel art sprites and animations
8. **Balance Testing**: Use BalanceAgent to tune numbers

## Technical Notes

- All core systems use events for decoupled communication
- ScriptableObjects are used extensively for data configuration
- The archetype system is component-based for flexibility
- Synergy bonuses are calculated dynamically based on party composition
- Meta-progression saves to PlayerPrefs (can be upgraded to JSON files)

## Design Philosophy

- **Party of Four**: Tactical decisions revolve around composing and positioning a 4-character team
- **Synergy-Driven Builds**: Characters share elemental and thematic tags that grant powerful bonuses
- **Roguelike Progression**: Each run is unique, with permanent meta-unlocks that expand possibilities
- **Simple, Readable Combat**: Auto-battler style combat with clear feedback and minimal micromanagement
