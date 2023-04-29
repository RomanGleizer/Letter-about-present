using TMPro;
using UnityEngine;

public class VegetablesMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetablesMenu;
    [SerializeField] private TextMeshProUGUI[] _stockTextes;
    [SerializeField] private TextMeshProUGUI[] _vegetableMenuTextes;
    private int[] _vegetableCounters;

    public TextMeshProUGUI[] StockTextes { get => _stockTextes; }
    public TextMeshProUGUI[] VegetableMenuTextes { get => _vegetableMenuTextes; }
    public int[] VegetableCounters { get => _vegetableCounters; set => _vegetableCounters = value; }

    private void Start()
    {
        _vegetableCounters = new int[_stockTextes.Length];

        for (int i = 0; i < _vegetableCounters.Length; i++)
            _vegetableCounters[i] = int.Parse(_stockTextes[i].text);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.I))
            _vegetablesMenu.SetActive(true);

        for (int i = 0; i < _stockTextes.Length; i++)
            _vegetableMenuTextes[i].text = SetVegetableMenuAmountText(_stockTextes[i]);
    }

    private string SetVegetableMenuAmountText(TextMeshProUGUI stockAmountText)
    {
        return "��������: " + stockAmountText.text;
    }
}
