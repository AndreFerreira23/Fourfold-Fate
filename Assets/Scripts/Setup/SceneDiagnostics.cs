using UnityEngine;
using FourfoldFate.Data;
using FourfoldFate.Roguelike;
using FourfoldFate.Party;
using FourfoldFate.Core;
using FourfoldFate.UI;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Diagnostic tool to check if scene is set up correctly.
    /// </summary>
    public class SceneDiagnostics : MonoBehaviour
    {
        [ContextMenu("Run Diagnostics")]
        public void RunDiagnostics()
        {
            Debug.Log("=== SCENE DIAGNOSTICS ===");
            
            // Check managers
            CheckManager<GameDataManager>("GameDataManager");
            CheckManager<Balance.BalanceManager>("BalanceManager");
            CheckManager<RunManager>("RunManager");
            CheckManager<EncounterManager>("EncounterManager");
            CheckManager<ProgressionManager>("ProgressionManager");
            CheckManager<PartyManager>("PartyManager");
            CheckManager<Relics.RelicManager>("RelicManager");
            CheckManager<BattleManager>("BattleManager");
            CheckManager<UIManager>("UIManager");
            
            // Check UI
            CheckUI();
            
            // Check data
            CheckData();
            
            Debug.Log("=== END DIAGNOSTICS ===");
        }

        private void CheckManager<T>(string name) where T : MonoBehaviour
        {
            T manager = FindFirstObjectByType<T>();
            if (manager == null)
            {
                Debug.LogError($"❌ {name} is MISSING from scene!");
            }
            else
            {
                Debug.Log($"✅ {name} found: {manager.gameObject.name}");
            }
        }

        private void CheckUI()
        {
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogError("❌ UIManager is MISSING!");
                return;
            }

            if (uiManager.mainMenuUI == null)
                Debug.LogError("❌ MainMenuUI is not assigned to UIManager!");
            else
                Debug.Log($"✅ MainMenuUI assigned: {uiManager.mainMenuUI.gameObject.name}");

            if (uiManager.battleArenaUI == null)
                Debug.LogError("❌ BattleArenaUI is not assigned to UIManager!");
            else
                Debug.Log($"✅ BattleArenaUI assigned: {uiManager.battleArenaUI.gameObject.name}");
        }

        private void CheckData()
        {
            if (GameDataManager.Instance == null)
            {
                Debug.LogError("❌ GameDataManager.Instance is null!");
                return;
            }

            var playerUnits = GameDataManager.Instance.GetAllPlayerUnits();
            Debug.Log($"Player units loaded: {playerUnits?.Count ?? 0}");

            var encounters = GameDataManager.Instance.GetAllEncounters();
            Debug.Log($"Encounters loaded: {encounters?.Count ?? 0}");

            if (playerUnits == null || playerUnits.Count == 0)
                Debug.LogError("❌ No player units loaded! Check UnitDefinitions.cs");

            if (encounters == null || encounters.Count == 0)
                Debug.LogError("❌ No encounters loaded! Check EncounterDefinitions.cs");
        }

        private void Start()
        {
            // Auto-run diagnostics on start
            RunDiagnostics();
        }
    }
}

