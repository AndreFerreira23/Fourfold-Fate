# How to Use Setup Tools

## Where to Find the Menu Items

The setup tools appear in **TWO places**:

### Option 1: Top Menu Bar (Recommended)
1. Look at the **top of Unity** (File, Edit, Assets, GameObject, Component, etc.)
2. Click on **"Fourfold Fate"** menu
3. You'll see:
   - Quick Scene Setup (All Managers)
   - Setup Battle UI
   - Setup Pixel Art Workflow
   - And other tools...

### Option 2: GameObject Menu (Alternative)
1. Right-click in the **Hierarchy** window
2. Or go to **GameObject** menu at the top
3. Look for **"Fourfold Fate"** submenu
4. Same options available there

## You DON'T Need to Select Anything!

**Important**: These are global menu items - you don't need to select anything in the Hierarchy. Just:
1. Click the menu item
2. It will set up everything automatically

## Step-by-Step Setup

### First Time Setup:
1. **Open your scene** (or create a new one)
2. **Don't select anything** - just click the menu
3. Go to: **Fourfold Fate > Quick Scene Setup (All Managers)**
4. Wait for it to finish (check Console for messages)
5. **Press Play** - the game should start!

### If Something is Missing:
1. Check the **Console** window (Window > General > Console)
2. Look for ❌ error messages
3. Run the specific setup tool that's missing:
   - **Setup Battle UI** - if battle screen doesn't work
   - **Setup Pixel Art** - if you want placeholder sprites

## What Gets Created

When you run "Quick Scene Setup":
- ✅ Creates "Managers" GameObject with all managers
- ✅ Creates Canvas (if it doesn't exist)
- ✅ Creates Main Menu UI
- ✅ Creates Battle Arena UI
- ✅ Connects everything together

## Troubleshooting

### "Nothing happens when I click Play"
- Make sure you ran "Quick Scene Setup" first
- Check Console for errors
- Make sure the scene is saved

### "I can't find the menu"
- Make sure scripts compiled successfully (no red errors)
- Try: **Assets > Reimport All** (if menu doesn't appear)
- The menu is at the top, not in Hierarchy

### "Menu item is grayed out"
- Make sure you're not in Play mode
- Exit Play mode first, then try again

## Visual Guide

```
Unity Top Menu Bar:
┌─────────────────────────────────────┐
│ File Edit Assets GameObject Component│
│ Window Help Fourfold Fate ← HERE!   │
└─────────────────────────────────────┘
         │
         └─> Quick Scene Setup
         └─> Setup Battle UI
         └─> Setup Pixel Art
         └─> etc...
```

Or in Hierarchy:
```
Right-click in Hierarchy
  └─> GameObject
      └─> Fourfold Fate
          └─> Quick Scene Setup
```

