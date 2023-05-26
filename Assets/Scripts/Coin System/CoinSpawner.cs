using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private float _firstTime;
    [SerializeField] private float _secondTime;

    public float FirstTime
    {
        get => _firstTime;
        set => _firstTime = value;
    }

    public float SecondTime
    {
        get => _secondTime;
        set => _secondTime = value;
    }

    private void Start()
    {
        InvokeRepeating(
            "SpawnCoin", 
            Random.Range(_firstTime, _secondTime), 
            Random.Range(_firstTime, _secondTime)
            );
    }

    private void SpawnCoin()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}