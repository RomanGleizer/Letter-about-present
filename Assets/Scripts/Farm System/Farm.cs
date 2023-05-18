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

    public void OnApplicationQuit()
    {
        SaveFarm();
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
        var data = JsonUtility.FromJson<FarmData>(json);

        _farmTileMap.ClearAllTiles();

        for (int i = 0; i < data.Cells.Count; i++)
        {
            _farmTileMap.SetTile(data.Cells[i], data.Tiles[i]);
            if (_farmTileMap.GetTile(data.Cells[i]) != null)
            {
                var tile = _farmTileMap.GetTile(data.Cells[i]);
                _cellsIndex = i;
                if (tile.name == "CarrotGround") GrowVegetable(0, data);
                if (tile.name == "PatatoGround") GrowVegetable(1, data);
                if (tile.name == "WheatGround") GrowVegetable(2, data);
            }
        }

        var droppedVegetables = data.DroppedVegetables.ToList();
        for (int i = 0; i < droppedVegetables.Count; i++)
        {
            if (droppedVegetables[i] == "carrot") InstantiateDroppedVegetables(0, i, data);
            if (droppedVegetables[i] == "patato") InstantiateDroppedVegetables(1, i, data);
            if (droppedVegetables[i] == "wheat") InstantiateDroppedVegetables(2, i, data);
        }

        for (int i = 0; i < 3; i++)
        {
            _vegetableMenu.VegetableCounters[i] = data.VegetableCounters[i];
            _vegetableMenu.StockTextes[i].text = data.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = data.VegetableCounters[i].ToString();
        }

        _tilePainter.BedsCounter = data.BedsCounter;
        _vegetableSeller.Balance = data.Balance;
    }

    private void GrowVegetable(int vegetableIndex, FarmData data)
    {
        var cell = _farmTileMap.CellToLocalInterpolated(data.Cells[_cellsIndex]);

        StartCoroutine(
            _harvestCollector.GrowVegetable(
                _harvestCollector.VegetablePrefabs[vegetableIndex], 
                cell,
                data.Cells[_cellsIndex]
            ));

        _farmTileMap.SetTile(data.Cells[_cellsIndex], data.Tiles[_cellsIndex]);
    }

    private void SaveDroppedVegetables(GameObject obj, FarmData data, string name)
    {
        data.DroppedVegetables.Add(name);
        data.DroppedVegetablesPositions.Add(obj.transform.position);
    }

    private void InstantiateDroppedVegetables(int vegetableIndex, int positonIndex, FarmData data)
    {
        Instantiate(
            _harvestCollector.VegetablePrefabs[vegetableIndex],
            data.DroppedVegetablesPositions[positonIndex],
            Quaternion.identity);
    }
}