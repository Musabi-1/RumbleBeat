using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int spawnCount = 4;
    [SerializeField] private float spawnSeconds = 2f;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject fallingPrefab;

    private List<float> availableHeights = new List<float>();
    private Queue<float> spawnQueue = new Queue<float>();

    private void Start()
    {
        if (tilemap == null || fallingPrefab == null)
        {
            Debug.LogError("Tilemap or Prefab not assigned");
            return;
        }

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

        Vector3Int baseCell = tilemap.WorldToCell(transform.position);
        int x = baseCell.x;

        for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
        {
            Vector3Int checkCell = new Vector3Int(x, y, 0);
            if (tilemap.HasTile(checkCell))
            {
                float worldY = tilemap.GetCellCenterWorld(checkCell).y;
                availableHeights.Add(worldY);
            }
        }
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
