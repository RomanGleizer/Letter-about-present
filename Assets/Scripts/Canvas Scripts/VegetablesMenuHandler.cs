using TMPro;
using UnityEngine;

public class VegetablesMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetablesMenu;

    [SerializeField] private TextMeshProUGUI[] _stockTextes;
    [SerializeField] private TextMeshProUGUI[] _vegetableMenuTextes;

    public TextMeshProUGUI[] StockTextes { get => _stockTextes; }
    public TextMeshProUGUI[] VegetableMenuTextes { get => _vegetableMenuTextes; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
            _vegetablesMenu.SetActive(true);

        for (int i = 0; i < _stockTextes.Length; i++)
            _vegetableMenuTextes[i].text = SetVegetableMenuAmountText(_stockTextes[i]);
    }

    private string SetVegetableMenuAmountText(TextMeshProUGUI stockAmountText)
    {
        return "Доступно: " + stockAmountText.text;
    }
}
