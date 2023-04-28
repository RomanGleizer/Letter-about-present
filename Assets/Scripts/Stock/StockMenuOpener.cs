using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockMenuOpener : MonoBehaviour
{
    [SerializeField] private GameObject _stockMenu;
    [SerializeField] private Player _player;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Y))
            _stockMenu.SetActive(true);

        if (_stockMenu.activeSelf)
        {
            _player.GetComponent<PlayerMover>().enabled = false;
            _player.GetComponent<PlayerAnimator>().enabled = false;
        }
        else
        {
            _player.GetComponent<PlayerMover>().enabled = true;
            _player.GetComponent<PlayerAnimator>().enabled = true;
        }
    }
}
