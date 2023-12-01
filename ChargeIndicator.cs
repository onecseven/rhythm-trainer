using Godot;
using System;

public class ChargeIndicator : TextureRect
{
    public Texture green = GD.Load<Texture>("static/green.tres");
    public Texture red = GD.Load<Texture>("static/red.tres");
    public Texture yellow = GD.Load<Texture>("static/yellow.tres");
    public int current = 0;
    public void OnChargingStateChanged(InputReader.ChargingState next)
    {
        if (current == ((int)next)) return;
        switch (next)
        {
            case InputReader.ChargingState.NOT:
                this.Texture = red;
                break;
            case InputReader.ChargingState.CHARGING:
                this.Texture = yellow;
                break;
            case InputReader.ChargingState.CHARGED:
                this.Texture = green;
                break;
            default:
                throw new Exception("THIS SHOULDN'T HAPPEN");
        }
        current = (int)next;    
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
