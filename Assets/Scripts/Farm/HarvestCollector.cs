using System.Collections;
using UnityEngine;

public class HarvestCollector : MonoBehaviour
{
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private GameObject[] _vegetablePrefabs;
    [SerializeField] private Player _player;

    public GameObject[] VegetablePrefabs { get => _vegetablePrefabs; }

    public void CollectHarvest(int vegetableIndex)
    {
        var playerPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
        var worldCurrentCell = _tilePainter.TileMap.WorldToCell(playerPosition);
        var localCurrentCell = _tilePainter.TileMap.WorldToLocal(playerPosition);
        StartCoroutine(GrowVegetable(_vegetablePrefabs[vegetableIndex], localCurrentCell, worldCurrentCell));
    }

    private IEnumerator GrowVegetable(GameObject obj, Vector3 position, Vector3Int cell)
    {
        var tile = _tilePainter.TileMap.GetTile(cell);
        if (tile != _tilePainter.GroundTile
            && _tilePainter.TileMap.ContainsTile(tile))
        {
            yield return new WaitForSeconds(2);
            Instantiate(obj, position, Quaternion.identity);
            _tilePainter.TileMap.SetTile(cell, _tilePainter.GroundTile);
        }
    }
}