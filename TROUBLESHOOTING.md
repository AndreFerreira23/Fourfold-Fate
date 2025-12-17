# Troubleshooting Guide

## If RunManager.Instance is null or Managers GameObject is destroyed:

### Solution 1: Hard Refresh (Recommended)
1. **Stop Play mode**
2. Go to: **Assets > Reimport All**
3. Wait for it to finish (this can take a few minutes)
4. Press **Play** again

### Solution 2: Clean Start
1. **Stop Play mode**
2. In Hierarchy, **delete ALL "Managers" GameObjects**
3. **Delete the AutoSceneSetup GameObject** (if it exists)
4. Press **Play** - AutoSceneSetup will create everything fresh

### Solution 3: Manual Setup (If Auto Setup Fails)
1. **Stop Play mode**
2. Create empty GameObject named "Managers"
3. Add these components one by one:
   - GameDataManager
   - BalanceManager
   - RunManager
   - EncounterManager
   - ProgressionManager
   - PartyManager
   - RelicManager
   - BattleManager
   - UIManager
4. Press **Play**

## Common Issues

### "RunManager.Instance is null"
- **Cause**: RunManager wasn't created, or Awake() didn't run
- **Fix**: Delete Managers GameObject and let AutoSceneSetup recreate it

### "Managers GameObject was destroyed"
- **Cause**: Duplicate RunManager detected, or Instance conflict
- **Fix**: Delete all Managers GameObjects, restart Unity, try again

### "Components on Managers: 1"
- **Cause**: Only Transform component exists, other components weren't added
- **Fix**: Delete Managers GameObject, let AutoSceneSetup recreate it

### Button Not Clickable
- **Cause**: EventSystem missing or Input System wrong
- **Fix**: 
  1. Check Project Settings > Player > Active Input Handling = "Input Manager (Old)"
  2. Make sure EventSystem exists in scene

## Quick Test
After setup, check Console for:
- ✅ "RunManager.Instance verified and ready"
- ✅ "All managers connected"
- ✅ "AUTO SETUP COMPLETE"

If you see ❌ errors, follow the solutions above.

