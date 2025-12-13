using System.Collections.Generic;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Base interface for all agents in the system.
    /// Agents handle different aspects of game development and design.
    /// </summary>
    public interface IAgent
    {
        string AgentName { get; }
        string Description { get; }
        
        /// <summary>
        /// Process a request and return a response
        /// </summary>
        AgentResponse ProcessRequest(AgentRequest request);
    }

    /// <summary>
    /// Request structure for agent communication
    /// </summary>
    public class AgentRequest
    {
        public string RequestType { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public string Context { get; set; }
    }

    /// <summary>
    /// Response structure from agent processing
    /// </summary>
    public class AgentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public List<string> Suggestions { get; set; } = new List<string>();
    }
}

