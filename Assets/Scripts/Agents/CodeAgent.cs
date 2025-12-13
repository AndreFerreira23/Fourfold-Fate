using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Agent responsible for code generation, architecture suggestions, and implementation patterns.
    /// Helps with code structure, best practices, and Unity-specific implementations.
    /// </summary>
    public class CodeAgent : MonoBehaviour, IAgent
    {
        public string AgentName => "Code Agent";
        public string Description => "Handles code generation, architecture, and implementation patterns";

        [Header("Code Settings")]
        [SerializeField] private List<string> codePatterns = new List<string>();
        [SerializeField] private List<string> architecturePrinciples = new List<string>();

        private void Awake()
        {
            InitializeCodePrinciples();
        }

        public AgentResponse ProcessRequest(AgentRequest request)
        {
            var response = new AgentResponse { Success = true };

            switch (request.RequestType.ToLower())
            {
                case "suggest_architecture":
                    response = SuggestArchitecture(request);
                    break;
                case "generate_code":
                    response = GenerateCode(request);
                    break;
                case "review_code":
                    response = ReviewCode(request);
                    break;
                case "optimize_code":
                    response = OptimizeCode(request);
                    break;
                default:
                    response.Success = false;
                    response.Message = $"Unknown request type: {request.RequestType}";
                    break;
            }

            return response;
        }

        private AgentResponse SuggestArchitecture(AgentRequest request)
        {
            var feature = request.Parameters.ContainsKey("feature") 
                ? request.Parameters["feature"].ToString() 
                : "generic";

            var response = new AgentResponse
            {
                Success = true,
                Message = $"Architecture suggestions for {feature}"
            };

            var suggestions = new Dictionary<string, object>
            {
                ["pattern"] = "Component-based architecture (Unity-friendly)",
                ["structure"] = "Separate data (ScriptableObjects) from logic (MonoBehaviours)",
                ["communication"] = "Use events for decoupled systems",
                ["data_persistence"] = "ScriptableObjects for game data, JSON for save files"
            };

            response.Data["architecture"] = suggestions;
            response.Suggestions.Add("Consider using a Manager pattern for core systems");
            response.Suggestions.Add("Implement interfaces for testability");

            return response;
        }

        private AgentResponse GenerateCode(AgentRequest request)
        {
            var componentType = request.Parameters.ContainsKey("component_type") 
                ? request.Parameters["component_type"].ToString() 
                : "generic";

            var response = new AgentResponse
            {
                Success = true,
                Message = $"Code generation template for {componentType}"
            };

            var template = GetCodeTemplate(componentType);
            response.Data["code_template"] = template;
            response.Data["best_practices"] = GetBestPractices(componentType);

            return response;
        }

        private AgentResponse ReviewCode(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Code review complete"
            };

            var review = new Dictionary<string, object>
            {
                ["performance"] = "Good - no obvious bottlenecks",
                ["maintainability"] = "Clear structure and naming",
                ["unity_best_practices"] = "Follows Unity conventions",
                ["suggestions"] = new List<string>
                {
                    "Consider caching GetComponent calls",
                    "Use object pooling for frequently instantiated objects"
                }
            };

            response.Data["review"] = review;

            return response;
        }

        private AgentResponse OptimizeCode(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Code optimization suggestions"
            };

            var optimizations = new List<string>
            {
                "Cache component references in Awake/Start",
                "Use object pooling for projectiles and effects",
                "Batch similar operations together",
                "Consider using Jobs System for heavy calculations"
            };

            response.Data["optimizations"] = optimizations;
            response.Suggestions = optimizations;

            return response;
        }

        private string GetCodeTemplate(string componentType)
        {
            return componentType switch
            {
                "manager" => "Use singleton pattern with instance check",
                "unit" => "Component-based with data/behavior separation",
                "ability" => "ScriptableObject-based for easy configuration",
                _ => "Standard MonoBehaviour with proper initialization"
            };
        }

        private List<string> GetBestPractices(string componentType)
        {
            return new List<string>
            {
                "Always null-check before accessing components",
                "Use [SerializeField] for private fields that need inspector access",
                "Implement proper cleanup in OnDestroy",
                "Use events for communication between systems"
            };
        }

        private void InitializeCodePrinciples()
        {
            architecturePrinciples.Add("Separation of Concerns");
            architecturePrinciples.Add("Single Responsibility Principle");
            architecturePrinciples.Add("Dependency Injection where appropriate");
            architecturePrinciples.Add("Event-driven communication");
            
            codePatterns.Add("Manager Pattern");
            codePatterns.Add("Component Pattern");
            codePatterns.Add("Observer Pattern");
            codePatterns.Add("State Pattern");
        }
    }
}

