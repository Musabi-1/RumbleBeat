using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameStateManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float stageTime = 90f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("CoinSettings")]
    [SerializeField] private CharCollectorManager charCollector;
    [SerializeField] private int targetCoinCount = 3;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameClearPanel;

    private float currentTime;
    private bool gameEnded = false;

    private void Start()
    {
        if (charCollector == null)
            Debug.LogError("CharCollectorManager not found!");

        currentTime = stageTime;
        UpdateTimerUI();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (gameClearPanel != null) gameClearPanel.SetActive(false);

    }

    private void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;
        if (currentTime < 0f) currentTime = 0f;

        UpdateTimerUI();

        if (charCollector != null && charCollector.GetCoinCount() >= targetCoinCount)
        {
            GameClear();
        }
        else if (currentTime <= 0f)
        {
            GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

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

    private void GameClear()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        if (gameClearPanel != null)
            gameClearPanel.SetActive(true);
    }
}
