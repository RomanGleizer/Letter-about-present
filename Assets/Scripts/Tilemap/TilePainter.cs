using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _framTileMap;
    [SerializeField] private Tile _groundTile;
    [SerializeField] private Tile[] _vegetableTiles;
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _bedsAmountText;
    [SerializeField] private float _bedTimeSpawn;

    private List<Vector3Int> _cells = new List<Vector3Int>();
    private float _bedsAmountSpawnTimer;
    private int _bedsCount;
    private Vector3 _harvestSpawn;

    public List<Vector3Int> Cells { get => _cells; }
    public Tile[] VegetableTiles { get => _vegetableTiles; }
    public Tilemap FarmTileMap { get => _framTileMap; }
    public Tile GroundTile { get => _groundTile; }
    public Vector3 HarvestSpawn
    {
        get => _harvestSpawn;
        private set => _harvestSpawn = value;
    }
    public int BedsCount
    {
        get => _bedsCount;
        set => _bedsCount = value;
    }

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
        if (_bedsAmountSpawnTimer > _bedTimeSpawn && _bedsAmountSpawnTimer < _bedTimeSpawn + 0.1)
        {
            _bedsCount++;
            _bedsAmountText.text = "Доступно грядок: " + _bedsCount.ToString();
            _bedsAmountSpawnTimer = 0;
        }
    }

    public void DrawCells()
    {
        var json = File.ReadAllText(
            Application.dataPath + "/FarmData.json",
            encoding: System.Text.Encoding.UTF8);

        FarmData data = JsonUtility.FromJson<FarmData>(json);

        var currentCell = _framTileMap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));

        var delta = Random.Range(-0.5f, 0.5f);
        HarvestSpawn = new Vector3(_player.transform.position.x + delta, _player.transform.position.y + delta, 0);

        if (_bedsCount > 0 && !data.Cells.Contains(currentCell) && !_cells.Contains(currentCell))
        {
            _bedsCount--;
            _bedsAmountText.text = "Доступно грядок: " + _bedsCount.ToString();

            _cells.Add(currentCell);
            _framTileMap.SetTile(currentCell, _groundTile);
        }
    }

    public void DeleteCells()
    {
    }
}