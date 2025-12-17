# Pixel Art Setup Guide for Fourfold Fate

## Quick Setup

1. **Run the Setup Tool**:
   - In Unity, go to: `Fourfold Fate > Setup Pixel Art Workflow`
   - This will create folders and placeholder sprites automatically

2. **Apply Settings to Your Sprites**:
   - Select your sprite(s) in the Project window
   - Go to: `Fourfold Fate > Apply Pixel Art Settings to Selected`
   - Or manually set import settings (see below)

## Manual Setup

### Folder Structure
```
Assets/
  Sprites/
    Player/      (Player character sprites)
    Enemy/       (Enemy sprites)
    UI/          (UI elements, buttons, icons)
```

### Import Settings for Pixel Art

When importing sprites, set these in the Inspector:

1. **Texture Type**: `Sprite (2D and UI)`
2. **Sprite Mode**: `Single` (or `Multiple` if using sprite sheets)
3. **Pixels Per Unit**: `32` (or `16` if using 16x16 sprites)
4. **Filter Mode**: `Point (no filter)` ⚠️ **CRITICAL**
5. **Compression**: `None` (or `High Quality` if needed)
6. **Max Size**: Match your sprite size (e.g., `32` for 32x32 sprites)
7. **Mip Maps**: `Off`

### Camera Settings

For 2D gameplay:

1. **Camera Type**: `Orthographic`
2. **Size**: `5` to `10` (adjust based on your sprite size)
3. **Pixel Perfect Component** (optional):
   - Add `Pixel Perfect Camera` component
   - Set `Assets Pixels Per Unit` to match your sprites (32)

### Canvas Settings (for UI)

1. **Render Mode**: `Screen Space - Overlay`
2. **Canvas Scaler**:
   - `UI Scale Mode`: `Scale With Screen Size`
   - `Reference Resolution`: `1920 x 1080`
   - `Match`: `0.5` (or `Width` for wider screens)

## Placeholder Sprites

The setup tool creates these colored placeholders:

- **Player**:
  - `warden.png` (Blue) - Tank
  - `blade.png` (Red) - Fighter
  - `seer.png` (Magenta) - Mage
  - `shadow.png` (Green) - Assassin

- **Enemy**:
  - `enemy_placeholder.png` (Gray)

- **UI**:
  - `health_bar.png` (Red)
  - `button_bg.png` (Dark Gray)

## Creating Your Own Sprites

### Recommended Tools
- **Aseprite** (paid, excellent for pixel art)
- **Piskel** (free, web-based)
- **GIMP** (free, with pixel art plugins)
- **Photoshop** (paid, with pixel art settings)

### Sprite Sizes
- **Characters**: 32x32 or 64x64 pixels
- **UI Elements**: 16x16 or 32x32 pixels
- **Icons**: 16x16 pixels

### Color Palette Tips
- Use limited color palettes (16-32 colors)
- Avoid gradients (use flat colors)
- Use dithering for smooth transitions
- Keep consistent style across all sprites

## Testing Your Sprites

1. **Import your sprite** into Unity
2. **Apply pixel art settings** (use the menu tool)
3. **Drag sprite** into Scene or assign to SpriteRenderer
4. **Check for blurriness** - if blurry, Filter Mode is wrong
5. **Check scaling** - adjust Pixels Per Unit if needed

## Common Issues

### Sprites Look Blurry
- **Fix**: Set `Filter Mode` to `Point (no filter)`
- **Fix**: Disable `Mip Maps`

### Sprites Too Big/Small
- **Fix**: Adjust `Pixels Per Unit` (higher = smaller, lower = bigger)
- **Fix**: Adjust Camera `Size`

### Sprites Not Aligning to Grid
- **Fix**: Use `Pixel Perfect Camera` component
- **Fix**: Set sprite `Pixels Per Unit` to match camera settings

## Next Steps

Once you have sprites:
1. Assign them to Unit prefabs
2. Create animations (idle, attack, hit, death)
3. Add to BattleArenaUI for display
4. Create particle effects for abilities

