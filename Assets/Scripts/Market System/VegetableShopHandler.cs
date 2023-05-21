using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VegetableShopHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetableShop;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
    [SerializeField] private VegetablesMenuHandler _vegetablesMenu;
    [SerializeField] private VegetableSeller _vegetableSeller;
    [SerializeField] private TextMeshProUGUI _carrotPriceText;
    [SerializeField] private TextMeshProUGUI _patatoPriceText;
    [SerializeField] private TextMeshProUGUI _wheatPriceText;

    private int _carrotPrice;
    private int _patatoPrice;
    private int _wheatPrice;
    private bool _isMenuOpen = false;

    private void Start()
    {
        _carrotPrice = _patatoPrice = _wheatPrice = 4;
    }

    private void Update()
    {
        _carrotPriceText.text = "Стоимость: " + _carrotPrice.ToString();
        _patatoPriceText.text = "Стоимость: " + _patatoPrice.ToString();
        _wheatPriceText.text = "Стоимость: " + _wheatPrice.ToString();

        if (Input.GetKeyDown(KeyCode.P) && _isMenuOpen == false)
        {
            _isMenuOpen = true;
            _vegetableShop.SetActive(true);
            foreach (var menu in _menues)
                menu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.P) && _isMenuOpen == true)
        {
            _vegetableShop.SetActive(false);
            _isMenuOpen = false;
        }

        if (_vegetableShop.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }

    public void BuyVegetable(Button buyButton)
    {
        if (buyButton.GetComponent("BuyCarrotButton") && _vegetableSeller.Balance >= _carrotPrice)
            DoBuy(_carrotPrice, 0);
        if (buyButton.GetComponent("BuyPatatoButton") && _vegetableSeller.Balance >= _patatoPrice)
            DoBuy(_patatoPrice, 1);
        if (buyButton.GetComponent("BuyWheatButton") && _vegetableSeller.Balance >= _wheatPrice)
            DoBuy(_wheatPrice, 2);
    }

    private void DoBuy(int price, int vegetableIndex)
    {
        _vegetableSeller.Balance -= price;
        var vegetableAmount = _vegetablesMenu.VegetableCounters[vegetableIndex];
        vegetableAmount++;

        _vegetablesMenu.StockTextes[vegetableIndex].text = vegetableAmount.ToString();
        _vegetableSeller.BalanceText.text = ": " + _vegetableSeller.Balance.ToString();
    }
}