using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlaying = true;
    public float time;
    public int point;
    public GameSettings gameSettings;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        time = gameSettings.timeAllowed;
        UIManager.instance.UpdateScoreBoard(point, gameSettings.totalPointToWin);
    }
    public void FreezeGame()
    {
        isPlaying = false;
    }

    void Update()
    {
        UpdateTime();
    }
    public void UpdateTime(){
        time -= Time.deltaTime;
        
        UIManager.instance.UpdateTimeUI(time);
        
        if(time<=0)
            GameOver();

    }

    private void GameOver()
    {
        UIManager.instance.ShowLosePanel(point,gameSettings.totalPointToWin);
        StopGame();
    }

    private void StopGame()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
    }

    internal void AddPoint()
    {
        point++;
        if (point >= gameSettings.totalPointToWin)
            Win();

        UIManager.instance.UpdateScoreBoard(point, gameSettings.totalPointToWin);
    }

    private void Win()
    {
        UIManager.instance.ShowWinPanel();
        StopGame();
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
