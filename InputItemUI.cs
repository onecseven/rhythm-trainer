    using Godot;
using inputreader;
using System;
using System.Linq;

public class InputItemUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private InputHistoryItem last = new InputHistoryItem(new Godot.Collections.Array<int>(), new Godot.Collections.Array<int>(), 0);
    public TileSet tileset = GD.Load<TileSet>("nicetileset.tres");


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("here");
        GD.Print(last == null);
    }
    public void update(InputHistoryItem item)
    {
        var mods = this.GetChild(0).GetChild(1);
        var cout = this.GetChild(0).GetChild(0);
        var keys = this.GetChild(0).GetChild(2);
        ((Mods)mods).update(item.mods);
        ((Keys)keys).update(item.keys);
        ((Godot.Label)cout).Text = item.count.ToString();

        //if (last == null)
        //{
        //    foreach (var keys in item.keys)  
        //    {
        //        this.GetChild(0).GetChild(1);
        //    }
        //    foreach (var mods in item.mods)
        //    {

        //    }


        //} else if (item.Equals(last))
        //{

        //} else
        //{

        //}
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
