using UnityEngine;

public class FarmMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _farmMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.U))
            _farmMenu.SetActive(true);
    }

    public void CloseMenu()
    { 
        _farmMenu.SetActive(false); 
    }
}
