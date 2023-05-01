using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VegetableTaker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _carrotStockAmountText;
    [SerializeField] private TextMeshProUGUI _patatoStockAmountText;
    [SerializeField] private TextMeshProUGUI _wheatStockAmountText;
    [SerializeField] private Sprite _carrotDefault;
    [SerializeField] private Sprite _patatoDefault;
    [SerializeField] private Sprite _wheatDefault;
    [SerializeField] private Sprite _carrotActive;
    [SerializeField] private Sprite _patatoActive;
    [SerializeField] private Sprite _wheatActive;

    private int _carrotCounter;
    private int _patatoCounter;
    private int _wheatCounter;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Carrot>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _carrotActive;

            if (Input.GetKey(KeyCode.T))
                Destroy(collision.gameObject);
        }
        if (collision.GetComponent<Patato>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _patatoActive;
        }
        if (collision.GetComponent<Wheat>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _wheatActive;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Carrot>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _carrotDefault;
        }
        if (collision.GetComponent<Patato>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _patatoDefault;
        }
        if (collision.GetComponent<Wheat>())
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = _wheatDefault;
        }
    }
}