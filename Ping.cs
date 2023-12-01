using Godot;
using System;

public class Ping : AudioStreamPlayer
{
    private bool locked = false;

    private void OnChargingStateChanged(InputReader.ChargingState next)
    {
        switch (next) {
            case InputReader.ChargingState.CHARGED:
                if (locked == true) return;
                else {
                    locked = true;
                    GD.Print("here");
                    this.Play();
                };
                break;
            default: 
                locked = false;
                break;
        }
    }

}
