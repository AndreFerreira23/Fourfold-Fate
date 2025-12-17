using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Core
{
    /// <summary>
    /// Manages turn-based combat encounters between party and enemies.
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [Header("Combat State")]
        public List<Unit> partyUnits = new List<Unit>();
        public List<Unit> enemyUnits = new List<Unit>();
        public bool isCombatActive = false;
        public bool isPlayerTurn = true;
        public Unit currentActor = null;
        public int currentTurnIndex = 0;

        [Header("Combat Events")]
        public System.Action<Unit, Unit, float> OnDamageDealt;
        public System.Action<Unit, float> OnUnitHealed;
        public System.Action<bool> OnCombatEnded; // bool = victory

        public static BattleManager Instance { get; private set; }

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
        /// Start a combat encounter.
        /// </summary>
        public void StartCombat(List<Unit> party, List<Unit> enemies)
        {
            partyUnits = new List<Unit>(party);
            enemyUnits = new List<Unit>(enemies);
            isCombatActive = true;
            isPlayerTurn = true;
            currentTurnIndex = 0;
            
            // Start turn-based combat
            StartCoroutine(CombatLoop());
        }

        /// <summary>
        /// Main combat loop - turn-based.
        /// </summary>
        private IEnumerator CombatLoop()
        {
            while (isCombatActive)
            {
                // Clean up dead units
                partyUnits.RemoveAll(u => u == null || u.CurrentHealth <= 0);
                enemyUnits.RemoveAll(u => u == null || u.CurrentHealth <= 0);

                // Check for combat end
                if (partyUnits.Count == 0)
                {
                    EndCombat(false);
                    yield break;
                }
                if (enemyUnits.Count == 0)
                {
                    EndCombat(true);
                    yield break;
                }

                // Player turn
                if (isPlayerTurn)
                {
                    yield return StartCoroutine(PlayerTurn());
                }
                // Enemy turn
                else
                {
                    yield return StartCoroutine(EnemyTurn());
                }

                // Switch turns
                isPlayerTurn = !isPlayerTurn;
                yield return new WaitForSeconds(0.5f);
            }
        }

        /// <summary>
        /// Handle player turn - wait for player input via UI.
        /// </summary>
        private IEnumerator PlayerTurn()
        {
            // Wait for player to make actions via UI
            // The UI will call PlayerAttack() or PlayerUseAbility()
            // Then call EndPlayerTurn() when done
            
            float turnTimeout = 30f; // 30 second timeout
            float elapsed = 0f;
            
            while (isPlayerTurn && elapsed < turnTimeout)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            // Auto-end turn if timeout (prevents infinite waiting)
            if (isPlayerTurn)
            {
                isPlayerTurn = false;
            }
        }

        /// <summary>
        /// Handle enemy turn - simple AI.
        /// </summary>
        private IEnumerator EnemyTurn()
        {
            foreach (var enemy in enemyUnits)
            {
                if (enemy == null || enemy.CurrentHealth <= 0) continue;
                if (partyUnits.Count == 0) break;

                // Simple AI: Attack the first party member
                Unit target = partyUnits[0];
                enemy.Attack(target);
                if (OnDamageDealt != null)
                    OnDamageDealt(enemy, target, enemy.attackDamage);

                yield return new WaitForSeconds(0.8f);
            }
        }

        /// <summary>
        /// Player uses an ability (called from UI).
        /// </summary>
        public void PlayerUseAbility(Unit caster, Ability ability, Unit target = null)
        {
            if (!isCombatActive || !isPlayerTurn) return;
            if (caster == null || ability == null) return;

            if (target == null && enemyUnits.Count > 0)
                target = enemyUnits[0];

            caster.UseAbility(ability, target);
        }

        /// <summary>
        /// Player attacks (called from UI).
        /// </summary>
        public void PlayerAttack(Unit attacker, Unit target)
        {
            if (!isCombatActive || !isPlayerTurn) return;
            if (attacker == null || target == null) return;

            attacker.Attack(target);
            if (OnDamageDealt != null)
                OnDamageDealt(attacker, target, attacker.attackDamage);
        }

        /// <summary>
        /// End player turn (called from UI when done).
        /// </summary>
        public void EndPlayerTurn()
        {
            if (isCombatActive && isPlayerTurn)
            {
                isPlayerTurn = false;
            }
        }

        /// <summary>
        /// End combat and return result.
        /// </summary>
        private void EndCombat(bool victory)
        {
            isCombatActive = false;
            if (OnCombatEnded != null)
                OnCombatEnded(victory);
        }
    }
}

