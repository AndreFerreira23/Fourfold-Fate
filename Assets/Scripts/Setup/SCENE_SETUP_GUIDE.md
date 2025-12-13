# Scene Setup Guide for "MyScene"

## Quick Setup Steps

### Step 1: Check Scripts Are Compiled
1. Look at the bottom-right of Unity - wait for "Compiling..." to finish
2. Check Console (Window → General → Console) for any errors
3. If there are errors, fix them first

### Step 2: Create SceneSetupHelper GameObject
1. In Hierarchy, right-click → **Create Empty**
2. Name it: **"SceneSetupHelper"**
3. Select it
4. In Inspector, click **Add Component**
5. Type: **"SceneSetupHelper"** in the search box
6. Click it to add

### Step 3: Run Setup
1. With SceneSetupHelper selected in Hierarchy
2. In Inspector, find the SceneSetupHelper component
3. Right-click on the component header (where it says "Scene Setup Helper")
4. Click **"Setup Scene"** from the context menu

OR

1. Check the **"Setup On Start"** checkbox
2. Press Play - it will auto-setup when the scene starts

### Step 4: Verify Setup
After running Setup Scene, you should see in Hierarchy:
- **Managers** (parent object)
  - GameDataManager
  - BalanceManager
  - GameManager
  - RunManager
  - BattleManager
  - PartyManager
  - RelicManager
  - MetaProgressionManager
  - AgentManager
  - AttackAnimationSystem
- **Canvas**
  - UIManager
- **Main Camera**

### Step 5: Create Prefabs
1. Create empty GameObject named **"PrefabCreator"**
2. Add **PrefabCreator** component
3. Right-click component → **"Create All Prefabs"**
4. This creates prefabs in `Assets/Prefabs/Units/`

### Step 6: Assign Prefabs
1. Find **GameManager** in Hierarchy (under Managers)
2. In Inspector, find **"Unit Prefab"** field
3. Drag `UnitPrefab` from Project window to this field
4. Find **EncounterManager** (under RunManager)
5. Drag `EnemyPrefab` to **"Enemy Prefab"** field

### Step 7: Test
1. Press **Play**
2. Check Console for initialization messages
3. Game should be ready!

## If SceneSetupHelper Doesn't Appear

### Option A: Manual Setup
Create these GameObjects manually:

**Managers** (parent)
- GameDataManager (Add Component: GameDataManager)
- BalanceManager (Add Component: BalanceManager)
- GameManager (Add Component: GameManager)
- RunManager (Add Component: RunManager)
  - Also add: EncounterManager, ProgressionManager, LevelUpSystem
- BattleManager (Add Component: BattleManager)
- PartyManager (Add Component: PartyManager)
- RelicManager (Add Component: RelicManager)
- MetaProgressionManager (Add Component: MetaProgressionManager)
- AgentManager (Add Component: AgentManager)
  - Also add: StoryAgent, BalanceAgent, CodeAgent
- AttackAnimationSystem (Add Component: AttackAnimationSystem)

**Canvas** (GameObject → UI → Canvas)
- UIManager (Add Component: UIManager)

### Option B: Check Script Location
1. In Project window, go to `Assets/Scripts/Setup/`
2. You should see `SceneSetupHelper.cs`
3. If missing, Unity might not have imported it
4. Try: Assets → Refresh (Ctrl+R)

## Common Issues

**Issue**: "Component not found"
- **Solution**: Wait for Unity to finish compiling scripts

**Issue**: "Script has compilation errors"
- **Solution**: Check Console, fix errors, wait for recompile

**Issue**: "Prefabs folder doesn't exist"
- **Solution**: Create folder `Assets/Prefabs/Units/` in Project window

## Next Steps After Setup

1. ✅ All managers created
2. ✅ Prefabs created
3. ✅ Assign prefabs to managers
4. ✅ Test the scene
5. 🎨 Add sprites to prefabs (later)
6. 🎨 Create UI prefabs (later)

Your scene should now be ready to test!

