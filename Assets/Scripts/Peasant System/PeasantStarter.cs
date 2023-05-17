using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantStarter : MonoBehaviour
{
    [SerializeField] private PeasantDataHandler _peasant;
    [SerializeField] private GameObject _loadGame;

    private void Update()
    {
        if (_loadGame.activeSelf)
            _peasant.Speed = 0;
        else _peasant.Speed = 1.2f;
    }
}
