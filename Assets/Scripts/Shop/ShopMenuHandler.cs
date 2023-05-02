using TMPro;
using UnityEngine;

public class ShopMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private TextMeshProUGUI _bedsSpawnSpeedText;
    [SerializeField] private TextMeshProUGUI _vagetableChanceText;
    [SerializeField] private TextMeshProUGUI _playerMoveText;

    private int _bedsSpawnSpeed;
    private int _vegetableChance;
    private int _playerMoveSpeed;

    private void Start()
    {
        _bedsSpawnSpeed = 1;
        _vegetableChance = 1;
        _playerMoveSpeed = 1;
    }

    private void Update()
    {
        _bedsSpawnSpeedText.text = "Уровень : " + _bedsSpawnSpeed.ToString();
        _vagetableChanceText.text = "Уровень : " + _vegetableChance.ToString();
        _playerMoveText.text = "Уровень : " + _playerMoveSpeed.ToString();

        if (Input.GetKey(KeyCode.O))
            _shop.SetActive(true);
    }
}
