using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RaindropManager : MonoBehaviour
{
    [SerializeField] private float spawnSec = 1.5f;
    [SerializeField] private int spawnCount = 7;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject raindropPrefab;

    private void Start()
    {
        if (tilemap == null || raindropPrefab == null)
        {
            Debug.LogError("Tilemap or raindrop Prefab not assigned");
            return;
        }

        StartCoroutine(RaindropSpawnLoop());
    }

    private IEnumerator RaindropSpawnLoop()
    {
        int spawned = 0;
        while (spawned < spawnCount)
        {
            yield return new WaitForSeconds(spawnSec);
            SpawnRaindrop();
            spawned++;
        }
    }

    private void SpawnRaindrop()
    {
        Vector3 spawnPos = transform.position;

        Vector3Int cell = tilemap.WorldToCell(spawnPos);
        spawnPos.x = tilemap.GetCellCenterWorld(new Vector3Int(cell.x, cell.y, cell.z)).x;

        Instantiate(raindropPrefab, spawnPos, Quaternion.identity);
    }
}
