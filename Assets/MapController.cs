using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject map; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isMapActive = map.activeSelf;

            map.SetActive(!isMapActive);
        }
    }
}
