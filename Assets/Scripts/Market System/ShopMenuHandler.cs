using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuHandler : MonoBehaviour
{
    [SerializeField] private TilePainter _tilePainter;
    [SerializeField] private PlayerMover _player;
    [SerializeField] private CoinSpawner _nobleWoman;
    [SerializeField] private CoinSpawner _peasant;
    [SerializeField] private GameObject _boostShop;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private VegetableSeller _vegetableSeller;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
    [SerializeField] private TextMeshProUGUI _bedsSpawnSpeedText;
    [SerializeField] private TextMeshProUGUI _bedsSpawnPriceText;
    [SerializeField] private TextMeshProUGUI _vagetableChanceText;
    [SerializeField] private TextMeshProUGUI _vagetableSpawnPriceText;
    [SerializeField] private TextMeshProUGUI _playerMoveSpeedText;
    [SerializeField] private TextMeshProUGUI _playerMovePriceSpeedText;

    private int _bedsSpawnSpeedLevel = 1;
    private int _bedsSpawnSpeedPrice = 4;
    private int _vegetableChanceLevel = 1;
    private int _vegetableChancePrice = 4;
    private int _playerMoveSpeedLevel = 1;
    private int _playerMoveSpeedPrice = 4;
    private bool _isMenuOpen = false;

    private void Update()
    {
        _bedsSpawnSpeedText.text = "Уровень: " + _bedsSpawnSpeedLevel.ToString();
        _vagetableChanceText.text = "Уровень: " + _vegetableChanceLevel.ToString();
        _playerMoveSpeedText.text = "Уровень: " + _playerMoveSpeedLevel.ToString();
        _bedsSpawnPriceText.text = "Стоимость: " + _bedsSpawnSpeedPrice.ToString();
        _vagetableSpawnPriceText.text = "Стоимость: " + _vegetableChancePrice.ToString();
        _playerMovePriceSpeedText.text = "Стоимость: " + _playerMoveSpeedPrice.ToString();

        if (Input.GetKeyDown(KeyCode.O) && _isMenuOpen == false)
        {
            _isMenuOpen = true;
            _boostShop.SetActive(true);
            foreach (var menu in _menues)
                menu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.O) && _isMenuOpen == true)
        {
            _isMenuOpen = false;
            _boostShop.SetActive(false);
        }

        if (_boostShop.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }

    public void BuyBoost(Button boostButton)
    {
        if (boostButton.GetComponent<BedsGrowButton>()
            && _vegetableSeller.Balance >= _bedsSpawnSpeedPrice)
        {
            _vegetableSeller.Balance -= _bedsSpawnSpeedPrice;
            _tilePainter.BedTimeSpawn--;
            ChangeTextValues(ref _bedsSpawnSpeedLevel, ref _bedsSpawnSpeedPrice, _bedsSpawnSpeedText, _bedsSpawnPriceText);
        }
        if (boostButton.GetComponent<VegetableChanceButton>()
            && _vegetableSeller.Balance >= _vegetableChancePrice)
        {
            _nobleWoman.FirstTime -= 3;
            _nobleWoman.SecondTime -= 3;
            _peasant.FirstTime -= 3;
            _peasant.SecondTime -= 3;

            _vegetableSeller.Balance -= _vegetableChancePrice;
            ChangeTextValues(ref _vegetableChanceLevel, ref _vegetableChancePrice, _vagetableChanceText, _vagetableSpawnPriceText);
        }
        if (boostButton.GetComponent<PlayerMoveButton>()
            && _vegetableSeller.Balance >= _playerMoveSpeedPrice)
        {
            _player.Speed += 0.1f;
            _vegetableSeller.Balance -= _playerMoveSpeedPrice;
            ChangeTextValues(ref _playerMoveSpeedLevel, ref _playerMoveSpeedPrice, _playerMoveSpeedText, _playerMovePriceSpeedText);
        }
        _vegetableSeller.BalanceText.text = ": " + _vegetableSeller.Balance.ToString();
    }

    public void SaveBoostShopData()
    {
        BoostShopData data = new BoostShopData();
        #region Values
        data.Speed = _player.Speed;
        data.NobleWomanFirstTime = _nobleWoman.FirstTime;
        data.NobleWomanSecondTime = _nobleWoman.SecondTime;
        data.PeasantFirstTime = _peasant.FirstTime;
        data.PeasantSecondTime = _peasant.SecondTime;
        data.BedTimeSpawn = _tilePainter.BedTimeSpawn;
        data.BedsSpawnSpeedPrice = _bedsSpawnSpeedPrice;
        data.VegetableChancePrice = _vegetableChancePrice;
        data.PlayerMovePrice = _playerMoveSpeedPrice;
        data.BedsSpawnSpeedLevel = _bedsSpawnSpeedLevel;
        data.VegetableChanceLevel = _vegetableChanceLevel;
        data.PlayerMoveLevel = _playerMoveSpeedLevel;
        #endregion
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(
            Application.dataPath + "/BoostShopData.json",
            json,
            encoding: System.Text.Encoding.UTF8);
    }
    public void LoadBoostShopData()
    {
        try
        {
            var json = File.ReadAllText(
            Application.dataPath + "/BoostShopData.json",
            encoding: System.Text.Encoding.UTF8);
            var data = JsonUtility.FromJson<BoostShopData>(json);
            #region Values
            _player.Speed = data.Speed;
            _nobleWoman.FirstTime = data.NobleWomanFirstTime;
            _nobleWoman.SecondTime = data.NobleWomanSecondTime;
            _peasant.FirstTime = data.PeasantFirstTime;
            _peasant.SecondTime = data.PeasantSecondTime;
            _tilePainter.BedTimeSpawn = data.BedTimeSpawn;
            _bedsSpawnSpeedPrice = data.BedsSpawnSpeedPrice;
            _vegetableChancePrice = data.VegetableChancePrice;
            _playerMoveSpeedPrice = data.PlayerMovePrice;
            _bedsSpawnSpeedLevel = data.BedsSpawnSpeedLevel;
            _vegetableChanceLevel = data.VegetableChanceLevel;
            _playerMoveSpeedLevel = data.PlayerMoveLevel;
            #endregion
        }
        catch { }
    }

    private void ChangeTextValues(
        ref int level, 
        ref int price, 
        TextMeshProUGUI levelText,
        TextMeshProUGUI priceText)
    {
        level += 1;
        price += 2;
        levelText.text = "Уровень:" + level.ToString();
        priceText.text = "Стоимость: " + price.ToString();
    }
}