using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile[] _tiles;
    [SerializeField] private GameObject _farm;
    [SerializeField] private Player _player;

    private List<Vector3Int> cells = new List<Vector3Int>();

    public void DrawCells()
    {
        Vector3Int currentCell = _tilemap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));

        for (int i = 0; i < _tiles.Length; i++)
        {
            if (!cells.Contains(currentCell))
            {
                cells.Add(currentCell);

                _tilemap.SetTile(currentCell, _tiles[i]);
                currentCell.x++;

                if (i % 2 != 0)
                {
                    currentCell.y--;
                    currentCell.x -= 2;
                }
            }
        }
    }

    public void DeleteCells(int number)
    {
        var cell = cells[number];

        if (_tiles.Length > 0)
        {
            _tilemap.SetTile(cell, null);
        }
    }
}
