using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SavePlayerProgress(Player player)
    {
        var formatter = new BinaryFormatter();
        var playerPath = Application.persistentDataPath + "/player.cs";
        var stream = new FileStream(playerPath, FileMode.Create);
        var data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.cs";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            var playerData = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return playerData;
        }
        else
        {
            Debug.Log("�� ������ ���� � " + path);
            return null;
        }
    }
}