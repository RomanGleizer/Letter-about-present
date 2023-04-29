using UnityEngine;
using UnityEngine.Tilemaps;

public class VegetablePlanter : MonoBehaviour
{
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private Tile[] _tiles;

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

            _tilePainter.TileMap.SetTile(_tilePainter.Cells[_cellsIndex++], _tiles[vegetableIndex]);
        }
    }
}
