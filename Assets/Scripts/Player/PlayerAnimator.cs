using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            ChangeBoolParameters(
                "isPlayerMoveLeft", 
                "isPlayerMoveRight", 
                "isPlayerMoveUp", 
                "isPlayerMoveDown", 
                "isPlayerIdle");

        else if (Input.GetKey(KeyCode.D))
            ChangeBoolParameters(
                "isPlayerMoveRight", 
                "isPlayerMoveLeft", 
                "isPlayerMoveUp", 
                "isPlayerMoveDown", 
                "isPlayerIdle");

        else if(Input.GetKey(KeyCode.W))
            ChangeBoolParameters(
                "isPlayerMoveUp",
                "isPlayerMoveRight", 
                "isPlayerMoveLeft", 
                "isPlayerMoveDown", 
                "isPlayerIdle");

        else if(Input.GetKey(KeyCode.S))
            ChangeBoolParameters(
                "isPlayerMoveDown", 
                "isPlayerMoveRight", 
                "isPlayerMoveUp", 
                "isPlayerMoveLeft", 
                "isPlayerIdle");
        else
            ChangeBoolParameters(
                "isPlayerIdle", 
                "isPlayerMoveRight", 
                "isPlayerMoveUp", 
                "isPlayerMoveLeft", 
                "isPlayerMoveDown");
    }
    
    private void ChangeBoolParameters(params string[] moves)
    {
        _playerAnimator.SetBool(moves[0], true);
        _playerAnimator.SetBool(moves[1], false);
        _playerAnimator.SetBool(moves[2], false);
        _playerAnimator.SetBool(moves[3], false);
        _playerAnimator.SetBool(moves[4], false);
    }
}
