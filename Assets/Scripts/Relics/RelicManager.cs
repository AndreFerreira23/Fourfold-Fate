using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Manages relics collected during a run.
    /// </summary>
    public class RelicManager : MonoBehaviour
    {
        [Header("Relic Collection")]
        [SerializeField] private List<Relic> collectedRelics = new List<Relic>();
        [SerializeField] private int maxRelics = 30;

        [Header("Relic Pool")]
        [SerializeField] private List<Relic> availableRelics = new List<Relic>();

        public int CollectedRelicCount => collectedRelics.Count;
        public List<Relic> CollectedRelics => new List<Relic>(collectedRelics);

        /// <summary>
        /// Add a relic to the collection
        /// </summary>
        public bool AddRelic(Relic relic)
        {
            if (collectedRelics.Count >= maxRelics)
            {
                Debug.LogWarning("Relic inventory is full!");
                return false;
            }

            if (collectedRelics.Contains(relic))
            {
                Debug.LogWarning($"Relic {relic.relicName} is already collected.");
                return false;
            }

            collectedRelics.Add(relic);
            OnRelicCollected?.Invoke(relic);
            ApplyRelicToParty(relic);

            return true;
        }

        /// <summary>
        /// Remove a relic from the collection
        /// </summary>
        public bool RemoveRelic(Relic relic)
        {
            if (collectedRelics.Remove(relic))
            {
                OnRelicRemoved?.Invoke(relic);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Apply a relic's effects to all party members
        /// </summary>
        private void ApplyRelicToParty(Relic relic)
        {
            var partyManager = FindObjectOfType<Party.PartyManager>();
            if (partyManager != null)
            {
                foreach (var unit in partyManager.PartyMembers)
                {
                    if (unit != null)
                    {
                        relic.ApplyPassiveEffects(unit);
                    }
                }
            }
        }

        /// <summary>
        /// Apply all relic effects when combat starts
        /// </summary>
        public void OnCombatStart(List<Unit> party)
        {
            foreach (var relic in collectedRelics)
            {
                foreach (var unit in party)
                {
                    if (unit != null)
                    {
                        relic.OnCombatStart(unit);
                    }
                }
            }
        }

        /// <summary>
        /// Apply all relic effects when combat ends
        /// </summary>
        public void OnCombatEnd(List<Unit> party)
        {
            foreach (var relic in collectedRelics)
            {
                foreach (var unit in party)
                {
                    if (unit != null)
                    {
                        relic.OnCombatEnd(unit);
                    }
                }
            }
        }

        /// <summary>
        /// Get a random relic from the available pool
        /// </summary>
        public Relic GetRandomRelic(Rarity? minRarity = null)
        {
            if (availableRelics == null || availableRelics.Count == 0)
                return null;

            var validRelics = availableRelics;
            if (minRarity.HasValue)
            {
                validRelics = availableRelics.FindAll(r => r.rarity >= minRarity.Value);
            }

            if (validRelics.Count == 0)
                validRelics = availableRelics;

            return validRelics[Random.Range(0, validRelics.Count)];
        }

        // Events
        public System.Action<Relic> OnRelicCollected;
        public System.Action<Relic> OnRelicRemoved;
    }
}

