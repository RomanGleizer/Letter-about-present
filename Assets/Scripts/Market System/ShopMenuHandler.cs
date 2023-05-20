using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuHandler : MonoBehaviour
{
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

    private void Update()
    {
        _bedsSpawnSpeedText.text = "Уровень: " + _bedsSpawnSpeedLevel.ToString();
        _vagetableChanceText.text = "Уровень: " + _vegetableChanceLevel.ToString();
        _playerMoveSpeedText.text = "Уровень: " + _playerMoveSpeedLevel.ToString();
        _bedsSpawnPriceText.text = "Стоимость: " + _bedsSpawnSpeedPrice.ToString();
        _vagetableSpawnPriceText.text = "Стоимость: " + _vegetableChancePrice.ToString();
        _playerMovePriceSpeedText.text = "Стоимость: " + _playerMoveSpeedPrice.ToString();

        if (Input.GetKey(KeyCode.O))
        {
            _boostShop.SetActive(true);
            foreach (var menu in _menues)
                menu.SetActive(false);
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
            ChangeTextValues(ref _bedsSpawnSpeedLevel, ref _bedsSpawnSpeedPrice, _bedsSpawnSpeedText, _bedsSpawnPriceText);
        }
        if (boostButton.GetComponent<VegetableChanceButton>()
            && _vegetableSeller.Balance >= _vegetableChancePrice)
        {
            _vegetableSeller.Balance -= _vegetableChancePrice;
            ChangeTextValues(ref _vegetableChanceLevel, ref _vegetableChancePrice, _vagetableChanceText, _vagetableSpawnPriceText);
        }
        if (boostButton.GetComponent<PlayerMoveButton>()
            && _vegetableSeller.Balance >= _playerMoveSpeedPrice)
        {
            _vegetableSeller.Balance -= _playerMoveSpeedPrice;
            ChangeTextValues(ref _playerMoveSpeedLevel, ref _playerMoveSpeedPrice, _playerMoveSpeedText, _playerMovePriceSpeedText);
        }
        _vegetableSeller.BalanceText.text = "Баланс: " + _vegetableSeller.Balance.ToString();
    }

    public void SaveBoostShopData()
    {
        BoostShopData data = new BoostShopData();
        #region Values
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