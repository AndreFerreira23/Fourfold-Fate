# Setup GitHub Repository - Step by Step

## Step 1: Create Repository on GitHub

1. **Go to GitHub**: https://github.com/new
2. **Repository name**: `Fourfold-Fate` (or any name you want)
3. **Description** (optional): "2D roguelike autobattler game"
4. **Visibility**: Choose **Private** or **Public**
5. **IMPORTANT**: 
   - ❌ DON'T check "Add a README file"
   - ❌ DON'T check "Add .gitignore"
   - ❌ DON'T check "Choose a license"
6. **Click "Create repository"**

## Step 2: After Creating Repository

GitHub will show you setup instructions. **DON'T follow those** - use the commands below instead.

## Step 3: Run These Commands

Open PowerShell or Command Prompt and run:

```powershell
cd "C:\Users\arset\Fourfold Fate"

# Check if git is initialized
git status

# If not initialized, run:
git init

# Add all files
git add .

# Commit
git commit -m "Initial setup: Core systems, UI, and scene setup"

# Add remote (replace 'Fourfold-Fate' with your actual repo name if different)
git remote add origin https://github.com/arset/Fourfold-Fate.git

# Push to GitHub
git branch -M main
git push -u origin main
```

## If You Get "Repository Not Found" Error

**Possible causes:**
1. Repository doesn't exist yet - Create it first (Step 1)
2. Wrong repository name - Check the exact name on GitHub
3. Wrong username - Make sure it's "arset"
4. Repository is private and you're not authenticated

**To check your repository exists:**
- Go to: https://github.com/arset?tab=repositories
- Look for "Fourfold-Fate" in your repositories list

**To fix authentication:**
- GitHub no longer accepts passwords
- You need a Personal Access Token
- Or use GitHub Desktop (easier!)

## Alternative: Use GitHub Desktop (Recommended)

1. Download: https://desktop.github.com/
2. Sign in with your GitHub account
3. File → Add Local Repository
4. Select: `C:\Users\arset\Fourfold Fate`
5. Click "Publish repository"
6. Done!

