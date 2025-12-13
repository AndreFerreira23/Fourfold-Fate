# UI Systems Documentation

## Overview

The UI system for **Fourfold Fate** is built with Unity's UI system (Canvas, UI elements) and TextMeshPro for text rendering. All UI screens integrate the game's lore and narrative canon.

## UI Architecture

### BaseUI
All UI screens inherit from `BaseUI`, which provides:
- Show/Hide functionality
- CanvasGroup management for fade effects
- Common initialization patterns

### UIManager
Central coordinator that manages UI state transitions and screen visibility.

## UI Screens

### 1. MainMenuUI
**Location**: `Assets/Scripts/UI/MainMenuUI.cs`

**Features**:
- Game title and tagline with lore text
- Start Run button
- Continue Run button (if active run exists)
- Meta-Progression button
- Settings button
- Quit button

**Lore Integration**:
- Uses `LoreTextManager.TAGLINE_A` for tagline
- Displays `LoreTextManager.RUN_START` as intro text

### 2. BattleArenaUI
**Location**: `Assets/Scripts/UI/BattleArenaUI.cs`

**Features**:
- Party member panels showing health, archetype, and resources
- Enemy panels showing health and status
- Combat log for battle events
- Synergy badge display
- Level and encounter type display
- Archetype-specific resource displays (Guard, Momentum, Surge, Opportunity)

**Components**:
- `UnitPanel`: Displays individual party member state
- `EnemyPanel`: Displays individual enemy state

**Lore Integration**:
- Boss names use `LoreTextManager.GetBossName()`
- Encounter types use lore terminology (Tollgate, Myth-Eater, etc.)
- Synergy badges show Court names

### 3. PartyManagementUI
**Location**: `Assets/Scripts/UI/PartyManagementUI.cs`

**Features**:
- Four party slots with unlock status
- Character selection panel
- Synergy display
- Party size and unlock information

**Components**:
- `PartySlotUI`: Individual party slot with unlock lore

**Lore Integration**:
- Party unlock milestones use `LoreTextManager.GetPartyUnlockLore()`
- Displays "The First Seat (Will)", "The Second Seat (Need)", etc.

### 4. LevelUpUI
**Location**: `Assets/Scripts/UI/LevelUpUI.cs`

**Features**:
- Level-up choice cards
- Path selection (Offense, Defense, Utility, Chaos)
- Upgrade preview
- Lore-integrated descriptions

**Components**:
- `LevelUpChoiceCard`: Individual choice card with path colors

**Lore Integration**:
- Path names use lore: "Path of Wrath", "Path of Endurance", etc.
- Uses `LoreTextManager.GetLevelUpPathName()`

### 5. RelicSelectionUI
**Location**: `Assets/Scripts/UI/RelicSelectionUI.cs`

**Features**:
- Relic cards with icons and descriptions
- Rarity display with color coding
- Skip option

**Components**:
- `RelicCard`: Individual relic display card

**Lore Integration**:
- Relic descriptions use `LoreTextManager.GetRelicFlavorText()`
- Framed as "Memory-Forged Relics"

### 6. RunProgressionUI
**Location**: `Assets/Scripts/UI/RunProgressionUI.cs`

**Features**:
- Current level display (Trial X/100)
- Progress bar
- Next boss information
- Party status
- Relic count
- Lore status text

**Lore Integration**:
- Uses `LoreTextManager.GetStatusText()` for dynamic status
- Boss names use lore terminology

## Animation System

### AttackAnimationSystem
**Location**: `Assets/Scripts/Animation/AttackAnimationSystem.cs`

**Features**:
- Attack animations with movement
- Damage number display
- Hit effects
- Archetype-specific visual effects
- Synergy effect support

**Usage**:
```csharp
AttackAnimationSystem animSystem = FindObjectOfType<AttackAnimationSystem>();
animSystem.PlayAttackAnimation(attacker, target, damage);
```

## Setup Instructions

### 1. Create UI Canvas
1. Create a Canvas in your scene
2. Set Canvas Scaler to "Scale With Screen Size"
3. Add `UIManager` component to a GameObject

### 2. Create UI Prefabs
You'll need to create prefabs for:
- `unitPanelPrefab`: Party member display panel
- `enemyPanelPrefab`: Enemy display panel
- `partySlotPrefab`: Party slot UI
- `characterCardPrefab`: Character selection card
- `choiceCardPrefab`: Level-up choice card
- `relicCardPrefab`: Relic display card
- `synergyBadgePrefab`: Synergy badge
- `damageNumberPrefab`: Damage number text
- `hitEffectPrefab`: Hit effect particle system

### 3. Wire Up UI Screens
1. Create GameObjects for each UI screen
2. Add the appropriate UI script component
3. Assign UI element references in the Inspector
4. Connect to `UIManager`

### 4. Connect to Game Systems
The UI systems automatically find managers using `FindObjectOfType`, but you can also:
- Assign references directly in the Inspector
- Use events to communicate between systems

## Visual Guidelines

### Color Coding
- **Tank**: Blue-ish (0.2, 0.6, 0.8)
- **Fighter**: Red-ish (0.8, 0.3, 0.2)
- **Mage**: Purple-ish (0.6, 0.2, 0.8)
- **Assassin**: Dark gray (0.3, 0.3, 0.3)

### Synergy Effects (from lore doc)
- **Fire**: Ember-motes, soot halos
- **Nature**: Pollen glow, thorn crowns
- **Shadow**: Violet fog, black candles
- **Holy**: Gold sigils, chime-like cleanses
- **Arcane**: Cyan runes, geometric rings
- **Steel**: Stamped ward-marks, iron sparks
- **Storm**: Teal streaks, arcing chains

## Events

All UI screens use C# events for communication:
- `OnStartRun`, `OnContinueRun`
- `OnLevelUpComplete`
- `OnRelicChosen`, `OnRelicSkipped`
- `OnPartyMemberAdded`, `OnPartyMemberRemoved`

## TextMeshPro Setup

All text uses TextMeshPro for better rendering. Make sure to:
1. Import TextMeshPro package (Window > TextMeshPro > Import TMP Essential Resources)
2. Use `TextMeshProUGUI` components instead of `Text`
3. Set up fonts for the game's aesthetic

## Next Steps

1. Create UI prefabs in Unity
2. Design UI layouts matching the game's 2D pixel art style
3. Create particle effects for combat animations
4. Implement sound effects for UI interactions
5. Add screen transitions/animations

