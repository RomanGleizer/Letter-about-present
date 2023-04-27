using UnityEngine;

public class LoadGameCanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject _loadScene;

    private void Update()
    {
        if (_loadScene.activeSelf) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}