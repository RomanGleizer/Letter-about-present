using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        var vegetableAmount = Random.Range(1, 3);
        for (int i = 0; i < vegetableAmount; i++)
        {
            StartCoroutine(GrowVegetable(_vegetablePrefabs[vegetableIndex], localCurrentCell, worldCurrentCell));
            var delta = Random.Range(-0.25f, 0.25f);
            localCurrentCell.x += delta;
            localCurrentCell.y += delta;
        }
    }

    public IEnumerator GrowVegetable(GameObject obj, Vector3 position, Vector3Int cell)
    {
        var tile = _tilePainter.TileMap.GetTile(cell);
        if (tile != _tilePainter.GroundTile && _tilePainter.TileMap.ContainsTile(tile))
        {
            yield return new WaitForSeconds(5);
            Instantiate(obj, position, Quaternion.identity);
            _tilePainter.TileMap.SetTile(cell, _tilePainter.GroundTile);
        }
    }
}