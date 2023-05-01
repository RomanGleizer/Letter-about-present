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
        Vector3Int currentCell = _tilePainter.TileMap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));
        
        StartCoroutine(GrowVegetable(_vegetablePrefabs[vegetableIndex], _tilePainter.HarvestSpawn, currentCell));
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