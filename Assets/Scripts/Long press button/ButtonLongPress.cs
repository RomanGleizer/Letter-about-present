using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool _buttonPressed;

    public bool IsButtonPressed { get => _buttonPressed; }
 
    public void OnPointerDown(PointerEventData eventData)
    {
         _buttonPressed = true;
    }
 
    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonPressed = false;
    }
}