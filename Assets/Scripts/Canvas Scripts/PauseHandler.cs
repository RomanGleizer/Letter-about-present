using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            _pauseMenu?.SetActive(true);

        if (_pauseMenu.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void ClosePause()
    {
        _pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }
}
