using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetablePlanter : MonoBehaviour
{
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private Tile[] _tiles;
    [SerializeField] private GameObject[] _vegetablePrefabs;

    private int _cellsIndex;

    public void PlantVegetable(int vegetableIndex)
    {
        if (_vegetableMenu.VegetableCounters[vegetableIndex] > 0 &&
            _tilePainter.Cells.Count > 0)
        {
            _vegetableMenu.VegetableCounters[vegetableIndex]--;
            var counter = _vegetableMenu.VegetableCounters[vegetableIndex];

            _vegetableMenu.VegetableMenuTextes[vegetableIndex].text = counter.ToString();
            _vegetableMenu.StockTextes[vegetableIndex].text = counter.ToString();

            _tilePainter.TileMap.SetTile(_tilePainter.Cells[_cellsIndex], _tiles[vegetableIndex]);
            StartCoroutine(GrowVegetable(
                _vegetablePrefabs[vegetableIndex], 
                _tilePainter.HarvestSpawn, 
                _cellsIndex, vegetableIndex));

            _cellsIndex++;
        }
    }

    private IEnumerator GrowVegetable(GameObject obj, Vector3 position, int index, int vegetableIndex)
    {
        yield return new WaitForSeconds(5);
        Instantiate(
                obj,
                position,
                Quaternion.identity);
        _tilePainter.TileMap.SetTile(_tilePainter.Cells[index], _tilePainter.Tiles[vegetableIndex]);
    }
}
