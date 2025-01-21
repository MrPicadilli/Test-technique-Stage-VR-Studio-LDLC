using System;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class manage all the rule of the game ( nbr of point, time)
/// </summary>
/// <remarks>
/// i put a CursorLockMode.Locked at the start so that when the player click the screen game it will make the cursor disappear
/// </remarks>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool isPlaying = true;

    [Header("Settings")]
    [Tooltip("Manage the total tieme allowed and the number of point to get to win the game")]
    public GameSettings gameSettings;
    private float _time;
    private int _point;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _time = gameSettings.timeAllowed;
        UIManager.instance.UpdateScoreBoard(_point, gameSettings.totalPointToWin);
    }
    public void FreezeGame()
    {
        isPlaying = false;
    }

    void Update()
    {
        UpdateTime();
    }
    public void UpdateTime()
    {
        _time -= Time.deltaTime;

        UIManager.instance.UpdateTimeUI(_time);

        if (_time <= 0)
            GameOver();

    }

    private void GameOver()
    {
        UIManager.instance.ShowLosePanel(_point, gameSettings.totalPointToWin);
        StopGame();
    }
    /// <summary>
    /// will meake the player lose control of the character and will make his mouse cursor appear to permit him to click on try again
    /// </summary>
    private void StopGame()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
    }

    internal void AddPoint()
    {
        _point++;
        if (_point >= gameSettings.totalPointToWin)
            Win();

        UIManager.instance.UpdateScoreBoard(_point, gameSettings.totalPointToWin);
    }

    private void Win()
    {
        UIManager.instance.ShowWinPanel();
        StopGame();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
