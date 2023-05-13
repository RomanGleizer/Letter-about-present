using UnityEngine;

public class NoblewomanStarter : MonoBehaviour
{
    [SerializeField] private NoblewomanMover _noblewoman;
    [SerializeField] private GameObject _loadGame;

    private void Update()
    {
        if (_loadGame.activeSelf)
            _noblewoman.Speed = 0;
        else _noblewoman.Speed = 1.2f;
    }
}