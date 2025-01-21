using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSettings))]
public class GameSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GameSettings gameSettings = (GameSettings)target;

        GUILayout.Label("Game Settings Editor", EditorStyles.boldLabel);

        CreatePointToWinSlider(gameSettings);

        CreateTimeAllowedSlider(gameSettings);

        CreateDifficultyBar(gameSettings);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(gameSettings);
        }
    }
    public void CreateTimeAllowedSlider(GameSettings gameSettings)
    {
        gameSettings.timeAllowed = EditorGUILayout.Slider(
            new GUIContent("Time Allowed (seconds)", "Time allowed to complete the game in seconds."),
            gameSettings.timeAllowed,
            1f,
            60f
        );
    }
    public void CreatePointToWinSlider(GameSettings gameSettings)
    {
        gameSettings.totalPointToWin = EditorGUILayout.IntSlider(
            new GUIContent("Points to Win", "Number of points needed to complete the game."),
            gameSettings.totalPointToWin,
            1,
            4
        );
    }
    /// <summary>
    /// Make a bar showing the estimated difficulty depending of the time allowed, the number of points needed to win and the estimated time to win 1 point
    /// </summary>
    public void CreateDifficultyBar(GameSettings gameSettings)
    {
        // calcul of the margin of error which is the number of second administred in addition to the number of second needed to do the objective.
        float timeEstimationNeeded = gameSettings.totalPointToWin * 5f; // 5 correspond to the time used to do one back and forth
        float marginOfError = gameSettings.timeAllowed - timeEstimationNeeded;

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Difficulty Estimed", EditorStyles.boldLabel);
        Color originalColor = GUI.color;

        string barText;
        Color barColor;

        // change the color and text of the bar depending of the margin of error
        if (marginOfError > 10f)
        {
            barColor = Color.green;
            barText = "EASY";
        }
        else if (marginOfError > 5f)
        {
            barColor = Color.yellow;
            barText = "MEDIUM";
        }
        else if (marginOfError > 1f)
        {
            barColor = Color.red;
            barText = "HARD";
        }
        else
        {
            barColor = Color.magenta;
            barText = "IMPOSSIBLE";
        }

        // Create a rect for the bar
        Rect barRect = GUILayoutUtility.GetRect(100, 25);
        EditorGUI.DrawRect(barRect, barColor);

        // Center text
        GUIStyle centeredTextStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            normal = new GUIStyleState { textColor = Color.black },
            fontStyle = FontStyle.Bold
        };

        EditorGUI.LabelField(barRect, barText, centeredTextStyle);

        GUI.color = originalColor;
    }
}
