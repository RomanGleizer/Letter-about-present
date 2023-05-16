using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farm : MonoBehaviour
{
    [SerializeField] private Tilemap _farmTileMap;
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private HarvestCollector _harvestCollector;
    [SerializeField] private VegetableSeller _vegetableSeller;
    
    private int _cellsIndex;
    private FarmData _farmData;

    private void Start()
    {
        _farmData = new FarmData();
    }

    public void SaveFarm()
    {
        BoundsInt bounds = _farmTileMap.cellBounds;
        FarmData data = new FarmData();

        for (int x = bounds.min.x; x < bounds.max.x; x++)
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {       
                var tile = _farmTileMap.GetTile(new Vector3Int(x, y, 0));
                data.Tiles.Add(tile);
                data.Cells.Add(new Vector3Int(x, y, 0));
            }

        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.GetComponent<Carrot>()) SaveDroppedVegetables(obj, data, "carrot");
            if (obj.GetComponent<Patato>()) SaveDroppedVegetables(obj, data, "patato");
            if (obj.GetComponent<Wheat>()) SaveDroppedVegetables(obj, data, "wheat");
        }

        for (int i = 0; i < 3; i++)
        {
            data.VegetableCounters.Add(_vegetableMenu.VegetableCounters[i]);
            
            _vegetableMenu.StockTextes[i].text = _vegetableMenu.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = _vegetableMenu.VegetableCounters[i].ToString();

            data.StockTextes.Add(_vegetableMenu.StockTextes[i]);
            data.VegetableMenuTextes.Add(_vegetableMenu.VegetableMenuTextes[i]);
        }

        data.BedsCounter = _tilePainter.BedsCounter;
        data.Balance = _vegetableSeller.Balance;

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(
            Application.dataPath + "/FarmData.json", 
            json, 
            encoding: System.Text.Encoding.UTF8);
    }

    public void LoadFarm()
    {
        var json = File.ReadAllText(
            Application.dataPath + "/FarmData.json", 
            encoding: System.Text.Encoding.UTF8);
        _farmData = JsonUtility.FromJson<FarmData>(json);
        _farmTileMap.ClearAllTiles();

        for (int i = 0; i < _farmData.Cells.Count; i++)
        {
            _farmTileMap.SetTile(_farmData.Cells[i], _farmData.Tiles[i]);
            if (_farmTileMap.GetTile(_farmData.Cells[i]) != null)
            {
                var tile = _farmTileMap.GetTile(_farmData.Cells[i]);
                _cellsIndex = i;
                if (tile.name == "CarrotGround") GrowVegetable(0);
                if (tile.name == "PatatoGround") GrowVegetable(1);
                if (tile.name == "WheatGround") GrowVegetable(2);
            }
        }

        var droppedVegetables = _farmData.DroppedVegetables.ToList();
        for (int i = 0; i < droppedVegetables.Count; i++)
        {
            if (droppedVegetables[i] == "carrot") InstantiateDroppedVegetables(0, i);
            if (droppedVegetables[i] == "patato") InstantiateDroppedVegetables(1, i);
            if (droppedVegetables[i] == "wheat") InstantiateDroppedVegetables(2, i);
        }

        for (int i = 0; i < 3; i++)
        {
            _vegetableMenu.VegetableCounters[i] = _farmData.VegetableCounters[i];
            _vegetableMenu.StockTextes[i].text = _farmData.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = _farmData.VegetableCounters[i].ToString();
        }
        _tilePainter.BedsCounter = _farmData.BedsCounter;
        _vegetableSeller.Balance = _farmData.Balance;
    }

    private void GrowVegetable(int vegetableIndex)
    {
        var cell = _farmTileMap.CellToLocalInterpolated(_farmData.Cells[_cellsIndex]);

        StartCoroutine(
            _harvestCollector.GrowVegetable(
                _harvestCollector.VegetablePrefabs[vegetableIndex], 
                cell, 
                _farmData.Cells[_cellsIndex]
            ));

        _farmTileMap.SetTile(_farmData.Cells[_cellsIndex], _farmData.Tiles[_cellsIndex]);
    }

    private void SaveDroppedVegetables(GameObject obj, FarmData data, string name)
    {
        data.DroppedVegetables.Add(name);
        data.DroppedVegetablesPositions.Add(obj.transform.position);
    }

    private void InstantiateDroppedVegetables(int vegetableIndex, int positonIndex)
    {
        Instantiate(
            _harvestCollector.VegetablePrefabs[vegetableIndex],
            _farmData.DroppedVegetablesPositions[positonIndex],
            Quaternion.identity);
    }
}