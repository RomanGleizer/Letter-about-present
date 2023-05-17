using Unity.VisualScripting;
using UnityEngine;

public class LetterOpener : MonoBehaviour
{
    [SerializeField] private GameObject _letterPaper;
    [SerializeField] private GameObject _letterNotify;
    [SerializeField] private GameObject[] _letters;
    [SerializeField] private GameObject[] _menues;

    private bool _isNotifyActive;

    private void Start()
    {
        InvokeRepeating("SetNotifyActive", Random.Range(35, 41), Random.Range(35, 41));
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
                var randomLetter = _letters[Random.Range(0, _letters.Length)];
                randomLetter.SetActive(true);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<MailBox>())
            _letterPaper.SetActive(false);
    }

    private void SetNotifyActive()
    {
        if (!_letterPaper.activeSelf)
        {
            _isNotifyActive = true;
            _letterNotify.SetActive(true);
        }
    }
}