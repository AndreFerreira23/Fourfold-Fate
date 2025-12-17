using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.MetaProgression
{
    /// <summary>
    /// Manages permanent unlocks and meta-progression.
    /// </summary>
    public class MetaProgressionManager : MonoBehaviour
    {
        [Header("Unlocks")]
        public List<string> unlockedCharacters = new List<string>();
        public List<string> unlockedRelics = new List<string>();
        public List<string> unlockedSynergyTags = new List<string>();

        [Header("Meta Currency")]
        public int metaCurrency = 0;

        public static MetaProgressionManager Instance { get; private set; }

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
        /// Unlock a character.
        /// </summary>
        public void UnlockCharacter(string characterId)
        {
            if (!unlockedCharacters.Contains(characterId))
            {
                unlockedCharacters.Add(characterId);
            }
        }

        /// <summary>
        /// Unlock a relic.
        /// </summary>
        public void UnlockRelic(string relicId)
        {
            if (!unlockedRelics.Contains(relicId))
            {
                unlockedRelics.Add(relicId);
            }
        }

        /// <summary>
        /// Check if a character is unlocked.
        /// </summary>
        public bool IsCharacterUnlocked(string characterId)
        {
            return unlockedCharacters.Contains(characterId);
        }
    }
}

