using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile[] _tiles;
    [SerializeField] private GameObject farm;
    [SerializeField] private Player player;


    private void Start()
    {
        Vector3Int currentCell = _tilemap.WorldToCell(
            new Vector3
            (
                player.transform.position.x - 1,
                player.transform.position.y, 0
            ));

        for (int i = 0; i < _tiles.Length; i++)
        {
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
