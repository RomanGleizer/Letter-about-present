using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private Player _player;
    [SerializeField] private LoadGameCanvasHandler _loadGameCanvas;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Y))
            _stockMenu.SetActive(true);

        if (_stockMenu.activeSelf) _loadGameCanvas.SetPlayerComponentState(false);
        else _loadGameCanvas.SetPlayerComponentState(true);
    }
}
