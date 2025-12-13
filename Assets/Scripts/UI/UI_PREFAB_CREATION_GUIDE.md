# UI Prefab Creation Guide

## Step 1: Create Main Menu UI Prefab

### Part A: Create the Main Menu Panel

1. **Select Canvas** in Hierarchy (should already exist from SceneSetupHelper)
2. **Right-click Canvas** → **UI** → **Panel**
   - Name it: "MainMenuPanel"
3. **Select MainMenuPanel** and in Inspector:
   - Set **Anchor Presets**: Hold Alt + Click bottom-right corner (stretches to fill screen)
   - Set **Color**: Dark background (e.g., R:20, G:20, B:30, A:255)

### Part B: Add Title Text

1. **Right-click MainMenuPanel** → **UI** → **Text**
   - Name it: "TitleText"
2. **Select TitleText** and in Inspector:
   - **Text**: "Fourfold Fate"
   - **Font Size**: 48
   - **Alignment**: Center (both horizontal and vertical)
   - **Color**: White or Gold
   - **Rect Transform**: 
     - Anchor: Top-center
     - Pos Y: -50
     - Width: 400
     - Height: 60

### Part C: Add Tagline Text

1. **Right-click MainMenuPanel** → **UI** → **Text**
   - Name it: "TaglineText"
2. **Select TaglineText** and in Inspector:
   - **Text**: "Four seats. One climb. A hundred chances to become worth the summit."
   - **Font Size**: 18
   - **Alignment**: Center
   - **Color**: Light gray
   - **Rect Transform**:
     - Anchor: Top-center
     - Pos Y: -120
     - Width: 600
     - Height: 50

### Part D: Add Lore Text

1. **Right-click MainMenuPanel** → **UI** → **Text**
   - Name it: "LoreText"
2. **Select LoreText** and in Inspector:
   - **Text**: "The Hall of Echoes is quiet until you arrive..."
   - **Font Size**: 14
   - **Alignment**: Center
   - **Color**: Gray
   - **Rect Transform**:
     - Anchor: Center
     - Pos Y: 50
     - Width: 700
     - Height: 100

### Part E: Add Buttons

1. **Right-click MainMenuPanel** → **UI** → **Button**
   - Name it: "StartRunButton"
   - **Text**: "Start New Run"
   - **Rect Transform**: Center, Pos Y: -50, Width: 200, Height: 40

2. **Right-click MainMenuPanel** → **UI** → **Button**
   - Name it: "ContinueRunButton"
   - **Text**: "Continue Run"
   - **Rect Transform**: Center, Pos Y: -100, Width: 200, Height: 40

3. **Right-click MainMenuPanel** → **UI** → **Button**
   - Name it: "MetaProgressionButton"
   - **Text**: "Meta Progression"
   - **Rect Transform**: Center, Pos Y: -150, Width: 200, Height: 40

4. **Right-click MainMenuPanel** → **UI** → **Button**
   - Name it: "SettingsButton"
   - **Text**: "Settings"
   - **Rect Transform**: Center, Pos Y: -200, Width: 200, Height: 40

5. **Right-click MainMenuPanel** → **UI** → **Button**
   - Name it: "QuitButton"
   - **Text**: "Quit"
   - **Rect Transform**: Center, Pos Y: -250, Width: 200, Height: 40

### Part F: Add MainMenuUI Component

1. **Select MainMenuPanel**
2. **Add Component** → search: "MainMenuUI"
3. **In Inspector**, assign the UI elements:
   - **Title Text**: Drag TitleText from Hierarchy
   - **Tagline Text**: Drag TaglineText from Hierarchy
   - **Lore Text**: Drag LoreText from Hierarchy
   - **Start Run Button**: Drag StartRunButton from Hierarchy
   - **Continue Run Button**: Drag ContinueRunButton from Hierarchy
   - **Meta Progression Button**: Drag MetaProgressionButton from Hierarchy
   - **Settings Button**: Drag SettingsButton from Hierarchy
   - **Quit Button**: Drag QuitButton from Hierarchy

### Part G: Create Prefab

1. **Create Prefabs folder** (if not exists):
   - In Project window: `Assets/Prefabs/UI/`
2. **Drag MainMenuPanel** from Hierarchy to `Assets/Prefabs/UI/`
   - This creates the prefab
3. **Delete MainMenuPanel** from Hierarchy (we'll use the prefab)

### Part H: Assign to UIManager

1. **Select UIManager** in Hierarchy (under Canvas)
2. **In Inspector**, find **Main Menu UI** field
3. **Drag MainMenuPanel prefab** from Project window to this field

---

## Step 2: Create Battle Arena UI Prefab

### Part A: Create Battle Panel

1. **Right-click Canvas** → **UI** → **Panel**
   - Name it: "BattleArenaPanel"
   - Set background color (darker, semi-transparent)

### Part B: Add Party Display Area

1. **Right-click BattleArenaPanel** → **UI** → **Panel**
   - Name it: "PartyContainer"
   - **Rect Transform**: Left side, Width: 300, Height: 400

2. **Add Text** for each party slot (or create dynamically later)
   - Name: "PartyStatusText"
   - Text: "Party: 1/4"

### Part C: Add Enemy Display Area

1. **Right-click BattleArenaPanel** → **UI** → **Panel**
   - Name it: "EnemyContainer"
   - **Rect Transform**: Right side, Width: 300, Height: 400

### Part D: Add Combat Info

1. **Right-click BattleArenaPanel** → **UI** → **Text**
   - Name: "LevelText"
   - Text: "Trial 1/100"

2. **Right-click BattleArenaPanel** → **UI** → **Text**
   - Name: "EncounterTypeText"
   - Text: "Standard Trial"

3. **Right-click BattleArenaPanel** → **UI** → **Scroll View**
   - Name: "CombatLogScroll"
   - Inside: Add Text for combat log

### Part E: Add BattleArenaUI Component

1. **Select BattleArenaPanel**
2. **Add Component** → "BattleArenaUI"
3. **Assign references**:
   - Party Container: Drag PartyContainer
   - Enemy Container: Drag EnemyContainer
   - Level Text: Drag LevelText
   - Encounter Type Text: Drag EncounterTypeText
   - Combat Log Text: Drag text from CombatLogScroll
   - Combat Log Scroll: Drag CombatLogScroll

### Part F: Create Prefab & Assign

1. **Drag BattleArenaPanel** to `Assets/Prefabs/UI/`
2. **Delete from Hierarchy**
3. **Assign to UIManager** → Battle Arena UI field

---

## Quick Setup Alternative

If you want to test quickly, you can create a minimal UI:

1. **Create simple MainMenuPanel** with just:
   - TitleText
   - StartRunButton
2. **Add MainMenuUI component**
3. **Assign the two elements**
4. **Create prefab**
5. **Assign to UIManager**

This will let you test the game flow!

---

## Tips

- **Anchor Presets**: Hold Alt when setting anchors to also set position
- **Rect Transform**: Use the anchor presets in the top-left of Rect Transform
- **Colors**: Use the color picker in Inspector
- **Text**: Unity's default font works fine for testing
- **Buttons**: Default button style is fine for now

## Testing

After creating Main Menu UI:
1. **Press Play**
2. **You should see the main menu!**
3. **Click "Start New Run"** to test

