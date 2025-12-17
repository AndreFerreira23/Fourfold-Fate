#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using FourfoldFate.UI;

namespace FourfoldFate.Editor
{
    /// <summary>
    /// Editor tool to automatically set up Battle Arena UI.
    /// </summary>
    public class BattleUISetup : EditorWindow
    {
        [MenuItem("Fourfold Fate/Setup Battle UI", false, 2)]
        [MenuItem("GameObject/Fourfold Fate/Setup Battle UI", false, 11)]
        public static void SetupBattleUI()
        {
            // Find or create Canvas
            Canvas canvas = FindFirstObjectByType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasObj = new GameObject("Canvas");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }

            // Create Battle Arena UI panel
            GameObject battlePanel = new GameObject("BattleArenaPanel");
            battlePanel.transform.SetParent(canvas.transform, false);
            RectTransform battleRect = battlePanel.AddComponent<RectTransform>();
            battleRect.anchorMin = Vector2.zero;
            battleRect.anchorMax = Vector2.one;
            battleRect.sizeDelta = Vector2.zero;
            battleRect.anchoredPosition = Vector2.zero;

            Image bg = battlePanel.AddComponent<Image>();
            bg.color = new Color(0.1f, 0.1f, 0.15f, 1f);

            // Create party container
            GameObject partyContainer = new GameObject("PartyContainer");
            partyContainer.transform.SetParent(battlePanel.transform, false);
            RectTransform partyRect = partyContainer.AddComponent<RectTransform>();
            partyRect.anchorMin = new Vector2(0, 0);
            partyRect.anchorMax = new Vector2(0.5f, 1);
            partyRect.sizeDelta = Vector2.zero;
            partyRect.anchoredPosition = Vector2.zero;

            // Create enemy container
            GameObject enemyContainer = new GameObject("EnemyContainer");
            enemyContainer.transform.SetParent(battlePanel.transform, false);
            RectTransform enemyRect = enemyContainer.AddComponent<RectTransform>();
            enemyRect.anchorMin = new Vector2(0.5f, 0);
            enemyRect.anchorMax = new Vector2(1, 1);
            enemyRect.sizeDelta = Vector2.zero;
            enemyRect.anchoredPosition = Vector2.zero;

            // Create action buttons panel
            GameObject actionPanel = new GameObject("ActionPanel");
            actionPanel.transform.SetParent(battlePanel.transform, false);
            RectTransform actionRect = actionPanel.AddComponent<RectTransform>();
            actionRect.anchorMin = new Vector2(0, 0);
            actionRect.anchorMax = new Vector2(1, 0.2f);
            actionRect.sizeDelta = Vector2.zero;
            actionRect.anchoredPosition = Vector2.zero;

            HorizontalLayoutGroup layout = actionPanel.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 10;
            layout.padding = new RectOffset(10, 10, 10, 10);
            layout.childAlignment = TextAnchor.MiddleCenter;

            // Create Attack button
            GameObject attackBtn = CreateButton("Attack", actionPanel.transform);
            // Create End Turn button
            GameObject endTurnBtn = CreateButton("End Turn", actionPanel.transform);

            // Create turn indicator
            GameObject turnIndicator = new GameObject("TurnIndicator");
            turnIndicator.transform.SetParent(battlePanel.transform, false);
            Text turnText = turnIndicator.AddComponent<Text>();
            turnText.text = "Your Turn";
            turnText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            turnText.fontSize = 24;
            turnText.color = Color.white;
            turnText.alignment = TextAnchor.UpperCenter;
            RectTransform turnRect = turnIndicator.GetComponent<RectTransform>();
            turnRect.anchorMin = new Vector2(0.5f, 0.8f);
            turnRect.anchorMax = new Vector2(0.5f, 0.8f);
            turnRect.sizeDelta = new Vector2(200, 50);
            turnRect.anchoredPosition = Vector2.zero;

            // Create combat log
            GameObject logPanel = new GameObject("CombatLogPanel");
            logPanel.transform.SetParent(battlePanel.transform, false);
            RectTransform logRect = logPanel.AddComponent<RectTransform>();
            logRect.anchorMin = new Vector2(0, 0.2f);
            logRect.anchorMax = new Vector2(1, 0.4f);
            logRect.sizeDelta = Vector2.zero;
            logRect.anchoredPosition = Vector2.zero;

            Image logBg = logPanel.AddComponent<Image>();
            logBg.color = new Color(0, 0, 0, 0.7f);

            GameObject logScroll = new GameObject("ScrollView");
            logScroll.transform.SetParent(logPanel.transform, false);
            RectTransform scrollRect = logScroll.AddComponent<RectTransform>();
            scrollRect.anchorMin = Vector2.zero;
            scrollRect.anchorMax = Vector2.one;
            scrollRect.sizeDelta = Vector2.zero;
            scrollRect.anchoredPosition = Vector2.zero;

            ScrollRect scrollView = logScroll.AddComponent<ScrollRect>();
            scrollView.horizontal = false;
            scrollView.vertical = true;

            GameObject logContent = new GameObject("Content");
            logContent.transform.SetParent(logScroll.transform, false);
            RectTransform contentRect = logContent.AddComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0, 1);
            contentRect.anchorMax = new Vector2(1, 1);
            contentRect.pivot = new Vector2(0.5f, 1);
            contentRect.sizeDelta = new Vector2(0, 0);

            Text logText = logContent.AddComponent<Text>();
            logText.text = "Combat Log:\n";
            logText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            logText.fontSize = 14;
            logText.color = Color.white;
            logText.alignment = TextAnchor.UpperLeft;

            scrollView.content = contentRect;
            scrollView.viewport = scrollRect;

            // Create synergy badges text
            GameObject synergyText = new GameObject("SynergyBadges");
            synergyText.transform.SetParent(battlePanel.transform, false);
            Text synergyTextComp = synergyText.AddComponent<Text>();
            synergyTextComp.text = "No Active Synergies";
            synergyTextComp.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            synergyTextComp.fontSize = 16;
            synergyTextComp.color = Color.yellow;
            synergyTextComp.alignment = TextAnchor.UpperLeft;
            RectTransform synergyRect = synergyText.GetComponent<RectTransform>();
            synergyRect.anchorMin = new Vector2(0, 0.95f);
            synergyRect.anchorMax = new Vector2(0.5f, 1);
            synergyRect.sizeDelta = Vector2.zero;
            synergyRect.anchoredPosition = Vector2.zero;

            // Add BattleArenaUI component
            BattleArenaUI battleUI = battlePanel.AddComponent<BattleArenaUI>();
            battleUI.rootPanel = battlePanel;
            battleUI.partyContainer = partyContainer.transform;
            battleUI.enemyContainer = enemyContainer.transform;
            battleUI.attackButton = attackBtn.GetComponent<Button>();
            battleUI.endTurnButton = endTurnBtn.GetComponent<Button>();
            battleUI.turnIndicatorText = turnText;
            battleUI.combatLogText = logText;
            battleUI.combatLogScrollRect = scrollView;
            battleUI.synergyBadgesText = synergyTextComp;

            // Assign to UIManager if it exists
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.battleArenaUI = battleUI;
            }

            Debug.Log("Battle UI setup complete!");
        }

        private static GameObject CreateButton(string text, Transform parent)
        {
            GameObject btn = new GameObject(text + "Button");
            btn.transform.SetParent(parent, false);

            Image img = btn.AddComponent<Image>();
            img.color = new Color(0.2f, 0.4f, 0.8f, 1f);

            Button button = btn.AddComponent<Button>();
            button.targetGraphic = img;

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btn.transform, false);
            Text txt = textObj.AddComponent<Text>();
            txt.text = text;
            txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            txt.fontSize = 18;
            txt.color = Color.white;
            txt.alignment = TextAnchor.MiddleCenter;

            RectTransform txtRect = textObj.GetComponent<RectTransform>();
            txtRect.anchorMin = Vector2.zero;
            txtRect.anchorMax = Vector2.one;
            txtRect.sizeDelta = Vector2.zero;
            txtRect.anchoredPosition = Vector2.zero;

            RectTransform btnRect = btn.GetComponent<RectTransform>();
            btnRect.sizeDelta = new Vector2(150, 50);

            return btn;
        }
    }
}
#endif

