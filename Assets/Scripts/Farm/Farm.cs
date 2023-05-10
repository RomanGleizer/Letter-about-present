using System;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farm : MonoBehaviour
{
    [SerializeField] private Tilemap _farmTileMap;
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private HarvestCollector _harvestCollector;

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

        for (int i = 0; i < 3; i++)
        {
            data.VegetableCounters.Add(_vegetableMenu.VegetableCounters[i]);
            
            _vegetableMenu.StockTextes[i].text = _vegetableMenu.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = _vegetableMenu.VegetableCounters[i].ToString();

            data.StockTextes.Add(_vegetableMenu.StockTextes[i]);
            data.VegetableMenuTextes.Add(_vegetableMenu.VegetableMenuTextes[i]);
        }

        data.BedsCounter = _tilePainter.BedsCounter;
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

        FarmData data = JsonUtility.FromJson<FarmData>(json);

        for (int i = 0; i < data.Cells.Count; i++)
        {
            _farmTileMap.SetTile(data.Cells[i], data.Tiles[i]);

            if (_farmTileMap.GetTile(data.Cells[i]) != null)
            {
                if (data.Tiles[i].name == "CarrotGround") GrowVegetable(data, i, 0);
                if (data.Tiles[i].name == "PatatoGround") GrowVegetable(data, i, 1);
                if (data.Tiles[i].name == "WheatGround") GrowVegetable(data, i, 2);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            _vegetableMenu.VegetableCounters[i] = data.VegetableCounters[i];
            _vegetableMenu.StockTextes[i].text = data.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = data.VegetableCounters[i].ToString();
        }

        _tilePainter.BedsCounter = data.BedsCounter;
    }

    private void GrowVegetable(FarmData data, int cellsIndex, int vegetableIndex)
    {
        var cell = _farmTileMap.CellToLocalInterpolated(data.Cells[cellsIndex]);

        StartCoroutine(
            _harvestCollector.GrowVegetable(_harvestCollector.VegetablePrefabs[vegetableIndex], cell, data.Cells[cellsIndex])
            );

        _farmTileMap.SetTile(data.Cells[cellsIndex], data.Tiles[cellsIndex]);
    }
}
