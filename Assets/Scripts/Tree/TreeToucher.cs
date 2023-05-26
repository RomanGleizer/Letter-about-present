using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeToucher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Tree>())
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Tree>())
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
    }
}
