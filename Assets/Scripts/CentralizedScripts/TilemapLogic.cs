using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TilemapLogic : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    BoundsInt bounds;

    private void Awake()
    {
        tilemap.CompressBounds();
        bounds = tilemap.cellBounds;
    }

    public List<float> GetSpawnColumns()
    {
        List<float> spawnColumns = GetSpawnPositions(pos => pos.x);
        return spawnColumns;
    }

    public bool IsWalkable(Vector3Int cellPos)
    {
        return tilemap.HasTile(cellPos);
    }

    public List<float> GetSpawnRows()
    {
        List<float> spawnRows = GetSpawnPositions(pos => pos.y);
        return spawnRows;
    }

    public Vector3 GetRightmostMiddleCellPosition()
    {
        int maxX = int.MinValue;

        foreach (var cellPos in GetAllTilePositions())
            maxX = Mathf.Max(maxX, cellPos.x);

        List<int> yPos = new List<int>();
        foreach (var cellPos in GetAllTilePositions())
        {
            if (cellPos.x == maxX)
                yPos.Add(cellPos.y);
        }

        int middleY = yPos[yPos.Count / 2];
        return tilemap.GetCellCenterWorld(new Vector3Int(maxX, middleY, 0));
    }

    public (float left, float right) GetHorizontalBoundaries(float buffer)
    {
        Vector3Int leftCell = new Vector3Int(bounds.xMin, bounds.yMin, 0);
        Vector3Int rightCell = new Vector3Int(bounds.xMax - 1, bounds.yMin, 0);

        float leftBoundary = tilemap.CellToWorld(leftCell).x - buffer;
        float rightBoundary = tilemap.CellToWorld(rightCell).x + tilemap.cellSize.x + buffer;

        return (leftBoundary, rightBoundary);
    }

    public List<float> GetTilemapYatX(float x)
    {
        List<float> walkableYs = new List<float>();

        int cellPosX = tilemap.WorldToCell(new Vector3(x, 0, 0)).x;

        for (int y = bounds.yMin; y < bounds.xMax; y++)
        {
            Vector3Int cellPos = new Vector3Int(cellPosX, y, 0);
            if (tilemap.HasTile(cellPos))
            {
                float worldY = tilemap.GetCellCenterWorld(cellPos).y;
                walkableYs.Add(worldY);
            }
        }

        return walkableYs;
    }

    private List<float> GetSpawnPositions(Func<Vector3, float> selector)
    {
        HashSet<float> uniqueValues = new HashSet<float>();

        foreach (var cellPos in GetAllTilePositions())
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);
            uniqueValues.Add(selector(worldPos));
        }

        return new List<float>(uniqueValues);
    }

    private IEnumerable<Vector3Int> GetAllTilePositions()
    {
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(cellPos))
                    yield return cellPos;
            }
        }
    }
}
