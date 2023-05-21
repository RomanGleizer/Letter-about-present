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
            "\n\n\nСмута в Европе Маринку пугает, боится, что и до нас дойдет. Я ей говорю, мол глупости все это. Государь не даст воли разыграться беспорядкам или, не дай бог, мятежам. Нужно ограничить иностранные издания, чтобы молодые умы не соблазнялись.",
            "В учебных заведениях дух вообще хорош, но Государь просит нас, родителей, братьев и родственников, наблюдать за мыслями и нравственностью молодых людей. Служить им сами примером благочестия и любви к царю и отечеству, направлять их мысли к добру и, заметив в них дурные наклонности, стараться мерами кротости и убеждением наставить их на прямую дорогу. По неопытности они могут быть вовлечены неблагонадежными людьми к вредным для большинства и пагубным для них самих последствиям. ",
            "\n\n\nНаш долг следить за ними. Когда Государь издал указ об обязанных крестьянах, то объявил, что вся без исключения земля принадлежит дворянину-помещику. Это вещь святая, и никто к ней прикасаться не может.",
            "Но Государь наш должен сказать с прискорбием, что у нас весьма мало хороших и попечительных помещиков, много посредственных и еще более худых, а при духе времени, кроме предписаний совести и закона, мы должны для собственного своего интереса заботиться о благосостоянии вверенных нам людей и стараться всеми силами снискать их любовь и уважение. Ежели окажется среди вас помещик безнравственный или жестокий, Вы обязаны предать его силе закона.",
            "\n\nБереги себя и следи за крестьянами, буйные они стали в последнее время. Как ты? Как поживает дочь твоя? Отправил/а ее учиться? Отсутствие и отделенность довольно гадкая вещь. Прощай, моя дорогой/ая! Поскорей дай нам весть о себе,\r\nС любовью, \r\nТвой вечный друг.\r\n"
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