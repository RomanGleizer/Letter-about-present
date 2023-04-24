using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    public void ChangeMenuState(bool state)
    {
        _menu.SetActive(state);
    }
}
