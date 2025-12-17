using UnityEngine;

namespace FourfoldFate.Balance
{
    /// <summary>
    /// Manages balance calculations and difficulty scaling.
    /// </summary>
    public class BalanceManager : MonoBehaviour
    {
        [Header("Balance Config")]
        public BalanceConfig config = new BalanceConfig();

        public static BalanceManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                
                // Only call DontDestroyOnLoad if this is a root GameObject
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Calculate difficulty multiplier for a level.
        /// </summary>
        public float GetDifficultyMultiplier(int level)
        {
            return Mathf.Pow(config.levelScalingMultiplier, level - 1);
        }

        /// <summary>
        /// Calculate scaled damage for a level.
        /// </summary>
        public float GetScaledDamage(float baseDamage, int level)
        {
            return baseDamage * GetDifficultyMultiplier(level);
        }
    }
}

