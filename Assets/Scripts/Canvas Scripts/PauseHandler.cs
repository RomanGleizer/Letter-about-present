using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private GameObject _loadGameMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !_loadGameMenu.activeSelf)
            _pauseMenu?.SetActive(true);

        if (_pauseMenu.activeSelf)
        {
            foreach (var menu in _menues)
                menu.SetActive(false);
            Time.timeScale = 0;
        }
        if (_pauseMenu.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ClosePause()
    {
        _pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }
}
