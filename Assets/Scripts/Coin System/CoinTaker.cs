using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    [SerializeField] private VegetableSeller _vegetableSeller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>())
        {
            _vegetableSeller.Balance++;
            _vegetableSeller.BalanceText.text = ": " + _vegetableSeller.Balance.ToString();
            Destroy(collision.gameObject);
        }
    }
}
