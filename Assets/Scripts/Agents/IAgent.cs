namespace FourfoldFate.Agents
{
    /// <summary>
    /// Base interface for AI agents (Story, Balance, Code).
    /// </summary>
    public interface IAgent
    {
        string ProcessRequest(string request);
    }
}

