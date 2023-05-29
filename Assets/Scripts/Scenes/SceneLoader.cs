using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _isPreviousSceneWasMenu;
    private bool _isPreviousSceneWasGame;
    private int _sceneIndex;

    public void LoadScene(int index)
    {
        var data = new PreviousSceneData();

        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (_sceneIndex == 0) _isPreviousSceneWasMenu = true;
        if (_sceneIndex == 1) _isPreviousSceneWasGame = true;

        data.IsPreviousSceneWasGame = _isPreviousSceneWasGame;
        data.IsPreviousSceneWasMenu = _isPreviousSceneWasMenu;
        var json = JsonUtility.ToJson(data, true);

        File.WriteAllText(
            Application.dataPath + "/PreviousSceneData.json",
            json,
            encoding: System.Text.Encoding.UTF8);

        SceneManager.LoadScene(index);
    }

    public void LeaveTutorialScene()
    {
        try
        {
            var json = File.ReadAllText(
                Application.dataPath + "/PreviousSceneData.json",
                encoding: System.Text.Encoding.UTF8);
            var data = JsonUtility.FromJson<PreviousSceneData>(json);

            if (data.IsPreviousSceneWasMenu)
            {
                SceneManager.LoadScene(0);
                _isPreviousSceneWasMenu = false;
            }
            else if (data.IsPreviousSceneWasGame)
            {
                SceneManager.LoadScene(1);
                _isPreviousSceneWasGame = false;
            }
        }
        catch { }
    }
}
