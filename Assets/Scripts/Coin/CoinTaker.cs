using UnityEngine;

public class CoinTaker : MonoBehaviour
{
    [SerializeField] private VegetableSeller _vegetableSeller;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() && Input.GetKey(KeyCode.T))
        {
            Destroy(collision.gameObject);
            _vegetableSeller.Balance++;
            _vegetableSeller.BalanceText.text = "Баланс: " + _vegetableSeller.Balance.ToString();
        }
    }
}
