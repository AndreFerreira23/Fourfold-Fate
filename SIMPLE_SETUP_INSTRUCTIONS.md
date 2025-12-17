# Simple Setup Instructions

## If AutoSceneSetup doesn't appear in Add Component:

### Option 1: Wait for Compilation
1. Look at the **bottom-right corner** of Unity
2. Wait for the progress bar to finish (it says "Compiling...")
3. Once it's done, try adding the component again

### Option 2: Force Recompile
1. Go to **Assets > Reimport All**
2. Wait for it to finish
3. Try adding the component again

### Option 3: Manual Setup (No Script Needed!)

If the script still doesn't work, you can set up manually:

1. **Create Managers GameObject**:
   - Right-click in Hierarchy → Create Empty
   - Name it "Managers"
   - Add these components one by one:
     - GameDataManager
     - BalanceManager  
     - RunManager
     - EncounterManager
     - ProgressionManager
     - PartyManager
     - RelicManager
     - BattleManager
     - UIManager

2. **Create Canvas**:
   - Right-click in Hierarchy → UI → Canvas
   - (This creates Canvas automatically)

3. **Create Main Menu**:
   - Right-click Canvas → UI → Panel
   - Name it "MainMenuPanel"
   - Add Component → MainMenuUI
   - Add a Button as child: Right-click MainMenuPanel → UI → Button
   - Name it "StartRunButton"
   - In MainMenuUI component, drag "StartRunButton" to the "Start Run Button" field

4. **Assign to UIManager**:
   - Select "Managers" GameObject
   - In UIManager component, drag "MainMenuPanel" to "Main Menu UI" field

5. **Press Play!**

### Option 4: Check Console for Errors
1. Open **Window > General > Console**
2. Look for **red error messages**
3. If you see errors about AutoSceneSetup, let me know what they say

## Quick Test:
Try typing "Auto" in the Add Component search box - it should filter and show "AutoSceneSetup" if it's compiled.

