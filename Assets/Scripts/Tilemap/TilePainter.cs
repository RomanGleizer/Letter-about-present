using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile _groundTile;
    [SerializeField] private Tile[] _vegetableTiles;
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _bedsAmountText;

    private List<Vector3Int> _cells = new List<Vector3Int>();
    private float _bedsAmountSpawnTimer;
    private int _bedsCount = 1;
    private Vector3 _harvestSpawn;

    public int BedsCount { get => _bedsCount; set => _bedsCount = value; }
    public List<Vector3Int> Cells { get => _cells; }
    public Tile[] VegetableTiles { get => _vegetableTiles; }
    public Tilemap TileMap { get => _tilemap; }
    public Tile GroundTile { get => _groundTile; }
    public Vector3 HarvestSpawn { get => _harvestSpawn; private set => _harvestSpawn = value; }

    private void Start()
    {
        var json = File.ReadAllText(
            Application.dataPath + "/FarmData.json", 
            encoding: System.Text.Encoding.UTF8);

        FarmData data = JsonUtility.FromJson<FarmData>(json);
        _bedsAmountText.text = "Доступно грядок: " + data.BedsCounter.ToString();
    }

    private void Update()
    {
        _bedsAmountSpawnTimer += Time.deltaTime;
        if (_bedsAmountSpawnTimer > 15 && _bedsAmountSpawnTimer < 15.1)
        {
            BedsCount++;
            _bedsAmountText.text = "Доступно грядок: " + BedsCount.ToString();
            _bedsAmountSpawnTimer = 0;
        }
    }

    public void DrawCells()
    {
        HarvestSpawn = new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
        Vector3Int currentCell = _tilemap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));

        if (BedsCount > 0)
        {
            BedsCount--;
            _bedsAmountText.text = "Доступно грядок: " + BedsCount.ToString();

            _cells.Add(currentCell);
            _tilemap.SetTile(currentCell, _groundTile);
        }
    }

    public void DeleteCells()
    {
        for (int i = 0; i < BedsCount; i++)
            if (_bedsCount > 0) _tilemap.SetTile(_cells[i], null);
    }
}