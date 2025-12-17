using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Manages player progression (level-ups, stats, unlocks).
    /// </summary>
    public class ProgressionManager : MonoBehaviour
    {
        public static ProgressionManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Grant rewards after completing an encounter.
        /// </summary>
        public void GrantRewards(int floor, int encounter)
        {
            // Grant gold, experience, etc.
        }
    }
}

