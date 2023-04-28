using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmData
{
    public List<TileBase> Tiles = new List<TileBase>();
    public List<Vector3Int> Poses = new List<Vector3Int>();
    public int BedsCounter = 0;
}
