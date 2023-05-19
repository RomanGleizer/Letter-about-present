using UnityEngine;

public class StockMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;
    [SerializeField] private GameObject _loadGameMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _vegetableMenu;
    [SerializeField] private GameObject _farmMenu;

    private bool _isMenuOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) 
            && !_loadGameMenu.activeSelf
            && !_pauseMenu.activeSelf && _isMenuOpen == false)
        {
            _isMenuOpen = true;
            _stockMenu.SetActive(true);
            _vegetableMenu.SetActive(false);
            _farmMenu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Y) && _isMenuOpen == true)
        {
            _isMenuOpen = false;
            _stockMenu.SetActive(false);
        }

        if (_stockMenu.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
