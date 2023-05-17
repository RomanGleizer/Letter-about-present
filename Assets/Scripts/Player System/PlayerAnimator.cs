using UnityEngine;

public enum Moves
{
    Left,
    Right,
    Down,
    Up,
    Idle
}

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            ChangeBoolParameter(Moves.Left);

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            ChangeBoolParameter(Moves.Right);

        else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            ChangeBoolParameter(Moves.Up);

        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            ChangeBoolParameter(Moves.Down);
        else
            ChangeBoolParameter(Moves.Idle);
    }
    
    private void ChangeBoolParameter(Moves move)
    {
        switch (move)
        {
            case Moves.Left: SetParameterTrue("isPlayerMoveLeft");
                break;
            case Moves.Right: SetParameterTrue("isPlayerMoveRight");
                break;
            case Moves.Up: SetParameterTrue("isPlayerMoveUp");
                break;
            case Moves.Down: SetParameterTrue("isPlayerMoveDown");
                break;
            case Moves.Idle: SetParameterTrue("isPlayerIdle");
                break;
            default: 
                break;
        }
    }

    private void SetParameterTrue(string parameter)
    {
        string[] parameters = new string[]
        {
            "isPlayerMoveLeft",
            "isPlayerMoveRight",
            "isPlayerMoveUp",
            "isPlayerMoveDown",
            "isPlayerIdle"
        };
        
        for (int i = 0; i < parameters.Length; i++)
            if (parameters[i] == parameter) _playerAnimator.SetBool(parameters[i], true);
            else _playerAnimator.SetBool(parameters[i], false);
    }
}
