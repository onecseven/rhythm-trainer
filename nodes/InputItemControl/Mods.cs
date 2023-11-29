using EnumExtension;
using Godot;
using Godot.Collections;
using System;
using System.Linq;

public class Mods : Control
{


    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void update(Godot.Collections.Array<int> directions)
    {
        string direction = utils.stringifyButtons(directions);
        TextureRect sprite = this.GetChild<TextureRect>(0);
        sprite.Texture = utils.strToSprite(direction);
    }
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
