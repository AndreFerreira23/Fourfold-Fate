using UnityEngine;

namespace FourfoldFate.Agents
{
    /// <summary>
    /// Manages AI agents for story, balance, and code generation.
    /// </summary>
    public class AgentManager : MonoBehaviour
    {
        public StoryAgent storyAgent;
        public BalanceAgent balanceAgent;
        public CodeAgent codeAgent;

        public static AgentManager Instance { get; private set; }

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
    }
}

