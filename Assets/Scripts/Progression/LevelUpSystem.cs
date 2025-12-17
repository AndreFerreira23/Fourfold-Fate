using FourfoldFate.Core;
using UnityEngine;

namespace FourfoldFate.Progression
{
    /// <summary>
    /// Manages level-up choices and stat upgrades.
    /// </summary>
    public class LevelUpSystem : MonoBehaviour
    {
        public static LevelUpSystem Instance { get; private set; }

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
        /// Level-up path types.
        /// </summary>
        public enum LevelUpPathType
        {
            Offense,
            Defense,
            Utility,
            Chaos
        }

        /// <summary>
        /// Apply a level-up path to a unit.
        /// </summary>
        public void ApplyLevelUp(Unit unit, LevelUpPathType path)
        {
            switch (path)
            {
                case LevelUpPathType.Offense:
                    unit.attackDamage += Random.Range(5f, 10f);
                    unit.attackSpeed += Random.Range(0.1f, 0.2f);
                    break;

                case LevelUpPathType.Defense:
                    unit.maxHealth += Random.Range(20f, 40f);
                    unit.armor += Random.Range(2f, 4f);
                    break;

                case LevelUpPathType.Utility:
                    unit.maxMana += Random.Range(10f, 20f);
                    unit.movementSpeed += Random.Range(0.5f, 1f);
                    break;

                case LevelUpPathType.Chaos:
                    // Random stat boost
                    float randomStat = Random.Range(0f, 1f);
                    if (randomStat < 0.33f)
                        unit.attackDamage += Random.Range(5f, 15f);
                    else if (randomStat < 0.66f)
                        unit.maxHealth += Random.Range(15f, 35f);
                    else
                        unit.maxMana += Random.Range(10f, 25f);
                    break;
            }
        }
    }
}

