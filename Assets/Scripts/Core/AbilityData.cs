using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// ScriptableObject containing ability configuration.
    /// </summary>
    [CreateAssetMenu(fileName = "New Ability", menuName = "Fourfold Fate/Ability Data")]
    public class AbilityData : ScriptableObject
    {
        [Header("Basic Info")]
        public string abilityName;
        public string description;
        public Sprite icon;
        
        [Header("Ability Type")]
        public AbilityType AbilityType;
        
        [Header("Costs")]
        public float ManaCost = 20f;
        public float Cooldown = 5f;
        
        [Header("Effects")]
        public float Damage = 0f;
        public float HealAmount = 0f;
        public float Duration = 0f;
        
        [Header("Targeting")]
        public TargetType TargetType;
        public float Range = 5f;
    }

    public enum AbilityType
    {
        Damage,
        Heal,
        Buff,
        Debuff,
        Utility
    }

    public enum TargetType
    {
        Self,
        Ally,
        Enemy,
        AllEnemies,
        AllAllies
    }
}

