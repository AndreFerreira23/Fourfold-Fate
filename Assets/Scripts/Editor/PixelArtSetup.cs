#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Editor tool to set up pixel art workflow and create placeholder sprites.
    /// </summary>
    public class PixelArtSetup : EditorWindow
    {
        [MenuItem("Fourfold Fate/Setup Pixel Art Workflow", false, 3)]
        [MenuItem("GameObject/Fourfold Fate/Setup Pixel Art", false, 12)]
        public static void SetupPixelArt()
        {
            // Create sprite folders
            string spritesPath = "Assets/Sprites";
            string playerPath = spritesPath + "/Player";
            string enemyPath = spritesPath + "/Enemy";
            string uiPath = spritesPath + "/UI";

            if (!Directory.Exists(spritesPath))
                Directory.CreateDirectory(spritesPath);
            if (!Directory.Exists(playerPath))
                Directory.CreateDirectory(playerPath);
            if (!Directory.Exists(enemyPath))
                Directory.CreateDirectory(enemyPath);
            if (!Directory.Exists(uiPath))
                Directory.CreateDirectory(uiPath);

            Debug.Log("Created sprite folders at Assets/Sprites/");

            // Create placeholder sprites
            CreatePlaceholderSprites();

            AssetDatabase.Refresh();
            Debug.Log("Pixel Art setup complete! Check Assets/Sprites/ for placeholder sprites.");
        }

        private static void CreatePlaceholderSprites()
        {
            // Create simple colored square textures as placeholders
            CreateColoredSprite("Assets/Sprites/Player/warden.png", Color.blue, "Tank - The Warden");
            CreateColoredSprite("Assets/Sprites/Player/blade.png", Color.red, "Fighter - The Blade");
            CreateColoredSprite("Assets/Sprites/Player/seer.png", Color.magenta, "Mage - The Seer");
            CreateColoredSprite("Assets/Sprites/Player/shadow.png", Color.green, "Assassin - The Shadow");

            // Create enemy placeholder
            CreateColoredSprite("Assets/Sprites/Enemy/enemy_placeholder.png", Color.gray, "Enemy Placeholder");

            // Create UI placeholders
            CreateColoredSprite("Assets/Sprites/UI/health_bar.png", Color.red, "Health Bar");
            CreateColoredSprite("Assets/Sprites/UI/button_bg.png", new Color(0.3f, 0.3f, 0.3f), "Button Background");
        }

        private static void CreateColoredSprite(string path, Color color, string description)
        {
            int size = 32; // 32x32 pixel sprites
            Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);

            // Fill with color
            Color[] pixels = new Color[size * size];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            texture.SetPixels(pixels);
            texture.Apply();

            // Save as PNG
            byte[] pngData = texture.EncodeToPNG();
            File.WriteAllBytes(path, pngData);

            // Set import settings
            AssetDatabase.Refresh();
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.spritePixelsPerUnit = 32;
                importer.filterMode = FilterMode.Point;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.mipmapEnabled = false;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }

            Debug.Log($"Created placeholder sprite: {description} at {path}");
        }

        [MenuItem("Fourfold Fate/Apply Pixel Art Settings to Selected")]
        public static void ApplyPixelArtSettings()
        {
            Object[] selected = Selection.objects;
            if (selected.Length == 0)
            {
                EditorUtility.DisplayDialog("No Selection", "Please select sprite(s) in the Project window.", "OK");
                return;
            }

            int count = 0;
            foreach (Object obj in selected)
            {
                string path = AssetDatabase.GetAssetPath(obj);
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                
                if (importer != null)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spritePixelsPerUnit = 32;
                    importer.filterMode = FilterMode.Point;
                    importer.textureCompression = TextureImporterCompression.Uncompressed;
                    importer.mipmapEnabled = false;
                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                    count++;
                }
            }

            Debug.Log($"Applied pixel art settings to {count} sprite(s).");
        }
    }
}
#endif

