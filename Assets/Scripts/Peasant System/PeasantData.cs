using System;
using UnityEngine;

[Serializable]
public class PeasantData
{
    public Vector3 Position = new Vector3();
    public Transform Target = null;
    public int CurrentPoint = 0;
    public bool IsPlayerMoveLeft = false;
    public bool IsPlayerMoveRight = false;
    public bool IsPlayerMoveUp = false;
    public bool IsPlayerMoveDown = false;
}