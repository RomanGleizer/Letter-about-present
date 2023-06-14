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
    [SerializeField] private GameObject _skipButton;

    private string[] _firstLetterContinue;
    private bool[] _isLettersWereRead = new bool[15];
    private bool[] _isLettersWereSkipped = new bool[15];
    private bool _isNotifyActive;
    private int _readLetterIndex;
    private int _skippedLetterIndex;
    private int _firstLetterContinueIndex;

    private void Awake()
    {
    }

    private void Start()
    {
        LoadPlayerData();
        InvokeRepeating(nameof(SetNotifyActive), Random.Range(60, 61), Random.Range(60, 61));

        _firstLetterContinue = new string[]
        {
            "\n\n\n����� � ������ ������� ������, ������, ��� � �� ��� ������. � �� ������, ��� �������� ��� ���. �������� �� ���� ���� ����������� ����������� ���, �� ��� ���, �������. ����� ���������� ����������� �������, ����� ������� ��� �� ������������.",
            "� ������� ���������� ��� ������ �����, �� �������� ������ ���, ���������, ������� � �������������, ��������� �� ������� � ��������������� ������� �����. ������� �� ���� �������� ����������� � ����� � ���� � ���������, ���������� �� ����� � ����� �, ������� � ��� ������ �����������, ��������� ������ �������� � ���������� ��������� �� �� ������ ������. �� ����������� ��� ����� ���� ��������� ���������������� ������ � ������� ��� ����������� � �������� ��� ��� ����� ������������. ",
            "\n\n\n��� ���� ������� �� ����. ����� �������� ����� ���� �� ��������� ����������, �� �������, ��� ��� ��� ���������� ����� ����������� ���������-��������. ��� ���� ������, � ����� � ��� ����������� �� �����.",
            "�� �������� ��� ������ ������� � �����������, ��� � ��� ������ ���� ������� � ������������� ���������, ����� �������������� � ��� ����� �����, � ��� ���� �������, ����� ����������� ������� � ������, �� ������ ��� ������������ ������ �������� ���������� � �������������� ��������� ��� ����� � ��������� ����� ������ �������� �� ������ � ��������. ����� �������� ����� ��� ������� ��������������� ��� ��������, �� ������� ������� ��� ���� ������.",
            "\n\n������ ���� � ����� �� �����������, ������ ��� ����� � ��������� �����. ��� ��? ��� �������� ���� ����? ��������/� �� �������? ���������� � ������������ �������� ������ ����. ������, ��� �������/��! �������� ��� ��� ����� � ����,\r\n� �������, \r\n���� ������ ����.\r\n"
        };
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MailBox>())
        {
            if ((Input.GetKey(KeyCode.L) && _isNotifyActive && _readLetterIndex != _letters.Length))
                OpenMenus();

            if ((Input.GetKey(KeyCode.L) && !_isNotifyActive && _isLettersWereSkipped[_skippedLetterIndex]))
            {
                OpenMenus();
                _letters[_readLetterIndex].gameObject.SetActive(true);
            }
        }
    }

    private void SetNotifyActive()
    {
        print(_readLetterIndex);
        if (!_letterPaper.activeSelf && _readLetterIndex != _letters.Length && !_isLettersWereSkipped[_skippedLetterIndex])
        {
            _isNotifyActive = true;
            _letterNotify.SetActive(true);
            _letters[_readLetterIndex].gameObject.SetActive(true);
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
        if (!_isLettersWereRead[_readLetterIndex])
        {
            ChangeReadValue(true);
            _letters[_readLetterIndex].gameObject.SetActive(false);
            _readLetterIndex++;
            _skippedLetterIndex++;
        }
    }

    public void SkipLetter()
    {
        if (!_isLettersWereRead[_readLetterIndex]) ChangeSkipValue(true);

        if (_readLetterIndex == 0)
        {
            _firstLetterContinueIndex = 0;
            _letters[_readLetterIndex].text = "1848 ���.\r\n\r\n��������� ���/���,\r\n" +
                "� ���� � �� ����, ��� �� � ���, ������� ���� �� ������ ����� � �������, " +
                "��� ������ ��� ����� ���������� �������� ���������. " +
                "���� ���� �� ������ �. ������ � ��� �����������, ����� ���� ������ ����, �� ������ ��-�������� ������, " +
                "� �� ���������� ���� �����, ����� � ������. ������ ������ ������� ����, ������� �����, �� ��� ������, " +
                "��� ����� �� ���������. ������� ��. ";
        }
    }

    public void StartReadNewLetterAfterSave()
    {
        if (!_isLettersWereRead[_readLetterIndex])
        {
            ChangeReadValue(true);
            _letters[_readLetterIndex].gameObject.SetActive(false);
        }
    }

    public void SaveLetterData()
    {
        var data = new LetterData();

        for (int i = 0; i < _isLettersWereRead.Length; i++)
        {
            data.IsLettersWereRead[i] = _isLettersWereRead[i];
            data.IsLettersWereSkiped[i] = _isLettersWereSkipped[i];
        }

        if (_readLetterIndex != _letters.Length)
        {
            StartReadNewLetterAfterSave();
            data.ReadIndex = _readLetterIndex;
            data.SkipIndex = _skippedLetterIndex;
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
            {
                _isLettersWereRead[i] = data.IsLettersWereRead[i];
                _isLettersWereSkipped[i] = data.IsLettersWereSkiped[i];
            }

            _readLetterIndex = data.ReadIndex;
            _skippedLetterIndex = data.SkipIndex;
        }
        catch { }
    }

    private void ChangeReadValue(bool value) => _isLettersWereRead[_readLetterIndex] = value;
    private void ChangeSkipValue(bool value) => _isLettersWereSkipped[_skippedLetterIndex] = value;

    private void OpenMenus()
    {
        foreach (var menu in _menues)
            menu.SetActive(false);

        _letterPaper.SetActive(true);
        _readButton.SetActive(true);
        _skipButton.SetActive(true);
        _letterNotify.SetActive(false);
    }
}