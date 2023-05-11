using TMPro;
using UnityEngine;

public class ShopMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _boostShop;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
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
        {
            _boostShop.SetActive(true);
            foreach (var menu in _menues)
                menu.SetActive(false);
        }
        
        if (_boostShop.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
