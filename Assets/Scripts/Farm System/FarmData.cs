using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class FarmData
{
    public List<TileBase> Tiles = new List<TileBase>();
    public List<Vector3Int> Cells = new List<Vector3Int>();
    public List<int> VegetableCounters = new List<int>();
    public List<string> DroppedVegetables = new List<string>();
    public List<Vector3> DroppedVegetablesPositions = new List<Vector3>();
    public List<TextMeshProUGUI> StockTextes = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> VegetableMenuTextes = new List<TextMeshProUGUI>();
    public int BedsCounter = 5;
    public int Balance = 0;
}
