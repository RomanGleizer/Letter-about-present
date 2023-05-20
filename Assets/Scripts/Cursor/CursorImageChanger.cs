using UnityEngine;

public class CursorImageChanger : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;

    private void Start()
    {
        //Cursor.SetCursor(_cursorTexture, Vector3.zero, CursorMode.ForceSoftware);
    }
}