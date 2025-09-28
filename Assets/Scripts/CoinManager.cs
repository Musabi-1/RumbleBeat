using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int spawnCount = 4;
    [SerializeField] private float spawnSeconds = 2f;
    [SerializeField] private TilemapLogic tilemapLogic;
    [SerializeField] private GameObject fallingPrefab;

    private List<float> availableHeights = new List<float>();
    private Queue<float> spawnQueue = new Queue<float>();

    private void Start()
    {
        RefreshAvailableHeights();
        PrepareSpawnQueue();

        if (availableHeights.Count == 0)
        {
            Debug.LogWarning("No valid tilemap rows to spawn on!");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    private void RefreshAvailableHeights()
    {
        availableHeights.Clear();

        availableHeights = tilemapLogic.GetTilemapYatX(transform.position.x);
    }

    private void PrepareSpawnQueue()
    {
        List<float> tempList = new List<float>(availableHeights);
        Shuffle(tempList);
        spawnQueue = new Queue<float>(tempList);
    }

    private IEnumerator SpawnLoop()
    {
        int spawned = 0;
        while (spawned < spawnCount && availableHeights.Count > 0)
        {
            SpawnCoins();
            spawned++;
            yield return new WaitForSeconds(spawnSeconds);
        }
    }

    private void SpawnCoins()
    {
        if (spawnQueue.Count == 0)
        {
            PrepareSpawnQueue();
        }

        float targetY = spawnQueue.Dequeue();

        GameObject obj = Instantiate(fallingPrefab, transform.position, Quaternion.identity);

        obj.GetComponent<CoinBehaviour>().SetTargetY(targetY);
    }

    private void Shuffle(List<float> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            float temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
}
