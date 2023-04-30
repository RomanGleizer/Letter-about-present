using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Tile[] _tiles;
    [SerializeField] private GameObject _farm;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _farmMenu;
    [SerializeField] private TextMeshProUGUI _bedsAmountText;

    private List<Vector3Int> _cells = new List<Vector3Int>();
    private float _bedsAmountSpawnTimer;
    private int _bedsCount = 1;
    private Vector3 _harvestSpawn;

    public int BedsCount { get => _bedsCount; set => _bedsCount = value; }
    public List<Vector3Int> Cells { get => _cells; }
    public Tilemap TileMap { get => _tilemap; }
    public Tile[] Tiles { get => _tiles; }
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

        for (int i = 0; i < _tiles.Length; i++)
        {
            if (BedsCount > 0)
            {
                BedsCount--;
                _bedsAmountText.text = "Доступно грядок: " + _bedsCount.ToString();

                if (!_cells.Contains(currentCell) ||
                (_cells.Contains(currentCell) && _tilemap.GetTile(currentCell) == null))
                {
                    _bedsAmountText.text = "Доступно грядок: " + _bedsCount.ToString();
                    _cells.Add(currentCell);

                    _tilemap.SetTile(currentCell, _tiles[i]);
                    currentCell.x++;

                    if (i % 2 != 0)
                    {
                        currentCell.y--;
                        currentCell.x -= 2;
                    }
                }
            }
        }
    }

    public void DeleteCells()
    {
        for (int i = 0; i < _cells.Count; i++)
            if (_tiles.Length > 0) _tilemap.SetTile(_cells[i], null);
    }
}