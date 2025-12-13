using UnityEngine;
using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Helper script to create unit and enemy prefabs.
    /// Use this to generate prefabs from code.
    /// </summary>
    public class PrefabCreator : MonoBehaviour
    {
        [Header("Prefab Settings")]
        [SerializeField] private string prefabPath = "Assets/Prefabs/Units/";
        [SerializeField] private Sprite defaultSprite;  // Assign a placeholder sprite

        [ContextMenu("Create Unit Prefab")]
        public void CreateUnitPrefab()
        {
            GameObject unitObj = new GameObject("UnitPrefab");
            
            // Add Unit component
            Unit unit = unitObj.AddComponent<Unit>();
            
            // Add SpriteRenderer
            SpriteRenderer spriteRenderer = unitObj.AddComponent<SpriteRenderer>();
            if (defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
            spriteRenderer.sortingOrder = 1;
            
            // Add BoxCollider2D for positioning
            BoxCollider2D collider = unitObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;
            
            // Add Rigidbody2D (optional, for physics if needed)
            Rigidbody2D rb = unitObj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            // Create prefab
            #if UNITY_EDITOR
            string path = prefabPath + "UnitPrefab.prefab";
            UnityEditor.PrefabUtility.SaveAsPrefabAsset(unitObj, path);
            Debug.Log($"Unit prefab created at: {path}");
            #endif
            
            DestroyImmediate(unitObj);
        }

        [ContextMenu("Create Enemy Prefab")]
        public void CreateEnemyPrefab()
        {
            GameObject enemyObj = new GameObject("EnemyPrefab");
            
            // Add Unit component
            Unit unit = enemyObj.AddComponent<Unit>();
            
            // Add SpriteRenderer
            SpriteRenderer spriteRenderer = enemyObj.AddComponent<SpriteRenderer>();
            if (defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
            spriteRenderer.sortingOrder = 1;
            spriteRenderer.color = new Color(1f, 0.8f, 0.8f);  // Slightly red tint for enemies
            
            // Add BoxCollider2D
            BoxCollider2D collider = enemyObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;
            
            // Add Rigidbody2D
            Rigidbody2D rb = enemyObj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            
            // Tag as Enemy
            enemyObj.tag = "Enemy";
            
            // Create prefab
            #if UNITY_EDITOR
            string path = prefabPath + "EnemyPrefab.prefab";
            UnityEditor.PrefabUtility.SaveAsPrefabAsset(enemyObj, path);
            Debug.Log($"Enemy prefab created at: {path}");
            #endif
            
            DestroyImmediate(enemyObj);
        }

        [ContextMenu("Create All Prefabs")]
        public void CreateAllPrefabs()
        {
            CreateUnitPrefab();
            CreateEnemyPrefab();
            Debug.Log("All prefabs created!");
        }
    }
}

