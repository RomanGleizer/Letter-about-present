using System;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private Text _gameBarText;
    [SerializeField] private string buttonName;

    private void Start()
    {
        _gameBarText.text = PlayerPrefs.GetString(buttonName);
        PlayerPrefs.Save();
    }

    public void ChangeText()
    {   
        _gameBarText.text = PlayerPrefs.GetString(buttonName);
        PlayerPrefs.SetString(buttonName, $"Игра была сохранена {DateTime.Now.ToString("dd/MM/yyyy")}");
        PlayerPrefs.Save();
    }
}