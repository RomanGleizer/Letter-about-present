using UnityEngine;

public class VegetablePlanter : MonoBehaviour
{
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private HarvestCollector _collector;
    [SerializeField] private Player _player;

    public void PlantVegetable(int vegetableIndex)
    {
        Vector3Int currentCell = _tilePainter.TileMap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));

        if (_vegetableMenu.VegetableCounters[vegetableIndex] > 0 &&
            _tilePainter.Cells.Contains(currentCell))
        {
            _vegetableMenu.VegetableCounters[vegetableIndex]--;

            var counter = _vegetableMenu.VegetableCounters[vegetableIndex];
            _vegetableMenu.VegetableMenuTextes[vegetableIndex].text = counter.ToString();
            _vegetableMenu.StockTextes[vegetableIndex].text = counter.ToString();

            _tilePainter.TileMap.SetTile(currentCell, _tilePainter.VegetableTiles[vegetableIndex]);
            _collector.CollectHarvest(vegetableIndex, currentCell);
        }
    }
}
