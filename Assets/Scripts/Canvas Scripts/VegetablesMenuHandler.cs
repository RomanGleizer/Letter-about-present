using TMPro;
using UnityEngine;

public class VegetablesMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetablesMenu;
    [SerializeField] private TextMeshProUGUI[] _stockTextes;
    [SerializeField] private TextMeshProUGUI[] _vegetableMenuTextes;
    [SerializeField] private GameObject _loadGameCanvas;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private GameObject _vegetableShop;
    [SerializeField] private GameObject _boostShop;

    private int[] _vegetableCounters;
    private bool _isMenuOpen = false;

    public TextMeshProUGUI[] StockTextes { get => _stockTextes; }
    public TextMeshProUGUI[] VegetableMenuTextes { get => _vegetableMenuTextes; }

    public int[] VegetableCounters { get => _vegetableCounters; }

    private void Start()
    {
        _vegetableCounters = new int[_stockTextes.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !_loadGameCanvas.activeSelf && !_pauseMenu.activeSelf 
            && !_stockMenu.activeSelf && !_vegetableShop.activeSelf && !_boostShop.activeSelf
            && _isMenuOpen == false)
        {
            _isMenuOpen = true;
            _vegetablesMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.I) && _isMenuOpen == true)
        {
            _isMenuOpen = false;
            _vegetablesMenu.SetActive(false);
        }

        for (int i = 0; i < _vegetableCounters.Length; i++)
            _vegetableCounters[i] = int.Parse(_stockTextes[i].text);

        for (int i = 0; i < _stockTextes.Length; i++)
            _vegetableMenuTextes[i].text = SetVegetableMenuAmountText(_stockTextes[i]); 
    }

    public string SetVegetableMenuAmountText(TextMeshProUGUI stockAmountText)
    {
        return "Доступно: " + stockAmountText.text;
    }
}
