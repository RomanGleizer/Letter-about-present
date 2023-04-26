using System;
using UnityEngine;
using UnityEngine.UI;

public class FarmFinder : MonoBehaviour
{
    [SerializeField] private Transform farm;
    [SerializeField] private Text distanceText;

    private void Update()
    {
        var x = Math.Round(Math.Abs(transform.position.x - farm.transform.position.x));
        var y = Math.Round(Math.Abs(transform.position.y - farm.transform.position.y));

        var distance = (x + y) * 10;
        distanceText.text = distance.ToString() + " ì";
    }
}
