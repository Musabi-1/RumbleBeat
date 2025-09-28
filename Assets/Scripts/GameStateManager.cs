using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

public class GameStateManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float stageTime = 90f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;

    private float currentTime;
    private bool gameEnded = false;
    private bool minutesNeeded = false;

    private void Start()
    {
        currentTime = stageTime;
        UpdateTimerUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(false);
        if (stageTime >= 60) minutesNeeded = true;
    }

    private void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        if (!minutesNeeded)
        {
            timerText.text = Mathf.FloorToInt(currentTime).ToString();
            return;
        }

        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void GameOver()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void GameClear()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        if (gameClearPanel != null)
            gameClearPanel.SetActive(true);
    }
}
