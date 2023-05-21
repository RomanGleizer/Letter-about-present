using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LetterOpener : MonoBehaviour
{
    [SerializeField] private GameObject _letterPaper;
    [SerializeField] private GameObject _letterNotify;
    [SerializeField] private TextMeshProUGUI[] _letters;
    [SerializeField] private GameObject[] _menues;
    [SerializeField] private GameObject _readButton;

    private string[] _firstLetterContinue;
    private bool[] _isLettersWereRead;
    private bool _isNotifyActive;
    private int _index;
    private int _firstLetterContinueIndex;

    private void Awake()
    {
        InvokeRepeating("SetNotifyActive", Random.Range(5, 7), Random.Range(5, 7));
    }

    private void Start()
    {
        _isLettersWereRead = new bool[15];
        _firstLetterContinue = new string[]
        {
            "\n\n\n����� � ������ ������� ������, ������, ��� � �� ��� ������. � �� ������, ��� �������� ��� ���. �������� �� ���� ���� ����������� ����������� ���, �� ��� ���, �������. ����� ���������� ����������� �������, ����� ������� ��� �� ������������.",
            "� ������� ���������� ��� ������ �����, �� �������� ������ ���, ���������, ������� � �������������, ��������� �� ������� � ��������������� ������� �����. ������� �� ���� �������� ����������� � ����� � ���� � ���������, ���������� �� ����� � ����� �, ������� � ��� ������ �����������, ��������� ������ �������� � ���������� ��������� �� �� ������ ������. �� ����������� ��� ����� ���� ��������� ���������������� ������ � ������� ��� ����������� � �������� ��� ��� ����� ������������. ",
            "\n\n\n��� ���� ������� �� ����. ����� �������� ����� ���� �� ��������� ����������, �� �������, ��� ��� ��� ���������� ����� ����������� ���������-��������. ��� ���� ������, � ����� � ��� ����������� �� �����.",
            "�� �������� ��� ������ ������� � �����������, ��� � ��� ������ ���� ������� � ������������� ���������, ����� �������������� � ��� ����� �����, � ��� ���� �������, ����� ����������� ������� � ������, �� ������ ��� ������������ ������ �������� ���������� � �������������� ��������� ��� ����� � ��������� ����� ������ �������� �� ������ � ��������. ����� �������� ����� ��� ������� ��������������� ��� ��������, �� ������� ������� ��� ���� ������.",
            "\n\n������ ���� � ����� �� �����������, ������ ��� ����� � ��������� �����. ��� ��? ��� �������� ���� ����? ��������/� �� �������? ���������� � ������������ �������� ������ ����. ������, ��� �������/��! �������� ��� ��� ����� � ����,\r\n� �������, \r\n���� ������ ����.\r\n"
        };
    }

    private void Update()
    {
        print(_index);    
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MailBox>())
        {
            if (Input.GetKey(KeyCode.L) && _isNotifyActive && _index != _letters.Length)
            {
                foreach (var menu in _menues)
                    menu.SetActive(false);

                _letterPaper.SetActive(true);
                _readButton.SetActive(true);
                _letterNotify.SetActive(false);
            }  
        }
    }

    private void SetNotifyActive()
    {
        if (!_letterPaper.activeSelf && _index != _letters.Length)
        {
            _isNotifyActive = true;
            _letterNotify.SetActive(true);
            _letters[_index].gameObject.SetActive(true);
        }
    }

    public void ChangeLetterText(GameObject letter)
    {
        if (_firstLetterContinueIndex != _firstLetterContinue.Length)
        {
            letter.GetComponent<TextMeshProUGUI>().text = _firstLetterContinue[_firstLetterContinueIndex];
            _firstLetterContinueIndex++;
        }
    }

    public void EndReadLetter()
    {
        if (!_isLettersWereRead[_index])
        {
            ChangeReadValue(true);
            _letters[_index].gameObject.SetActive(false);
            _index++;
        }
    }

    public void SkipLetter()
    {
        if (!_isLettersWereRead[_index])
        {
            ChangeReadValue(false);
            _letters[_index].gameObject.SetActive(false);
        }
    }

    public void StartReadNewLetterAfterSave()
    {
        if (!_isLettersWereRead[_index])
        {
            ChangeReadValue(true);
            _letters[_index].gameObject.SetActive(false);
        }
    }

    public void SaveLetterData()
    {
        var data = new LetterData();

        for (int i = 0; i < _isLettersWereRead.Length; i++)
            data.IsLettersWereRead[i] = _isLettersWereRead[i];

        if (_index != _letters.Length)
        {
            StartReadNewLetterAfterSave();
            data.Index = _index;
        }

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(
            Application.dataPath + "/LetterData.json",
            json,
            encoding: System.Text.Encoding.UTF8);
    }

    public void LoadPlayerData()
    {
        try
        {
            var json = File.ReadAllText(
                Application.dataPath + "/LetterData.json",
                encoding: System.Text.Encoding.UTF8);
            var data = JsonUtility.FromJson<LetterData>(json);

            for (int i = 0; i < _isLettersWereRead.Length; i++)
                _isLettersWereRead[i] = data.IsLettersWereRead[i];
            _index = data.Index;
        }
        catch { }
    }

    private void ChangeReadValue(bool value) => _isLettersWereRead[_index] = value;
}