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
        _bedsSpawnSpeedPrice = 5;
        _bedsSpawnSpeedLevel = 1;
        _vegetableChanceLevel = 1;
        _playerMoveSpeedLevel = 1;
    }

    private void Update()
    {
        _bedsSpawnSpeedText.text = "Уровень : " + _bedsSpawnSpeedLevel.ToString();
        _vagetableChanceText.text = "Уровень : " + _vegetableChanceLevel.ToString();
        _playerMoveText.text = "Уровень : " + _playerMoveSpeedLevel.ToString();

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
        if (_vegetableSeller.Balance >= 5)
        {
            if (boostButton.GetComponent("BedsGrowButton") && _vegetableSeller.Balance >= _bedsSpawnSpeedPrice)
            {
                _bedsSpawnSpeedLevel += 1;
                _bedsSpawnSpeedPrice += 2;
                _vegetableSeller.Balance -= 5;

                _bedsSpawnSpeedText.text = "Уровень : " + _bedsSpawnSpeedLevel.ToString();
                _bedsSpawnPriceText.text = "Стоимость: " + _bedsSpawnSpeedPrice.ToString();
                _vegetableSeller.BalanceText.text = "Баланс: " + _vegetableSeller.Balance.ToString();
            }
        }
    }
}
