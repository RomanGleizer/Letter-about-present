using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _pauseMenu?.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ClosePause()
    {
        _pauseMenu?.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetPauseMenuActive()
    {
        _pauseMenu.SetActive(true);
    }
}
