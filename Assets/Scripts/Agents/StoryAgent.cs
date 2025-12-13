using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Agent responsible for story, lore, and thematic elements.
    /// Debates and suggests narrative themes, character backstories, world-building, etc.
    /// </summary>
    public class StoryAgent : MonoBehaviour, IAgent
    {
        public string AgentName => "Story Agent";
        public string Description => "Handles story, lore, and thematic elements of the game";

        [Header("Story Settings")]
        [SerializeField] private List<string> currentThemes = new List<string>();
        [SerializeField] private List<string> loreElements = new List<string>();

        private void Awake()
        {
            InitializeDefaultLore();
        }

        public AgentResponse ProcessRequest(AgentRequest request)
        {
            var response = new AgentResponse { Success = true };

            switch (request.RequestType.ToLower())
            {
                case "suggest_theme":
                    response = SuggestTheme(request);
                    break;
                case "debate_theme":
                    response = DebateTheme(request);
                    break;
                case "create_lore":
                    response = CreateLore(request);
                    break;
                case "validate_story":
                    response = ValidateStory(request);
                    break;
                default:
                    response.Success = false;
                    response.Message = $"Unknown request type: {request.RequestType}";
                    break;
            }

            return response;
        }

        private AgentResponse SuggestTheme(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Theme suggestions generated"
            };

            // Example themes for a roguelike autobattler
            var themes = new List<string>
            {
                "Fate and Destiny - Characters bound by cosmic forces",
                "Four Elements - Fire, Water, Earth, Air as core mechanics",
                "Time Loops - Each run is a cycle of fate",
                "Ascension - Climbing through layers of reality",
                "Balance - Maintaining equilibrium between opposing forces"
            };

            response.Data["themes"] = themes;
            response.Suggestions = themes;

            return response;
        }

        private AgentResponse DebateTheme(AgentRequest request)
        {
            var theme = request.Parameters.ContainsKey("theme") 
                ? request.Parameters["theme"].ToString() 
                : "";

            var response = new AgentResponse
            {
                Success = true,
                Message = $"Debating theme: {theme}"
            };

            // Analyze theme pros/cons
            var pros = new List<string>
            {
                "Fits roguelike structure well",
                "Allows for meaningful player choices",
                "Creates emotional investment"
            };

            var cons = new List<string>
            {
                "May be too complex for casual players",
                "Requires careful narrative integration"
            };

            response.Data["pros"] = pros;
            response.Data["cons"] = cons;
            response.Data["recommendation"] = "Theme is viable with proper implementation";

            return response;
        }

        private AgentResponse CreateLore(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Lore element created"
            };

            var lore = new Dictionary<string, string>
            {
                ["world"] = "A realm where fate is woven in four threads",
                ["purpose"] = "Characters seek to rewrite their destiny",
                ["conflict"] = "Ancient forces vie for control of fate"
            };

            response.Data["lore"] = lore;

            return response;
        }

        private AgentResponse ValidateStory(AgentRequest request)
        {
            var response = new AgentResponse
            {
                Success = true,
                Message = "Story validation complete"
            };

            var validation = new Dictionary<string, object>
            {
                ["coherence"] = true,
                ["themes_consistent"] = true,
                ["lore_established"] = true
            };

            response.Data["validation"] = validation;

            return response;
        }

        private void InitializeDefaultLore()
        {
            loreElements.Add("The Fourfold Fate - A cosmic force that binds all existence");
            loreElements.Add("The Weavers - Beings who manipulate the threads of fate");
            loreElements.Add("The Shattered Realms - Fragmented dimensions where battles take place");
        }
    }
}

