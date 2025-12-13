using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.MetaProgression
{
    /// <summary>
    /// Manages meta-progression - permanent unlocks between runs.
    /// Expands variety, not raw power.
    /// </summary>
    public class MetaProgressionManager : MonoBehaviour
    {
        [Header("Unlocks")]
        [SerializeField] private List<string> unlockedCharacters = new List<string>();
        [SerializeField] private List<string> unlockedRelics = new List<string>();
        [SerializeField] private List<string> unlockedSynergyTags = new List<string>();
        [SerializeField] private List<string> unlockedDifficultyModifiers = new List<string>();
        
        [Header("Currency")]
        [SerializeField] private int metaCurrency = 0;

        private const string SAVE_KEY_CHARACTERS = "UnlockedCharacters";
        private const string SAVE_KEY_RELICS = "UnlockedRelics";
        private const string SAVE_KEY_SYNERGIES = "UnlockedSynergies";
        private const string SAVE_KEY_CURRENCY = "MetaCurrency";

        public int MetaCurrency => metaCurrency;
        public List<string> UnlockedCharacters => new List<string>(unlockedCharacters);
        public List<string> UnlockedRelics => new List<string>(unlockedRelics);

        private void Awake()
        {
            LoadMetaProgression();
        }

        /// <summary>
        /// Unlock a new character
        /// </summary>
        public bool UnlockCharacter(string characterId)
        {
            if (unlockedCharacters.Contains(characterId))
            {
                return false;
            }

            unlockedCharacters.Add(characterId);
            SaveMetaProgression();
            OnCharacterUnlocked?.Invoke(characterId);
            return true;
        }

        /// <summary>
        /// Unlock a new relic
        /// </summary>
        public bool UnlockRelic(string relicId)
        {
            if (unlockedRelics.Contains(relicId))
            {
                return false;
            }

            unlockedRelics.Add(relicId);
            SaveMetaProgression();
            OnRelicUnlocked?.Invoke(relicId);
            return true;
        }

        /// <summary>
        /// Unlock a new synergy tag
        /// </summary>
        public bool UnlockSynergyTag(string tagId)
        {
            if (unlockedSynergyTags.Contains(tagId))
            {
                return false;
            }

            unlockedSynergyTags.Add(tagId);
            SaveMetaProgression();
            OnSynergyTagUnlocked?.Invoke(tagId);
            return true;
        }

        /// <summary>
        /// Add meta currency (earned between runs)
        /// </summary>
        public void AddMetaCurrency(int amount)
        {
            metaCurrency += amount;
            SaveMetaProgression();
            OnMetaCurrencyChanged?.Invoke(metaCurrency);
        }

        /// <summary>
        /// Spend meta currency
        /// </summary>
        public bool SpendMetaCurrency(int amount)
        {
            if (metaCurrency >= amount)
            {
                metaCurrency -= amount;
                SaveMetaProgression();
                OnMetaCurrencyChanged?.Invoke(metaCurrency);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a character is unlocked
        /// </summary>
        public bool IsCharacterUnlocked(string characterId)
        {
            return unlockedCharacters.Contains(characterId);
        }

        /// <summary>
        /// Check if a relic is unlocked
        /// </summary>
        public bool IsRelicUnlocked(string relicId)
        {
            return unlockedRelics.Contains(relicId);
        }

        private void SaveMetaProgression()
        {
            // Save characters
            string charactersJson = JsonUtility.ToJson(new StringListWrapper(unlockedCharacters));
            PlayerPrefs.SetString(SAVE_KEY_CHARACTERS, charactersJson);
            
            // Save relics
            string relicsJson = JsonUtility.ToJson(new StringListWrapper(unlockedRelics));
            PlayerPrefs.SetString(SAVE_KEY_RELICS, relicsJson);
            
            // Save synergies
            string synergiesJson = JsonUtility.ToJson(new StringListWrapper(unlockedSynergyTags));
            PlayerPrefs.SetString(SAVE_KEY_SYNERGIES, synergiesJson);
            
            // Save currency
            PlayerPrefs.SetInt(SAVE_KEY_CURRENCY, metaCurrency);
            
            PlayerPrefs.Save();
        }

        private void LoadMetaProgression()
        {
            // Load characters
            if (PlayerPrefs.HasKey(SAVE_KEY_CHARACTERS))
            {
                string charactersJson = PlayerPrefs.GetString(SAVE_KEY_CHARACTERS);
                var wrapper = JsonUtility.FromJson<StringListWrapper>(charactersJson);
                unlockedCharacters = wrapper.items ?? new List<string>();
            }
            
            // Load relics
            if (PlayerPrefs.HasKey(SAVE_KEY_RELICS))
            {
                string relicsJson = PlayerPrefs.GetString(SAVE_KEY_RELICS);
                var wrapper = JsonUtility.FromJson<StringListWrapper>(relicsJson);
                unlockedRelics = wrapper.items ?? new List<string>();
            }
            
            // Load synergies
            if (PlayerPrefs.HasKey(SAVE_KEY_SYNERGIES))
            {
                string synergiesJson = PlayerPrefs.GetString(SAVE_KEY_SYNERGIES);
                var wrapper = JsonUtility.FromJson<StringListWrapper>(synergiesJson);
                unlockedSynergyTags = wrapper.items ?? new List<string>();
            }
            
            // Load currency
            metaCurrency = PlayerPrefs.GetInt(SAVE_KEY_CURRENCY, 0);
        }

        // Events
        public System.Action<string> OnCharacterUnlocked;
        public System.Action<string> OnRelicUnlocked;
        public System.Action<string> OnSynergyTagUnlocked;
        public System.Action<int> OnMetaCurrencyChanged;
    }

    [System.Serializable]
    public class StringListWrapper
    {
        public List<string> items;
        
        public StringListWrapper(List<string> items)
        {
            this.items = items;
        }
    }
}

