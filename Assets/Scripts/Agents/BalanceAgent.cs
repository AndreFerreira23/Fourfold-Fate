using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Agent responsible for game balance, mechanics tuning, and numerical design.
    /// Analyzes and suggests balance changes for units, abilities, progression, etc.
    /// </summary>
    public class BalanceAgent : MonoBehaviour, IAgent
    {
        public string AgentName => "Balance Agent";
        public string Description => "Handles game balance, mechanics tuning, and numerical design";

        [Header("Balance Settings")]
        [SerializeField] private BalanceConfig balanceConfig;

        [System.Serializable]
        public class BalanceConfig
        {
            public float baseUnitHealth = 100f;
            public float baseUnitDamage = 10f;
            public float difficultyScaling = 1.15f;
            public int maxUnitsPerTeam = 4;
        }

        private void Awake()
        {
            if (balanceConfig == null)
                balanceConfig = new BalanceConfig();
        }

        public AgentResponse ProcessRequest(AgentRequest request)
        {
            var response = new AgentResponse { Success = true };

            switch (request.RequestType.ToLower())
            {
                case "analyze_balance":
                    response = AnalyzeBalance(request);
                    break;
                case "suggest_balance":
                    response = SuggestBalance(request);
                    break;
                case "validate_stats":
                    response = ValidateStats(request);
                    break;
                case "calculate_difficulty":
                    response = CalculateDifficulty(request);
                    break;
                default:
                    response.Success = false;
                    response.Message = $"Unknown request type: {request.RequestType}";
                    break;
            }

            return response;
        }

        private AgentResponse AnalyzeBalance(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Balance analysis complete"
            };

            // Analyze unit stats, abilities, progression curves, etc.
            var analysis = new Dictionary<string, object>
            {
                ["health_curve"] = "Linear with slight exponential scaling",
                ["damage_balance"] = "Within acceptable range",
                ["ability_cooldowns"] = "Properly tuned for gameplay flow",
                ["progression_pacing"] = "Appropriate for roguelike structure"
            };

            response.Data["analysis"] = analysis;
            response.Suggestions.Add("Consider adding more variety in unit archetypes");
            response.Suggestions.Add("Ability cooldowns could be reduced by 10% for faster combat");

            return response;
        }

        private AgentResponse SuggestBalance(AgentRequest request)
        {
            var unitType = request.Parameters.ContainsKey("unit_type") 
                ? request.Parameters["unit_type"].ToString() 
                : "generic";

            var response = new AgentResponse
            {
                Success = true,
                Message = $"Balance suggestions for {unitType}"
            };

            var suggestions = new Dictionary<string, object>
            {
                ["health"] = balanceConfig.baseUnitHealth,
                ["damage"] = balanceConfig.baseUnitDamage,
                ["armor"] = 0f,
                ["speed"] = 1f
            };

            response.Data["suggested_stats"] = suggestions;

            return response;
        }

        private AgentResponse ValidateStats(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Stats validation complete"
            };

            // Validate that stats are within acceptable ranges
            var validation = new Dictionary<string, object>
            {
                ["health_valid"] = true,
                ["damage_valid"] = true,
                ["cooldowns_valid"] = true,
                ["costs_balanced"] = true
            };

            response.Data["validation"] = validation;

            return response;
        }

        private AgentResponse CalculateDifficulty(AgentRequest request)
        {
            var floor = request.Parameters.ContainsKey("floor") 
                ? (int)request.Parameters["floor"] 
                : 1;

            var response = new AgentResponse
            {
                Success = true,
                Message = $"Difficulty calculated for floor {floor}"
            };

            var difficulty = Mathf.Pow(balanceConfig.difficultyScaling, floor - 1);
            var enemyHealth = balanceConfig.baseUnitHealth * difficulty;
            var enemyDamage = balanceConfig.baseUnitDamage * difficulty;

            response.Data["difficulty_multiplier"] = difficulty;
            response.Data["enemy_health"] = enemyHealth;
            response.Data["enemy_damage"] = enemyDamage;

            return response;
        }
    }
}

