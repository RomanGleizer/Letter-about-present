using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coin;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", Random.Range(40, 41), Random.Range(40, 41));
    }

    private void SpawnCoin()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}