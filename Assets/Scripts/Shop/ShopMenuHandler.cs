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
    [SerializeField] private TextMeshProUGUI _playerMoveText;
    [SerializeField] private TextMeshProUGUI _playerMovePriceText;

    private int _bedsSpawnSpeedLevel;
    private int _bedsSpawnSpeedPrice;
    private int _vegetableChanceLevel;
    private int _vagetableChancePrice;
    private int _playerMoveSpeedLevel;
    private int _playerMovePrice;

    private void Start()
    {
        _bedsSpawnSpeedPrice = _vagetableChancePrice = _playerMovePrice = 5;
        _bedsSpawnSpeedLevel = _vegetableChanceLevel = _playerMoveSpeedLevel = 1;
    }

    private void Update()
    {
        _bedsSpawnSpeedText.text = "Уровень : " + _bedsSpawnSpeedLevel.ToString();
        _vagetableChanceText.text = "Уровень : " + _vegetableChanceLevel.ToString();
        _playerMoveText.text = "Уровень : " + _playerMoveSpeedLevel.ToString();
        _bedsSpawnPriceText.text = "Стоимость : " + _bedsSpawnSpeedPrice.ToString();
        _vagetableSpawnPriceText.text = "Стоимость : " + _vagetableChancePrice.ToString();
        _playerMovePriceText.text = "Стоимость : " + _playerMovePrice.ToString();

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
        if (boostButton.GetComponent("BedsGrowButton")
            && _vegetableSeller.Balance >= _bedsSpawnSpeedPrice)
        {
            _vegetableSeller.Balance -= _bedsSpawnSpeedPrice;
            ChangeTextValues(
                ref _bedsSpawnSpeedLevel,
                ref _bedsSpawnSpeedPrice,
                _bedsSpawnSpeedText,
                _bedsSpawnPriceText);
        }
        if (boostButton.GetComponent("VegetableChanceButton")
            && _vegetableSeller.Balance >= _vagetableChancePrice)
        {
            _vegetableSeller.Balance -= _vagetableChancePrice;
            ChangeTextValues(
                ref _vegetableChanceLevel,
                ref _vagetableChancePrice,
                _vagetableChanceText,
                _vagetableSpawnPriceText);
        }
        if (boostButton.GetComponent("PlayerMoveButton")
            && _vegetableSeller.Balance >= _playerMovePrice)
        {
            _vegetableSeller.Balance -= _playerMovePrice;
            ChangeTextValues(
                ref _playerMoveSpeedLevel,
                ref _playerMovePrice,
                _playerMoveText,
                _playerMovePriceText);
        }
        _vegetableSeller.BalanceText.text = "Баланс: " + _vegetableSeller.Balance.ToString();
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