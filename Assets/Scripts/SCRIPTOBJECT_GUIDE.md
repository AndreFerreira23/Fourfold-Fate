# ScriptableObject Configuration Guide

## What are ScriptableObjects?

**ScriptableObjects** are Unity assets that store data separately from GameObjects. Think of them as data files that you can create, edit, and reuse in Unity's Inspector without writing code.

### Why Use ScriptableObjects?

1. **Easy Content Creation**: Designers can create game content (characters, abilities, relics) without coding
2. **Reusable Data**: One ScriptableObject can be used by multiple GameObjects
3. **Inspector Editing**: Edit all properties in Unity's Inspector with a nice UI
4. **No Code Changes**: Add new content without modifying scripts
5. **Performance**: Data is stored once and referenced, not duplicated

## How ScriptableObjects Work in Fourfold Fate

In this project, we use ScriptableObjects for:
- **UnitData**: Character stats, archetypes, synergy tags
- **AbilityData**: Abilities and their effects
- **Relic**: Relic properties and effects
- **EncounterData**: Enemy encounters
- **RunData**: Run configuration

## Creating ScriptableObjects in Unity

### Step 1: Create the Asset

1. In Unity, right-click in the **Project** window
2. Go to **Create** → **Fourfold Fate** → **[Type Name]**
   - For example: **Create** → **Fourfold Fate** → **Unit Data**
3. A new asset file appears in your project
4. Name it (e.g., "Warrior_Tank_FireSteel")

### Step 2: Configure in Inspector

1. Select the ScriptableObject asset
2. The **Inspector** shows all the fields from the script
3. Fill in the values:
   - Name, description, icon
   - Stats (health, damage, etc.)
   - Archetype type
   - Synergy tags
   - Abilities

### Step 3: Use in Game

The ScriptableObject can be:
- Assigned to a Unit component
- Referenced in code
- Used by multiple units

## Example Configurations

### Example 1: Creating a Tank Character

**UnitData ScriptableObject: "Guardian_Shield"**

```
Basic Info:
  - Unit Name: "Guardian Shield"
  - Description: "A steadfast protector bound to the Court of Dawn"
  - Icon: [Assign sprite]

Stats:
  - Max Health: 150
  - Max Mana: 30
  - Attack Damage: 8
  - Attack Speed: 0.8
  - Armor: 15
  - Magic Resist: 10
  - Movement Speed: 1.0

Combat:
  - Attack Range: 1.5
  - Unit Type: Tank
  - Unit Role: Frontline

Archetype:
  - Archetype Type: Tank

Synergy Tags:
  - Synergy Tag 1: Holy
  - Synergy Tag 2: Steel

Abilities:
  - [Array of AbilityData references]
```

### Example 2: Creating a Fighter Character

**UnitData ScriptableObject: "Blade_Dancer"**

```
Basic Info:
  - Unit Name: "Blade Dancer"
  - Description: "A relentless warrior of the Court of Tempest"
  - Icon: [Assign sprite]

Stats:
  - Max Health: 100
  - Max Mana: 50
  - Attack Damage: 15
  - Attack Speed: 1.2
  - Armor: 5
  - Magic Resist: 5
  - Movement Speed: 1.5

Combat:
  - Attack Range: 1.2
  - Unit Type: Warrior
  - Unit Role: Frontline

Archetype:
  - Archetype Type: Fighter

Synergy Tags:
  - Synergy Tag 1: Storm
  - Synergy Tag 2: Fire

Abilities:
  - [Array of AbilityData references]
```

### Example 3: Creating a Mage Character

**UnitData ScriptableObject: "Rune_Scribe"**

```
Basic Info:
  - Unit Name: "Rune Scribe"
  - Description: "A scholar of the Court of Aether"
  - Icon: [Assign sprite]

Stats:
  - Max Health: 80
  - Max Mana: 100
  - Attack Damage: 12
  - Attack Speed: 1.0
  - Armor: 2
  - Magic Resist: 15
  - Movement Speed: 1.0

Combat:
  - Attack Range: 4.0
  - Unit Type: Mage
  - Unit Role: Backline

Archetype:
  - Archetype Type: Mage

Synergy Tags:
  - Synergy Tag 1: Arcane
  - Synergy Tag 2: Shadow

Abilities:
  - [Array of AbilityData references]
```

### Example 4: Creating an Ability

**AbilityData ScriptableObject: "Fireball"**

```
Basic Info:
  - Ability Name: "Fireball"
  - Description: "Hurls a ball of flame at enemies"
  - Icon: [Assign sprite]

Ability Type:
  - Ability Type: Damage

Costs:
  - Mana Cost: 25
  - Cooldown: 3.0

Effects:
  - Damage: 50
  - Heal Amount: 0
  - Duration: 0

Targeting:
  - Target Type: Enemy
  - Range: 5.0
```

### Example 5: Creating a Relic

**Relic ScriptableObject: "Arcane_Battery"**

```
Relic Info:
  - Relic Name: "Arcane Battery"
  - Description: "A caged thunderheart, eager to be spent before it learns restraint."
  - Icon: [Assign sprite]
  - Rarity: Uncommon

Relic Effects:
  - [Array of RelicEffect configurations]
```

## Step-by-Step: Creating Your First Unit

### 1. Create the UnitData Asset

1. In Unity Project window, navigate to `Assets/Data/Units/` (create folder if needed)
2. Right-click → **Create** → **Fourfold Fate** → **Unit Data**
3. Name it: `Tank_HolySteel`

### 2. Configure the Unit

1. Select the `Tank_HolySteel` asset
2. In Inspector, fill in:

```
Unit Name: "Guardian Shield"
Description: "A steadfast protector bound to the Court of Dawn and Court of Anvil"

Max Health: 150
Max Mana: 30
Attack Damage: 8
Attack Speed: 0.8
Armor: 15
Magic Resist: 10

Archetype Type: Tank
Synergy Tag 1: Holy
Synergy Tag 2: Steel
```

### 3. Create a Prefab

1. Create an empty GameObject in scene
2. Add `Unit` component
3. In Inspector, drag `Tank_HolySteel` into the "Unit Data" field
4. The Unit will automatically:
   - Initialize with the data
   - Create the Tank archetype component
   - Set up stats

### 4. Save as Prefab

1. Drag the GameObject from scene to Project window
2. Now you have a reusable unit prefab!

## Organizing Your Assets

Recommended folder structure:

```
Assets/
├── Data/
│   ├── Units/
│   │   ├── Tanks/
│   │   ├── Fighters/
│   │   ├── Mages/
│   │   └── Assassins/
│   ├── Abilities/
│   │   ├── Damage/
│   │   ├── Heal/
│   │   └── Utility/
│   ├── Relics/
│   │   ├── Common/
│   │   ├── Uncommon/
│   │   └── Rare/
│   └── Encounters/
│       ├── Level_1_10/
│       ├── Level_11_30/
│       └── Bosses/
└── Prefabs/
    ├── Units/
    ├── Enemies/
    └── Effects/
```

## Tips for Configuration

### 1. Use Descriptive Names
- Good: `Tank_HolySteel_Guardian`
- Bad: `Unit1`, `Tank1`

### 2. Fill Descriptions
- Use lore text from `LoreTextManager`
- Include synergy information
- Mention archetype mechanics

### 3. Balance Stats
- Use `BalanceAgent` to validate numbers
- Test different combinations
- Consider synergy bonuses

### 4. Create Variants
- Copy existing ScriptableObjects
- Modify stats for different tiers
- Create level-scaled versions

## Example: Complete Character Setup

### Step 1: Create UnitData
```
Name: "Blade Dancer"
Archetype: Fighter
Tags: Storm, Fire
Stats: [configured]
```

### Step 2: Create Abilities
```
Ability 1: "Whirlwind Strike"
- Type: Damage
- Damage: 30
- Cooldown: 5s

Ability 2: "Momentum Rush"
- Type: Buff
- Effect: Increases Momentum gain
```

### Step 3: Assign to UnitData
- Drag abilities into UnitData's Abilities array

### Step 4: Create Prefab
- GameObject with Unit component
- Assign UnitData
- Save as prefab

### Step 5: Use in Game
- Instantiate prefab
- Add to party via PartyManager

## Common Patterns

### Pattern 1: Base Stats + Variants
```
Base_Tank (ScriptableObject)
  ↓
Variant_Tank_Level1 (copy, modify stats)
Variant_Tank_Level10 (copy, modify stats)
Variant_Tank_Level50 (copy, modify stats)
```

### Pattern 2: Archetype Templates
```
Template_Tank (base stats for all tanks)
  ↓
Tank_HolySteel (copy, add tags)
Tank_NatureSteel (copy, change tags)
```

### Pattern 3: Synergy Combinations
Create units that showcase synergy combinations:
- Fire + Storm (aggressive combo)
- Nature + Holy (defensive combo)
- Shadow + Arcane (utility combo)

## Testing Your Configurations

1. **Create Test Scene**
   - Add units with your ScriptableObjects
   - Test combat
   - Verify stats

2. **Use Balance Agent**
   ```csharp
   BalanceAgent agent = FindObjectOfType<BalanceAgent>();
   var request = new AgentRequest {
       RequestType = "validate_stats",
       Parameters = { ["unit_data"] = yourUnitData }
   };
   var response = agent.ProcessRequest(request);
   ```

3. **Check Lore Integration**
   - Verify descriptions use lore text
   - Check archetype names
   - Confirm synergy court names

## Quick Reference

### Creating ScriptableObjects
- Right-click in Project → Create → Fourfold Fate → [Type]

### Editing ScriptableObjects
- Select asset → Edit in Inspector

### Using ScriptableObjects
- Assign to component fields
- Reference in code: `unitData.MaxHealth`
- Load at runtime: `Resources.Load<UnitData>("path")`

### Best Practices
- ✅ Use descriptive names
- ✅ Organize in folders
- ✅ Fill all fields
- ✅ Use lore text
- ✅ Test configurations
- ❌ Don't duplicate unnecessarily
- ❌ Don't leave fields empty
- ❌ Don't use generic names

## Next Steps

1. Create a few example UnitData assets
2. Create AbilityData assets
3. Create Relic assets
4. Test them in a scene
5. Iterate and balance

The ScriptableObject system makes content creation easy - you can create dozens of characters, abilities, and relics without writing any code!

