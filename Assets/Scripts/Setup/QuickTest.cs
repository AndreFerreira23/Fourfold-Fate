using UnityEngine;
using FourfoldFate.Data;
using FourfoldFate.Core;
using FourfoldFate.Relics;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Quick test script to verify game systems are working.
    /// Attach to any GameObject and it will print status to Console.
    /// </summary>
    public class QuickTest : MonoBehaviour
    {
        [Header("Test Options")]
        [SerializeField] private bool runTestOnStart = true;
        [SerializeField] private bool testDataLoading = true;
        [SerializeField] private bool testUnitCreation = false;

        private void Start()
        {
            if (runTestOnStart)
            {
                RunTests();
            }
        }

        [ContextMenu("Run Tests")]
        public void RunTests()
        {
            Debug.Log("=== FOURFOLD FATE SYSTEM TEST ===");
            
            if (testDataLoading)
            {
                TestDataLoading();
            }
            
            if (testUnitCreation)
            {
                TestUnitCreation();
            }
            
            Debug.Log("=== TEST COMPLETE ===");
        }

        private void TestDataLoading()
        {
            Debug.Log("Testing Data Loading...");
            
            GameDataManager dataManager = GameDataManager.Instance;
            if (dataManager == null)
            {
                Debug.LogError("❌ GameDataManager not found!");
                return;
            }
            Debug.Log("✅ GameDataManager found");
            
            // Test getting a unit
            UnitData testUnit = dataManager.GetUnit("guardian_shield");
            if (testUnit != null)
            {
                Debug.Log($"✅ Unit loaded: {testUnit.unitName} (HP: {testUnit.MaxHealth}, DMG: {testUnit.AttackDamage})");
            }
            else
            {
                Debug.LogWarning("⚠️ Could not load 'guardian_shield' unit");
            }
            
            // Test getting an enemy
            UnitData testEnemy = dataManager.GetUnit("briar_cairn_footpad");
            if (testEnemy != null)
            {
                Debug.Log($"✅ Enemy loaded: {testEnemy.unitName} (HP: {testEnemy.MaxHealth}, DMG: {testEnemy.AttackDamage})");
            }
            else
            {
                Debug.LogWarning("⚠️ Could not load 'briar_cairn_footpad' enemy");
            }
            
            // Test getting an encounter
            var encounters = dataManager.GetEncountersForLevel(5);
            Debug.Log($"✅ Found {encounters.Count} encounters for level 5");
            
            // Test getting relics
            Relic testRelic = dataManager.GetRelic("arcane_battery");
            if (testRelic != null)
            {
                Debug.Log($"✅ Relic loaded: {testRelic.relicName}");
            }
            else
            {
                Debug.LogWarning("⚠️ Could not load 'arcane_battery' relic");
            }
        }

        private void TestUnitCreation()
        {
            Debug.Log("Testing Unit Creation...");
            
            GameDataManager dataManager = GameDataManager.Instance;
            if (dataManager == null)
            {
                Debug.LogError("❌ GameDataManager not found!");
                return;
            }
            
            UnitData unitData = dataManager.GetUnit("guardian_shield");
            if (unitData == null)
            {
                Debug.LogError("❌ Could not get unit data");
                return;
            }
            
            // Try to create a unit GameObject
            GameObject unitObj = new GameObject("TestUnit");
            Unit unit = unitObj.AddComponent<Unit>();
            
            if (unit != null)
            {
                unit.Initialize(unitData);
                Debug.Log($"✅ Unit created: {unit.Data.unitName} at position {unit.transform.position}");
                Debug.Log($"   Health: {unit.CurrentHealth}/{unit.Data.MaxHealth}");
                Debug.Log($"   Mana: {unit.CurrentMana}/{unit.Data.MaxMana}");
                
                // Clean up test unit
                Destroy(unitObj, 2f);
            }
            else
            {
                Debug.LogError("❌ Could not create Unit component");
            }
        }
    }
}

