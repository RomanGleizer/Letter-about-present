//using System;

//[Serializable]
//public class PlayerData
//{ 
//    private float _healthAmount;
//    public float[] PlayerPosition { get; }

//    public float HealthAmount 
//    { 
//        get => _healthAmount; 
//        set => _healthAmount = value;
//    }

//    public PlayerData(Player player)
//    {
//        HealthAmount = player.HealthAmount;
//        PlayerPosition = new float[3]
//        {
//            player.transform.position.x,
//            player.transform.position.y,
//            player.transform.position.z
//        };
//    }
//}