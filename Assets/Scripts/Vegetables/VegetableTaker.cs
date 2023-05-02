using TMPro;
using UnityEngine;

public class VegetableTaker : MonoBehaviour
{
    [SerializeField] private VegetablesMenuHandler _menu;

    [SerializeField] private Sprite _carrotDefault;
    [SerializeField] private Sprite _patatoDefault;
    [SerializeField] private Sprite _wheatDefault;
    [SerializeField] private Sprite _carrotActive;
    [SerializeField] private Sprite _patatoActive;
    [SerializeField] private Sprite _wheatActive;

    private int _carrotCounter;
    private int _patatoCounter;
    private int _wheatCounter;

    public int CarrotCounter { get => _carrotCounter; set => _carrotCounter = value; }
    public int PatatoCounter { get => _carrotCounter; set => _carrotCounter = value; }
    public int WheatCounter { get => _carrotCounter; set => _carrotCounter = value; }

    private void Update()
    {
        _carrotCounter = int.Parse(_menu.StockTextes[0].text);
        _patatoCounter = int.Parse(_menu.StockTextes[1].text);
        _wheatCounter = int.Parse(_menu.StockTextes[2].text);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Carrot>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _carrotActive;
            if (Input.GetKey(KeyCode.T))
                TakeVegetable(ref _carrotCounter, _menu.StockTextes[0], _menu.VegetableMenuTextes[0], collision);
        }
        if (collision.GetComponent<Patato>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _patatoActive;
            if (Input.GetKey(KeyCode.T))
                TakeVegetable(ref _patatoCounter, _menu.StockTextes[1], _menu.VegetableMenuTextes[1], collision);
        }
        if (collision.GetComponent<Wheat>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _wheatActive;
            if (Input.GetKey(KeyCode.T))
                TakeVegetable(ref _wheatCounter, _menu.StockTextes[2], _menu.VegetableMenuTextes[2], collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Carrot>())
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _carrotDefault;
        if (collision.GetComponent<Patato>())
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _patatoDefault;
        if (collision.GetComponent<Wheat>())
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _wheatDefault;
    }

    private void TakeVegetable(ref int counter, TextMeshProUGUI stockText, TextMeshProUGUI menuText, Collider2D collision)
    {
        counter++;
        stockText.text = counter.ToString();
        menuText.text = counter.ToString();
        Destroy(collision.gameObject);
    }
}