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

        data.BedsCounter = _tilePainter.BedsCount;
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
        _farmTileMap.ClearAllTiles();

        for (int i = 0; i < data.Cells.Count; i++)
            _farmTileMap.SetTile(data.Cells[i], data.Tiles[i]);

        for (int i = 0; i < data.Tiles.Count; i++)
        {
            try
            {
                var cell = _farmTileMap.CellToLocalInterpolated(data.Cells[i]);
                _tilePainter.Cells.Add(data.Cells[i]);
                _farmTileMap.SetTile(data.Cells[i], data.Tiles[i]);

                if (data.Tiles[i].name == "CarrotGround")
                    StartCoroutine(_harvestCollector.GrowVegetable
                        (_harvestCollector.VegetablePrefabs[0], cell, data.Cells[i]));
                if (data.Tiles[i].name == "PatatoGround")
                    StartCoroutine(_harvestCollector.GrowVegetable
                            (_harvestCollector.VegetablePrefabs[1], cell, data.Cells[i]));
                if (data.Tiles[i].name == "WheatGround")
                    StartCoroutine(_harvestCollector.GrowVegetable
                            (_harvestCollector.VegetablePrefabs[2], cell, data.Cells[i]));
            }
            catch (Exception) { }
        }

        for (int i = 0; i < 3; i++)
        {
            _vegetableMenu.VegetableCounters[i] = data.VegetableCounters[i];
            _vegetableMenu.StockTextes[i].text = data.VegetableCounters[i].ToString();
            _vegetableMenu.VegetableMenuTextes[i].text = data.VegetableCounters[i].ToString();
        }

        _tilePainter.BedsCount = data.BedsCounter;
    }
}
