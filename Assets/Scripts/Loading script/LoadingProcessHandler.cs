using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingProcessHandler : MonoBehaviour
{
    [SerializeField] private Scrollbar loadingScrollbar;
    [SerializeField] private TextMeshProUGUI loadingText;

    private int loadingsCounter;
    private int loadingAmount;

    private void Start()
    {
        loadingAmount = 6;
    }

    private void Update()
    {
        for (int i = 0; i < loadingAmount; i++)
        {
            if (loadingScrollbar.size == 1)
            {
                loadingsCounter++;
                loadingScrollbar.size = 0;
            }

            if (loadingsCounter == 1)
                loadingText.text = "Загрузка ресурсов...";
            else if (loadingsCounter == 2)
                loadingText.text = "Загрузка персонажей...";
            else if (loadingsCounter == 3)
                loadingText.text = "Ещё совсем чуть-чуть...";
            else if (loadingsCounter == 5)
            {
                SceneManager.LoadScene(1);
                break;
            }

            loadingScrollbar.size += Time.deltaTime / 35;
        }
    }
}
