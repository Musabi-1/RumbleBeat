using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SakanaManager : MonoBehaviour
{
    [SerializeField] private GameObject sakanaPrefab;
    [SerializeField] private TilemapLogic tilemapLogic;
    [SerializeField] private float defaultSpawnInterval = 2f;
    [SerializeField] private float intervalRandomBias = 1f;

    private List<float> spawnRows = new List<float>();
    private float spawnInterval;

    private void Start()
    {
        spawnRows = tilemapLogic.GetSpawnRows();

        spawnRows.Sort();
        StartCoroutine(SakanaSpawn());
    }

    private IEnumerator SakanaSpawn()
    {
        while (true)
        {
            if (spawnRows.Count > 0)
            {
                int spawnCount = Random.Range(1, 3);

                List<float> shuffled = new List<float>(spawnRows);

                for (int i = 0; i < shuffled.Count; i++)
                {
                    int rand = Random.Range(i, shuffled.Count);
                    float temp = shuffled[i];
                    shuffled[i] = shuffled[rand];
                    shuffled[rand] = temp;
                }

                for (int i = 0; i < spawnCount && i < shuffled.Count; i++)
                {
                    Vector3 spawnPos = new Vector3(transform.position.x, shuffled[i], transform.position.z);
                    Instantiate(sakanaPrefab, spawnPos, Quaternion.identity);
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
