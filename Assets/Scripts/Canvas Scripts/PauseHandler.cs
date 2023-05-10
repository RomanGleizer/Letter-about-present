using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _firstVegetableMenu;
    [SerializeField] private GameObject _secondVegetableMenu;
    [SerializeField] private GameObject _stockMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            _pauseMenu?.SetActive(true);

        if (_pauseMenu.activeSelf)
        {
            _firstVegetableMenu?.SetActive(false);
            _secondVegetableMenu?.SetActive(false);
            _stockMenu?.SetActive(false);
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
