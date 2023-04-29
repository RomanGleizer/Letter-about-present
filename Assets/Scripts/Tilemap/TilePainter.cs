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

    private List<Vector3Int> cells = new List<Vector3Int>();
    private float _bedsAmountSpawnTimer;
    private int _bedsCount = 1;

    public int BedsCount { get => _bedsCount; set => _bedsCount = value; }
    public Tile[] Tiles { get => _tiles; }

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
        _bedsAmountSpawnTimer++;
        if (_bedsAmountSpawnTimer > 2000 && _bedsAmountSpawnTimer < 2500)
        {
            BedsCount++;
            _bedsAmountText.text = "Доступно грядок: " + BedsCount.ToString();
            _bedsAmountSpawnTimer = 0;
        }
    }

    public void DrawCells()
    {
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

                if (!cells.Contains(currentCell) ||
                (cells.Contains(currentCell) && _tilemap.GetTile(currentCell) == null))
                {
                    _bedsAmountText.text = "Доступно грядок: " + _bedsCount.ToString();
                    cells.Add(currentCell);

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
        for (int i = 0; i < cells.Count; i++)
            if (_tiles.Length > 0) _tilemap.SetTile(cells[i], null);
    }
}