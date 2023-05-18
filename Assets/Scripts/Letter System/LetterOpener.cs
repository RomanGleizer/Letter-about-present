using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LetterOpener : MonoBehaviour
{
    [SerializeField] private GameObject _letterPaper;
    [SerializeField] private GameObject _letterNotify;
    [SerializeField] private TextMeshProUGUI[] _letters;
    [SerializeField] private GameObject[] _menues;

    private string[] _firstLetterContinue;
    private bool _isNotifyActive;
    private int _index;
    private int _firstLetterContinueIndex;
    private List<TextMeshProUGUI> _activeLetters;

    private void Awake()
    {
        InvokeRepeating("SetNotifyActive", Random.Range(20, 30), Random.Range(20, 30));
    }

    private void Start()
    {
        _firstLetterContinue = new string[]
        {
            "\n\n\n����� � ������ ������� ������, ������, ��� � �� ��� ������. � �� ������, ��� �������� ��� ���. �������� �� ���� ���� ����������� ����������� ���, �� ��� ���, �������. ����� ���������� ����������� �������, ����� ������� ��� �� ������������.",
            "� ������� ���������� ��� ������ �����, �� �������� ������ ���, ���������, ������� � �������������, ��������� �� ������� � ��������������� ������� �����. ������� �� ���� �������� ����������� � ����� � ���� � ���������, ���������� �� ����� � ����� �, ������� � ��� ������ �����������, ��������� ������ �������� � ���������� ��������� �� �� ������ ������. �� ����������� ��� ����� ���� ��������� ���������������� ������ � ������� ��� ����������� � �������� ��� ��� ����� ������������. ",
            "\n\n\n��� ���� ������� �� ����. ����� �������� ����� ���� �� ��������� ����������, �� �������, ��� ��� ��� ���������� ����� ����������� ���������-��������. ��� ���� ������, � ����� � ��� ����������� �� �����.",
            "�� �������� ��� ������ ������� � �����������, ��� � ��� ������ ���� ������� � ������������� ���������, ����� �������������� � ��� ����� �����, � ��� ���� �������, ����� ����������� ������� � ������, �� ������ ��� ������������ ������ �������� ���������� � �������������� ��������� ��� ����� � ��������� ����� ������ �������� �� ������ � ��������. ����� �������� ����� ��� ������� ��������������� ��� ��������, �� ������� ������� ��� ���� ������.",
            "\n\n������ ���� � ����� �� �����������, ������ ��� ����� � ��������� �����. ��� ��? ��� �������� ���� ����? ��������/� �� �������? ���������� � ������������ �������� ������ ����. ������, ��� �������/��! �������� ��� ��� ����� � ����,\r\n� �������, \r\n���� ������ ����.\r\n"
        };

        _activeLetters = new List<TextMeshProUGUI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MailBox>())
        {
            if (Input.GetKey(KeyCode.L) && _isNotifyActive 
                && !_menues[0].activeSelf && !_menues[1].activeSelf
                && !_menues[2].activeSelf && !_menues[3].activeSelf
                && !_menues[4].activeSelf && !_menues[5].activeSelf
                && !_menues[6].activeSelf)
            {
                _letterPaper.SetActive(true);
                _letterNotify.SetActive(false);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MailBox>())
        {
            _letterPaper.SetActive(false);
            foreach (var item in _activeLetters)
                item.gameObject.SetActive(false);
        }
    }

    private void SetNotifyActive()
    {
        if (!_letterPaper.activeSelf && _index != _letters.Length - 1)
        {
            _activeLetters.Add(_letters[_index]);

            _isNotifyActive = true;
            _letterNotify.SetActive(true);
            _letters[_index].gameObject.SetActive(true);

            _index++;
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
}