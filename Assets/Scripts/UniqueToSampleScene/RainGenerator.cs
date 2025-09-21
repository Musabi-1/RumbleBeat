using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RainGenerator : MonoBehaviour
{
    [SerializeField] private float defaultSpawnInterval = 1.5f;
    [SerializeField] private float intervalRandomBias = 1f;
    [SerializeField] private GameObject raindropPrefab;
    [SerializeField] private Tilemap tilemap;

    private List<float> spawnColumns = new List<float>();
    private float spawnInterval;

    private void Start()
    {
        if (tilemap == null)
        {
            Debug.LogError("tilemap not assigned");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.xMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))
                {
                    float worldX = tilemap.GetCellCenterWorld(cellPos).x;

                    if (!spawnColumns.Contains(worldX))
                    {
                        spawnColumns.Add(worldX);
                    }

                    break;
                }
            }
        }

        spawnColumns.Sort();

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (spawnColumns.Count > 0)
            {
                int spawnCount = Random.Range(1, 6);

                List<float> shuffled = new List<float>(spawnColumns);
                for (int i = 0; i < shuffled.Count; i++)
                {
                    int rand = Random.Range(i, shuffled.Count);
                    float temp = shuffled[i];
                    shuffled[i] = shuffled[rand];
                    shuffled[rand] = temp;
                }

                for (int i = 0; i < spawnCount && i < shuffled.Count; i++)
                {
                    Vector3 spawnPos = new Vector3(shuffled[i], transform.position.y, 0f);
                    Instantiate(raindropPrefab, spawnPos, Quaternion.identity);
                }
            }

            Randomizer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Randomizer()
    {
        spawnInterval = defaultSpawnInterval * intervalRandomBias * Random.Range(0.5f, 1.5f);
    }
}
