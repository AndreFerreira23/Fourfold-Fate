# How to Fix Input System Error - Step by Step

## The Error
```
InvalidOperationException: You are trying to read Input using the UnityEngine.Input class, 
but you have switched active Input handling to Input System package in Player Settings.
```

This means Unity is using the **NEW Input System** but the game needs the **OLD Input System**.

## Step-by-Step Fix

### Step 1: Open Project Settings
- In Unity's top menu, click **Edit**
- Click **Project Settings...**

### Step 2: Find Player Settings
- In the left sidebar of the Project Settings window, click **Player**
- (It's usually near the top of the list)

### Step 3: Find Active Input Handling
- In the Player settings, scroll down to find **Other Settings**
- Expand **Other Settings** if it's collapsed
- Look for **Active Input Handling**
- It's probably set to **Input System Package (New)** or **Both**

### Step 4: Change the Setting
- Click the dropdown next to **Active Input Handling**
- Select: **Input Manager (Old)**
- (This is the option that says "Old" in it)

### Step 5: Restart Unity (if prompted)
- Unity may show a dialog asking to restart
- Click **Yes** or **Restart**
- If no dialog appears, you can continue

### Step 6: Test
- Press **Play**
- The errors should be gone
- Buttons should now be clickable

## Visual Guide

```
Unity Menu Bar:
┌─────────────────────────────────────┐
│ File Edit Assets GameObject Component│
│ Window Help                          │
└─────────────────────────────────────┘
         │
         └─> Edit > Project Settings

Project Settings Window:
┌─────────────────────────────────────┐
│ [Player] ← Click this               │
│ [Audio]                              │
│ [Time]                               │
│ ...                                  │
└─────────────────────────────────────┘

Player Settings:
┌─────────────────────────────────────┐
│ Other Settings                      │
│   Active Input Handling:            │
│   [Input Manager (Old)] ← Select this│
└─────────────────────────────────────┘
```

## If You Can't Find It

1. Make sure you're in **Project Settings** (not Preferences)
2. Make sure you clicked **Player** in the left sidebar
3. Scroll down - it's in the **Other Settings** section
4. It might be called "Active Input Handling" or just "Input Handling"

## After Fixing

- Stop Play mode
- Press Play again
- Check Console - errors should be gone
- Try clicking the "Start Run" button - it should work!

