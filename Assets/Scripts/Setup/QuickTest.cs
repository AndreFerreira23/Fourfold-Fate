using UnityEngine;
using FourfoldFate.Data;

namespace FourfoldFate.Setup
{
    /// <summary>
    /// Quick test script to verify system initialization.
    /// </summary>
    public class QuickTest : MonoBehaviour
    {
        private void Start()
        {
            if (GameDataManager.Instance != null)
            {
                Debug.Log("GameDataManager found!");
            }
            else
            {
                Debug.LogWarning("GameDataManager not found!");
            }
        }
    }
}

