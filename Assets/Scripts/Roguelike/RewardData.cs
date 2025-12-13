using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// ScriptableObject containing reward configuration.
    /// </summary>
    [CreateAssetMenu(fileName = "New Reward", menuName = "Fourfold Fate/Reward Data")]
    public class RewardData : ScriptableObject
    {
        [Header("Reward Info")]
        public string rewardName;
        public string description;
        public Sprite icon;
        
        [Header("Reward Type")]
        public RewardType rewardType;
        
        [Header("Reward Values")]
        public int goldAmount = 0;
        public UnitData unitReward;
        public AbilityData abilityReward;
        public StatUpgrade statUpgrade;
    }

    public enum RewardType
    {
        Gold,
        Unit,
        Ability,
        StatUpgrade,
        Item
    }

    [System.Serializable]
    public class StatUpgrade
    {
        public string statName;
        public float value;
    }
}

