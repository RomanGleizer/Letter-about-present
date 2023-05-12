using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coin;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", Random.Range(20, 31), Random.Range(20, 31));
    }

    private void SpawnCoin()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}