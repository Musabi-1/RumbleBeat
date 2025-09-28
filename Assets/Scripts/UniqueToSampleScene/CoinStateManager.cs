using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinStateManager : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private CharCollectorManager charCollectorManager;
    [SerializeField] private int targetCoinCount = 5;

    [Header("Scripts")]
    [SerializeField] private GameStateManager gameStateManager;

    private void Update()
    {
        if (charCollectorManager != null && charCollectorManager.GetCoinCount() >= targetCoinCount)
        {
            gameStateManager.GameClear();
        }
    }
}
