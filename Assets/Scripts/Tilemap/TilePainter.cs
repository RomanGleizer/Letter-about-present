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
    [SerializeField] private TilePainter _instance;

    private float _bedsAmountSpawnTimer;
    private int _bedsCounter;
    private Vector3 _harvestSpawn;

    public Tile[] VegetableTiles { get => _vegetableTiles; }
    public Tilemap FarmTileMap { get => _framTileMap; }
    public Tile GroundTile { get => _groundTile; }

    public Vector3 HarvestSpawn
    {
        get => _harvestSpawn;
        private set => _harvestSpawn = value;
    }

    public int BedsCounter
    {
        get => _bedsCounter;
        set => _bedsCounter = value;
    }

    public float BedTimeSpawn
    {
        get => _bedTimeSpawn;
        set => _bedTimeSpawn = value;
    }

    private void Start()
    {
        _bedsCounter = 0;
    }

    private void Update()
    {
        _bedsAmountText.text = "Доступно грядок: " + _bedsCounter.ToString();

        _bedsAmountSpawnTimer += Time.deltaTime;
        if (_bedsAmountSpawnTimer > _bedTimeSpawn && _bedsAmountSpawnTimer < _bedTimeSpawn + 0.1)
        {
            _bedsCounter++;
            _bedsAmountText.text = "Доступно грядок: " + _bedsCounter.ToString();
            _bedsAmountSpawnTimer = 0;
        }
    }

    public void DrawCells()
    {
        var currentCell = _framTileMap.WorldToCell(
            new Vector3
            (
                _player.transform.position.x,
                _player.transform.position.y,
                0
            ));

        var delta = Random.Range(-0.5f, 0.5f);
        HarvestSpawn = new Vector3(_player.transform.position.x + delta, _player.transform.position.y + delta, 0);

        if (_bedsCounter > 0 && _framTileMap.GetTile(currentCell) == null)
        {
            _bedsCounter--;
            _bedsAmountText.text = "Доступно грядок: " + _bedsCounter.ToString();
            _framTileMap.SetTile(currentCell, _groundTile);
        }
    }
}