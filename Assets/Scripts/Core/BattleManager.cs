using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Manages autobattler combat encounters.
    /// Handles turn-based automatic combat between teams.
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [Header("Teams")]
        [SerializeField] private List<Unit> playerTeam = new List<Unit>();
        [SerializeField] private List<Unit> enemyTeam = new List<Unit>();

        public List<Unit> PlayerTeam => new List<Unit>(playerTeam);
        public List<Unit> EnemyTeam => new List<Unit>(enemyTeam);
        
        [Header("Battle State")]
        [SerializeField] private bool isBattleActive = false;
        [SerializeField] private float battleTimer = 0f;
        
        private BattleState currentState = BattleState.Preparation;

        public bool IsBattleActive => isBattleActive;
        public BattleState CurrentState => currentState;

        private void Update()
        {
            if (isBattleActive)
            {
                battleTimer += Time.deltaTime;
                UpdateBattle();
            }
        }

        public void StartBattle(List<Unit> playerUnits, List<Unit> enemyUnits)
        {
            playerTeam = new List<Unit>(playerUnits);
            enemyTeam = new List<Unit>(enemyUnits);
            
            InitializeTeams();
            isBattleActive = true;
            currentState = BattleState.Active;
            battleTimer = 0f;
            
            OnBattleStarted?.Invoke();
        }

        private void InitializeTeams()
        {
            // Set targets for all units
            foreach (var unit in playerTeam)
            {
                unit.SetTarget(GetNearestEnemy(unit, enemyTeam));
                unit.OnUnitDied += OnUnitDied;
            }
            
            foreach (var unit in enemyTeam)
            {
                unit.SetTarget(GetNearestEnemy(unit, playerTeam));
                unit.OnUnitDied += OnUnitDied;
            }
        }

        private Unit GetNearestEnemy(Unit unit, List<Unit> enemies)
        {
            if (enemies == null || enemies.Count == 0) return null;
            
            Unit nearest = null;
            float nearestDistance = float.MaxValue;
            
            foreach (var enemy in enemies)
            {
                if (enemy == null || !enemy.IsAlive) continue;
                
                float distance = Vector3.Distance(unit.transform.position, enemy.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = enemy;
                }
            }
            
            return nearest;
        }

        private void UpdateBattle()
        {
            // Update targets for units (in case current target died)
            UpdateTargets();
            
            // Check win/lose conditions
            if (playerTeam.All(u => u == null || !u.IsAlive))
            {
                EndBattle(BattleResult.Defeat);
                return;
            }
            
            if (enemyTeam.All(u => u == null || !u.IsAlive))
            {
                EndBattle(BattleResult.Victory);
                return;
            }
        }

        private void UpdateTargets()
        {
            foreach (var unit in playerTeam)
            {
                if (unit == null || !unit.IsAlive) continue;
                if (unit.Target == null || !unit.Target.IsAlive)
                {
                    unit.SetTarget(GetNearestEnemy(unit, enemyTeam));
                }
            }
            
            foreach (var unit in enemyTeam)
            {
                if (unit == null || !unit.IsAlive) continue;
                if (unit.Target == null || !unit.Target.IsAlive)
                {
                    unit.SetTarget(GetNearestEnemy(unit, playerTeam));
                }
            }
        }

        private void OnUnitDied(Unit unit)
        {
            // Remove from appropriate team
            playerTeam.Remove(unit);
            enemyTeam.Remove(unit);
        }

        private void EndBattle(BattleResult result)
        {
            isBattleActive = false;
            currentState = BattleState.Ended;
            
            OnBattleEnded?.Invoke(result);
        }

        // Events
        public System.Action OnBattleStarted;
        public System.Action<BattleResult> OnBattleEnded;
    }

    public enum BattleState
    {
        Preparation,
        Active,
        Paused,
        Ended
    }

    public enum BattleResult
    {
        Victory,
        Defeat,
        Draw
    }
}

