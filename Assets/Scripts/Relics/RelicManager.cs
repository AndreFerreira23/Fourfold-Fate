using System.Collections.Generic;
using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Relics
{
    /// <summary>
    /// Manages relic collection and application to party.
    /// </summary>
    public class RelicManager : MonoBehaviour
    {
        [Header("Relic Collection")]
        public List<Relic> collectedRelics = new List<Relic>();

        public static RelicManager Instance { get; private set; }

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
        /// Add a relic to the collection.
        /// </summary>
        public void AddRelic(Relic relic)
        {
            if (relic != null && !collectedRelics.Contains(relic))
            {
                collectedRelics.Add(relic);
            }
        }

        /// <summary>
        /// Apply all relic effects to a unit.
        /// </summary>
        public void ApplyRelicEffects(Unit unit)
        {
            foreach (var relic in collectedRelics)
            {
                if (relic != null)
                {
                    relic.ApplyPassiveEffects(unit);
                }
            }
        }

        /// <summary>
        /// Notify relics that combat has started.
        /// </summary>
        public void OnCombatStart(Unit unit)
        {
            foreach (var relic in collectedRelics)
            {
                if (relic != null)
                {
                    relic.OnCombatStart(unit);
                }
            }
        }

        /// <summary>
        /// Notify relics that combat has ended.
        /// </summary>
        public void OnCombatEnd(Unit unit)
        {
            foreach (var relic in collectedRelics)
            {
                if (relic != null)
                {
                    relic.OnCombatEnd(unit);
                }
            }
        }
    }
}

