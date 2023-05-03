using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmData
{
    public List<TileBase> Tiles = new List<TileBase>();
    public List<Vector3Int> Cells = new List<Vector3Int>();
    public List<Vector3> LocalCells = new List<Vector3>();
    public List<int> VegetableCounters = new List<int>();
    public List<TextMeshProUGUI> StockTextes = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> VegetableMenuTextes = new List<TextMeshProUGUI>();
    public int BedsCounter = 0;
}
