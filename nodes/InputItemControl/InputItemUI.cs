using Godot;
using System;
using System.Linq;

public class InputItemUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private InputHistoryItem last = new InputHistoryItem(new Godot.Collections.Array<int>(), new Godot.Collections.Array<int>(), 0);
    public TileSet tileset = GD.Load<TileSet>("static/nicetileset.tres");
    private bool isOff = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }
    private void turnOn()
    {
        isOff = true;
        ((Sprite)this.GetChild(0)).Visible = true;
    }
    public void update(Godot.Collections.Array<int> k, Godot.Collections.Array<int> m, int c)
    {
        if (!isOff) turnOn();
        var mods = this.GetChild(2).GetChild(0);
        var cout = this.GetChild(1);
        var keys = this.GetChild(2).GetChild(1);
        ((Mods)mods).update(m);
        ((Keys)keys).update(k);
        ((Godot.Label)cout).Text = c.ToString();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
