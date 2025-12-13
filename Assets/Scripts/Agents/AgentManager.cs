using System.Collections.Generic;
using UnityEngine;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Central manager for all agents in the system.
    /// Coordinates communication between different agents.
    /// </summary>
    public class AgentManager : MonoBehaviour
    {
        [Header("Agents")]
        [SerializeField] private StoryAgent storyAgent;
        [SerializeField] private BalanceAgent balanceAgent;
        [SerializeField] private CodeAgent codeAgent;

        private Dictionary<string, IAgent> agents = new Dictionary<string, IAgent>();

        private void Awake()
        {
            RegisterAgents();
        }

        private void RegisterAgents()
        {
            if (storyAgent != null)
                agents["story"] = storyAgent;
            
            if (balanceAgent != null)
                agents["balance"] = balanceAgent;
            
            if (codeAgent != null)
                agents["code"] = codeAgent;
        }

        /// <summary>
        /// Send a request to a specific agent
        /// </summary>
        public AgentResponse RequestAgent(string agentName, AgentRequest request)
        {
            if (agents.TryGetValue(agentName.ToLower(), out IAgent agent))
            {
                return agent.ProcessRequest(request);
            }

            return new AgentResponse
            {
                Success = false,
                Message = $"Agent '{agentName}' not found"
            };
        }

        /// <summary>
        /// Get a specific agent by name
        /// </summary>
        public T GetAgent<T>(string agentName) where T : class, IAgent
        {
            if (agents.TryGetValue(agentName.ToLower(), out IAgent agent))
            {
                return agent as T;
            }
            return null;
        }

        /// <summary>
        /// Get all registered agents
        /// </summary>
        public Dictionary<string, IAgent> GetAllAgents()
        {
            return new Dictionary<string, IAgent>(agents);
        }
    }
}

