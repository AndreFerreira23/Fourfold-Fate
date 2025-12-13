using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// ScriptableObject containing encounter configuration.
    /// </summary>
    [CreateAssetMenu(fileName = "New Encounter", menuName = "Fourfold Fate/Encounter Data")]
    public class EncounterData : ScriptableObject
    {
        [Header("Encounter Info")]
        public string encounterName;
        public string description;
        
        [Header("Level Range")]
        public int minLevel = 1;
        public int maxLevel = 100;
        
        [Header("Encounter Type")]
        public bool isMiniboss = false;
        public bool isMajorMiniboss = false;
        public bool isFinalBoss = false;
        
        [Header("Enemies")]
        public List<UnitData> enemyUnits = new List<UnitData>();
        
        [Header("Rewards")]
        public int goldReward = 50;
        public List<RewardData> possibleRewards = new List<RewardData>();
    }
}

