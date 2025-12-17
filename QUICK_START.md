# Quick Start Guide - Fourfold Fate

## 🚀 Getting Started

### Step 1: Set Up Pixel Art (One-Click)
1. Open Unity
2. Go to: **Fourfold Fate > Setup Pixel Art Workflow**
3. This creates:
   - `Assets/Sprites/` folders
   - Placeholder sprites (colored squares)
   - Proper import settings

### Step 2: Set Up Battle UI (One-Click)
1. Go to: **Fourfold Fate > Setup Battle UI**
2. This creates:
   - Battle Arena UI panel
   - Party and enemy containers
   - Action buttons (Attack, End Turn)
   - Combat log
   - Turn indicator
   - Synergy badges

### Step 3: Set Up Scene
1. Create a new scene or use existing
2. Run: **Fourfold Fate > Complete Scene Setup** (if available)
   - Or manually add managers:
     - GameManager
     - GameDataManager
     - BalanceManager
     - RunManager
     - EncounterManager
     - ProgressionManager
     - PartyManager
     - RelicManager
     - BattleManager
     - UIManager

### Step 4: Test the Game
1. Press Play
2. Click "Start Run" on main menu
3. Battle should start automatically
4. Use Attack button to fight
5. Use End Turn when done

## 🎮 How to Play

### Battle Controls
- **Attack Button**: Attack the first enemy
- **End Turn Button**: End your turn (enemies will act)
- **Click Units**: Select party members or target enemies (future feature)

### Current Features
- ✅ Turn-based combat
- ✅ 4 player characters (Tank, Fighter, Mage, Assassin)
- ✅ 12 abilities (3 per character)
- ✅ 6 encounters (including first miniboss)
- ✅ Health bars
- ✅ Combat log
- ✅ Turn indicators

## 🎨 Customizing Sprites

### Replace Placeholder Sprites
1. Create your own 32x32 pixel sprites
2. Import them into `Assets/Sprites/`
3. Select sprite(s) in Project window
4. Go to: **Fourfold Fate > Apply Pixel Art Settings to Selected**
5. Assign sprites to units (future: via prefabs)

### Sprite Colors (Current Placeholders)
- **Blue** = Tank (The Warden)
- **Red** = Fighter (The Blade)
- **Magenta** = Mage (The Seer)
- **Green** = Assassin (The Shadow)
- **Gray** = Enemies

## 📁 Project Structure

```
Assets/
  Scripts/
    Core/           (Unit, Ability, BattleManager)
    Data/           (UnitDefinitions, AbilityDefinitions, etc.)
    UI/             (BattleArenaUI, MainMenuUI, etc.)
    Roguelike/      (RunManager, EncounterManager)
    Party/          (PartyManager)
    Relics/         (Relic system)
    Setup/          (Helper scripts)
    Editor/         (Editor tools)
  Sprites/
    Player/         (Player character sprites)
    Enemy/          (Enemy sprites)
    UI/             (UI elements)
```

## 🐛 Troubleshooting

### Battle UI Not Showing
- Check that BattleArenaUI is assigned to UIManager
- Make sure Canvas exists in scene
- Check that rootPanel is set on BattleArenaUI

### Units Not Appearing
- Units are created dynamically in BattleArenaUI
- Check that BattleManager has party/enemy units
- Check Console for errors

### Sprites Look Blurry
- Select sprite → Inspector
- Set Filter Mode to "Point (no filter)"
- Disable Mip Maps

### Combat Not Starting
- Check that RunManager.StartNewRun() is called
- Check that EncounterManager can find encounters
- Check Console for errors

## 🔧 Next Steps

1. **Add More Encounters**: Edit `EncounterDefinitions.cs`
2. **Add More Abilities**: Edit `AbilityDefinitions.cs`
3. **Add More Characters**: Edit `UnitDefinitions.cs`
4. **Improve Enemy AI**: Edit `BattleManager.EnemyTurn()`
5. **Add Visual Effects**: Create particle effects for abilities
6. **Add Animations**: Create attack/hit animations
7. **Add Sound**: Add sound effects and music

## 📚 Documentation

- `IMPLEMENTATION_STATUS.md` - What's done and what's next
- `PIXEL_ART_GUIDE.md` - Detailed pixel art setup
- `NEXT_STEPS.md` - Overall roadmap

## 💡 Tips

- Start with placeholder sprites to test mechanics
- Add real art later once gameplay is solid
- Use the combat log to debug battle flow
- Check Console for any errors or warnings
- All game data is in code - easy to modify!

