using UnityEngine;

public class FarmMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _farmMenu;
    [SerializeField] private GameObject _loadGameCanvas;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _stockMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.U) 
            && !_loadGameCanvas.activeSelf 
            && !_pauseMenu.activeSelf
            && !_stockMenu.activeSelf) 
            _farmMenu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false); 
    }
}
