using UnityEngine;

public class FarmMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _farmMenu;
    [SerializeField] private GameObject _loadGameCanvas;

    private void Update()
    {
        if (Input.GetKey(KeyCode.U) && !_loadGameCanvas.activeSelf) 
            _farmMenu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false); 
    }
}
