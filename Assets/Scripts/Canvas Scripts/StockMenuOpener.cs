using UnityEngine;

public class StockMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
    [SerializeField] private GameObject _loadGameMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _vegetableMenu;
    [SerializeField] private GameObject _farmMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Y) 
            && !_loadGameMenu.activeSelf
            && !_pauseMenu.activeSelf)
        {
            _stockMenu.SetActive(true);
            _vegetableMenu.SetActive(false);
            _farmMenu.SetActive(false);
        }

        if (_stockMenu.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
