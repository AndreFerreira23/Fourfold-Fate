# How to Clean Up Your Scene

## Check What GameObjects You Have:

In Hierarchy, look for:
- ✅ **AutoSceneSetup** (or GameObject with AutoSceneSetup component) - KEEP THIS
- ❌ **GameInitializer** (or GameObject with GameInitializer component) - DELETE THIS (conflicts with AutoSceneSetup)
- ❌ **SceneDiagnostics** (or GameObject with SceneDiagnostics component) - OPTIONAL (can keep for debugging)
- ❌ **InputSystemChecker** (or GameObject with InputSystemChecker component) - OPTIONAL (can keep)
- ✅ **Managers** - KEEP THIS (should have all manager components)
- ❌ **GameDataManager** (standalone GameObject) - DELETE THIS (should be on Managers)
- ❌ **BalanceManager** (standalone GameObject) - DELETE THIS (should be on Managers)

## Clean Setup (Recommended):

1. **Stop Play mode**
2. **Delete these GameObjects** (if they exist):
   - GameInitializer (or GameObject with GameInitializer)
   - Any standalone GameDataManager GameObject
   - Any standalone BalanceManager GameObject
3. **Keep these**:
   - AutoSceneSetup (or GameObject with AutoSceneSetup component)
   - Managers GameObject (with all components)
4. **Press Play** - AutoSceneSetup will handle everything

## What Each Script Does:

- **AutoSceneSetup**: Creates everything automatically ✅ USE THIS
- **GameInitializer**: Creates separate GameObjects (conflicts) ❌ REMOVE
- **SceneDiagnostics**: Just checks what's there (optional)
- **InputSystemChecker**: Checks Input System settings (optional)

## After Cleanup:

You should have:
- 1 GameObject with **AutoSceneSetup** component
- 1 GameObject named **Managers** with all manager components
- Canvas (created automatically)
- UI panels (created automatically)

Try deleting GameInitializer and any standalone manager GameObjects, then press Play!

