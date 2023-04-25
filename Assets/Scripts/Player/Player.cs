using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _healthAmount;

    public float HealthAmount
    {
        get => _healthAmount;
        set => _healthAmount = value;
    }

    public void SavePlayerData()
    {
        //SaveSystem.SavePlayerProgress(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        HealthAmount = playerData.HealthAmount;

        Vector3 position = new Vector3
        {
            x = playerData.PlayerPosition[0],
            y = playerData.PlayerPosition[1],
            z = playerData.PlayerPosition[2]
        };

        transform.position = position;
    }
}
