using UnityEngine;

public class LoadGameCanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _loadScene;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        if (_loadScene.activeSelf) SetPlayerComponentState(false);
        else SetPlayerComponentState(true);
    }

    public void SetPlayerComponentState(bool state)
    {
        _player.GetComponent<PlayerMover>().enabled = state;
        _player.GetComponent<PlayerAnimator>().enabled = state;
    }
}