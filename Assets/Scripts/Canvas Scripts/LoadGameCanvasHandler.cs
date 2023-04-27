using UnityEngine;

public class LoadGameCanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _loadScene;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        if (_loadScene.activeSelf)
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