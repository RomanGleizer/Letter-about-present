using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Farm : MonoBehaviour
{
    [SerializeField] private Tilemap farmTileMap;

    public void SaveFarm()
    {
        BoundsInt bounds = farmTileMap.cellBounds;
        FarmData data = new FarmData();

        for (int x = bounds.min.x; x < bounds.max.x; x++)
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                var temp = farmTileMap.GetTile(new Vector3Int(x, y, 0));

                data.Tiles.Add(temp);
                data.Poses.Add(new Vector3Int(x, y, 0));
            }

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/FarmData.json", json);
        print("Save");
    }

    public void LoadFarm()
    {
        var json = File.ReadAllText(Application.dataPath + "/FarmData.json");
        FarmData data = JsonUtility.FromJson<FarmData>(json);

        farmTileMap.ClearAllTiles();

        for (int i = 0; i < data.Poses.Count; i++)
            farmTileMap.SetTile(data.Poses[i], data.Tiles[i]);
    }
}
