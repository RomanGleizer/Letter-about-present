using UnityEngine;

public class FarmMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _farmMenu;
    [SerializeField] private GameObject _loadGameCanvas;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private GameObject _vegetableShop;
    [SerializeField] private GameObject _boostShop;

    private bool _isMenuOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) 
            && !_loadGameCanvas.activeSelf && !_pauseMenu.activeSelf && !_stockMenu.activeSelf
            && !_vegetableShop.activeSelf && !_boostShop.activeSelf && _isMenuOpen == false)
        {
            _isMenuOpen = true;
            _farmMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U) && _isMenuOpen == true)
        {
            _isMenuOpen = false;
            _farmMenu.SetActive(false);
        }
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false); 
    }
}
