using System.Collections.Generic;
using FourfoldFate.Core;
using FourfoldFate.Core.Archetypes;
using UnityEngine;

namespace FourfoldFate.Party
{
    /// <summary>
    /// Manages party composition, unlocks, and synergy bonuses.
    /// </summary>
    public class PartyManager : MonoBehaviour
    {
        [Header("Party State")]
        public List<Unit> partyMembers = new List<Unit>();
        public int maxPartySize = 1; // Unlocks at levels 1, 5, 10, 15

        [Header("Unlocks")]
        public int partyUnlockLevel1 = 1;
        public int partyUnlockLevel5 = 5;
        public int partyUnlockLevel10 = 10;
        public int partyUnlockLevel15 = 15;

        public static PartyManager Instance { get; private set; }

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
        /// Add a unit to the party.
        /// </summary>
        public bool AddPartyMember(Unit unit)
        {
            if (partyMembers.Count >= maxPartySize) return false;
            if (partyMembers.Contains(unit)) return false;

            partyMembers.Add(unit);
            return true;
        }

        /// <summary>
        /// Remove a unit from the party.
        /// </summary>
        public bool RemovePartyMember(Unit unit)
        {
            return partyMembers.Remove(unit);
        }

        /// <summary>
        /// Update max party size based on unlocks.
        /// </summary>
        public void UpdateMaxPartySize(int playerLevel)
        {
            if (playerLevel >= partyUnlockLevel15) maxPartySize = 4;
            else if (playerLevel >= partyUnlockLevel10) maxPartySize = 3;
            else if (playerLevel >= partyUnlockLevel5) maxPartySize = 2;
            else maxPartySize = 1;
        }

        /// <summary>
        /// Get active synergy tags from party.
        /// </summary>
        public HashSet<SynergyTag> GetActiveSynergies()
        {
            HashSet<SynergyTag> activeTags = new HashSet<SynergyTag>();
            Dictionary<SynergyTag, int> tagCounts = new Dictionary<SynergyTag, int>();

            foreach (var member in partyMembers)
            {
                if (member == null) continue;

                if (!tagCounts.ContainsKey(member.synergyTag1))
                    tagCounts[member.synergyTag1] = 0;
                tagCounts[member.synergyTag1]++;

                if (!tagCounts.ContainsKey(member.synergyTag2))
                    tagCounts[member.synergyTag2] = 0;
                tagCounts[member.synergyTag2]++;
            }

            // Synergy activates with 2+ units sharing a tag
            foreach (var kvp in tagCounts)
            {
                if (kvp.Value >= 2)
                {
                    activeTags.Add(kvp.Key);
                }
            }

            return activeTags;
        }
    }
}

