using UnityEngine;

public class VegetableShopHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetableShop;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            _vegetableShop.SetActive(true);
            foreach (var menu in _menues)
                menu.SetActive(false);
        }

        if (_vegetableShop.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
