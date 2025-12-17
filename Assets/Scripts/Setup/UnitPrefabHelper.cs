using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Helper component to easily set up unit prefabs with sprites.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class UnitPrefabHelper : MonoBehaviour
    {
        [Header("Unit Setup")]
        public Sprite unitSprite;
        public Color spriteColor = Color.white;

        [Header("Display")]
        public bool showHealthBar = true;
        public Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);

        private SpriteRenderer spriteRenderer;
        private Unit unit;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            unit = GetComponent<Unit>();

            if (spriteRenderer != null && unitSprite != null)
            {
                spriteRenderer.sprite = unitSprite;
                spriteRenderer.color = spriteColor;
            }

            // Set sorting layer for 2D
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingLayerName = "Units";
                spriteRenderer.sortingOrder = 0;
            }
        }

        private void Update()
        {
            // Update sprite color based on health (optional visual feedback)
            if (unit != null && spriteRenderer != null)
            {
                float healthPercent = unit.CurrentHealth / unit.MaxHealth;
                if (healthPercent < 0.3f)
                {
                    spriteRenderer.color = Color.red;
                }
                else if (healthPercent < 0.6f)
                {
                    spriteRenderer.color = Color.yellow;
                }
                else
                {
                    spriteRenderer.color = spriteColor;
                }
            }
        }
    }
}

