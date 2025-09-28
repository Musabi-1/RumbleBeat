using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapLogic : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private void Awake()
    {
        tilemap.CompressBounds();
    }
}
