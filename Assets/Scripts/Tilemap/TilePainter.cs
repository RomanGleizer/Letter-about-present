using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile[] tiles;

    private Vector3Int previousCell;

    private void Update()
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);

        for (int i = 0; i < tiles.Length; i++)
        {
            tilemap.SetTile(currentCell, tiles[i]);
            currentCell.x++;
        }
    }
}
