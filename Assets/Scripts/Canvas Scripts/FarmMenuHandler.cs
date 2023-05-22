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
        if (Input.GetKeyDown(KeyCode.U) && IsInsideFarmArea() && !_loadGameCanvas.activeSelf && !_pauseMenu.activeSelf && !_stockMenu.activeSelf && !_vegetableShop.activeSelf && !_boostShop.activeSelf && !_isMenuOpen)
        {
            _isMenuOpen = true;
            _farmMenu.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.U) || !IsInsideFarmArea()) && _isMenuOpen)
        {
            _isMenuOpen = false;
            _farmMenu.SetActive(false);
        }
    }

    private bool IsInsideFarmArea()
    {
        // Проверка, находится ли игрок в определенной области фермы.

        Vector2 playerPosition = transform.position;
        float xMin = -22f;
        float xMax = -7f;
        float yMin = -11f;
        float yMax = 11f;

        if (playerPosition.x >= xMin && playerPosition.x <= xMax && playerPosition.y >= yMin && playerPosition.y <= yMax)
        {
            return true;
        }

        return false;
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }
}
