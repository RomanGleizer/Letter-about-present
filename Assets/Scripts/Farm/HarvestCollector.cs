using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HarvestCollector : MonoBehaviour
{
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private GameObject[] _vegetablePrefabs;

    public GameObject[] VegetablePrefabs { get => _vegetablePrefabs; }

    public void CollectHarvest(int vegetableIndex, Vector3Int cell)
    {
        StartCoroutine(GrowVegetable(_vegetablePrefabs[vegetableIndex], _tilePainter.HarvestSpawn, cell, vegetableIndex));
    }

    private IEnumerator GrowVegetable(GameObject obj, Vector3 position, Vector3Int cell, int vegetableIndex)
    {
        yield return new WaitForSeconds(5);
        Instantiate(obj, position, Quaternion.identity);
        _tilePainter.TileMap.SetTile(cell, _tilePainter.GroundTile);
    }
}