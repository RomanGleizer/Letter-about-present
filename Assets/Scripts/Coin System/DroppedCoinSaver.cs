using System.IO;
using UnityEngine;

public class DroppedCoinSaver : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;

    public void SaveDroppedCoinData()
    {
        var data = new CoinData();
        foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
            if (obj.GetComponent<Coin>()) data.CoinsPositions.Add(obj.transform.position);

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(
            Application.dataPath + "/CoinData.json",
            json,
            encoding: System.Text.Encoding.UTF8);
    }

    public void LoadDroppedCoinData()
    {
        try
        {
            var json = File.ReadAllText(
                Application.dataPath + "/CoinData.json",
                encoding: System.Text.Encoding.UTF8);
            var data = JsonUtility.FromJson<CoinData>(json);

            foreach (var position in data.CoinsPositions)
                Instantiate(_coinPrefab, position, Quaternion.identity);
        }
        catch { }
    }   
}