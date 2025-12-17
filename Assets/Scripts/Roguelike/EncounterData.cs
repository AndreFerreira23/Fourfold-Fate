using System.Collections.Generic;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Encounter data structure for runtime use.
    /// </summary>
    [System.Serializable]
    public class EncounterData
    {
        public string encounterId;
        public string encounterName;
        public string description;
        public int minLevel;
        public int maxLevel;
        public bool isMiniboss;
        public bool isMajorMiniboss;
        public bool isFinalBoss;
        public List<UnitData> enemyUnits = new List<UnitData>();
        public int goldReward;
    }
}

