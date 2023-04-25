using System;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private Text _gameBarText;
    [SerializeField] private string _buttonName;

    public void ChangeText(string text)
    {   
        _gameBarText.text = PlayerPrefs.GetString(_buttonName);
        PlayerPrefs.SetString(_buttonName, text);
        PlayerPrefs.Save();
    }
}