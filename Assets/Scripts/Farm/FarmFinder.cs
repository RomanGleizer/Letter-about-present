using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmFinder : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Farm>())
            print("Ферма найдена! ");
    }
}
