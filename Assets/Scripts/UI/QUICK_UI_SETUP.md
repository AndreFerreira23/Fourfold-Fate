# Quick UI Setup - Minimal Version

## Fastest Way to Get Something Visible

### Step 1: Create Simple Main Menu (5 minutes)

1. **Select Canvas** in Hierarchy

2. **Create Panel**:
   - Right-click Canvas → **UI** → **Panel**
   - Name: "MainMenuPanel"
   - **Rect Transform**: Click the anchor square (top-left) → Hold Alt → Click "stretch-stretch" (bottom-right)
   - This makes it fill the screen

3. **Add Title**:
   - Right-click MainMenuPanel → **UI** → **Text**
   - Name: "TitleText"
   - **Text**: "Fourfold Fate"
   - **Font Size**: 60
   - **Alignment**: Center
   - **Rect Transform**: 
     - Anchor: Top-center
     - Pos Y: -100
     - Width: 500
     - Height: 80

4. **Add Start Button**:
   - Right-click MainMenuPanel → **UI** → **Button**
   - Name: "StartRunButton"
   - **Text** (inside button): "Start New Run"
   - **Rect Transform**: 
     - Anchor: Center
     - Pos Y: 0
     - Width: 250
     - Height: 50

5. **Add MainMenuUI Component**:
   - Select MainMenuPanel
   - **Add Component** → "MainMenuUI"
   - **Assign**:
     - Title Text: Drag TitleText
     - Start Run Button: Drag StartRunButton
     - (Leave others empty for now)

6. **Create Prefab**:
   - Create folder: `Assets/Prefabs/UI/`
   - Drag MainMenuPanel to this folder
   - Delete MainMenuPanel from Hierarchy

7. **Assign to UIManager**:
   - Select UIManager (under Canvas)
   - Drag MainMenuPanel prefab to "Main Menu UI" field

8. **Test**:
   - Press Play
   - You should see "Fourfold Fate" title and "Start New Run" button!

---

## Step 2: Create Simple Battle UI (Optional, 5 minutes)

1. **Create Panel**: "BattleArenaPanel" (same as above)

2. **Add Text**:
   - "LevelText": "Trial 1/100" (top-left)
   - "PartyStatusText": "Party: 1/4" (left side)
   - "EnemyStatusText": "Enemies: 0" (right side)

3. **Add BattleArenaUI Component**:
   - Assign the text elements

4. **Create Prefab** and assign to UIManager

---

## That's It!

With just the Main Menu, you can:
- See the game title
- Click "Start New Run"
- Test the game flow

You can add more UI elements later!

