using UnityEngine;

public class VegetableShopHandler : MonoBehaviour
{
    [SerializeField] private GameObject _vegetableShop;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            _vegetableShop.SetActive(true);
    }
}
