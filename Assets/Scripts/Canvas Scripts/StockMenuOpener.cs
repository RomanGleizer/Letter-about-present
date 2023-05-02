using UnityEngine;

public class StockMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private Player _player;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
    [SerializeField] private GameObject _loadGameMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Y) && !_loadGameMenu.activeSelf)
            _stockMenu.SetActive(true);

        if (_stockMenu.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
