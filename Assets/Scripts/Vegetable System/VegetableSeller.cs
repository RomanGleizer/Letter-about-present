using System.IO;
using TMPro;
using UnityEngine;

public class VegetableSeller : MonoBehaviour
{
    [SerializeField] private VegetablesMenuHandler _vegetableMenu;
    [SerializeField] private TextMeshProUGUI _balanceText;

    private int _balance;
    
    public int Balance 
    { 
        get => _balance; 
        set => _balance = value;
    }
    public TextMeshProUGUI BalanceText
    {
        get => _balanceText;
        set => _balanceText = value;
    }

    private void Start()
    {
        var json = File.ReadAllText(
            Application.dataPath + "/FarmData.json",
            encoding: System.Text.Encoding.UTF8);
        var data = JsonUtility.FromJson<FarmData>(json);

        _balance = data.Balance;
        _balanceText.text = ": " + _balance.ToString();
    }

    public void SellVegetable(int index)
    {
        var menuText = _vegetableMenu.VegetableMenuTextes[index];
        var stockText = _vegetableMenu.StockTextes[index];
        var vegetableAmount = _vegetableMenu.VegetableCounters[index];

        if (vegetableAmount > 0 && index >= 0 && index <= 3)
        {
            _balance++;
            vegetableAmount--;

            _balanceText.text = ": " + _balance.ToString();
            menuText.text = "Доступно: " + vegetableAmount.ToString();
            stockText.text = vegetableAmount.ToString();
        }
    }
}
