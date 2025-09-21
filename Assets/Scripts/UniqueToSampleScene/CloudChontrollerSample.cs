using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CloudChontrollerSample : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float boundaryBuffer = -2f;
    [SerializeField] private float stopTime = 1f;
    [SerializeField] private Tilemap walkableTilemap;

    private float leftBoundary;
    private float rightBoundary;
    private LightningStrike lightningStrike;

    private void Start()
    {
        if (walkableTilemap == null)
        {
            Debug.LogError("Tilemap not assigned");
            return;
        }

        lightningStrike = GetComponentInChildren<LightningStrike>();

        BoundsInt bounds = walkableTilemap.cellBounds;
        Vector3Int leftCell = new Vector3Int(bounds.xMin, bounds.yMin, 0);
        Vector3Int rightCell = new Vector3Int(bounds.xMax - 1, bounds.yMin, 0);

        leftBoundary = walkableTilemap.CellToWorld(leftCell).x - boundaryBuffer;
        rightBoundary = walkableTilemap.CellToWorld(rightCell).x + walkableTilemap.cellSize.x + boundaryBuffer;

        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            yield return MoveTo(rightBoundary);
            yield return new WaitForSeconds(stopTime);

            yield return MoveTo(leftBoundary);
            yield return new WaitForSeconds(stopTime);
        }
    }

    private IEnumerator MoveTo(float targetX)
    {
        Vector3 pos = transform.position;
        while (Mathf.Abs(pos.x - targetX) > 0.01f)
        {
            if (lightningStrike != null && lightningStrike.isOn)
            {
                yield return null;
                continue;
            }
            
            pos.x = Mathf.MoveTowards(pos.x, targetX, moveSpeed * Time.deltaTime);
            transform.position = pos;
            yield return null;
        }
        pos.x = targetX;
        transform.position = pos;
    }
}