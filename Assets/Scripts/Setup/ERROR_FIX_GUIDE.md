# Error Fix Guide

## Common Errors and Solutions

### Error 1: "The type or namespace name 'TMPro' could not be found"
**Cause**: TextMeshPro package not installed

**Solution**:
1. In Unity, go to **Window → Package Manager**
2. Click **+** button (top-left) → **Add package by name**
3. Type: `com.unity.textmeshpro`
4. Click **Add**
5. Wait for installation
6. Unity will recompile automatically

### Error 2: "The type or namespace name 'X' could not be found"
**Cause**: Missing using statement or namespace issue

**Solution**:
- Check if the script has all required `using` statements at the top
- Make sure all scripts are in the `Assets/Scripts/` folder
- Wait for Unity to finish compiling

### Error 3: "Cannot find type 'X' in the current context"
**Cause**: Script not compiled yet or missing reference

**Solution**:
1. Check Console for the specific error
2. Make sure the referenced script exists
3. Wait for Unity to finish compiling
4. Try: **Assets → Refresh** (Ctrl+R)

### Error 4: Multiple namespace errors
**Cause**: Scripts not properly organized

**Solution**:
- All scripts should be in `Assets/Scripts/` with proper subfolders
- Make sure `.meta` files exist (Unity creates these automatically)

## Quick Fix Steps

1. **Check Console** (Window → General → Console)
   - Look for red error messages
   - Read the error text carefully

2. **Install TextMeshPro** (most common issue)
   - Window → Package Manager
   - Add `com.unity.textmeshpro`

3. **Refresh Assets**
   - Assets → Refresh (Ctrl+R)
   - Wait for compilation

4. **Check Script Locations**
   - All scripts should be in `Assets/Scripts/`
   - Unity should have created `.meta` files

## If Errors Persist

**Please share**:
1. The exact error messages from Console (copy/paste)
2. Which scripts are showing errors
3. Any red underlines in the code editor

This will help me fix the specific issues!

