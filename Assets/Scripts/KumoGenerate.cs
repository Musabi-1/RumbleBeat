using System.Collections;
using UnityEngine;

public class KumoGenerate : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject raindropPrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("Counts")]
    [SerializeField] private int rainDrops = 7;
    [SerializeField] private int coins = 3;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float horizontalRandom = 1f;
    [SerializeField] private float verticalRandom = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        int rainSpawned = 0;
        int coinsSpawned = 0;

        while (rainSpawned < rainDrops || coinsSpawned < coins)
        {
            float offsetX = Random.Range(-horizontalRandom, horizontalRandom);
            float offsetY = Random.Range(-verticalRandom, verticalRandom);

            Vector3 spawnPos = transform.position + new Vector3(offsetX, offsetY, 0);

            bool spawnCoin = (coinsSpawned < coins) && Random.value < 0.3f;

            if (spawnCoin)
            {
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
                coinsSpawned++;
            }
            else if (rainSpawned < rainDrops)
            {
                Instantiate(raindropPrefab, spawnPos, Quaternion.identity);
                rainSpawned++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
