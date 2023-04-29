using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _farmMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.U)) _farmMenu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false); 
    }
}
