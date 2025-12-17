using System.Collections.Generic;
using FourfoldFate.Core;

namespace FourfoldFate.Roguelike
{
    /// <summary>
    /// Data structure for saving/loading runs.
    /// </summary>
    [System.Serializable]
    public class RunData
    {
        public int currentLevel;
        public bool isRunActive;
        public List<string> partyMemberIds = new List<string>();
        public List<string> relicIds = new List<string>();
        public int gold;
        public int experience;
    }
}

