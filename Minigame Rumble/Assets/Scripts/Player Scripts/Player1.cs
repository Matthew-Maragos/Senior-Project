using UnityEngine;

public class Player1 : PlayerBase
{
    protected override void Start()
    {
        base.Start();
        playerInput.SwitchCurrentControlScheme("Gamepad1", playerInput.devices[0]); 
    }
}