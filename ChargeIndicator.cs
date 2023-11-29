using Godot;
using System;

public class ChargeIndicator : TextureRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public Texture green = GD.Load<Texture>("static/green.tres");
    public Texture red = GD.Load<Texture>("static/red.tres");
    public Texture yellow = GD.Load<Texture>("static/yellow.tres");
    public int current = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void turn(int a)
    {
        if (current == a) return;
        switch (a)
        {
            case 0:
                this.Texture = red;
                break;
            case 1:
                this.Texture = yellow;
                break;
            case 2:
                this.Texture = green;
                break;
        }
        current = a;    
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
