using System.Collections.Generic;
using UnityEngine;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// ScriptableObject containing run configuration.
    /// </summary>
    [CreateAssetMenu(fileName = "New Run Data", menuName = "Fourfold Fate/Run Data")]
    public class RunData : ScriptableObject
    {
        [Header("Run Configuration")]
        public string runName;
        public int maxFloors = 5;
        public int encountersPerFloor = 3;
        
        [Header("Starting Units")]
        public List<UnitData> startingUnits = new List<UnitData>();
        
        [Header("Difficulty")]
        public float difficultyMultiplier = 1f;
        public AnimationCurve difficultyCurve = AnimationCurve.Linear(0, 1, 1, 2);
    }
}

