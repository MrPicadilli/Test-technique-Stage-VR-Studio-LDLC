using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI resultsText;
    public GameObject gamePanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ShowLosePanel(int point, int pointToWin)
    {
        losePanel.SetActive(true);
        gamePanel.SetActive(false);
        resultsText.text = "You managed to throw " + point + " out of " + pointToWin + " trash in the garbage can.";
    }




    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void UpdateScoreBoard(int point, int totalPointToWin)
    {
        scoreText.text = point + " / " + totalPointToWin;
    }

    internal void UpdateTimeUI(float time)
    {
        int printableTime = (int)time;
        timeText.text = printableTime + "";
    }
}
