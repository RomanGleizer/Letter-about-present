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

    private void Awake()
    {
        InvokeRepeating("SetNotifyActive", Random.Range(40, 45), Random.Range(40, 45));
    }

    private void Start()
    {
        _firstLetterContinue = new string[]
        {
            "\n\n\nСмута в Европе Маринку пугает, боится, что и до нас дойдет. Я ей говорю, мол глупости все это. Государь не даст воли разыграться беспорядкам или, не дай бог, мятежам. Нужно ограничить иностранные издания, чтобы молодые умы не соблазнялись.",
            "В учебных заведениях дух вообще хорош, но Государь просит нас, родителей, братьев и родственников, наблюдать за мыслями и нравственностью молодых людей. Служить им сами примером благочестия и любви к царю и отечеству, направлять их мысли к добру и, заметив в них дурные наклонности, стараться мерами кротости и убеждением наставить их на прямую дорогу. По неопытности они могут быть вовлечены неблагонадежными людьми к вредным для большинства и пагубным для них самих последствиям. ",
            "\n\n\nНаш долг следить за ними. Когда Государь издал указ об обязанных крестьянах, то объявил, что вся без исключения земля принадлежит дворянину-помещику. Это вещь святая, и никто к ней прикасаться не может.",
            "Но Государь наш должен сказать с прискорбием, что у нас весьма мало хороших и попечительных помещиков, много посредственных и еще более худых, а при духе времени, кроме предписаний совести и закона, мы должны для собственного своего интереса заботиться о благосостоянии вверенных нам людей и стараться всеми силами снискать их любовь и уважение. Ежели окажется среди вас помещик безнравственный или жестокий, Вы обязаны предать его силе закона.",
            "\n\nБереги себя и следи за крестьянами, буйные они стали в последнее время. Как ты? Как поживает дочь твоя? Отправил/а ее учиться? Отсутствие и отделенность довольно гадкая вещь. Прощай, моя дорогой/ая! Поскорей дай нам весть о себе,\r\nС любовью, \r\nТвой вечный друг.\r\n"
        };
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

    private void SetNotifyActive()
    {
        if (!_letterPaper.activeSelf && _index != _letters.Length - 1)
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

    public void IncrementIndex()
    {
        _letters[_index].gameObject.SetActive(false);
        _index++;
    }
}